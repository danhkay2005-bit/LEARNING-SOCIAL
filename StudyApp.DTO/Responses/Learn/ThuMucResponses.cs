using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ thông tin thư mục
    /// </summary>
    public class ThuMucResponse
    {
        public int MaThuMuc { get; set; }

        public Guid MaNguoiDung { get; set; }

        public string TenThuMuc { get; set; } = null!;

        public string? MoTa { get; set; }

        public int? MaThuMucCha { get; set; }

        public int? ThuTu { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public DateTime? ThoiGianCapNhat { get; set; }
    }

    /// <summary>
    /// Response rút gọn (dùng cho cây thư mục)
    /// </summary>
    public class ThuMucTreeResponse
    {
        public int MaThuMuc { get; set; }

        public string TenThuMuc { get; set; } = null!;

        public int? MaThuMucCha { get; set; }

        public List<ThuMucTreeResponse> ThuMucCon { get; set; } = new();
    }

    /// <summary>
    /// Response rút gọn cho Select / ComboBox
    /// </summary>
    public class ThuMucSelectResponse
    {
        public int MaThuMuc { get; set; }
        public string TenThuMuc { get; set; } = null!;
    }
}
