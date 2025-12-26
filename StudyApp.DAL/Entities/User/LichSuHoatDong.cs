using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class LichSuHoatDong
{
    public int MaHoatDong { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string LoaiHoatDong { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? DuLieuThem { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
