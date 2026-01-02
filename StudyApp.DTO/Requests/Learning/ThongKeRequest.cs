using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.DTO.Requests.Learning
{
    // Request lấy thống kê ngày
    public class LayThongKeNgayRequest
    {
        public DateOnly? TuNgay { get; set; }

        public DateOnly? DenNgay { get; set; }
    }

    // Request lấy thống kê tổng quan
    public class LayThongKeTongQuanRequest
    {
        public string KhoangThoiGian { get; set; } = "7_ngay"; // "7_ngay", "30_ngay", "tat_ca"
    }

    // Request lấy lịch sử học bộ đề
    public class LayLichSuHocBoDeRequest
    {
        public int? MaBoDe { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }

    // Request lấy streak (chuỗi ngày học)
    public class LayStreakRequest
    {
        public int? Nam { get; set; }

        public int? Thang { get; set; }
    }
}