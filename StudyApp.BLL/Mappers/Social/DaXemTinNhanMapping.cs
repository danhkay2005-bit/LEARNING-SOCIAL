using AutoMapper;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers.Social
{
    /// <summary>
    /// Mapping cho Đã Xem Tin Nhắn
    /// </summary>
    public class DaXemTinNhanMapping : Profile
    {
        public DaXemTinNhanMapping()
        {
            CreateMap<DaXemTinNhan, NguoiXemTinNhanResponse>()
                .ForMember(d => d.MaNguoiXem,
                    o => o.MapFrom(s => s.MaNguoiXem))
                .ForMember(d => d.ThoiGianXem,
                    o => o.MapFrom(s => s.ThoiGianXem))
                // navigation
                .ForMember(d => d.NguoiXem,
                    o => o.Ignore());
        }
    }
}
