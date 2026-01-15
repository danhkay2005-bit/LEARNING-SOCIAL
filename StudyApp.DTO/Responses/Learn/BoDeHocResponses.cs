using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    public class HocBoDeResponse
    {
        // Thông tin chung của bộ đề (Tiêu đề, mô tả...)
        public BoDeHocResponse ThongTinChung { get; set; } = null!;

        // Danh sách các câu hỏi đã được đóng gói kèm đáp án (View)
        public List<ChiTietCauHoiHocResponse> DanhSachCauHoi { get; set; } = new();
    }

    public class ChiTietCauHoiHocResponse
    {
        // Thông tin chính của thẻ (Câu hỏi, Hình ảnh, Loại thẻ)
        public TheFlashcardResponse ThongTinThe { get; set; } = null!;

        // Dữ liệu mở rộng dựa trên LoaiThe (Sử dụng các ViewResponse để bảo mật)
        public List<DapAnTracNghiemResponse>? TracNghiem { get; set; }
        public List<PhanTuSapXepResponse>? SapXep { get; set; }
        public List<TuDienKhuyetResponse>? DienKhuyet { get; set; }
        public List<CapGhepResponse>? CapGhep { get; set; }
    }
    public class BoDeHocResponse
    {
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; }

        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; }
        public bool LaCongKhai { get; set; }

        public int SoLuongThe { get; set; }
        public int SoLuotHoc { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}