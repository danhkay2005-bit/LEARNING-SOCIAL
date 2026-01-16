using AutoMapper;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Linq;

namespace StudyApp.BLL.Mappings.Learn
{
    public class TheFlashcardProfile : Profile
    {
        public TheFlashcardProfile()
        {
            // =================================================
            // 1. REQUEST -> ENTITY (Tạo mới & Cập nhật)
            // =================================================
            CreateMap<TaoTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.MaThe, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiThe, opt => opt.MapFrom(src => src.LoaiThe.ToString()))
                .ForMember(dest => dest.DoKho, opt => opt.MapFrom(src => src.DoKho.HasValue ? (byte?)src.DoKho.Value : null))
                .ForMember(dest => dest.ThoiGianTao, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<CapNhatTheFlashcardRequest, TheFlashcard>()
                .ForMember(dest => dest.MaBoDe, opt => opt.Ignore())
                .ForMember(dest => dest.LoaiThe, opt => opt.MapFrom(src => src.LoaiThe.ToString()))
                .ForMember(dest => dest.ThoiGianCapNhat, opt => opt.MapFrom(_ => DateTime.Now));

            // =================================================
            // 2. ENTITY -> RESPONSE CƠ BẢN (UPDATE MAPPING NGÀY ÔN TẬP)
            // =================================================
            CreateMap<TheFlashcard, TheFlashcardResponse>()
                .ForMember(dest => dest.LoaiThe, opt => opt.MapFrom(src => Enum.Parse<LoaiTheEnum>(src.LoaiThe ?? "TracNghiem")))
                .ForMember(dest => dest.MatTruoc, opt => opt.MapFrom(src => src.MatTruoc))
                .ForMember(dest => dest.MatSau, opt => opt.MapFrom(src => src.MatSau))
                .ForMember(dest => dest.DoKho, opt => opt.MapFrom(src => src.DoKho.HasValue ? (MucDoKhoEnum?)src.DoKho.Value : null))

               // --- LẤY DỮ LIỆU TỪ BẢNG TIẾN ĐỘ HỌC TẬP ---
               // Vì f.TienDoHocTaps đã được lọc theo User trong Service, ta lấy bản ghi đầu tiên
               .ForMember(dest => dest.NgayOnTapTiepTheo, opt => opt.MapFrom(src =>
                src.TienDoHocTaps.Select(t => t.NgayOnTapTiepTheo).FirstOrDefault()))

            .ForMember(dest => dest.SoLanDung, opt => opt.MapFrom(src =>
                src.TienDoHocTaps.Select(t => (int?)t.SoLanDung).FirstOrDefault() ?? 0))

            .ForMember(dest => dest.SoLanSai, opt => opt.MapFrom(src =>
                src.TienDoHocTaps.Select(t => (int?)t.SoLanSai).FirstOrDefault() ?? 0));

            // Map các loại đáp án chi tiết
            CreateMap<DapAnTracNghiem, DapAnTracNghiemResponse>();
            CreateMap<PhanTuSapXep, PhanTuSapXepViewResponse>();
            CreateMap<TuDienKhuyet, TuDienKhuyetViewResponse>();
            CreateMap<CapGhep, CapGhepResponse>();

            // =================================================
            // 3. ENTITY -> CHI TIẾT CÂU HỎI HỌC
            // =================================================
            CreateMap<TheFlashcard, ChiTietCauHoiHocResponse>()
                .ForMember(dest => dest.ThongTinThe, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.TracNghiem, opt => opt.MapFrom(src => src.DapAnTracNghiems))
                .ForMember(dest => dest.SapXep, opt => opt.MapFrom(src => src.PhanTuSapXeps))
                .ForMember(dest => dest.DienKhuyet, opt => opt.MapFrom(src => src.TuDienKhuyets))
                .ForMember(dest => dest.CapGhep, opt => opt.MapFrom(src => src.CapGheps));

            CreateMap<BoDeHoc, BoDeHocResponse>();
        }
    }
}