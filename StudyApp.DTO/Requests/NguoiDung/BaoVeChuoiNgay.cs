using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.NguoiDung;

// DTO sử dụng bảo vệ chuỗi ngày (Streak Freeze)
public class SuDungBaoVeChuoiNgayRequest
{
    [Required(ErrorMessage = "Loại bảo vệ là bắt buộc")]
    public LoaiBaoVeStreakEnum LoaiBaoVe { get; set; }// StreakFreeze, StreakHoiSinh
}