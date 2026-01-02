using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response thẻ flashcard chi tiết
    public class TheFlashcardResponse
    {
        public int MaThe { get; set; }
        public int MaBoDe { get; set; }
        public LoaiTheEnum LoaiThe { get; set; }
        public string MatTruoc { get; set; } = null!;
        public string MatSau { get; set; } = null!;
        public string? GiaiThich { get; set; }
        public string? GoiY { get; set; }
        public string? VietTat { get; set; }
        public string? HinhAnhTruoc { get; set; }
        public string? HinhAnhSau { get; set; }
        public string? AmThanhTruoc { get; set; }
        public string? AmThanhSau { get; set; }
        public int ThuTu { get; set; }
        public MucDoKhoEnum? DoKho { get; set; }
        public int SoLuotHoc { get; set; }
        public int SoLanDung { get; set; }
        public int SoLanSai { get; set; }
        public double TyLeDungTb { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }

        // Thông tin của người dùng hiện tại
        public TienDoTheResponse? TienDoCuaToi { get; set; }
        public List<DanhDauTheResponse>? DanhDauCuaToi { get; set; }
        public List<GhiChuTheResponse>? GhiChuCuaToi { get; set; }

        // Dữ liệu theo loại thẻ
        public List<DapAnTracNghiemResponse>? DapAnTracNghiems { get; set; }
        public List<CapGhepResponse>? CapGheps { get; set; }
        public List<PhanTuSapXepResponse>? PhanTuSapXeps { get; set; }
        public List<TuDienKhuyetResponse>? TuDienKhuyets { get; set; }
    }

    // Response thẻ flashcard tóm tắt
    public class TheFlashcardTomTatResponse
    {
        public int MaThe { get; set; }
        public LoaiTheEnum LoaiThe { get; set; }
        public string MatTruocRutGon { get; set; } = null!;
        public string MatSauRutGon { get; set; } = null!;
        public string? HinhAnhTruoc { get; set; }
        public int ThuTu { get; set; }
        public MucDoKhoEnum? DoKho { get; set; }
        public TrangThaiSRSEnum? TrangThaiSRS { get; set; }
    }

    // Response thẻ để học
    public class TheHocResponse
    {
        public int MaThe { get; set; }
        public int MaBoDe { get; set; }
        public LoaiTheEnum LoaiThe { get; set; }
        public string MatTruoc { get; set; } = null!;
        public string MatSau { get; set; } = null!;
        public string? GoiY { get; set; }
        public string? HinhAnhTruoc { get; set; }
        public string? HinhAnhSau { get; set; }
        public string? AmThanhTruoc { get; set; }
        public string? AmThanhSau { get; set; }
        public MucDoKhoEnum? DoKho { get; set; }

        // Dữ liệu để trả lời
        public List<DapAnTracNghiemHocResponse>? DapAnTracNghiems { get; set; }
        public List<CapGhepHocResponse>? CapGheps { get; set; }
        public List<PhanTuSapXepHocResponse>? PhanTuSapXeps { get; set; }
        public List<TuDienKhuyetHocResponse>? TuDienKhuyets { get; set; }

        // Thông tin SRS
        public bool LaTheMoi { get; set; }
        public int? SoLanDaHoc { get; set; }
        public DateTime? LanHocCuoi { get; set; }
    }

    // Response thẻ sau khi trả lời
    public class TheKetQuaResponse
    {
        public int MaThe { get; set; }
        public bool TraLoiDung { get; set; }
        public string? DapAnDungDayDu { get; set; }
        public string? GiaiThich { get; set; }

        // Thông tin SRS mới
        public TrangThaiSRSEnum TrangThaiMoi { get; set; }
        public DateTime? NgayOnTapTiepTheo { get; set; }
        public int? KhoangCachNgayMoi { get; set; }
    }

    // Response danh sách thẻ
    public class DanhSachTheResponse
    {
        public List<TheFlashcardResponse> Thes { get; set; } = [];
        public int TongSo { get; set; }
        public int TrangHienTai { get; set; }
        public int TongSoTrang { get; set; }
    }

    // Response sau khi tạo/cập nhật thẻ
    public class TaoTheResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public TheFlashcardResponse? The { get; set; }
    }

    // Response import thẻ
    public class ImportTheResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public int SoTheImport { get; set; }
        public int SoTheLoi { get; set; }
        public List<string>? DanhSachLoi { get; set; }
    }
}