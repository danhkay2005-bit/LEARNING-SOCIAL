using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class TinNhan
{
    public int MaTinNhan { get; set; }

    public int MaCuocTroChuyen { get; set; }

    public Guid MaNguoiGui { get; set; }

    public string? NoiDung { get; set; }

    public string? LoaiTinNhan { get; set; }

    public string? DuongDanFile { get; set; }

    public string? TenFile { get; set; }

    public int? KichThuocFile { get; set; }

    public int? MaStickerDung { get; set; }

    public int? MaBoDeDinhKem { get; set; }

    public int? MaThachDauDinhKem { get; set; }

    public int? ReplyToId { get; set; }

    public bool? DaThuHoi { get; set; }

    public DateTime? ThoiGianThuHoi { get; set; }

    public DateTime? ThoiGianGui { get; set; }

    public virtual ICollection<DaXemTinNhan> DaXemTinNhans { get; set; } = new List<DaXemTinNhan>();

    public virtual ICollection<TinNhan> InverseReplyTo { get; set; } = new List<TinNhan>();

    public virtual CuocTroChuyen MaCuocTroChuyenNavigation { get; set; } = null!;

    public virtual ICollection<ReactionTinNhan> ReactionTinNhans { get; set; } = new List<ReactionTinNhan>();

    public virtual TinNhan? ReplyTo { get; set; }
}
