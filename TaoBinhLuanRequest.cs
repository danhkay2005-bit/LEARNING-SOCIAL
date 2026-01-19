// File: StudyApp.DTO\Requests\Social\BinhLuanBaiDangRequests.cs

/// <summary>
/// Request tạo bình luận mới (có thể là top-level hoặc reply)
/// </summary>
public class TaoBinhLuanRequest
{
    [Required(ErrorMessage = "Mã bài đăng là bắt buộc")]
    public int MaBaiDang { get; set; }

    [Required(ErrorMessage = "Mã người dùng là bắt buộc")]
    public Guid MaNguoiDung { get; set; }

    [Required(ErrorMessage = "Nội dung bình luận là bắt buộc")]
    [MinLength(1, ErrorMessage = "Nội dung không được rỗng")]
    [MaxLength(5000, ErrorMessage = "Nội dung không được quá 5000 ký tự")]
    public string NoiDung { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "URL ảnh không được quá 500 ký tự")]
    public string? HinhAnh { get; set; }

    // ⭐⭐⭐ NULLABLE = Top-level comment ⭐⭐⭐
    // ⭐⭐⭐ HAS VALUE = Reply comment ⭐⭐⭐
    public int? MaBinhLuanCha { get; set; }
}

/// <summary>
/// Request cập nhật bình luận
/// </summary>
public class CapNhatBinhLuanRequest
{
    [Required]
    public int MaBinhLuan { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(5000)]
    public string NoiDung { get; set; } = null!;

    public string? HinhAnh { get; set; }
}

/// <summary>
/// Request xóa bình luận
/// </summary>
public class XoaBinhLuanRequest
{
    [Required]
    public int MaBinhLuan { get; set; }

    [Required]
    public Guid MaNguoiDung { get; set; } // Để verify ownership
}

/// <summary>
/// Request lấy danh sách replies của 1 comment
/// </summary>
public class LayRepliesRequest
{
    [Required]
    public int MaBinhLuanCha { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}