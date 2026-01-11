using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.Social
{
    public interface IReactionService
    {
        // ===== BÀI ĐĂNG =====
        KetQuaReactionResponse ReactionBaiDang(ReactionBaiDangRequest request);
        KetQuaReactionResponse XoaReactionBaiDang(XoaReactionBaiDangRequest request);

        // ===== BÌNH LUẬN =====
        KetQuaThichBinhLuanResponse ThichBinhLuan(ThichBinhLuanRequest request);
        KetQuaThichBinhLuanResponse BoThichBinhLuan(BoThichBinhLuanRequest request);
    }
}
