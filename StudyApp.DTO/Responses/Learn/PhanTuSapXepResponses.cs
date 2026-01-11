namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ phần tử sắp xếp
    /// </summary>
    public class PhanTuSapXepResponse
    {
        public int MaPhanTu { get; set; }

        public int MaThe { get; set; }

        public string NoiDung { get; set; } = null!;

        /// <summary>
        /// Thứ tự đúng (chỉ dùng cho chấm bài / admin)
        /// </summary>
        public int ThuTuDung { get; set; }
    }

    /// <summary>
    /// Response hiển thị cho người học (không lộ thứ tự đúng)
    /// </summary>
    public class PhanTuSapXepViewResponse
    {
        public int MaPhanTu { get; set; }

        public string NoiDung { get; set; } = null!;
    }
}
