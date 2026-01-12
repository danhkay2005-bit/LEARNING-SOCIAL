using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace StudyApp.BLL.Services.Learn
{
    public class TienDoHocTapService : ITienDoHocTapService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public TienDoHocTapService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy các thẻ đã đến hạn ôn tập (NgayOnTapTiepTheo <= Hiện tại)
        /// </summary>
        public async Task<IEnumerable<TienDoHocTapSummaryResponse>> GetDanhSachCanOnTapAsync(Guid userId, int? maBoDe = null)
        {
            var query = _context.TienDoHocTaps
                .Include(x => x.MaTheNavigation)
                .Where(x => x.MaNguoiDung == userId &&
                            x.NgayOnTapTiepTheo <= DateTime.Now);

            if (maBoDe.HasValue)
            {
                query = query.Where(x => x.MaTheNavigation.MaBoDe == maBoDe.Value);
            }

            var list = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TienDoHocTapSummaryResponse>>(list);
        }

        public async Task<TienDoHocTapResponse> InitTienDoAsync(TaoTienDoHocTapRequest request)
        {
            // Kiểm tra xem đã có bản ghi tiến độ chưa
            var existing = await _context.TienDoHocTaps
                .FirstOrDefaultAsync(x => x.MaNguoiDung == request.MaNguoiDung && x.MaThe == request.MaThe);

            if (existing != null) return _mapper.Map<TienDoHocTapResponse>(existing);

            // Nếu chưa, tạo mới ở trạng thái "New"
            var tienDo = _mapper.Map<TienDoHocTap>(request);
            tienDo.HeSoDe = 2.5;
            tienDo.KhoangCachNgay = 0;
            tienDo.SoLanLap = 0;
            tienDo.ThoiGianTao = DateTime.Now;
            tienDo.NgayOnTapTiepTheo = DateTime.Now; // Học ngay lập tức

            _context.TienDoHocTaps.Add(tienDo);
            await _context.SaveChangesAsync();

            return _mapper.Map<TienDoHocTapResponse>(tienDo);
        }

        public async Task<object> GetThongKeTienDoAsync(Guid userId, int maBoDe)
        {
            var statusCounts = await _context.TienDoHocTaps
                .Where(x => x.MaNguoiDung == userId && x.MaTheNavigation.MaBoDe == maBoDe)
                .GroupBy(x => x.TrangThai)
                .Select(g => new
                {
                    TrangThai = (TrangThaiHocEnum)(g.Key ?? 0),
                    SoLuong = g.Count()
                })
                .ToListAsync();

            return statusCounts;
        }

        public async Task<bool> ResetTienDoAsync(int maTienDo)
        {
            var tienDo = await _context.TienDoHocTaps.FindAsync(maTienDo);
            if (tienDo == null) return false;

            tienDo.TrangThai = (byte)TrangThaiHocEnum.New;
            tienDo.HeSoDe = 2.5;
            tienDo.KhoangCachNgay = 0;
            tienDo.SoLanLap = 0;
            tienDo.NgayOnTapTiepTheo = DateTime.Now;
            tienDo.SoLanDung = 0;
            tienDo.SoLanSai = 0;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}