// File: StudyApp.DTO\Responses\Social\BinhLuanBaiDangResponses.cs

/// <summary>
/// Response thông tin bình lu?n
/// </summary>
public class BinhLuanBaiDangResponse
{
    public int MaBinhLuan { get; set; }
    public int MaBaiDang { get; set; }
    public Guid MaNguoiDung { get; set; }

    // ===== USER INFO (JOIN t? UserDb) =====
    public string? TenDangNhap { get; set; }
    public string? HoVaTen { get; set; }
    public string? HinhDaiDien { get; set; }

    // ===== CONTENT =====
    public string NoiDung { get; set; } = null!;
    public string? HinhAnh { get; set; }

    // ??? REPLY INFO ???
    public int? MaBinhLuanCha { get; set; }
    
    // ? Thông tin comment cha (n?u là reply)
    public string? TenNguoiDungCha { get; set; }  // Tên ng??i ???c reply
    
    // ? S? l??ng replies c?a comment này
    public int SoLuotReplies { get; set; }

    // ===== STATS =====
    public int SoLuotReactions { get; set; }

    // ===== AUDIT =====
    public bool DaChinhSua { get; set; }
    public bool DaXoa { get; set; }
    public DateTime? ThoiGianTao { get; set; }
    public DateTime? ThoiGianSua { get; set; }

    // ? Optional: Load nested replies (tùy ch?n)
    public List<BinhLuanBaiDangResponse>? Replies { get; set; }
}

/// <summary>
/// Response danh sách comments (with pagination)
/// </summary>
public class DanhSachBinhLuanResponse
{
    public List<BinhLuanBaiDangResponse> Comments { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;
}