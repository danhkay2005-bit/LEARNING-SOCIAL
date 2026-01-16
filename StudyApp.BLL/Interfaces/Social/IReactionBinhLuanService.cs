using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IReactionBinhLuanService
    {
     
        /// Thêm hoặc cập nhật reaction cho bình luận (Upsert)
    
        Task<ReactionBinhLuanResponse> TaoHoacCapNhatReactionAsync(TaoHoacCapNhatReactionBinhLuanRequest request);
        /// Xóa reaction khỏi bình luận
      
        Task<bool> XoaReactionAsync(XoaReactionBinhLuanRequest request);

        /// Lấy danh sách tất cả reaction của một bình luận
        Task<List<ReactionBinhLuanResponse>> LayDanhSachReactionTheoMaBinhLuanAsync(int maBinhLuan);

        
        /// Lấy thống kê số lượng reaction theo từng loại của bình luận
        
        Task<List<ThongKeReactionResponse>> LayThongKeReactionTheoMaBinhLuanAsync(int maBinhLuan);

        /// Kiểm tra người dùng đã reaction bình luận chưa
        
        Task<ReactionBinhLuanResponse?> KiemTraReactionCuaNguoiDungAsync(int maBinhLuan, Guid maNguoiDung);

        /// Lấy tổng số reaction của bình luận
       
        Task<int> LayTongSoReactionAsync(int maBinhLuan);
    }
}