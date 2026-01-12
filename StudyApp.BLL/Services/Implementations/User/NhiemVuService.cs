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
    public class NhiemVuService(UserDbContext db, IMapper mapper) : INhiemVuService
    {
        private readonly UserDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<List<NhiemVuResponse>> GetUserQuestsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            // 1. Lấy danh sách Nhiệm vụ còn hiệu lực
            var allQuests = await _db.NhiemVus
                .AsNoTracking()
                .Where(n => n.ConHieuLuc == true)
                .ToListAsync(cancellationToken);

            // 2. Lấy tiến độ của User
            var userProgress = await _db.TienDoNhiemVus
                .AsNoTracking()
                .Where(td => td.MaNguoiDung == maNguoiDung)
                .ToListAsync(cancellationToken);

            // 3. Ghép dữ liệu (Mapping thủ công hoặc AutoMapper tùy chỉnh)
            var responseList = new List<NhiemVuResponse>();

            foreach (var quest in allQuests)
            {
                var progress = userProgress.FirstOrDefault(p => p.MaNhiemVu == quest.MaNhiemVu);

                // Map từ Entity sang DTO
                var dto = _mapper.Map<NhiemVuResponse>(quest);

                // Điền thêm thông tin tiến độ
                dto.TienDoHienTai = progress?.TienDoHienTai ?? 0;
                dto.DaHoanThanh = progress?.DaHoanThanh ?? false;
                dto.DaNhanThuong = progress?.DaNhanThuong ?? false;

                responseList.Add(dto);
            }

            return responseList;
        }

        public async Task UpdateQuestProgressAsync(Guid maNguoiDung, string loaiDieuKien, int giaTriThem, CancellationToken cancellationToken = default)
        {
            // Tìm tất cả nhiệm vụ có điều kiện tương ứng (VD: 'HocBai', 'DangNhap')
            var quests = await _db.NhiemVus
                .Where(n => n.LoaiDieuKien == loaiDieuKien && n.ConHieuLuc == true)
                .ToListAsync(cancellationToken);

            foreach (var quest in quests)
            {
                var progress = await _db.TienDoNhiemVus
                    .FirstOrDefaultAsync(p => p.MaNguoiDung == maNguoiDung && p.MaNhiemVu == quest.MaNhiemVu, cancellationToken);

                if (progress == null)
                {
                    // Nếu chưa có tiến độ thì tạo mới
                    progress = new TienDoNhiemVu
                    {
                        MaNguoiDung = maNguoiDung,
                        MaNhiemVu = quest.MaNhiemVu,
                        TienDoHienTai = 0,
                        DaHoanThanh = false,
                        DaNhanThuong = false,
                        NgayBatDau = DateOnly.FromDateTime(DateTime.Now)
                    };
                    _db.TienDoNhiemVus.Add(progress);
                }

                // Nếu chưa hoàn thành thì cộng tiến độ
                if (progress.DaHoanThanh != true)
                {
                    progress.TienDoHienTai += giaTriThem;

                    // Kiểm tra xem đã đủ điều kiện hoàn thành chưa
                    if (progress.TienDoHienTai >= quest.DieuKienDatDuoc)
                    {
                        progress.DaHoanThanh = true;
                        progress.NgayHoanThanh = DateTime.Now;
                    }
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ClaimQuestRewardAsync(Guid maNguoiDung, int maNhiemVu, CancellationToken cancellationToken = default)
        {
            var progress = await _db.TienDoNhiemVus
                .FirstOrDefaultAsync(p => p.MaNguoiDung == maNguoiDung && p.MaNhiemVu == maNhiemVu, cancellationToken);

            // Validate: Phải hoàn thành và chưa nhận thưởng
            if (progress == null || progress.DaHoanThanh != true || progress.DaNhanThuong == true)
                return false;

            var quest = await _db.NhiemVus.FindAsync([maNhiemVu], cancellationToken);
            var user = await _db.NguoiDungs.FindAsync([maNguoiDung], cancellationToken);

            if (quest == null || user == null) return false;

            // Cộng thưởng cho User
            user.Vang = (user.Vang ?? 0) + (quest.ThuongVang ?? 0);
            user.KimCuong = (user.KimCuong ?? 0) + (quest.ThuongKimCuong ?? 0);
            user.TongDiemXp = (user.TongDiemXp ?? 0) + (quest.ThuongXp ?? 0);

            // Đánh dấu đã nhận
            progress.DaNhanThuong = true;

            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}