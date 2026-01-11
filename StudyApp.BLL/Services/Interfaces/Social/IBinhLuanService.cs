using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.Social
{
    public interface IBinhLuanService
    {
        DanhSachBinhLuanResponse LayDanhSachBinhLuan(LayBinhLuanRequest request);

        TaoBinhLuanResponse TaoBinhLuan(TaoBinhLuanRequest request);
        void CapNhatBinhLuan(CapNhatBinhLuanRequest request);
        void XoaBinhLuan(XoaBinhLuanRequest request);
    }
}
