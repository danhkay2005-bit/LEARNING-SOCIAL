using System;
using System.Collections.Generic;

namespace StudyApp.DTO.Responses.Learning
{
    // Response thư mục chi tiết
    public class ThuMucResponse
    {
        public int MaThuMuc { get; set; }
        public Guid MaNguoiDung { get; set; }
        public string TenThuMuc { get; set; } = null!;
        public string? MoTa { get; set; }
        public int? MaThuMucCha { get; set; }
        public int ThuTu { get; set; }
        public int SoBoDeTrongThuMuc { get; set; }
        public int SoThuMucCon { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }

        // Danh sách con
        public List<ThuMucTomTatResponse>? ThuMucCons { get; set; }
        public List<BoDeHocTomTatResponse>? BoDes { get; set; }
    }

    // Response thư mục tóm tắt
    public class ThuMucTomTatResponse
    {
        public int MaThuMuc { get; set; }
        public string TenThuMuc { get; set; } = null!;
        public int? MaThuMucCha { get; set; }
        public int SoBoDe { get; set; }
        public int ThuTu { get; set; }
    }

    // Response cây thư mục
    public class CayThuMucResponse
    {
        public List<ThuMucNodeResponse> Nodes { get; set; } = [];
        public int TongSoThuMuc { get; set; }
        public int TongSoBoDe { get; set; }
    }

    public class ThuMucNodeResponse
    {
        public int MaThuMuc { get; set; }
        public string TenThuMuc { get; set; } = null!;
        public int? MaThuMucCha { get; set; }
        public int SoBoDe { get; set; }
        public int ThuTu { get; set; }
        public List<ThuMucNodeResponse>? Children { get; set; }
    }

    // Response kết quả thư mục
    public class KetQuaThuMucResponse
    {
        public bool ThanhCong { get; set; }
        public string? ThongBao { get; set; }
        public ThuMucResponse? ThuMuc { get; set; }
    }
}