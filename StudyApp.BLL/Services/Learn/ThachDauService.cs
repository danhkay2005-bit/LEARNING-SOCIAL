using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Learn
{
    public class ThachDauService : IThachDauService
    {
        private readonly LearningDbContext _context;
        private readonly UserDbContext _userDb;
        private readonly IMapper _mapper;
        private readonly IThachDauNotifier _notifier;// Tiêm HubContext

        public ThachDauService(LearningDbContext context, UserDbContext userDb, IMapper mapper, IThachDauNotifier notifier)
        {
            _context = context;
            _mapper = mapper;
            _notifier = notifier;
            _userDb = userDb;
        }

        // ==========================================
        // QUẢN LÝ PHÒNG (ROOM)
        // ==========================================

        public async Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request)
        {
            Random rnd = new Random();
            int pin;
            bool isExist;

            // Đảm bảo mã PIN 6 số là duy nhất trong các phòng đang hoạt động
            do
            {
                pin = rnd.Next(100000, 999999);
                isExist = await _context.ThachDaus.AnyAsync(x => x.MaThachDau == pin);
            } while (isExist);

            var thachDau = new ThachDau
            {
                MaThachDau = pin,
                MaBoDe = request.MaBoDe,
                NguoiTao = request.NguoiTao,
                TrangThai = "ChoNguoiChoi",
                ThoiGianTao = DateTime.Now
            };

            _context.ThachDaus.Add(thachDau);

            // Thêm ngay Người tạo (User A) vào danh sách người chơi
            _context.ThachDauNguoiChois.Add(new ThachDauNguoiChoi
            {
                MaThachDau = pin,
                MaNguoiDung = request.NguoiTao,
                Diem = 0
            });

            await _context.SaveChangesAsync();
            return _mapper.Map<ThachDauResponse>(thachDau);
        }

        public async Task<bool> BatDauThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.FindAsync(maThachDau);
            if (room == null) return false;

            room.TrangThai = "DangDau";
            room.ThoiGianBatDau = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HoanThanhVaCleanupAsync(int maThachDau)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var room = await _context.ThachDaus
                    .Include(x => x.ThachDauNguoiChois)
                    .FirstOrDefaultAsync(x => x.MaThachDau == maThachDau);

                if (room == null) return false;

                // 1. Xác định người thắng cuộc
                var winner = room.ThachDauNguoiChois
                    .OrderByDescending(x => x.Diem)
                    .ThenBy(x => x.ThoiGianLamBaiGiay)
                    .FirstOrDefault();

                // 2. Lưu lịch sử và Cập nhật chỉ số User
                foreach (var p in room.ThachDauNguoiChois)
                {
                    // --- PHẦN LƯU LỊCH SỬ VĨNH VIỄN ---
                    bool isWinner = (winner != null && p.MaNguoiDung == winner.MaNguoiDung);

                    _context.LichSuThachDaus.Add(new LichSuThachDau
                    {
                        MaNguoiDung = p.MaNguoiDung,
                        MaBoDe = room.MaBoDe,
                        MaThachDauGoc = room.MaThachDau,
                        Diem = p.Diem ?? 0,
                        SoTheDung = p.SoTheDung ?? 0,
                        SoTheSai = p.SoTheSai ?? 0,
                        ThoiGianLamBaiGiay = p.ThoiGianLamBaiGiay ?? 0,
                        LaNguoiThang = isWinner,
                        ThoiGianKetThuc = DateTime.Now
                    });

                    // --- PHẦN CẬP NHẬT BẢNG NGUOIDUNG ---
                    // Tìm user tương ứng trong DbContext
                    var user = await _userDb.NguoiDungs.FindAsync(p.MaNguoiDung);
                    if (user != null)
                    {
                        // Tăng tổng số trận thách đấu
                        user.SoTranThachDau = (user.SoTranThachDau ?? 0) + 1;

                        if (isWinner)
                        {
                            // Nếu thắng
                            user.SoTranThang = (user.SoTranThang ?? 0) + 1;
                        }
                        else
                        {
                            // Nếu thua (hoặc hòa nhưng không phải winner)
                            user.SoTranThua = (user.SoTranThua ?? 0) + 1;
                        }

                        _userDb.NguoiDungs.Update(user);
                    }
                }

                // 3. Xóa phòng tạm
                _context.ThachDauNguoiChois.RemoveRange(room.ThachDauNguoiChois);
                _context.ThachDaus.Remove(room);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> HuyThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == maThachDau);

            if (room == null) return false;

            _context.ThachDauNguoiChois.RemoveRange(room.ThachDauNguoiChois);
            _context.ThachDaus.Remove(room);

            return await _context.SaveChangesAsync() > 0;
        }

        // ==========================================
        // QUẢN LÝ NGƯỜI CHƠI
        // ==========================================

        public async Task<bool> ThamGiaThachDauAsync(ThamGiaThachDauRequest request)
        {
            var room = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau);

            if (room == null || room.ThachDauNguoiChois.Count >= 2) return false;

            // Tránh thêm trùng
            if (!room.ThachDauNguoiChois.Any(x => x.MaNguoiDung == request.MaNguoiDung))
            {
                _context.ThachDauNguoiChois.Add(new ThachDauNguoiChoi
                {
                    MaThachDau = request.MaThachDau,
                    MaNguoiDung = request.MaNguoiDung,
                    Diem = 0
                });
            }

            var isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved)
            {
                var currentCount = await _context.ThachDauNguoiChois.CountAsync(x => x.MaThachDau == request.MaThachDau);
                if (currentCount == 2)
                {
                    // Gọi qua Notifier
                    await _notifier.NotifyReadyToStart(request.MaThachDau);
                }
            }
            return isSaved;
        }

        public async Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request)
        {
            var playerRecord = await _context.ThachDauNguoiChois
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau && x.MaNguoiDung == request.MaNguoiDung);

            if (playerRecord == null) return false;

            // Ánh xạ dữ liệu từ request vào record trong DB
            _mapper.Map(request, playerRecord);
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                // Gửi thông báo SignalR thông qua Notifier
                // request.Diem ?? 0 giải quyết lỗi "cannot convert from 'int?' to 'int'"
                await _notifier.NotifyUpdateScore(request.MaThachDau, request.MaNguoiDung, request.Diem ?? 0);
            }
            return result;
        }

        // ==========================================
        // TRUY VẤN DỮ LIỆU
        // ==========================================

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
                .ThenBy(x => x.ThoiGianLamBaiGiay) // Ưu tiên người nhanh hơn nếu bằng điểm
                .ToListAsync();

            return _mapper.Map<IEnumerable<ThachDauNguoiChoiResponse>>(players);
        }

        public async Task<bool> BaoCaoReadyNextAsync(int maThachDau, Guid userId, int questionIndex)
        {
            // 1. Cập nhật trạng thái câu hỏi hiện tại của người chơi vào DB (Tùy chọn - Tăng tính an toàn)
            var player = await _context.ThachDauNguoiChois
                .FirstOrDefaultAsync(x => x.MaThachDau == maThachDau && x.MaNguoiDung == userId);

            if (player == null) return false;

            // Giả sử bạn có cột ViTriCauHoiHienTai trong bảng ThachDauNguoiChoi
            // player.ViTriCauHoiHienTai = questionIndex; 

            var result = await _context.SaveChangesAsync() >= 0; // Luôn trả về true nếu Notify thành công

            if (result)
            {
                // 2. Gửi tín hiệu Real-time qua Notifier
                // Notifier sẽ gọi _hubContext.Clients.Group(...).SendAsync("OpponentReadyNext", ...)
                await _notifier.NotifyOpponentReadyNext(maThachDau, userId, questionIndex);
            }

            return result;
        }
    }
}