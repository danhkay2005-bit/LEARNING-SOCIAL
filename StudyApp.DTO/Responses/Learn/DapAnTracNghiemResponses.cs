namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ đáp án trắc nghiệm
    /// </summary>
    public class DapAnTracNghiemResponse
    {
        public int MaDapAn { get; set; }

        public int MaThe { get; set; }

        public string NoiDung { get; set; } = null!;

        public bool LaDapAnDung { get; set; }

        public int? ThuTu { get; set; }
    }

    /// <summary>
    /// Response rút gọn (dùng cho hiển thị câu hỏi)
    /// Không trả LaDapAnDung để tránh lộ đáp án
    /// </summary>
    public class DapAnTracNghiemViewResponse
    {
        public int MaDapAn { get; set; }

        public string NoiDung { get; set; } = null!;

        public int? ThuTu { get; set; }
    }
}
