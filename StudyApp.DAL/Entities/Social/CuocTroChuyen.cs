using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class CuocTroChuyen
{
    public int MaCuocTroChuyen { get; set; }

    public string? LoaiCuocTroChuyen { get; set; }

    public string? TenNhomChat { get; set; }

    public string? AnhNhomChat { get; set; }

    public Guid? MaNguoiTaoNhom { get; set; }

    public int? TinNhanCuoiId { get; set; }

    public string? NoiDungTinCuoi { get; set; }

    public DateTime? ThoiGianTinCuoi { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public virtual ICollection<ThanhVienCuocTroChuyen> ThanhVienCuocTroChuyens { get; set; } = new List<ThanhVienCuocTroChuyen>();

    public virtual ICollection<TinNhan> TinNhans { get; set; } = new List<TinNhan>();
}
