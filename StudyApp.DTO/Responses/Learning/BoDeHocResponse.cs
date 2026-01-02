using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response bộ đề chi tiết
    public class BoDeHocResponse
    {
        public int MaBoDe { get; set; }
        public Guid MaNguoiDung { get; set; }
        public NguoiDungTomTatResponse? NguoiTao { get; set; }
        public int? MaChuDe { get; set; }
        public ChuDeResponse? ChuDe { get; set; }
        public int? MaThuMuc { get; set; }
        public ThuMucTomTatResponse? ThuMuc { get; set; }
        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }
        public MucDoKhoEnum? MucDoKho { get; set; }
        public bool LaCongKhai { get; set; }
        public bool ChoPhepBinhLuan { get; set; }
        public int? MaBoDeGoc { get; set; }
        public BoDeGocTomTatResponse? BoDeGoc { get; set; }
        public int SoLuongThe { get; set; }
        public int SoLuotHoc { get; set; }
        public int SoLuotChiaSe { get; set; }
        public double DiemDanhGiaTb { get; set; }
        public int SoDanhGia { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }

        // Thông tin của người dùng hiện tại
        public bool LaCuaToi { get; set; }
        public bool DaLuuYeuThich { get; set; }
        public bool DaHoc { get; set; }
        public bool DaDanhGia { get; set; }
        public TienDoHocBoDeResponse? TienDoHoc { get; set; }

        // Danh sách liên quan
        public List<TagResponse>? Tags { get; set; }
        public List<TheFlashcardTomTatResponse>? TheMau { get; set; }
    }

    // Response bộ đề tóm tắt
    public class BoDeHocTomTatResponse
    {
        public int MaBoDe { get; set; }
        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }
        public NguoiDungTomTatResponse? NguoiTao { get; set; }
        public MucDoKhoEnum? MucDoKho { get; set; }
        public int SoLuongThe { get; set; }
        public int SoLuotHoc { get; set; }
        public double DiemDanhGiaTb { get; set; }
        public int SoDanhGia { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public bool DaLuuYeuThich { get; set; }
        public double? TienDoHocPhanTram { get; set; }
    }

    // Response bộ đề gốc tóm tắt
    public class BoDeGocTomTatResponse
    {
        public int MaBoDe { get; set; }
        public string TieuDe { get; set; } = null!;
        public NguoiDungTomTatResponse? NguoiTao { get; set; }
        public bool ConTonTai { get; set; }
    }

    // Response tiến độ học bộ đề
    public class TienDoHocBoDeResponse
    {
        public int MaBoDe { get; set; }
        public int TongSoThe { get; set; }
        public int SoTheDaHoc { get; set; }
        public int SoTheMoi { get; set; }
        public int SoTheDangHoc { get; set; }
        public int SoTheCanOnTap { get; set; }
        public int SoTheThanhThao { get; set; }
        public double TienDoPhanTram { get; set; }
        public double TyLeDungTrungBinh { get; set; }
        public int TongThoiGianHocPhut { get; set; }
        public DateTime? LanHocCuoi { get; set; }
        public DateTime? LanOnTapTiepTheo { get; set; }
    }

    // Response danh sách bộ đề
    public class DanhSachBoDeResponse
    {
        public List<BoDeHocTomTatResponse> BoDes { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
        public bool CoTrangTiep { get; set; }
        public bool CoTrangTruoc { get; set; }
    }

    // Response sau khi tạo/cập nhật bộ đề
    public class TaoBoDeResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BoDeHocResponse? BoDe { get; set; }
        public int? SoTheImport { get; set; }
    }

    // Response clone bộ đề
    public class CloneBoDeResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public BoDeHocResponse? BoDeClone { get; set; }
    }
}