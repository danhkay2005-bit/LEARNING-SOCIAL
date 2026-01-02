using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Social
{
    // Request thêm thành viên vào nhóm
    public class ThemThanhVienNhomRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [Required(ErrorMessage = "Danh sách thành viên là bắt buộc")]
        public List<Guid> MaNguoiDungs { get; set; } = [];
    }

    // Request xóa thành viên khỏi nhóm
    public class XoaThanhVienNhomRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
        public Guid MaNguoiDung { get; set; }
    }

    // Request rời khỏi nhóm
    public class RoiKhoiNhomRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }
    }

    // Request cập nhật cài đặt thành viên
    public class CapNhatCaiDatThanhVienRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [MaxLength(50, ErrorMessage = "Biệt danh không được vượt quá 50 ký tự")]
        public string? BiDanh { get; set; }

        public bool? TatThongBao { get; set; }

        public bool? GhimCuocTro { get; set; }
    }

    // Request thay đổi vai trò thành viên
    public class ThayDoiVaiTroThanhVienRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
        public Guid MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc")]
        public VaiTroThanhVienChatEnum VaiTro { get; set; }
    }

    // Request lấy danh sách thành viên
    public class LayThanhVienNhomRequest
    {
        [Required(ErrorMessage = "Mã cuộc trò chuyện là bắt buộc")]
        public int MaCuocTroChuyen { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}