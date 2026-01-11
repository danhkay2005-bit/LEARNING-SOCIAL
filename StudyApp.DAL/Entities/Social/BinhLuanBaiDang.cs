using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class BinhLuanBaiDang
{
    public int MaBinhLuan { get; set; }

    public int MaBaiDang { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string NoiDung { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public int? MaBinhLuanCha { get; set; }

    public int? SoLuotReaction { get; set; }

    public bool? DaChinhSua { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianSua { get; set; }

    public virtual ICollection<BinhLuanBaiDang> InverseMaBinhLuanChaNavigation { get; set; } = new List<BinhLuanBaiDang>();

    public virtual BaiDang MaBaiDangNavigation { get; set; } = null!;

    public virtual BinhLuanBaiDang? MaBinhLuanChaNavigation { get; set; }

    public virtual ICollection<MentionBinhLuan> MentionBinhLuans { get; set; } = new List<MentionBinhLuan>();

    public virtual ICollection<ReactionBinhLuan> ReactionBinhLuans { get; set; } = new List<ReactionBinhLuan>();
}
