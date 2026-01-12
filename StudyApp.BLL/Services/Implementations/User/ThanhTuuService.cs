using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class ThanhTuuService(UserDbContext db, IMapper mapper) : IThanhTuuService
    {
        private readonly UserDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<List<ThanhTuuResponse>> GetAchievementsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var allAchievements = await _db.ThanhTuus
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var userAchievements = await _db.ThanhTuuDatDuocs
                .AsNoTracking()
                .Where(x => x.MaNguoiDung == maNguoiDung)
                .ToListAsync(cancellationToken);

            var result = new List<ThanhTuuResponse>();

            foreach (var ach in allAchievements)
            {
                var unlocked = userAchievements.FirstOrDefault(x => x.MaThanhTuu == ach.MaThanhTuu);

                var dto = _mapper.Map<ThanhTuuResponse>(ach);
                dto.DaDatDuoc = (unlocked != null);
                dto.NgayDat = unlocked?.NgayDat;

                result.Add(dto);
            }

            return result;
        }

        public async Task CheckAndUnlockAchievementAsync(Guid maNguoiDung, string loaiThanhTuu, int giaTriHienTai, CancellationToken cancellationToken = default)
        {
            // Lấy các thành tựu thuộc loại này mà user CHƯA đạt được
            // Logic: Lấy tất cả thành tựu loại X -> Lọc bỏ những cái đã có trong bảng ThanhTuuDatDuoc

            var potentialAchievements = await _db.ThanhTuus
                .Where(t => t.LoaiThanhTuu == loaiThanhTuu)
                .ToListAsync(cancellationToken);

            var alreadyUnlockedIds = await _db.ThanhTuuDatDuocs
                .Where(x => x.MaNguoiDung == maNguoiDung)
                .Select(x => x.MaThanhTuu)
                .ToListAsync(cancellationToken);

            var user = await _db.NguoiDungs.FindAsync([maNguoiDung], cancellationToken);
            if (user == null) return;

            bool hasNewUnlock = false;

            foreach (var ach in potentialAchievements)
            {
                // Nếu đã đạt rồi thì bỏ qua
                if (alreadyUnlockedIds.Contains(ach.MaThanhTuu)) continue;

                // Kiểm tra điều kiện (Ví dụ: Đạt 1000 điểm XP >= 1000)
                if (giaTriHienTai >= ach.DieuKienGiaTri)
                {
                    // Mở khóa thành tựu
                    _db.ThanhTuuDatDuocs.Add(new ThanhTuuDatDuoc
                    {
                        MaNguoiDung = maNguoiDung,
                        MaThanhTuu = ach.MaThanhTuu,
                        NgayDat = DateTime.Now,
                        DaXem = false,
                        DaChiaSe = false
                    });

                    // Cộng thưởng ngay lập tức
                    user.Vang = (user.Vang ?? 0) + (ach.ThuongVang ?? 0);
                    user.KimCuong = (user.KimCuong ?? 0) + (ach.ThuongKimCuong ?? 0);
                    user.TongDiemXp = (user.TongDiemXp ?? 0) + (ach.ThuongXp ?? 0);

                    hasNewUnlock = true;
                }
            }

            if (hasNewUnlock)
            {
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}