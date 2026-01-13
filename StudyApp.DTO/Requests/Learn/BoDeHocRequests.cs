using StudyApp.DTO.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learn
{
    public class LuuToanBoBoDeRequest
    {
        public TaoBoDeHocRequest ThongTinChung { get; set; } = null!;
        public List<ChiTietTheRequest> DanhSachThe { get; set; } = new List<ChiTietTheRequest>();
    }

    public class ChiTietTheRequest
    {
        public TaoTheFlashcardRequest TheChinh { get; set; } = null!;
        // Dựa trên LoaiThe, một trong các list dưới đây sẽ có dữ liệu
        public List<TaoDapAnTracNghiemRequest>? DapAnTracNghiem { get; set; }
        public List<TaoPhanTuSapXepRequest>? PhanTuSapXeps { get; set; }
        public List<TaoTuDienKhuyetRequest>? TuDienKhuyets { get; set; }
        public List<CapGhepRequest>? CapGheps { get; set; }
    }
    /// <summary>
    /// Request tạo bộ đề học
    /// </summary>
    public class TaoBoDeHocRequest
    {
        [Required]
        public Guid MaNguoiDung { get; set; }

        public int? MaChuDe { get; set; }
        public int? MaThuMuc { get; set; }

        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; } = MucDoKhoEnum.TrungBinh;

        public bool LaCongKhai { get; set; } = true;
    }

    /// <summary>
    /// Request cập nhật bộ đề học
    /// </summary>
    public class CapNhatBoDeHocRequest
    {
        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; }

        public bool LaCongKhai { get; set; }
        public bool ChoPhepBinhLuan { get; set; }
    }

    /// <summary>
    /// Request sao chép (fork) bộ đề học
    /// </summary>
    public class SaoChepBoDeHocRequest
    {
        public int MaBoDeGoc { get; set; }
        public Guid MaNguoiDungMoi { get; set; }
    }
}
