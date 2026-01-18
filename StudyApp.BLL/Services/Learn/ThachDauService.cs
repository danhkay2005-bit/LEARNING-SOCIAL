using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Learn
{
    public class ThachDauService : IThachDauService
    {
        private readonly LearningDbContext _context;
        private readonly UserDbContext _userDb;
        private readonly IMapper _mapper;
        private readonly IThachDauNotifier _notifier;

        public ThachDauService(LearningDbContext context, UserDbContext userDb, IMapper mapper, IThachDauNotifier notifier)
        {
            _context = context;
            _userDb = userDb;
            _mapper = mapper;
            _notifier = notifier;
        }

        // ==========================================
        // QUẢN LÝ PHÒNG (ROOM)
        // ==========================================

        public async Task<ThachDauResponse> TaoThachDauAsync(TaoThachDauRequest request)
        {
            Random rnd = new Random();
            int pin;
            bool isExist;

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

            _context.LichSuThachDaus.Add(new LichSuThachDau
            {
                MaNguoiDung = request.NguoiTao,
                MaBoDe = request.MaBoDe,
                MaThachDauGoc = pin,
                Diem = null, // null nghĩa là đang thi
                SoTheDung = 0,
                SoTheSai = 0,
                ThoiGianLamBaiGiay = 0,
                LaNguoiThang = false,
                ThoiGianKetThuc = DateTime.Now
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
            // 1. Lấy danh sách người chơi
            var players = await _context.LichSuThachDaus
                .Where(x => x.MaThachDauGoc == maThachDau)
                .ToListAsync();

            if (players.Count == 0) return false;

            // FIX: Cho phép dọn dẹp nếu có ít nhất 1 người đã nộp bài (phòng trường hợp người kia thoát ngang)
            // Nếu muốn chặt chẽ hơn, bạn có thể giữ players.Count >= 2
            if (!players.Any(p => p.Diem != null)) return false;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Quy đổi tất cả Diem null thành 0 để tính toán thắng thua
                foreach (var p in players) { if (p.Diem == null) p.Diem = 0; }

                // 2. PHÂN ĐỊNH THẮNG THUA
                var winner = players
                    .OrderByDescending(x => x.Diem)
                    .ThenBy(x => x.ThoiGianLamBaiGiay)
                    .FirstOrDefault();

                if (winner != null)
                {
                    foreach (var p in players)
                    {
                        p.LaNguoiThang = (p.MaNguoiDung == winner.MaNguoiDung);
                        p.ThoiGianKetThuc = DateTime.Now;
                        _context.Entry(p).State = EntityState.Modified;

                        // 3. TÍNH TOÁN HIỆU NĂNG & XP
                        int totalCards = (p.SoTheDung ?? 0) + (p.SoTheSai ?? 0);
                        double accuracy = totalCards > 0 ? (double)(p.SoTheDung ?? 0) / totalCards : 0;

                        var user = await _userDb.NguoiDungs.FindAsync(p.MaNguoiDung);
                        if (user != null)
                        {
                            user.SoTranThachDau = (user.SoTranThachDau ?? 0) + 1;
                            if (p.LaNguoiThang == true)
                            {
                                user.SoTranThang = (user.SoTranThang ?? 0) + 1;
                                user.TongDiemXp = (user.TongDiemXp ?? 0) + (accuracy >= 0.8 ? 100 : 50);
                            }
                            else
                            {
                                user.SoTranThua = (user.SoTranThua ?? 0) + 1;
                                if (accuracy >= 0.5) user.TongDiemXp = (user.TongDiemXp ?? 0) + 10;
                            }
                        }
                    }
                }

                // 4. XÓA PHÒNG CHỜ (QUAN TRỌNG: Đây là chỗ xóa ID thách đấu)
                var room = await _context.ThachDaus.FindAsync(maThachDau);
                if (room != null)
                {
                    _context.ThachDaus.Remove(room);
                }

                await _context.SaveChangesAsync();
                await _userDb.SaveChangesAsync();

                await transaction.CommitAsync();
                Debug.WriteLine($"[Cleanup] Thành công phòng: {maThachDau}");
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.WriteLine($"[Cleanup Error] {ex.Message}");
                return false;
            }
        }

        public async Task<bool> HuyThachDauAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.FindAsync(maThachDau);
            if (room != null) _context.ThachDaus.Remove(room);

            // Xóa tất cả bản ghi liên quan đến phòng này để không làm rác bảng Lịch sử
            var pendingEntries = _context.LichSuThachDaus.Where(x => x.MaThachDauGoc == maThachDau);
            _context.LichSuThachDaus.RemoveRange(pendingEntries);

            return await _context.SaveChangesAsync() > 0;
        }

        // ==========================================
        // QUẢN LÝ NGƯỜI CHƠI
        // ==========================================

        public async Task<bool> ThamGiaThachDauAsync(LichSuThachDauRequest request)
        {
            var room = await _context.ThachDaus.FindAsync(request.MaThachDau);
            if (room == null || (room.TrangThai != "ChoNguoiChoi" && room.TrangThai != "DangDau")) return false;

            bool isAlreadyJoined = await _context.LichSuThachDaus
                .AnyAsync(x => x.MaThachDauGoc == request.MaThachDau && x.MaNguoiDung == request.MaNguoiDung);

            if (isAlreadyJoined) return true;

            _context.LichSuThachDaus.Add(new LichSuThachDau
            {
                MaNguoiDung = request.MaNguoiDung,
                MaBoDe = room.MaBoDe,
                MaThachDauGoc = request.MaThachDau,
                Diem = null,
                ThoiGianLamBaiGiay = 0,
                ThoiGianKetThuc = DateTime.Now
            });

            room.TrangThai = "DangDau";
            room.ThoiGianBatDau = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;
            if (result) await _notifier.NotifyReadyToStart(request.MaThachDau);

            return result;
        }

        public async Task<bool> CapNhatKetQuaNguoiChoiAsync(CapNhatKetQuaThachDauRequest request)
        {
            // FIX: Bỏ điều kiện Diem == null để có thể ghi đè dữ liệu test nhiều lần
            var record = await _context.LichSuThachDaus
                .FirstOrDefaultAsync(x => x.MaThachDauGoc == request.MaThachDau
                                       && x.MaNguoiDung == request.MaNguoiDung);

            if (record == null) return false;

            record.Diem = request.Diem ?? 0;
            record.SoTheDung = request.SoTheDung ?? 0;
            record.SoTheSai = request.SoTheSai ?? 0;
            record.ThoiGianLamBaiGiay = request.ThoiGianLamBaiGiay ?? 0;
            record.ThoiGianKetThuc = DateTime.Now;

            _context.Entry(record).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ThachDauResponse?> GetByIdAsync(int maThachDau)
        {
            var room = await _context.ThachDaus.AsNoTracking().FirstOrDefaultAsync(x => x.MaThachDau == maThachDau);
            return _mapper.Map<ThachDauResponse>(room);
        }

        public async Task<IEnumerable<ThachDauNguoiChoiResponse>> GetBangXepHangAsync(int maThachDau)
        {
            var players = await _context.LichSuThachDaus
                .AsNoTracking()
                .Where(x => x.MaThachDauGoc == maThachDau)
                .OrderByDescending(x => x.Diem)
                .ThenBy(x => x.ThoiGianLamBaiGiay)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ThachDauNguoiChoiResponse>>(players);
        }

        public async Task<bool> BaoCaoReadyNextAsync(int maThachDau, Guid userId, int questionIndex)
        {
            await _notifier.NotifyOpponentReadyNext(maThachDau, userId, questionIndex);
            return true;
        }
    }
}