using System;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    /// <summary>
    /// Request tạo bộ đề học
    /// </summary>
    public class TaoBoDeHocRequest
    {
        public Guid MaNguoiDung { get; set; }

        public int? MaChuDe { get; set; }
        public int? MaThuMuc { get; set; }

        public string TieuDe { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? AnhBia { get; set; }

        public MucDoKhoEnum MucDoKho { get; set; } = MucDoKhoEnum.TrungBinh;

        public bool LaCongKhai { get; set; } = true;
        public bool ChoPhepBinhLuan { get; set; } = true;
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
