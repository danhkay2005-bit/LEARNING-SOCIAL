using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class LogsGenerateAi
{
    public int MaLog { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int? MaThe { get; set; } = null;

    public string Prompt { get; set; } = null!;

    public string? UrlHinhAnh { get; set; }

    public string? TrangThai { get; set; }

    public string? Loi { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual TheFlashcard MaTheNavigation { get; set; } = null!;
}
