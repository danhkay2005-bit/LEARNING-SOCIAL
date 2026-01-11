using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ThongBao
{
    public int MaThongBao { get; set; }

    public Guid MaNguoiNhan { get; set; }

    public int LoaiThongBao { get; set; }

    public string NoiDung { get; set; } = null!;

    public bool DaDoc { get; set; }

    public DateTime ThoiGian { get; set; }

    public int? MaBaiDang { get; set; }

    public int? MaBinhLuan { get; set; }

    public int? MaThachDau { get; set; }

    public Guid? MaNguoiGayRa { get; set; }
}
