using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Services.User;

public class GamificationService(UserDbContext _context, IMapper _mapper) : IGamificationService
{
    public async Task AddXpAsync(Guid userId, int xpAmount)
    {
        var user = await _context.NguoiDungs
            .FirstOrDefaultAsync(u => u.MaNguoiDung == userId);

        if (user == null)
        {
            return;
        }

        user.TongDiemXp = (user.TongDiemXp ?? 0) + xpAmount;

        if (user.MaCapDo.HasValue)
        {
            var currentLevel = await _context.CapDos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.MaCapDo == user.MaCapDo.Value);

            if (currentLevel != null)
            {
                var currentXp = user.TongDiemXp ?? 0;

                if (currentXp >= currentLevel.MucXptoiDa)
                {
                    var nextLevel = await _context.CapDos
                        .AsNoTracking()
                        .Where(c => c.MucXptoiThieu > currentLevel.MucXptoiDa)
                        .OrderBy(c => c.MucXptoiThieu)
                        .FirstOrDefaultAsync();

                    if (nextLevel != null)
                    {
                        user.MaCapDo = nextLevel.MaCapDo;
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<UserStatsResponses> GetUserStatsAsync(Guid userId)
    {
        var user = await _context.NguoiDungs
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.MaNguoiDung == userId);

        if (user == null)
        {
            return new UserStatsResponses();
        }

        return _mapper.Map<UserStatsResponses>(user);
    }

    public async Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId)
    {
        var questIds = await _context.NhiemVus
            .AsNoTracking()
            .Where(q => q.ConHieuLuc == true)
            .Select(q => q.MaNhiemVu)
            .ToListAsync();

        var progresses = await _context.TienDoNhiemVus
            .AsNoTracking()
            .Where(td => td.MaNguoiDung == userId && questIds.Contains(td.MaNhiemVu))
            .ToListAsync();

        // Map tiến độ có sẵn bằng AutoMapper (TienDoNhiemVuProfile đã có)
        var result = _mapper.Map<List<TienDoNhiemVuResponse>>(progresses);

        // Nếu người dùng chưa có record tiến độ cho quest nào đó -> trả về mặc định để UI vẫn hiển thị
        var existedQuestIds = progresses.Select(x => x.MaNhiemVu).ToHashSet();

        foreach (var questId in questIds)
        {
            if (existedQuestIds.Contains(questId))
            {
                continue;
            }

            result.Add(new TienDoNhiemVuResponse
            {
                MaNguoiDung = userId,
                MaNhiemVu = questId,
                TienDoHienTai = 0,
                DaHoanThanh = false,
                DaNhanThuong = false,
                NgayBatDau = null,
                NgayHoanThanh = null
            });
        }

        return result;
    }

    public async Task<bool> UpdateQuestProgressAsync(Guid userId, string loaiDieuKien, int giaTriThem)
    {
        var quests = await _context.NhiemVus
            .Where(q => q.ConHieuLuc == true && q.LoaiDieuKien == loaiDieuKien)
            .ToListAsync();

        foreach (var q in quests)
        {
            var progress = await _context.TienDoNhiemVus
                .FirstOrDefaultAsync(td => td.MaNguoiDung == userId && td.MaNhiemVu == q.MaNhiemVu);

            if (progress == null)
            {
                progress = new TienDoNhiemVu
                {
                    MaNguoiDung = userId,
                    MaNhiemVu = q.MaNhiemVu,
                    TienDoHienTai = 0,
                    NgayBatDau = DateOnly.FromDateTime(DateTime.Now),
                    DaHoanThanh = false,
                    DaNhanThuong = false
                };

                _context.TienDoNhiemVus.Add(progress);
            }

            if ((progress.DaHoanThanh ?? false) == false)
            {
                progress.TienDoHienTai = (progress.TienDoHienTai ?? 0) + giaTriThem;

                if ((progress.TienDoHienTai ?? 0) >= q.DieuKienDatDuoc)
                {
                    progress.DaHoanThanh = true;
                    progress.NgayHoanThanh = DateTime.Now;
                }
            }
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<string> ClaimQuestRewardAsync(Guid userId, int maNhiemVu)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var progress = await _context.TienDoNhiemVus
                .Include(td => td.MaNhiemVuNavigation)
                .FirstOrDefaultAsync(td => td.MaNguoiDung == userId && td.MaNhiemVu == maNhiemVu);

            if (progress == null || (progress.DaHoanThanh ?? false) == false)
            {
                return "Chưa hoàn thành!";
            }

            if (progress.DaNhanThuong ?? false)
            {
                return "Đã nhận rồi!";
            }

            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user != null)
            {
                user.Vang = (user.Vang ?? 0) + (progress.MaNhiemVuNavigation.ThuongVang ?? 0);
                user.TongDiemXp = (user.TongDiemXp ?? 0) + (progress.MaNhiemVuNavigation.ThuongXp ?? 0);
            }

            progress.DaNhanThuong = true;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return "Thành công!";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return $"Lỗi: {ex.Message}";
        }
    }

    public async Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid userId)
    {
        var achievements = await _context.ThanhTuus
            .AsNoTracking()
            .ToListAsync();

        var achievedIds = await _context.ThanhTuuDatDuocs
            .AsNoTracking()
            .Where(dd => dd.MaNguoiDung == userId)
            .Select(dd => dd.MaThanhTuu)
            .ToListAsync();

        var achievedSet = new HashSet<int>(achievedIds);

        var result = _mapper.Map<List<ThanhTuuResponse>>(achievements);

        foreach (var dto in result)
        {
            dto.DaDatDuoc = achievedSet.Contains(dto.MaThanhTuu);
        }

        return result;
    }
}