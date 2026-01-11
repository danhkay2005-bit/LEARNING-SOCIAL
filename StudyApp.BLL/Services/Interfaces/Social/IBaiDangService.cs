using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.Social
{
    public interface IBaiDangService
    {
        DanhSachBaiDangResponse LayDanhSachBaiDang(LayBaiDangRequest request);
        BaiDangResponse LayChiTietBaiDang(int maBaiDang);

        TaoBaiDangResponse TaoBaiDang(TaoBaiDangRequest request);
        void CapNhatBaiDang(CapNhatBaiDangRequest request);
        void XoaBaiDang(XoaBaiDangRequest request);

        void GhimBaiDang(GhimBaiDangRequest request);
    }
}
