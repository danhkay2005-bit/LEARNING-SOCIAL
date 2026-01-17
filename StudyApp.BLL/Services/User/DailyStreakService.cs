using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;

namespace StudyApp.BLL.Services.User;

public class DailyStreakService(UserDbContext _context,  IMapper _mapper) : IDailyStreakService
{
    public async Task<DiemDanhHangNgayResponse> CheckInDailyAsync(DiemDanhHangNgayRequest request)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == request.MaNguoiDung);
        if (user == null) throw new Exception("User not found");

        var homNay = request.NgayDiemDanh;
        var ngayHoatDongCuoi = user.NgayHoatDongCuoi;

        var soNgayChenhlech = 2;
        if (ngayHoatDongCuoi.HasValue)
            soNgayChenhlech = homNay.DayNumber - ngayHoatDongCuoi.Value.DayNumber;

        if (soNgayChenhlech == 0)
        {
            var existed = await _context.DiemDanhHangNgays
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.MaNguoiDung == user.MaNguoiDung && d.NgayDiemDanh == homNay);

            if (existed != null)
                return _mapper.Map<DiemDanhHangNgayResponse>(existed);

            throw new Exception("Hôm nay đã điểm danh rồi.");
        }

        if (soNgayChenhlech == 1)
        {
            user.ChuoiNgayHocLienTiep = (user.ChuoiNgayHocLienTiep ?? 0) + 1;
            if ((user.ChuoiNgayDaiNhat ?? 0) < (user.ChuoiNgayHocLienTiep ?? 0))
                user.ChuoiNgayDaiNhat = user.ChuoiNgayHocLienTiep;
        }
        else
        {
            if ((user.SoStreakFreeze ?? 0) > 0)
            {
                user.SoStreakFreeze = (user.SoStreakFreeze ?? 0) - 1;

                _context.BaoVeChuoiNgays.Add(new BaoVeChuoiNgay
                {
                    MaNguoiDung = user.MaNguoiDung,
                    NgaySuDung = homNay,
                    LoaiBaoVe = "Freeze",
                    ChuoiNgayTruocKhi = user.ChuoiNgayHocLienTiep,
                    ChuoiNgaySauKhi = user.ChuoiNgayHocLienTiep
                });
            }
            else
            {
                user.ChuoiNgayHocLienTiep = 1;
            }
        }

        user.Vang = (user.Vang ?? 0) + 10;
        user.NgayHoatDongCuoi = homNay;

        var diemDanh = new DiemDanhHangNgay
        {
            MaNguoiDung = user.MaNguoiDung,
            NgayDiemDanh = homNay,
            NgayThuMay = user.ChuoiNgayHocLienTiep,
            ThuongVang = 10,
            ThuongXp = 0
        };

        _context.DiemDanhHangNgays.Add(diemDanh);
        await _context.SaveChangesAsync();

        return _mapper.Map<DiemDanhHangNgayResponse>(diemDanh);
    }

    public async Task MarkLessonCompletedTodayAsync(Guid userId)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == userId);
        if (user == null) return;

        var today = DateOnly.FromDateTime(DateTime.Now);

        // Đánh dấu đã học hôm nay (đủ điều kiện để claim quest thì cộng streak)
        user.NgayHoatDongCuoi = today;
        await _context.SaveChangesAsync();
    }

    public async Task TryGrantStreakForTodayAsync(Guid userId)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == userId);
        if (user == null) return;

        var today = DateOnly.FromDateTime(DateTime.Now);

        // Chỉ cộng nếu hôm nay đã học xong ít nhất 1 bộ đề
        if (user.NgayHoatDongCuoi != today)
            return;

        // Chặn cộng nhiều lần trong ngày (đã cộng rồi thì thôi)
        var alreadyGranted = await _context.BaoVeChuoiNgays
            .AsNoTracking()
            .AnyAsync(x => x.MaNguoiDung == userId && x.NgaySuDung == today && x.LoaiBaoVe == "StreakGranted");

        if (alreadyGranted)
            return;

        // Tăng streak theo ngày
        // Nếu hôm qua vẫn hoạt động => +1, ngược lại reset về 1
        var lastDay = user.NgayHoatDongCuoi; // today

        // Ta cần last-real-streak-day: dùng LanOnlineCuoi hoặc lưu riêng sẽ chuẩn hơn.
        // Nhanh nhất: dùng BaoVeChuoiNgay record gần nhất để biết đã cộng streak ngày nào.
        var lastGrantedDay = await _context.BaoVeChuoiNgays
            .AsNoTracking()
            .Where(x => x.MaNguoiDung == userId && x.LoaiBaoVe == "StreakGranted")
            .OrderByDescending(x => x.NgaySuDung)
            .Select(x => (DateOnly?)x.NgaySuDung)
            .FirstOrDefaultAsync();

        if (lastGrantedDay.HasValue && today.DayNumber - lastGrantedDay.Value.DayNumber == 1)
            user.ChuoiNgayHocLienTiep = (user.ChuoiNgayHocLienTiep ?? 0) + 1;
        else
            user.ChuoiNgayHocLienTiep = 1;

        user.ChuoiNgayDaiNhat = Math.Max(user.ChuoiNgayDaiNhat ?? 0, user.ChuoiNgayHocLienTiep ?? 0);

        _context.BaoVeChuoiNgays.Add(new BaoVeChuoiNgay
        {
            MaNguoiDung = userId,
            NgaySuDung = today,
            LoaiBaoVe = "StreakGranted",
            ChuoiNgayTruocKhi = (user.ChuoiNgayHocLienTiep ?? 1) - 1,
            ChuoiNgaySauKhi = user.ChuoiNgayHocLienTiep
        });

        await _context.SaveChangesAsync();
    }
}