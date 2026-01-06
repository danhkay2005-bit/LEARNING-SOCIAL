using AutoMapper;
using StudyApp.DAL.Entities.Learning;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learning;
using StudyApp.DTO.Responses.Learning;
using static StudyApp.BLL.Mappers.MappingHelpers;

namespace StudyApp.BLL.Mappers.Learning
{
    public class TheFlashCardMapping : Profile
    {
        public TheFlashCardMapping()
        {
            CreateMap<TaoTheFlashcardRequest, TheFlashcard>()
            .ForMember(d => d.MaThe, o => o.Ignore())
            .ForMember(d => d.LoaiThe, o => o.MapFrom(s => s.LoaiThe.ToString()))
            .ForMember(d => d.DoKho, o => o.MapFrom(s => s.DoKho.HasValue ? (byte)s.DoKho.Value : (byte?)null))
            .ForMember(d => d.SoLuotHoc, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLanDung, o => o.MapFrom(_ => 0))
            .ForMember(d => d.SoLanSai, o => o.MapFrom(_ => 0))
            .ForMember(d => d.TyLeDungTb, o => o.MapFrom(_ => 0.0))
            .ForMember(d => d.ThoiGianTao, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow));

            
            CreateMap<CapNhatTheFlashcardRequest, TheFlashcard>()
                .ForMember(d => d.MaThe, o => o.Ignore())
                .ForMember(d => d.MaBoDe, o => o.Ignore())
                .ForMember(d => d.ThoiGianTao, o => o.Ignore())
                .ForMember(d => d.ThoiGianCapNhat, o => o.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.LoaiThe, o => o.MapFrom((s, d) => s.LoaiThe.HasValue ? s.LoaiThe.Value.ToString() : d.LoaiThe))
                .ForMember(d => d.DoKho, o => o.MapFrom((s, d) => s.DoKho.HasValue ? (byte)s.DoKho.Value : d.DoKho))                
                .ForAllMembers(o => o.Condition((s, d, sm) => sm != null));

           
            CreateMap<TheFlashcard, TheFlashcardResponse>()
                .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
                .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
                .ForMember(d => d.ThuTu, o => o.MapFrom(s => s.ThuTu ?? 0))
                .ForMember(d => d.SoLuotHoc, o => o.MapFrom(s => s.SoLuotHoc ?? 0))
                .ForMember(d => d.SoLanDung, o => o.MapFrom(s => s.SoLanDung ?? 0))
                .ForMember(d => d.SoLanSai, o => o.MapFrom(s => s.SoLanSai ?? 0))
                .ForMember(d => d.TyLeDungTb, o => o.MapFrom(s => s.TyLeDungTb ?? 0))
                .ForMember(d => d.TienDoCuaToi, o => o.Ignore())
                .ForMember(d => d.DanhDauCuaToi, o => o.Ignore())
                .ForMember(d => d.GhiChuCuaToi, o => o.Ignore());

            CreateMap<TheFlashcard, TheFlashcardTomTatResponse>()
                .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
                .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
                .ForMember(d => d.MatTruocRutGon, o => o.MapFrom(s => Truncate(s.MatTruoc, 100)))
                .ForMember(d => d.MatSauRutGon, o => o.MapFrom(s => Truncate(s.MatSau, 100)))
                .ForMember(d => d.TrangThaiSRS, o => o.Ignore());

            CreateMap<TheFlashcard, TheHocResponse>()
                .ForMember(d => d.LoaiThe, o => o.MapFrom(s => ParseEnum<LoaiTheEnum>(s.LoaiThe)))
                .ForMember(d => d.DoKho, o => o.MapFrom(s => ByteToEnum<MucDoKhoEnum>(s.DoKho)))
                .ForMember(d => d.DapAnTracNghiems, o => o.Ignore())
                .ForMember(d => d.CapGheps, o => o.Ignore())
                .ForMember(d => d.PhanTuSapXeps, o => o.Ignore())
                .ForMember(d => d.TuDienKhuyets, o => o.Ignore());

           
            CreateMap<TaoDapAnTracNghiemRequest, DapAnTracNghiem>();
            CreateMap<TaoCapGhepRequest, CapGhep>();
            CreateMap<TaoPhanTuSapXepRequest, PhanTuSapXepMapping>();
            CreateMap<TaoTuDienKhuyetRequest, TuDienKhuyet>();

        }
    }
}
