using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class BaiDang
{
    public int MaBaiDang { get; set; }

    public Guid MaNguoiDung { get; set; }

    public string? NoiDung { get; set; }

    public string? LoaiBaiDang { get; set; }

    public string? HinhAnh { get; set; }

    public string? Video { get; set; }

    public int? MaBoDeLienKet { get; set; }

    public int? MaThanhTuuLienKet { get; set; }

    public int? MaThachDauLienKet { get; set; }

    public int? SoChuoiNgay { get; set; }

    public byte? QuyenRiengTu { get; set; }

    public int? SoReaction { get; set; }

    public int? SoBinhLuan { get; set; }

    public int? SoLuotXem { get; set; }

    public int? SoLuotChiaSe { get; set; }

    public bool? GhimBaiDang { get; set; }

    public bool? TatBinhLuan { get; set; }

    public bool? DaChinhSua { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public DateTime? ThoiGianSua { get; set; }

    public virtual ICollection<BinhLuanBaiDang> BinhLuanBaiDangs { get; set; } = new List<BinhLuanBaiDang>();

    public virtual ICollection<ChiaSeBaiDang> ChiaSeBaiDangMaBaiDangGocNavigations { get; set; } = new List<ChiaSeBaiDang>();

    public virtual ICollection<ChiaSeBaiDang> ChiaSeBaiDangMaBaiDangMoiNavigations { get; set; } = new List<ChiaSeBaiDang>();

    public virtual ICollection<MentionBaiDang> MentionBaiDangs { get; set; } = new List<MentionBaiDang>();

    public virtual ICollection<ReactionBaiDang> ReactionBaiDangs { get; set; } = new List<ReactionBaiDang>();

    public virtual ICollection<Hashtag> MaHashtags { get; set; } = new List<Hashtag>();
}
