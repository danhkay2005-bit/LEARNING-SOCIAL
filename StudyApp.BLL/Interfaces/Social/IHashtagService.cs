using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    /// <summary>
    /// ??? Interface cho Hashtag Service
    /// 
    /// CH?C N?NG:
    /// 1. Tìm ki?m bài vi?t theo hashtag
    /// 2. L?y danh sách hashtag trending (ph? bi?n)
    /// 3. G?i ý hashtag khi gõ
    /// </summary>
    public interface IHashtagService
    {
        /// <summary>
        /// ?? Tìm ki?m bài vi?t theo hashtag
        /// </summary>
        /// <param name="tenHashtag">Tên hashtag (không c?n d?u #)</param>
        /// <param name="page">Trang (m?c ??nh 1)</param>
        /// <param name="pageSize">S? bài/trang (m?c ??nh 20)</param>
        /// <returns>Danh sách bài ??ng</returns>
        Task<List<BaiDangResponse>> TimKiemBaiDangTheoHashtagAsync(string tenHashtag, int page = 1, int pageSize = 20);

        /// <summary>
        /// ?? L?y danh sách hashtag trending (ph? bi?n nh?t)
        /// </summary>
        /// <param name="top">S? l??ng hashtag (m?c ??nh 10)</param>
        /// <returns>Danh sách hashtag</returns>
        Task<List<HashtagResponse>> LayDanhSachHashtagTrendingAsync(int top = 10);

        /// <summary>
        /// ?? G?i ý hashtag khi user gõ
        /// </summary>
        /// <param name="tuKhoa">T? khóa tìm ki?m (vd: "h?c")</param>
        /// <param name="limit">S? l??ng g?i ý (m?c ??nh 5)</param>
        /// <returns>Danh sách hashtag phù h?p</returns>
        Task<List<HashtagResponse>> GoiYHashtagAsync(string tuKhoa, int limit = 5);

        /// <summary>
        /// ?? L?y thông tin chi ti?t 1 hashtag
        /// </summary>
        /// <param name="tenHashtag">Tên hashtag</param>
        /// <returns>Thông tin hashtag</returns>
        Task<HashtagResponse?> LayThongTinHashtagAsync(string tenHashtag);
    }
}
