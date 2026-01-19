using StudyApp.DTO.Responses.Learn;

public interface ITagService
{
    // --- User Methods ---
    Task<IEnumerable<TagResponse>> GetAllAsync();
    Task<IEnumerable<TagSelectResponse>> SearchTagsAsync(string keyword);
    Task<bool> GanTagsChoBoDeAsync(int maBoDe, List<string> danhSachTenTag);

    // --- Admin Methods ---
    Task<bool> DeleteTagAsync(int tagId); // Xóa tag vi phạm hoặc rác
    Task<bool> UpdateTagNameAsync(int tagId, string newName);

    // Gộp Tag sai vào Tag chuẩn và cập nhật lại toàn bộ liên kết bộ đề
    Task<bool> MergeTagsAsync(int tagSaiId, string tenTagChuan);
}