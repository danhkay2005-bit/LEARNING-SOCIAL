using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.BaiHoc
// DTO cho bảng BoDeHoc trong database App_HeThongBaiHoc
// Đại diện cho một bộ đề flashcard
{
    // PROPERTIES CƠ BẢN (Ánh xạ trực tiếp từ database)
    public class BoDeDTO
    {
        // Mã bộ đề (Primary Key, Identity)
        public int MaBoDe { get; set; }

        // Mã người dùng tạo bộ đề (Foreign Key → App_NguoiDung. NguoiDung)
        public int MaNguoiDung { get; set; }

        // Mã chủ đề (Foreign Key → ChuDe), có thể null
        public int? MaChuDe { get; set; }

        // Mã thư mục chứa bộ đề (Foreign Key → ThuMuc), có thể null
        public int? MaThuMuc { get; set; }



        // NỘI DUNG
        // Tiêu đề bộ đề (bắt buộc, max 200 ký tự)
        // VD: "1000 Từ Vựng TOEIC Quan Trọng"
        public string? TieuDe { get; set; }

        /// <summary>
        /// Mô tả chi tiết về bộ đề (max 1000 ký tự)
        /// VD: "Bộ từ vựng cơ bản dành cho người mới bắt đầu..."
        /// </summary>
        public string? MoTa { get; set; }

        /// <summary>
        /// URL hoặc Base64 của ảnh bìa
        /// </summary>
        public string? AnhBia { get; set; }

        // ============================================================
        // CÀI ĐẶT BỘ ĐỀ
        // ============================================================

        /// <summary>
        /// Mức độ khó (1-5)
        /// 1 = Rất dễ, 2 = Dễ, 3 = Trung bình, 4 = Khó, 5 = Rất khó
        /// </summary>
        public byte MucDoKho { get; set; }

        /// <summary>
        /// Bộ đề có công khai cho người khác xem không? 
        /// true = Công khai, false = Chỉ mình tôi
        /// </summary>
        public bool LaCongKhai { get; set; }

        /// <summary>
        /// Cho phép người khác clone (sao chép) bộ đề này không?
        /// </summary>
        public bool ChoPhepClone { get; set; }

        /// <summary>
        /// Cho phép người khác bình luận vào bộ đề không?
        /// </summary>
        public bool ChoPhepBinhLuan { get; set; }

        /// <summary>
        /// Nếu bộ đề này được clone từ bộ khác thì lưu ID bộ gốc
        /// null = Bộ đề tự tạo (không phải clone)
        /// </summary>
        public int? MaBoDeGoc { get; set; }

        // ============================================================
        // THỐNG KÊ (Database tự động tính)
        // ============================================================

        /// <summary>
        /// Tổng số thẻ trong bộ đề
        /// </summary>
        public int SoLuongThe { get; set; }

        /// <summary>
        /// Số lần bộ đề được học (bởi bất kỳ ai)
        /// </summary>
        public int SoLuotHoc { get; set; }

        /// <summary>
        /// Số lần bộ đề được lưu (bookmark)
        /// </summary>
        public int SoLuotLuu { get; set; }

        /// <summary>
        /// Số lần bộ đề được chia sẻ
        /// </summary>
        public int SoLuotChiaSe { get; set; }

        /// <summary>
        /// Điểm đánh giá trung bình (1.0 - 5.0 sao)
        /// </summary>
        public double DiemDanhGiaTB { get; set; }

        /// <summary>
        /// Tổng số người đã đánh giá
        /// </summary>
        public int SoDanhGia { get; set; }

        // ============================================================
        // TRẠNG THÁI & THỜI GIAN
        // ============================================================

        /// <summary>
        /// Bộ đề đã bị xóa (soft delete) chưa?
        /// true = Đã xóa (ẩn), false = Đang hoạt động
        /// </summary>
        public bool DaXoa { get; set; }

        /// <summary>
        /// Thời điểm tạo bộ đề
        /// </summary>
        public DateTime ThoiGianTao { get; set; }

        /// <summary>
        /// Lần cập nhật gần nhất (sửa tiêu đề, thêm thẻ...)
        /// </summary>
        public DateTime ThoiGianCapNhat { get; set; }

        // ============================================================
        // PROPERTIES BỔ SUNG (Từ JOIN với bảng khác)
        // Không có trong database BoDeHoc, lấy khi JOIN
        // ============================================================

        /// <summary>
        /// Tên người tạo bộ đề (JOIN từ App_NguoiDung. NguoiDung)
        /// VD: "Nguyễn Văn A"
        /// </summary>
        public string? TenNguoiTao { get; set; }

        /// <summary>
        /// URL ảnh đại diện người tạo (JOIN từ App_NguoiDung.NguoiDung)
        /// </summary>
        public string? HinhDaiDienNguoiTao { get; set; }

        /// <summary>
        /// Tên chủ đề (JOIN từ ChuDe)
        /// VD: "Tiếng Anh", "TOEIC", "Lập trình"
        /// </summary>
        public string? TenChuDe { get; set; }

        
        public string? BieuTuongChuDe { get; set; }

        /// <summary>
        /// Tên thư mục chứa bộ đề (JOIN từ ThuMuc)
        /// VD: "Học TOEIC", "Từ vựng cơ bản"
        /// </summary>
        public string? TenThuMuc { get; set; }
    }
}
