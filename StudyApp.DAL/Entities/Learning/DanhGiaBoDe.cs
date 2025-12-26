using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learning;

public partial class DanhGiaBoDe
{
    public int MaDanhGia { get; set; }

    public int MaBoDe { get; set; }

    public Guid MaNguoiDung { get; set; }

    public byte SoSao { get; set; }

    public string? NoiDung { get; set; }

    public byte? DoKhoThucTe { get; set; }

    public byte? ChatLuongNoiDung { get; set; }

    public int? SoLuotThich { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;
}
