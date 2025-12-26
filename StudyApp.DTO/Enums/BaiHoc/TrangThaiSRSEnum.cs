namespace StudyApp.DTO.Enums.BaiHoc
{
    public enum TrangThaiSRSEnum : byte
    {
        New = 0,      // Thẻ mới chưa học
        Learning = 1, // Đang trong giai đoạn ghi nhớ ngắn hạn
        Review = 2,   // Giai đoạn ôn tập dài hạn
        Mastered = 3  // Đã thành thạo
    }
}