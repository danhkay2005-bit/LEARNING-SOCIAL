using StudyApp.DTO.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // =========================
    // 2. LẤY DANH SÁCH CHI TIẾT TRẢ LỜI
    // ↔ DanhSachChiTietTraLoiResponse
    // =========================
    public class LayDanhSachChiTietTraLoiRequest
    {
        [Required]
        public int MaPhien { get; set; }
    }

    // =========================
    // 3. LẤY CHI TIẾT 1 CÂU TRẢ LỜI
    // ↔ ChiTietTraLoiResponse
    // =========================
    public class LayChiTietTraLoiRequest
    {
        [Required]
        public int MaTraLoi { get; set; }
    }

    // =========================
    // 4. CẬP NHẬT ĐỘ KHÓ SAU KHI TRẢ LỜI
    // (chỉnh lại đánh giá SRS)
    // =========================
    public class CapNhatDoKhoTraLoiRequest
    {
        [Required]
        public int MaTraLoi { get; set; }

        [Required]
        public MucDoKhoEnum DoKhoUserDanhGia { get; set; }
    }

    // =========================
    // 5. XOÁ CHI TIẾT TRẢ LỜI
    // (admin / rollback phiên)
    // =========================
    public class XoaChiTietTraLoiRequest
    {
        [Required]
        public int MaTraLoi { get; set; }
    }
}
