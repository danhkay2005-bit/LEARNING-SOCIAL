using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.Social
{
    public interface IMentionService
    {
        void XuLyMentionBaiDang(int maBaiDang, string? noiDung);
        void XuLyMentionBinhLuan(int maBinhLuan, string? noiDung);

        DanhSachMentionResponse LayDanhSachMention(LayMentionRequest request);
        void DanhDauDaDoc(DanhDauMentionDaDocRequest request);
    }
}
