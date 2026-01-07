using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyApp.BLL.Mappers.Learning
{
    /// <summary>
    /// Mapping cho Phần tử sắp xếp (PhanTuSapXep)
    /// </summary>
    public class PhanTuSapXepMapping : Profile
    {
        public PhanTuSapXepMapping()
        {
            // =====================================================
            // REQUEST -> ENTITY
            // Tạo phần tử sắp xếp
            // =====================================================
            CreateMap<TaoPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(dest => dest.MaPhanTu, opt => opt.Ignore())       // identity
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())         // lấy từ route
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore());

            // =====================================================
            // REQUEST -> ENTITY
            // Cập nhật phần tử sắp xếp
            // =====================================================
            CreateMap<CapNhatPhanTuSapXepRequest, PhanTuSapXep>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.MaTheNavigation, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, value) => value != null));

            // =====================================================
            // ENTITY -> RESPONSE
            // PhanTuSapXep -> PhanTuSapXepResponse
            // =====================================================
            CreateMap<PhanTuSapXep, PhanTuSapXepResponse>();

            // =====================================================
            // ENTITY -> RESPONSE (KHI HỌC)
            // Thứ tự hiển thị được service trộn sẵn
            // =====================================================
            CreateMap<PhanTuSapXep, PhanTuSapXepHocResponse>()
                .ForMember(dest => dest.ThuTuHienThi, opt => opt.Ignore());
        }
    }
}
