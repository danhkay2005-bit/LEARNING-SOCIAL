namespace StudyApp.DTO.Responses.Learn
{
    /// <summary>
    /// Response đầy đủ từ điền khuyết
    /// </summary>
    public class TuDienKhuyetResponse
    {
        public int MaTuDienKhuyet { get; set; }

        public int MaThe { get; set; }

        public string TuCanDien { get; set; } = null!;

        public int ViTriTrongCau { get; set; }
    }

    /// <summary>
    /// Response hiển thị cho người học (không lộ đáp án)
    /// </summary>
    public class TuDienKhuyetViewResponse
    {
        public int MaTuDienKhuyet { get; set; }

        public int ViTriTrongCau { get; set; }
    }
}
