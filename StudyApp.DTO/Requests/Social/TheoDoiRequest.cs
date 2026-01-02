using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request theo dõi người dùng
    public class TheoDoiRequest
    {
        [Required(ErrorMessage = "Mã người được theo dõi là bắt buộc")]
        public Guid MaNguoiDuocTheoDoi { get; set; }

        public bool ThongBaoBaiMoi { get; set; } = true;
    }

    // Request bỏ theo dõi
    public class BoTheoDoiRequest
    {
        [Required(ErrorMessage = "Mã người được theo dõi là bắt buộc")]
        public Guid MaNguoiDuocTheoDoi { get; set; }
    }

    // Request cập nhật cài đặt theo dõi
    public class CapNhatCaiDatTheoDoiRequest
    {
        [Required(ErrorMessage = "Mã người được theo dõi là bắt buộc")]
        public Guid MaNguoiDuocTheoDoi { get; set; }

        [Required(ErrorMessage = "Cài đặt thông báo là bắt buộc")]
        public bool ThongBaoBaiMoi { get; set; }
    }

    // Request lấy danh sách theo dõi
    public class LayDanhSachTheoDoiRequest
    {
        public Guid? MaNguoiDung { get; set; }
        public bool? LayNguoiTheoDoi { get; set; } // true:  lấy người theo dõi mình, false: lấy người mình theo dõi
        public string? TuKhoa { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}