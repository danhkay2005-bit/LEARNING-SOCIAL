using StudyApp.DTO.Responses.Learn;

public interface ITagService
{
    Task<IEnumerable<TagResponse>> GetAllAsync();
    Task<IEnumerable<TagSelectResponse>> SearchTagsAsync(string keyword);
    Task<bool> GanTagsChoBoDeAsync(int maBoDe, List<string> danhSachTenTag);
}