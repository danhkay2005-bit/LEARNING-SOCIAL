using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class BanBe
{
    public Guid MaNguoiDung1 { get; set; }

    public Guid MaNguoiDung2 { get; set; }

    public string? TrangThai { get; set; }

    public Guid MaNguoiGui { get; set; }

    public DateTime? ThoiGianGui { get; set; }

    public DateTime? ThoiGianChapNhan { get; set; }
}
