using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.User;
using StudyApp.BLL.Services.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Services.User;

public class GamificationService : IGamificationService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDailyStreakService _dailyStreakService;
    

    public GamificationService(
        UserDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider,
        IDailyStreakService dailyStreakService)
    {
        _context = context;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _dailyStreakService = dailyStreakService;
    }

    private static LoaiNhiemVuEnum ParseLoaiNhiemVu(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return LoaiNhiemVuEnum.HangNgay;

        if (Enum.TryParse<LoaiNhiemVuEnum>(value, ignoreCase: true, out var parsed))
            return parsed;

        return LoaiNhiemVuEnum.HangNgay;
    }

    public async Task AddXpAsync(Guid userId, int xpAmount)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == userId);
        if (user == null) return;

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
                        user.MaCapDo = nextLevel.MaCapDo;
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

        return user == null ? new UserStatsResponses() : _mapper.Map<UserStatsResponses>(user);
    }

    #region Tiến độ nhiệm vụ Response
    public async Task<List<TienDoNhiemVuResponse>> GetMyQuestsAsync(Guid userId)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var quests = await _context.NhiemVus.AsNoTracking()
            .Where(q => q.ConHieuLuc == true)
            .ToListAsync();

        var result = new List<TienDoNhiemVuResponse>();

        foreach (var q in quests)
        {
            if (q.LoaiNhiemVu == "SuKien" && q.NgayKetThuc < today)
                continue;

            var prog = await _context.TienDoNhiemVus
                .FirstOrDefaultAsync(x => x.MaNguoiDung == userId && x.MaNhiemVu == q.MaNhiemVu);

            var needReset = false;

            if (prog != null)
            {
                if (q.LoaiNhiemVu == "HangNgay" && prog.NgayBatDau < today)
                    needReset = true;

                var monday = today.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
                if (q.LoaiNhiemVu == "HangTuan" && prog.NgayBatDau < monday)
                    needReset = true;
            }

            if (needReset && prog != null)
            {
                prog.TienDoHienTai = 0;
                prog.DaHoanThanh = false;
                prog.DaNhanThuong = false;
                prog.NgayBatDau = today;
                _context.Entry(prog).State = EntityState.Modified;
            }

            if (prog == null)
            {
                prog = new TienDoNhiemVu
                {
                    MaNguoiDung = userId,
                    MaNhiemVu = q.MaNhiemVu,
                    TienDoHienTai = 0,
                    NgayBatDau = today
                };
                _context.TienDoNhiemVus.Add(prog);
            }

            var dto = _mapper.Map<TienDoNhiemVuResponse>(prog);
            dto.TenNhiemVu = q.TenNhiemVu;
            dto.MoTa = q.MoTa;
            dto.DieuKienDatDuoc = q.DieuKienDatDuoc;
            dto.ThuongVang = q.ThuongVang ?? 0;
            dto.ThuongXP = q.ThuongXp ?? 0;
            dto.LoaiNhiemVu = ParseLoaiNhiemVu(q.LoaiNhiemVu);

            result.Add(dto);
        }

        await _context.SaveChangesAsync();
        return result;
    }
    #endregion

    #region: gọi khi hoàn thành bài học(BoDEHOCService)
    public async Task ProcessLessonCompletionAsync(Guid userId, int xpEarned)
    {
        await AddXpAsync(userId, xpEarned);

        var activeQuests = await _context.NhiemVus
            .Where(q => q.ConHieuLuc == true && q.LoaiDieuKien == "CompleteQuiz")
            .ToListAsync();

        foreach (var quest in activeQuests)
            await UpdateProgressInternalAsync(userId, quest, 1);
    }
    #endregion

    #region : gọi timer online
    public async Task ProcessOnlineTimeAsync(Guid userId, int minutes)
    {
        var timeQuests = await _context.NhiemVus
            .Where(q => q.ConHieuLuc == true && q.LoaiDieuKien == "OnlineTime")
            .ToListAsync();

        foreach (var quest in timeQuests)
            await UpdateProgressInternalAsync(userId, quest, minutes);
    }
    #endregion

    #region gọi khi hoàn thành trận đấu (GameService)
    private async Task UpdateProgressInternalAsync(Guid userId, NhiemVu quest, int amount)
    {
        var progress = await _context.TienDoNhiemVus
            .FirstOrDefaultAsync(td => td.MaNguoiDung == userId && td.MaNhiemVu == quest.MaNhiemVu);

        if (progress == null)
        {
            progress = new TienDoNhiemVu
            {
                MaNguoiDung = userId,
                MaNhiemVu = quest.MaNhiemVu,
                TienDoHienTai = 0,
                DaHoanThanh = false,
                NgayBatDau = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.TienDoNhiemVus.Add(progress);
        }

        if (progress.DaHoanThanh == true)
            return;

        progress.TienDoHienTai = (progress.TienDoHienTai ?? 0) + amount;

        if ((progress.TienDoHienTai ?? 0) >= quest.DieuKienDatDuoc)
        {
            progress.TienDoHienTai = quest.DieuKienDatDuoc;
            progress.DaHoanThanh = true;
            progress.NgayHoanThanh = DateTime.Now;
        }

        await _context.SaveChangesAsync();
    }
    #endregion

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
        using var trans = await _context.Database.BeginTransactionAsync();
        try
        {
            var progress = await _context.TienDoNhiemVus
                .Include(t => t.MaNhiemVuNavigation)
                .FirstOrDefaultAsync(td => td.MaNguoiDung == userId && td.MaNhiemVu == maNhiemVu);

            if (progress == null || !(progress.DaHoanThanh ?? false))
                return "Chưa hoàn thành!";

            if (progress.DaNhanThuong ?? false)
                return "Đã nhận rồi!";

            var user = await _context.NguoiDungs.FindAsync(userId);
            var q = progress.MaNhiemVuNavigation;

            if (user == null || q == null)
                return "Lỗi hệ thống";

            user.Vang = (user.Vang ?? 0) + (q.ThuongVang ?? 0);
            user.KimCuong = (user.KimCuong ?? 0) + (q.ThuongKimCuong ?? 0);
            user.TongDiemXp = (user.TongDiemXp ?? 0) + (q.ThuongXp ?? 0);
            progress.DaNhanThuong = true;

            var today = DateOnly.FromDateTime(DateTime.Now);
            var lastActivityDate = user.NgayHoatDongCuoi ?? DateOnly.FromDateTime(DateTime.Now.AddDays(-10));

            System.Diagnostics.Debug.WriteLine($"[BEFORE] lastActivityDate = {lastActivityDate}, today = {today}");
            System.Diagnostics.Debug.WriteLine($"[BEFORE] ChuoiNgayHocLienTiep = {user.ChuoiNgayHocLienTiep}");
            System.Diagnostics.Debug.WriteLine($"[BEFORE] NgayHoatDongCuoi = {user.NgayHoatDongCuoi}");

            // ========== LẦN ĐẦU TIÊN (ChuoiNgayHocLienTiep == 0) ==========
            if ((user.ChuoiNgayHocLienTiep ?? 0) == 0)
            {
                user.ChuoiNgayHocLienTiep = 1;
                user.ChuoiNgayDaiNhat = 1;
                user.SoNhiemVuTrongNgay = 1;
                user.NgayHoatDongCuoi = today;
                user.IsStreakFrozen = false;

                System.Diagnostics.Debug.WriteLine($"[FIRST TIME] NgayHoatDongCuoi was null → Set ChuoiNgayHocLienTiep = 1");
            }
            // ========== NGÀY MỚI (lần thứ 2 trở đi) ==========
            else if (lastActivityDate < today)
            {
                var yesterday = today.AddDays(-1);

                System.Diagnostics.Debug.WriteLine($"[NEW DAY] lastActivityDate < today");
                System.Diagnostics.Debug.WriteLine($"[NEW DAY] yesterday = {yesterday}, lastActivityDate = {lastActivityDate}");

                if (lastActivityDate == yesterday)
                {
                    // Hôm qua có hoạt động → cộng chuỗi
                    user.ChuoiNgayHocLienTiep = (user.ChuoiNgayHocLienTiep ?? 0) + 1;
                    System.Diagnostics.Debug.WriteLine($"[LOGIC] lastActivityDate == yesterday → Cộng chuỗi = {user.ChuoiNgayHocLienTiep}");

                    // Cập nhật chuỗi dài nhất
                    if ((user.ChuoiNgayHocLienTiep ?? 0) > (user.ChuoiNgayDaiNhat ?? 0))
                    {
                        user.ChuoiNgayDaiNhat = user.ChuoiNgayHocLienTiep;
                    }
                }
                else if (lastActivityDate < yesterday)
                {
                    // Mất chuỗi: reset = 1 (chỉ có thể phục hồi bằng vật phẩm)
                    user.ChuoiNgayHocLienTiep = 1;
                    System.Diagnostics.Debug.WriteLine($"[LOGIC] lastActivityDate < yesterday → Reset chuỗi = 1");
                }

                // Reset counter hàng ngày
                user.SoNhiemVuTrongNgay = 1;
                user.NgayHoatDongCuoi = today;
                user.IsStreakFrozen = false;

                System.Diagnostics.Debug.WriteLine($"[LOGIC] Reset NgayHoatDongCuoi = {today}");
            }
            // ========== CÙNG NGÀY ==========
            else
            {
                // Cùng ngày: tăng counter
                user.SoNhiemVuTrongNgay++;
                System.Diagnostics.Debug.WriteLine($"[SAME DAY] SoNhiemVuTrongNgay = {user.SoNhiemVuTrongNgay}");
                
                // ✅ XÓA: Logic khôi phục 3 nhiệm vụ không còn dùng nữa
            }

            await _context.SaveChangesAsync();
            await trans.CommitAsync();

            System.Diagnostics.Debug.WriteLine($"[AFTER SaveChanges] ChuoiNgayHocLienTiep = {user.ChuoiNgayHocLienTiep}");

            var currentStreak = user.ChuoiNgayHocLienTiep ?? 0;
            var msg = $"Nhận thành công: +{q.ThuongVang ?? 0} Vàng | 🔥 Chuỗi: {currentStreak} ngày";

            if (StudyApp.DTO.UserSession.CurrentUser != null)
            {
                StudyApp.DTO.UserSession.CurrentUser.Vang = user.Vang ?? 0;
                StudyApp.DTO.UserSession.CurrentUser.KimCuong = user.KimCuong ?? 0;
                StudyApp.DTO.UserSession.CurrentUser.TongDiemXp = user.TongDiemXp ?? 0;
                StudyApp.DTO.UserSession.CurrentUser.ChuoiNgayHocLienTiep = currentStreak;

                System.Diagnostics.Debug.WriteLine($"[UserSession Update] ChuoiNgayHocLienTiep = {StudyApp.DTO.UserSession.CurrentUser.ChuoiNgayHocLienTiep}");
            }

            // ✅ GỌI NGOÀI TRANSACTION
            await CheckAndGrantAchievementsAsync(userId);

            return msg;
        }
        catch (Exception ex)
        {
            await trans.RollbackAsync();
            System.Diagnostics.Debug.WriteLine($"[ERROR] {ex.Message}");
            return $"Lỗi: {ex.Message}";
        }
    }



    #region ResetDailyStreaks
    public async Task ResetDailyQuestCountAsync(Guid userId)
    {
        var user = await _context.NguoiDungs.FindAsync(userId);
        if (user == null) return;

        var today = DateOnly.FromDateTime(DateTime.Now);
        var lastActivityDate = user.NgayHoatDongCuoi ?? DateOnly.FromDateTime(DateTime.Now.AddDays(-10));

        // Nếu sang ngày mới: reset counter
        if (lastActivityDate < today)
        {
            user.SoNhiemVuTrongNgay = 0;
            await _context.SaveChangesAsync();
        }
    }
    #endregion

    #region Thành tựu
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
            if (unlockedData.TryGetValue(item.MaThanhTuu, out var ngayDat))
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
    #endregion

    public async Task CheckAndGrantAchievementsAsync(Guid userId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user == null) return;

            var achievedIds = await _context.ThanhTuuDatDuocs
                .Where(x => x.MaNguoiDung == userId)
                .Select(x => x.MaThanhTuu)
                .ToListAsync();

            var pending = await _context.ThanhTuus
                .Where(x => !achievedIds.Contains(x.MaThanhTuu))
                .ToListAsync();

            var hasUpdate = false;

            foreach (var ach in pending)
            {
                if (!Enum.TryParse<LoaiDieuKienEnum>(ach.DieuKienLoai, true, out var condition))
                    continue;

                var qualified = condition switch
                {
                    LoaiDieuKienEnum.TongSoTheHoc => user.TongSoTheHoc >= ach.DieuKienGiaTri,
                    LoaiDieuKienEnum.ChuoiNgayLienTiep => (user.ChuoiNgayHocLienTiep ?? 0) >= ach.DieuKienGiaTri,
                    LoaiDieuKienEnum.TongDiemXP => (user.TongDiemXp ?? 0) >= ach.DieuKienGiaTri,
                    LoaiDieuKienEnum.SoTranThang => (user.SoTranThang ?? 0) >= ach.DieuKienGiaTri,
                    _ => false
                };

                if (!qualified)
                    continue;

                _context.ThanhTuuDatDuocs.Add(new ThanhTuuDatDuoc
                {
                    MaNguoiDung = userId,
                    MaThanhTuu = ach.MaThanhTuu,
                    NgayDat = DateTime.Now
                });

                user.Vang = (user.Vang ?? 0) + (ach.ThuongVang ?? 0);
                user.KimCuong = (user.KimCuong ?? 0) + (ach.ThuongKimCuong ?? 0);
                user.TongDiemXp = (user.TongDiemXp ?? 0) + (ach.ThuongXp ?? 0);

                hasUpdate = true;
            }

            if (hasUpdate)
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Hồi sinh chuỗi đã mất
    /// </summary>
    public async Task<string> RestoreStreakAsync(Guid userId)
    {
        var user = await _context.NguoiDungs.FindAsync(userId);
        if (user == null)
            return "Người dùng không tồn tại!";

        // Nếu chuỗi đã bị reset (= 1), hồi sinh lên 2
        if ((user.ChuoiNgayHocLienTiep ?? 0) == 1)
        {
            user.ChuoiNgayHocLienTiep = 2;
            user.IsStreakFrozen = false;
            user.NgayHoatDongCuoi = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();
            return "Đã hồi sinh chuỗi! Chuỗi hiện tại: 2 ngày";
        }
        else if ((user.ChuoiNgayHocLienTiep ?? 0) == 0)
        {
            user.ChuoiNgayHocLienTiep = 1;
            user.ChuoiNgayDaiNhat = 1;
            user.NgayHoatDongCuoi = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();
            return "Đã khởi động chuỗi! Chuỗi hiện tại: 1 ngày";
        }

        return "Chuỗi còn hoạt động, không cần hồi sinh!";
    }
}
