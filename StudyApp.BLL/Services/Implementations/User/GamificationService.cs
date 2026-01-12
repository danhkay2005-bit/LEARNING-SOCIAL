using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class GamificationService(UserDbContext db) : IGamificationService
    {
        private readonly UserDbContext _db = db;

        public async Task<DiemDanhHangNgayResponse?> DiemDanhHangNgayAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var daDiemDanh = await _db.DiemDanhHangNgays.AnyAsync(
                d => d.MaNguoiDung == maNguoiDung && d.NgayDiemDanh == today,
                cancellationToken);

            if (daDiemDanh)
                return null;

            int dayCount = 1;

            var lastCheckin = await _db.DiemDanhHangNgays
                .AsNoTracking()
                .Where(d => d.MaNguoiDung == maNguoiDung)
                .OrderByDescending(d => d.NgayDiemDanh)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastCheckin != null && lastCheckin.NgayDiemDanh == today.AddDays(-1))
            {
                dayCount = (lastCheckin.NgayThuMay ?? 0) + 1;
            }

            int thuongVang = 10 + (dayCount * 5);
            int thuongXp = 20;



            

            _db.DiemDanhHangNgays.AddRange([new DiemDanhHangNgay
            {
                MaNguoiDung = maNguoiDung,
                NgayDiemDanh = today,
                NgayThuMay = dayCount,
                ThuongVang = thuongVang,
                ThuongXp = thuongXp
            }]);

            var user = await _db.NguoiDungs.FindAsync([maNguoiDung], cancellationToken);
            if (user != null)
            {
                user.Vang = (user.Vang ?? 0) + thuongVang;
                user.TongDiemXp = (user.TongDiemXp ?? 0) + thuongXp;

                user.ChuoiNgayHocLienTiep = dayCount;
                if (dayCount > (user.ChuoiNgayDaiNhat ?? 0))
                {
                    user.ChuoiNgayDaiNhat = dayCount;
                }
            }

            await _db.SaveChangesAsync(cancellationToken);

            return new DiemDanhHangNgayResponse
            {
                MaNguoiDung = maNguoiDung,
                NgayDiemDanh = today,
                NgayThuMay = dayCount,
                ThuongVang = thuongVang,
                ThuongXp = thuongXp
            };
        }

        public async Task<bool> CheckDiemDanhTodayAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            return await _db.DiemDanhHangNgays.AnyAsync(
                d => d.MaNguoiDung == maNguoiDung && d.NgayDiemDanh == today,
                cancellationToken);
        }

        public async Task ProcessLoginStreakAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var user = await _db.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung, cancellationToken);
            if (user == null) return;

            var today = DateOnly.FromDateTime(DateTime.Today);
            var lastActive = user.NgayHoatDongCuoi ?? today;

            if (lastActive < today.AddDays(-1))
            {
                if ((user.SoStreakFreeze ?? 0) > 0)
                {
                    user.SoStreakFreeze--;                  
                    _db.BaoVeChuoiNgays.AddRange([new BaoVeChuoiNgay
                    {
                        MaNguoiDung = maNguoiDung,
                        NgaySuDung = today.AddDays(-1),
                        LoaiBaoVe = "Freeze",
                        ChuoiNgayTruocKhi = user.ChuoiNgayHocLienTiep,
                        ChuoiNgaySauKhi = user.ChuoiNgayHocLienTiep
                    }]);
                }
                else
                {
                    user.ChuoiNgayHocLienTiep = 0;
                }
            }

            user.NgayHoatDongCuoi = today;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}