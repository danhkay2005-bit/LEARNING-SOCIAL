namespace StudyApp.DAL.Entities.Social;

public partial class BinhLuanBaiDang
{
    // ===== PRIMARY KEY =====
    public int MaBinhLuan { get; set; }

    // ===== FOREIGN KEYS =====
    public int MaBaiDang { get; set; }
    public Guid MaNguoiDung { get; set; }

    // ===== CONTENT =====
    public string NoiDung { get; set; } = null!;
    public string? HinhAnh { get; set; }

    // ⭐⭐⭐ SELF-REFERENCE (Nullable) ⭐⭐⭐
    public int? MaBinhLuanCha { get; set; }
    // NULL = Top-level comment
    // Value = Reply to comment with ID = MaBinhLuanCha

    // ===== COUNTERS =====
    public int? SoLuotReaction { get; set; } = 0;

    // ===== AUDIT =====
    public bool? DaChinhSua { get; set; } = false;
    public bool? DaXoa { get; set; } = false;
    public DateTime? ThoiGianTao { get; set; } = DateTime.Now;
    public DateTime? ThoiGianSua { get; set; }

    // ⭐⭐⭐ NAVIGATION PROPERTIES ⭐⭐⭐
    
    // N:1 → BaiDang
    public virtual BaiDang MaBaiDangNavigation { get; set; } = null!;

    // ⭐ N:1 → BinhLuanBaiDang (Parent Comment)
    public virtual BinhLuanBaiDang? MaBinhLuanChaNavigation { get; set; }

    // ⭐ 1:N → BinhLuanBaiDang (Child Comments / Replies)
    public virtual ICollection<BinhLuanBaiDang> InverseMaBinhLuanChaNavigation { get; set; } 
        = new List<BinhLuanBaiDang>();
    // Đây là collection chứa tất cả replies của comment này

    // 1:N → ReactionBinhLuan
    public virtual ICollection<ReactionBinhLuan> ReactionBinhLuans { get; set; } 
        = new List<ReactionBinhLuan>();

    // 1:N → MentionBinhLuan
    public virtual ICollection<MentionBinhLuan> MentionBinhLuans { get; set; } 
        = new List<MentionBinhLuan>();
}