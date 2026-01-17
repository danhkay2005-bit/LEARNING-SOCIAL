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

                if (room == null) return true; // Đã xóa bởi luồng khác

                // 1. Xác định người thắng
                var winner = room.ThachDauNguoiChois
                    .OrderByDescending(x => x.Diem)
                    .ThenBy(x => x.ThoiGianLamBaiGiay)
                    .FirstOrDefault();

                DateTime finishTime = DateTime.Now;

                // 2. Di chuyển dữ liệu sang bảng Lịch sử vĩnh viễn
                foreach (var p in room.ThachDauNguoiChois)
                {
                    bool isWinner = (winner != null && p.MaNguoiDung == winner.MaNguoiDung);

                    _context.LichSuThachDaus.Add(new LichSuThachDau
                    {
                        MaNguoiDung = p.MaNguoiDung,
                        MaBoDe = room.MaBoDe,
                        MaThachDauGoc = room.MaThachDau,
                        Diem = p.Diem ?? 0,
                        LaNguoiThang = isWinner,
                        ThoiGianKetThuc = finishTime
                    });

                    // Cập nhật bảng NguoiDung (User DB)
                    var user = await _userDb.NguoiDungs.FindAsync(p.MaNguoiDung);
                    if (user != null)
                    {
                        user.SoTranThachDau = (user.SoTranThachDau ?? 0) + 1;
                        if (isWinner) user.SoTranThang = (user.SoTranThang ?? 0) + 1;
                        else user.SoTranThua = (user.SoTranThua ?? 0) + 1;
                    }
                }

                // 3. LỆNH XÓA BẢNG TẠM: Giải quyết việc rác bảng ThachDau
                _context.ThachDauNguoiChois.RemoveRange(room.ThachDauNguoiChois);
                _context.ThachDaus.Remove(room);

                await _userDb.SaveChangesAsync();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> HuyThachDauAsync(int maThachDau)
        {
            // Thêm log để debug trong Visual Studio Output
            System.Diagnostics.Debug.WriteLine($"[Service] Đang yêu cầu hủy phòng: {maThachDau}");

            var room = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == maThachDau);

            if (room == null)
            {
                System.Diagnostics.Debug.WriteLine($"[Service] Không tìm thấy phòng {maThachDau} để xóa.");
                return false;
            }

            _context.ThachDauNguoiChois.RemoveRange(room.ThachDauNguoiChois);
            _context.ThachDaus.Remove(room);

            var result = await _context.SaveChangesAsync() > 0;
            System.Diagnostics.Debug.WriteLine($"[Service] Kết quả xóa phòng {maThachDau}: {result}");
            return result;
        }

        // ==========================================
        // QUẢN LÝ NGƯỜI CHƠI
        // ==========================================

        public async Task<bool> ThamGiaThachDauAsync(ThamGiaThachDauRequest request)
        {
            // 1. Lấy thông tin phòng kèm danh sách người chơi hiện tại
            var room = await _context.ThachDaus
                .Include(x => x.ThachDauNguoiChois)
                .FirstOrDefaultAsync(x => x.MaThachDau == request.MaThachDau);

            // Kiểm tra phòng tồn tại và chưa đủ 2 người
            if (room == null || room.ThachDauNguoiChois.Count >= 2) return false;

            // 2. Thêm người chơi thứ 2 nếu chưa có trong danh sách
            if (!room.ThachDauNguoiChois.Any(x => x.MaNguoiDung == request.MaNguoiDung))
            {
                _context.ThachDauNguoiChois.Add(new ThachDauNguoiChoi
                {
                    MaThachDau = request.MaThachDau,
                    MaNguoiDung = request.MaNguoiDung,
                    Diem = 0
                });
            }

            // 3. LOGIC QUAN TRỌNG: Nếu đủ 2 người, cập nhật trạng thái và thời gian bắt đầu
            // Điều này giúp giải quyết các dòng NULL trong ảnh bạn gửi
            if (room.ThachDauNguoiChois.Count + 1 == 2)
            {
                room.TrangThai = "DangDau";
                room.ThoiGianBatDau = DateTime.Now;
            }

            var isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved && room.TrangThai == "DangDau")
            {
                // Gửi tín hiệu SignalR để hai máy bắt đầu nhảy vào màn hình học
                await _notifier.NotifyReadyToStart(request.MaThachDau);
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