using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class ChiTietTraLoi
{
    public int MaTraLoi { get; set; }

    public int MaPhien { get; set; }

    public int MaThe { get; set; }

    public bool TraLoiDung { get; set; }

    public string? CauTraLoiUser { get; set; }

    public string? DapAnDung { get; set; }

    public int? ThoiGianTraLoiGiay { get; set; }

    public byte? DoKhoUserDanhGia { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual PhienHoc MaPhienNavigation { get; set; } = null!;

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
