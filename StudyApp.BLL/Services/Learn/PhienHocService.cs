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
    public class PhienHocService : IPhienHocService
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public PhienHocService(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PhienHocResponse> BatDauPhienHocAsync(BatDauPhienHocRequest request)
        {
            var phienHoc = _mapper.Map<PhienHoc>(request);

            // Khởi tạo các giá trị ban đầu
            phienHoc.ThoiGianBatDau = DateTime.Now;
            phienHoc.SoTheDung = 0;
            phienHoc.SoTheSai = 0;

            _context.PhienHocs.Add(phienHoc);
            await _context.SaveChangesAsync();

            return _mapper.Map<PhienHocResponse>(phienHoc);
        }

        public async Task<ChiTietTraLoiResponse> NopCauTraLoiAsync(ChiTietTraLoiRequest request)
        {
            // 1. Lấy thông tin thẻ để lấy đáp án đúng
            var the = await _context.TheFlashcards.FindAsync(request.MaThe);
            if (the == null) throw new Exception("Thẻ không tồn tại.");

            // 2. Logic chấm điểm (So khớp đáp án)
            // Lưu ý: Tùy vao LoaiThe mà logic so sánh sẽ khác nhau (Text hoặc ID)
            bool isCorrect = string.Equals(
                request.CauTraLoiUser?.Trim(),
                the.MatSau?.Trim(),
                StringComparison.OrdinalIgnoreCase);

            // 3. Lưu chi tiết câu trả lời
            var chiTiet = _mapper.Map<ChiTietTraLoi>(request);
            chiTiet.TraLoiDung = isCorrect;
            chiTiet.DapAnDung = the.MatSau;
            chiTiet.ThoiGian = DateTime.Now;

            _context.ChiTietTraLois.Add(chiTiet);

            // 4. CẬP NHẬT TIẾN ĐỘ HỌC TẬP (SM-2 Algorithm)
            await CapNhatTienDoSM2(the.MaThe, request.MaPhien, isCorrect);

            // 5. Cập nhật thống kê nhanh cho Thẻ
            the.SoLuongHoc = (the.SoLuongHoc ?? 0) + 1;
            if (isCorrect) the.SoLanDung = (the.SoLanDung ?? 0) + 1;
            else the.SoLanSai = (the.SoLanSai ?? 0) + 1;

            await _context.SaveChangesAsync();
            return _mapper.Map<ChiTietTraLoiResponse>(chiTiet);
        }

        public async Task<PhienHocResponse> KetThucPhienHocAsync(KetThucPhienHocRequest request)
        {
            var phienHoc = await _context.PhienHocs.FindAsync(request.MaPhien);
            if (phienHoc == null) throw new Exception("Không tìm thấy phiên học.");

            // Map kết quả tổng kết từ Client hoặc tính toán từ DB
            _mapper.Map(request, phienHoc);
            phienHoc.ThoiGianKetThuc = DateTime.Now;

            // Tự động tạo một bản ghi lịch sử học bộ đề
            if (phienHoc.MaBoDe.HasValue)
            {
                var lichSu = new LichSuHocBoDe
                {
                    MaNguoiDung = phienHoc.MaNguoiDung,
                    MaBoDe = phienHoc.MaBoDe.Value,
                    MaPhien = phienHoc.MaPhien,
                    SoTheHoc = phienHoc.TongSoThe,
                    TyLeDung = phienHoc.TyLeDung,
                    ThoiGian = DateTime.Now
                };
                _context.LichSuHocBoDes.Add(lichSu);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<PhienHocResponse>(phienHoc);
        }

        /// <summary>
        /// Thuật toán lặp lại ngắt quãng SM-2 đơn giản hóa
        /// </summary>
        private async Task CapNhatTienDoSM2(int maThe, int maPhien, bool isCorrect)
        {
            var phien = await _context.PhienHocs.FindAsync(maPhien);
            if (phien == null)
                throw new Exception("Không tìm thấy phiên học.");

            var tienDo = await _context.TienDoHocTaps
                .FirstOrDefaultAsync(x => x.MaThe == maThe && x.MaNguoiDung == phien.MaNguoiDung);

            if (tienDo == null)
            {
                // Nếu thẻ mới học lần đầu
                tienDo = new TienDoHocTap
                {
                    MaNguoiDung = phien.MaNguoiDung,
                    MaThe = maThe,
                    HeSoDe = 2.5,
                    KhoangCachNgay = 0,
                    SoLanLap = 0,
                    ThoiGianTao = DateTime.Now,
                    SoLanDung = 0,
                    SoLanSai = 0
                };
                _context.TienDoHocTaps.Add(tienDo);
            }

            // Đảm bảo các trường nullable luôn có giá trị mặc định
            tienDo.HeSoDe ??= 2.5;
            tienDo.KhoangCachNgay ??= 0;
            tienDo.SoLanLap ??= 0;
            tienDo.SoLanDung ??= 0;
            tienDo.SoLanSai ??= 0;

            // Tính toán SM-2 cơ bản
            if (isCorrect)
            {
                if (tienDo.SoLanLap == 0)
                    tienDo.KhoangCachNgay = 1;
                else if (tienDo.SoLanLap == 1)
                    tienDo.KhoangCachNgay = 6;
                else
                    tienDo.KhoangCachNgay = (int)Math.Round(
                        (tienDo.KhoangCachNgay ?? 1) * (tienDo.HeSoDe ?? 2.5));

                tienDo.SoLanLap = (tienDo.SoLanLap ?? 0) + 1;
                tienDo.SoLanDung = (tienDo.SoLanDung ?? 0) + 1;
            }
            else
            {
                tienDo.SoLanLap = 0;
                tienDo.KhoangCachNgay = 1;
                tienDo.SoLanSai = (tienDo.SoLanSai ?? 0) + 1;
                tienDo.HeSoDe = Math.Max(1.3, (tienDo.HeSoDe ?? 2.5) - 0.1);
            }

            tienDo.NgayOnTapTiepTheo = DateTime.Now.AddDays(tienDo.KhoangCachNgay.Value);
            tienDo.LanHocCuoi = DateTime.Now;
        }
    }
}