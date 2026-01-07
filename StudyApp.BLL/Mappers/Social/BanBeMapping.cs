using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper cho chức năng Bạn Bè
    /// </summary>
    public class BanBeMapping : Profile
    {
        public BanBeMapping()
        {
            MapEntityToResponse();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== DANH SÁCH BẠN BÈ =====
            CreateMap<BanBe, BanBeResponse>()
                .ForMember(d => d.MaNguoiDung,
                    o => o.Ignore()) // xác định trong Service (là ai trong mối quan hệ)
                .ForMember(d => d.TrangThai,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<TrangThaiBanBeEnum>(s.TrangThai)))
                .ForMember(d => d.LaNguoiGui,
                    o => o.Ignore()) // user-context
                .ForMember(d => d.SoBanChung,
                    o => o.Ignore()) // tính toán
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore()); // map từ User

            // ===== LỜI MỜI KẾT BẠN =====
            CreateMap<BanBe, LoiMoiKetBanResponse>()
                .ForMember(d => d.MaNguoiGui,
                    o => o.MapFrom(s => s.MaNguoiGui))
                .ForMember(d => d.ThoiGianGui,
                    o => o.MapFrom(s => s.ThoiGianGui))
                .ForMember(d => d.SoBanChung,
                    o => o.Ignore())
                .ForMember(d => d.BanChung,
                    o => o.Ignore())
                .ForMember(d => d.NguoiGui,
                    o => o.Ignore());

            // ===== GỢI Ý KẾT BẠN =====
            CreateMap<BanBe, GoiYKetBanResponse>()
                .ForMember(d => d.MaNguoiDung,
                    o => o.Ignore()) // user được gợi ý
                .ForMember(d => d.SoBanChung,
                    o => o.Ignore())
                .ForMember(d => d.BanChung,
                    o => o.Ignore())
                .ForMember(d => d.LyDoGoiY,
                    o => o.Ignore())
                .ForMember(d => d.NguoiDung,
                    o => o.Ignore());
        }

        #endregion
    }
}
