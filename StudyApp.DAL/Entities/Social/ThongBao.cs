using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyApp.DAL.Entities.Social;

[Table("ThongBao")]
public partial class ThongBao
{
    [Key]
    public int MaThongBao { get; set; }

    [Required]
    public Guid MaNguoiNhan { get; set; }

    [Required]
    public int LoaiThongBao { get; set; }

    [Required]
    [StringLength(500)]
    public string NoiDung { get; set; } = null!;

    [Required]
    public bool DaDoc { get; set; }

    [Required]
    public DateTime ThoiGian { get; set; }

    // Tham chiếu mềm (soft reference)
    public int? MaBaiDang { get; set; }

    public int? MaBinhLuan { get; set; }

    public int? MaThachDau { get; set; }

    public Guid? MaNguoiGayRa { get; set; }
}
