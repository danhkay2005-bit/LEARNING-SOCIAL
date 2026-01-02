using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request tạo cuộc trò chuyện nhóm
    public class TaoCuocTroChuyenNhomRequest
    {
        [Required(ErrorMessage = "Tên nhóm chat là bắt buộc")]
        [MaxLength(100, ErrorMessage = "Tên nhóm không được vượt quá 100 ký tự")]
        public string TenNhomChat { get; set; } = null!;

        public string? AnhNhomChat { get; set; }

        [Required(ErrorMessage = "Danh sách thành viên là bắt buộc")]
        [MinLength(2, ErrorMessage = "Nhóm phải có ít nhất 2 thành viên")]
        public List<Guid> MaNguoiDungs { get; set; } = [];
    }

    // Request tạo cuộc trò chuyện cá nhân
    public class TaoCuocTroChuyenCaNhanRequest
    {
        [Required(ErrorMessage = "Mã người nhận là bắt buộc")]
        public Guid MaNguoiNhan { get; set; }
    }

    // Request cập nhật thông tin nhóm chat
    public class CapNhatNhomChatRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [MaxLength(100, ErrorMessage = "Tên nhóm không được vượt quá 100 ký tự")]
        public string? TenNhomChat { get; set; }

        public string? AnhNhomChat { get; set; }
    }

    // Request lấy danh sách cuộc trò chuyện
    public class LayCuocTroChuyenRequest
    {
        public LoaiCuocTroChuyenEnum? LoaiCuocTroChuyen { get; set; }
        public string? TuKhoa { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}