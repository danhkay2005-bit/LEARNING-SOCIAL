using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IReactionBaiDangService
    {
        
        /// Thêm ho?c c?p nh?t reaction cho bài ??ng (Upsert)
        Task<ReactionBaiDangResponse> TaoHoacCapNhatReactionAsync(TaoHoacCapNhatReactionBaiDangRequest request);

       
        /// Xóa reaction khỏi bài đăng        
        Task<bool> XoaReactionAsync(XoaReactionBaiDangRequest request);

     
        /// Lấy danh sách tất cae reaction của một bài
        Task<List<ReactionBaiDangResponse>> LayDanhSachReactionTheoMaBaiDangAsync(int maBaiDang);


        /// Lấy thống kê số lượng reaction theo từng loại cho bài đăng
        Task<List<ThongKeReactionResponse>> LayThongKeReactionTheoMaBaiDangAsync(int maBaiDang);

      
        /// Kiểm tra người dùng đã reaction bài đăng chưa 
        Task<ReactionBaiDangResponse?> KiemTraReactionCuaNguoiDungAsync(int maBaiDang, Guid maNguoiDung);

        
        /// Lấy từng số  reaction của bài đăng 
        Task<int> LayTongSoReactionAsync(int maBaiDang);

    }
}