using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class ChiaSeBaiDang
{
    public int MaChiaSe { get; set; }

    public int MaBaiDangGoc { get; set; }

    public Guid MaNguoiChiaSe { get; set; }

    public string? NoiDungThem { get; set; }

    public int? MaBaiDangMoi { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BaiDang MaBaiDangGocNavigation { get; set; } = null!;

    public virtual BaiDang? MaBaiDangMoiNavigation { get; set; }
}
