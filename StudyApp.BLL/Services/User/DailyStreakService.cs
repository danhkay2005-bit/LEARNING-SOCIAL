using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Services.User;

public class DailyStreakService(UserDbContext _context, IMapper _mapper) : IDailyStreakService
{
    public async Task<DiemDanhHangNgayResponse> CheckInDailyAsync(DiemDanhHangNgayRequest request)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == request.MaNguoiDung);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var homNay = request.NgayDiemDanh;
        var ngayHoatDongCuoi = user.NgayHoatDongCuoi;

        var soNgayChenhlech = 2;
        if (ngayHoatDongCuoi.HasValue)
        {
            soNgayChenhlech = homNay.DayNumber - ngayHoatDongCuoi.Value.DayNumber;
        }

        if (soNgayChenhlech == 0)
        {
            var existed = await _context.DiemDanhHangNgays
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.MaNguoiDung == user.MaNguoiDung && d.NgayDiemDanh == homNay);

            if (existed != null)
            {
                return _mapper.Map<DiemDanhHangNgayResponse>(existed);
            }

            throw new Exception("Hôm nay đã điểm danh rồi.");
        }

        if (soNgayChenhlech == 1)
        {
            user.ChuoiNgayHocLienTiep = (user.ChuoiNgayHocLienTiep ?? 0) + 1;
            if ((user.ChuoiNgayDaiNhat ?? 0) < (user.ChuoiNgayHocLienTiep ?? 0))
            {
                user.ChuoiNgayDaiNhat = user.ChuoiNgayHocLienTiep;
            }
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
}