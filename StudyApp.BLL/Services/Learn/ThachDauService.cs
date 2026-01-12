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
    public class ThachDauService : IThachDauService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public ThachDauService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. Tạo phòng thách đấu mới
        public async Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request)
        {
            var thachDau = _mapper.Map<ThachDau>(request);
            // TrangThai và ThoiGianTao đã được xử lý trong MappingProfile

            _context.ThachDaus.Add(thachDau);
            await _context.SaveChangesAsync();

            // Tự động thêm người tạo vào danh sách người chơi
            var chuPhong = new ThachDauNguoiChoi
            {
                MaThachDau = thachDau.MaThachDau,
                MaNguoiDung = request.NguoiTao,
                Diem = 0
            };
            _context.ThachDauNguoiChois.Add(chuPhong);
            await _context.SaveChangesAsync();

            return _mapper.Map<ThachDauResponse>(thachDau);
        }

        // 2. Tham gia phòng (dành cho đối thủ)
        public async Task<bool> ThamGiaThachDauAsync(ThamGiaThachDauRequest request)
        {
            var room = await _context.ThachDaus.FindAsync(request.MaThachDau);
            if (room == null || room.TrangThai != TrangThaiThachDauEnum.ChoNguoiChoi.ToString())
                return false;

            var isJoined = await _context.ThachDauNguoiChois
                .AnyAsync(x => x.MaThachDau == request.MaThachDau && x.MaNguoiDung == request.MaNguoiDung);

            if (isJoined) return true;

            var player = _mapper.Map<ThachDauNguoiChoi>(request);
            _context.ThachDauNguoiChois.Add(player);

            return await _context.SaveChangesAsync() > 0;
        }

        // 3. Bắt đầu thách đấu (Khóa phòng, không cho join thêm)
        public async Task<bool> BatDauThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.FindAsync(maThachDau);
            if (room == null) return false;

            room.TrangThai = TrangThaiThachDauEnum.DangDau.ToString();
            room.ThoiGianBatDau = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        // 4. Cập nhật kết quả sau khi người chơi hoàn thành bài học
        public async Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request)
        {
            var playerRecord = await _context.ThachDauNguoiChois
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau && x.MaNguoiDung == request.MaNguoiDung);

            if (playerRecord == null) return false;

            _mapper.Map(request, playerRecord);

            return await _context.SaveChangesAsync() > 0;
        }

        // 5. Kết thúc trận đấu và xếp hạng
        public async Task<bool> KetThucThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == maThachDau);

            if (room == null) return false;

            room.TrangThai = TrangThaiThachDauEnum.DaKetThuc.ToString();
            room.ThoiGianKetThuc = DateTime.Now;

            // Logic tìm người thắng cuộc (Dựa trên điểm cao nhất, nếu bằng điểm thì ai nhanh hơn)
            var winner = room.ThachDauNguoiChois
                .OrderByDescending(x => x.Diem)
                .ThenBy(x => x.ThoiGianLamBaiGiay)
                .FirstOrDefault();

            if (winner != null)
            {
                foreach (var p in room.ThachDauNguoiChois)
                {
                    p.LaNguoiThang = (p.MaNguoiDung == winner.MaNguoiDung);
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ThachDauResponse?> GetByIdAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.FindAsync(maThachDau);
            return _mapper.Map<ThachDauResponse>(room);
        }

        public async Task<IEnumerable<ThachDauNguoiChoiResponse>> GetBangXepHangAsync(int maThachDau)
        {
            var players = await _context.ThachDauNguoiChois
                .Where(x => x.MaThachDau == maThachDau)
                .OrderByDescending(x => x.Diem)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ThachDauNguoiChoiResponse>>(players);
        }

        public async Task<bool> HuyThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.FindAsync(maThachDau);
            if (room == null) return false;

            room.TrangThai = TrangThaiThachDauEnum.Huy.ToString();
            return await _context.SaveChangesAsync() > 0;
        }
    }
}