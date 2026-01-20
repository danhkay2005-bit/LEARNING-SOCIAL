using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.User
{
    public class DailyStreakService : IDailyStreakService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public DailyStreakService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // =========================================================================
        // 1. HÀM CHO NÚT ĐIỂM DANH (CHỈ NHẬN QUÀ - KHÔNG TĂNG STREAK)
        // =========================================================================
        public async Task<DiemDanhHangNgayResponse> CheckInDailyAsync(DiemDanhHangNgayRequest request)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            // B1: Kiểm tra xem hôm nay đã bấm nút nhận quà chưa
            bool daNhanQua = await _context.DiemDanhHangNgays
                .AnyAsync(x => x.MaNguoiDung == request.MaNguoiDung && x.NgayDiemDanh == today);

            if (daNhanQua)
                throw new Exception("Hôm nay bạn đã nhận quà điểm danh rồi!");

            var user = await _context.NguoiDungs.FindAsync(request.MaNguoiDung);
            if (user == null) throw new Exception("User không tồn tại");

            // B2: Lấy streak hiện tại để tính mức thưởng (Streak cao -> Quà to)
            // LƯU Ý: Chỉ lấy ra để tính, KHÔNG cộng thêm streak ở đây.
            int currentStreak = user.ChuoiNgayHocLienTiep ?? 0;

            // B3: Tính quà (Ví dụ: 100 vàng + 10 * streak)
            int rewardGold = 100 + (currentStreak * 10);
            int rewardXP = 50 + (currentStreak * 5);

            // B4: Lưu lịch sử "Đã nhận quà"
            var entity = new DiemDanhHangNgay
            {
                MaNguoiDung = request.MaNguoiDung,
                NgayDiemDanh = today,
                NgayThuMay = currentStreak,
                ThuongVang = rewardGold,
                ThuongXp = rewardXP,
                ThuongDacBiet = null
            };

            _context.DiemDanhHangNgays.Add(entity);

            // B5: CỘNG TIỀN VÀO VÍ
            user.Vang = (user.Vang ?? 0) + rewardGold;
            user.TongDiemXp = (user.TongDiemXp ?? 0) + rewardXP;

            // ⚠️ QUAN TRỌNG: Không đụng vào user.ChuoiNgayHocLienTiep ở hàm này

            await _context.SaveChangesAsync();
            return _mapper.Map<DiemDanhHangNgayResponse>(entity);
        }

        // =========================================================================
        // 2. HÀM CHO NHIỆM VỤ (XỬ LÝ STREAK + BẢO VỆ STREAK CŨ CỦA BẠN)
        // Gọi hàm này khi User hoàn thành 1 nhiệm vụ/bài học
        // =========================================================================
        public async Task TryGrantStreakForTodayAsync(Guid userId)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == userId);
            if (user == null) return;

            var today = DateOnly.FromDateTime(DateTime.Now);

            // Lấy ngày hoạt động gần nhất (đã được tính streak)
            // Nếu dùng NgayHoatDongCuoi thì đơn giản, hoặc check bảng BaoVeChuoiNgay
            var lastActive = user.NgayHoatDongCuoi.HasValue
                    ? user.NgayHoatDongCuoi.Value
                    : (DateOnly?)null;

            // Nếu hôm nay đã tính streak rồi thì thôi
            if (lastActive == today) return;

            int streakHienTai = user.ChuoiNgayHocLienTiep ?? 0;
            int khoangCachNgay = 0;

            if (lastActive.HasValue)
            {
                khoangCachNgay = today.DayNumber - lastActive.Value.DayNumber;
            }
            else
            {
                // Người dùng mới toanh
                khoangCachNgay = 1;
            }

            // --- LOGIC CŨ CỦA BẠN: XỬ LÝ STREAK & BẢO VỆ ---

            // Trường hợp 1: Liên tục (Hôm qua có học)
            if (khoangCachNgay == 1)
            {
                //user.ChuoiNgayHocLienTiep = streakHienTai + 1;
                user.ChuoiNgayHocLienTiep = (user.ChuoiNgayHocLienTiep ?? 0) + 1;
            }
            // Trường hợp 2: Bị ngắt quãng (Khoảng cách > 1 ngày) -> Cần dùng BẢO VỆ (Freeze)
            else if (khoangCachNgay > 1)
            {
                // Kiểm tra xem có item Freeze không (SoStreakFreeze > 0)
                if ((user.SoStreakFreeze ?? 0) > 0)
                {
                    // Dùng 1 cái Freeze để cứu chuỗi
                    user.SoStreakFreeze--;

                    // Giữ nguyên chuỗi cũ (hoặc cộng thêm 1 tùy logic game của bạn)
                    // Ở đây tôi để cộng thêm 1 vì hôm nay đã quay lại học
                    user.ChuoiNgayHocLienTiep = streakHienTai + 1;

                    // Ghi log bảo vệ
                    _context.BaoVeChuoiNgays.Add(new BaoVeChuoiNgay
                    {
                        MaNguoiDung = userId,
                        NgaySuDung = today,
                        LoaiBaoVe = "Freeze",
                        ChuoiNgayTruocKhi = streakHienTai,
                        ChuoiNgaySauKhi = user.ChuoiNgayHocLienTiep
                    });
                }
                else
                {
                    // Không có bảo vệ -> Reset về 1
                    user.ChuoiNgayHocLienTiep = 1;
                }
            }
            // Trường hợp 3: Người mới
            else
            {
                user.ChuoiNgayHocLienTiep = 1;
            }

            // Cập nhật kỷ lục
            if ((user.ChuoiNgayHocLienTiep ?? 0) > (user.ChuoiNgayDaiNhat ?? 0))
            {
                user.ChuoiNgayDaiNhat = user.ChuoiNgayHocLienTiep;
            }

            // Đánh dấu hôm nay đã hoạt động
            user.NgayHoatDongCuoi = DateOnly.FromDateTime(DateTime.Now);

            // Thưởng thêm Vàng/XP cho việc hoàn thành nhiệm vụ (nếu muốn)
            // user.Vang += 50; 

            await _context.SaveChangesAsync();
        }

        // Hàm giữ chỗ để tránh lỗi Interface
        // public Task MarkLessonCompletedTodayAsync(Guid userId) => Task.CompletedTask;
        public async Task MarkLessonCompletedTodayAsync(Guid userId)
        {
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == userId);
            if (user == null) return;

            var today = DateOnly.FromDateTime(DateTime.Now);

            // Đánh dấu đã học hôm nay (đủ điều kiện để claim quest thì cộng streak)
            user.NgayHoatDongCuoi = today;
            await _context.SaveChangesAsync();
        }
    }
}