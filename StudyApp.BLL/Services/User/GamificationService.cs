using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using System;


namespace StudyApp.BLL.Services.User;

public class GamificationService(UserDbContext _context, IMapper _mapper) : IGamificationService
{
    private static LoaiNhiemVuEnum ParseLoaiNhiemVu(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return LoaiNhiemVuEnum.HangNgay;
        }

        if (Enum.TryParse<LoaiNhiemVuEnum>(value, ignoreCase: true, out var parsed))
        {
            return parsed;
        }

        return LoaiNhiemVuEnum.HangNgay;
    }

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

    /* public async Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId)
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
                 TienDoHien Tai = 0,
                 DaHoanThanh = false,
                 DaNhanThuong = false,
                 NgayBatDau = null,
                 NgayHoanThanh = null
             });
         }

         return result;
     }
    */
    public async Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId)
    {
        var progresses = await _context.TienDoNhiemVus
            .AsNoTracking()
            .Include(td => td.MaNhiemVuNavigation)
            .Where(td => td.MaNguoiDung == userId && td.MaNhiemVuNavigation.ConHieuLuc == true)
            .ToListAsync();

        var result = _mapper.Map<List<TienDoNhiemVuResponse>>(progresses);

        return result
            .OrderByDescending(x => x.DaHoanThanh && !x.DaNhanThuong)
            .ThenBy(x => x.DaNhanThuong)
            .ToList();
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
                return "Bạn chưa hoàn thành nhiệm vụ này!";
            }

            if (progress.DaNhanThuong ?? false)
            {
                return "Bạn đã nhận thưởng rồi!";
            }

            if (progress.MaNhiemVuNavigation == null)
            {
                return "Không tìm thấy thông tin nhiệm vụ!";
            }

            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user != null)
            {
                user.Vang = (user.Vang ?? 0) + (progress.MaNhiemVuNavigation.ThuongVang ?? 0);
                user.KimCuong = (user.KimCuong ?? 0) + (progress.MaNhiemVuNavigation.ThuongKimCuong ?? 0);
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

    /*  public async Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid userId)
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
      }*/

    public async Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid userId)
    {
        var allAchievements = await _context.ThanhTuus
            .AsNoTracking()
            .ToListAsync();

        var responseList = _mapper.Map<List<ThanhTuuResponse>>(allAchievements);

        var unlockedData = await _context.ThanhTuuDatDuocs
            .AsNoTracking()
            .Where(x => x.MaNguoiDung == userId)
            .Select(x => new { x.MaThanhTuu, x.NgayDat })
            .ToDictionaryAsync(x => x.MaThanhTuu, x => x.NgayDat);

        foreach (var item in responseList)
        {
            DateTime? ngayDat;
            if (unlockedData.TryGetValue(item.MaThanhTuu, out ngayDat))
            {
                item.DaDatDuoc = true;
                item.NgayDat = ngayDat;
            }
            else
            {
                item.DaDatDuoc = false;
                item.NgayDat = null;
            }
        }

        return responseList.OrderByDescending(x => x.DaDatDuoc).ToList();
    }

    public async Task CheckAndGrantAchievementsAsync(Guid userId)
    {
        // Dùng Transaction để đảm bảo: Đã nhận huy hiệu là phải nhận được tiền!
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user == null) return;

            // Lấy danh sách ID đã đạt (Tránh trao trùng)
            var achievedIds = await _context.ThanhTuuDatDuocs
                .Where(x => x.MaNguoiDung == userId).Select(x => x.MaThanhTuu).ToListAsync();

            // Lấy danh sách chưa đạt (Tiềm năng)
            var pending = await _context.ThanhTuus
                .Where(x => !achievedIds.Contains(x.MaThanhTuu)).ToListAsync();

            bool hasUpdate = false;

            foreach (var ach in pending)
            {
                // Parse Enum Điều Kiện (TongSoTheHoc, ChuoiNgayLienTiep...)
                if (!Enum.TryParse<LoaiDieuKienEnum>(ach.DieuKienLoai, true, out var condition)) continue;

                bool qualified = false;

                // --- LOGIC SO SÁNH CHỈ SỐ NGƯỜI DÙNG ---
                switch (condition)
                {
                    case LoaiDieuKienEnum.TongSoTheHoc:
                        if (user.TongSoTheHoc >= ach.DieuKienGiaTri) qualified = true; break;
                    case LoaiDieuKienEnum.ChuoiNgayLienTiep:
                        if ((user.ChuoiNgayHocLienTiep ?? 0) >= ach.DieuKienGiaTri) qualified = true; break;
                    case LoaiDieuKienEnum.TongDiemXP:
                        if ((user.TongDiemXp ?? 0) >= ach.DieuKienGiaTri) qualified = true; break;
                    case LoaiDieuKienEnum.SoTranThang:
                        if ((user.SoTranThang ?? 0) >= ach.DieuKienGiaTri) qualified = true; break;
                }

                if (qualified)
                {
                    // A. Ghi nhận thành tích
                    _context.ThanhTuuDatDuocs.Add(new ThanhTuuDatDuoc
                    {
                        MaNguoiDung = userId,
                        MaThanhTuu = ach.MaThanhTuu,
                        NgayDat = DateTime.Now
                    });

                    // B. Cộng thưởng ngay lập tức
                    user.Vang = (user.Vang ?? 0) + (ach.ThuongVang ?? 0);
                    user.KimCuong = (user.KimCuong ?? 0) + (ach.ThuongKimCuong ?? 0);
                    user.TongDiemXp = (user.TongDiemXp ?? 0) + (ach.ThuongXp ?? 0);

                    hasUpdate = true;
                }
            }

            if (hasUpdate)
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch { await transaction.RollbackAsync(); throw; }
    }
}