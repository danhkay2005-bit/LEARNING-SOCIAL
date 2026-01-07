using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // =========================
    // 1. BẮT ĐẦU PHIÊN HỌC
    // ↔ BatDauPhienHocResponse
    // =========================
    public class BatDauPhienHocRequest
    {
        /// <summary>
        /// Bộ đề học (null nếu học daily / random)
        /// </summary>
        public int? MaBoDe { get; set; }

        [Required]
        public LoaiPhienHocEnum LoaiPhien { get; set; }

        /// <summary>
        /// Giới hạn số thẻ học
        /// </summary>
        [Range(1, int.MaxValue)]
        public int? GioiHanSoThe { get; set; }

        /// <summary>
        /// Ưu tiên thẻ mới hay không
        /// </summary>
        public bool UuTienTheMoi { get; set; } = true;

        /// <summary>
        /// Dùng cho chế độ thách đấu
        /// </summary>
        public int? MaThachDau { get; set; }
    }

    // =========================
    // 2. GỬI CÂU TRẢ LỜI
    // ↔ GuiCauTraLoiResponse
    // =========================
    public class GuiCauTraLoiRequest
    {
        [Required]
        public int MaPhien { get; set; }

        [Required]
        public int MaThe { get; set; }

        /// <summary>
        /// Câu trả lời người dùng
        /// </summary>
        public string? CauTraLoiUser { get; set; }

        /// <summary>
        /// Thời gian trả lời (giây)
        /// </summary>
        [Range(0, int.MaxValue)]
        public int ThoiGianTraLoiGiay { get; set; }

        /// <summary>
        /// Độ khó người dùng đánh giá (SRS)
        /// </summary>
        public MucDoKhoEnum? DoKhoUserDanhGia { get; set; }

        /// <summary>
        /// Người dùng bỏ qua thẻ
        /// </summary>
        public bool BoQua { get; set; }
    }

    // =========================
    // 3. KẾT THÚC PHIÊN HỌC
    // ↔ KetThucPhienHocResponse
    // =========================
    public class KetThucPhienHocRequest
    {
        [Required]
        public int MaPhien { get; set; }

        /// <summary>
        /// Kết thúc sớm hay học hết
        /// </summary>
        public bool KetThucSom { get; set; }

        /// <summary>
        /// Cảm xúc sau phiên học
        /// </summary>
        public CamXucHocEnum? CamXuc { get; set; }

        /// <summary>
        /// Ghi chú của người học
        /// </summary>
        public string? GhiChu { get; set; }
    }

    // =========================
    // 4. LẤY CHI TIẾT PHIÊN HỌC
    // ↔ PhienHocResponse
    // =========================
    public class LayChiTietPhienHocRequest
    {
        [Required]
        public int MaPhien { get; set; }

        /// <summary>
        /// Có lấy thống kê chi tiết hay không
        /// </summary>
        public bool LayThongKe { get; set; } = true;
    }

    // =========================
    // 5. LẤY THỐNG KÊ PHIÊN HỌC
    // ↔ ThongKePhienHocResponse
    // =========================
    public class LayThongKePhienHocRequest
    {
        [Required]
        public int MaPhien { get; set; }
    }

    // =========================
    // 6. LẤY DANH SÁCH PHIÊN HỌC
    // ↔ DanhSachPhienHocResponse
    // =========================
    public class LayDanhSachPhienHocRequest
    {
        [Range(1, int.MaxValue)]
        public int Trang { get; set; } = 1;

        [Range(1, 100)]
        public int SoLuongMoiTrang { get; set; } = 10;

        public LoaiPhienHocEnum? LoaiPhien { get; set; }

        public int? MaBoDe { get; set; }

        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
