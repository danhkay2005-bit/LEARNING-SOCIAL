using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Mappings.User
{
    public class BaoVeChuoiNgayProfile : Profile
    {
        public BaoVeChuoiNgayProfile()
        {
            // =========================================================
            // REQUEST -> ENTITY (Khi user dùng vật phẩm bảo vệ)
            // =========================================================
            // Giả sử bạn có class Request này, nếu chưa dùng thì có thể comment lại
            // CreateMap<SuDungBaoVeChuoiNgayRequest, BaoVeChuoiNgay>()
            //     .ForMember(dest => dest.LoaiBaoVe, opt => opt.MapFrom(src => src.LoaiBaoVe.ToString()));

            // =========================================================
            // ENTITY -> RESPONSE (Hiển thị lịch sử dùng bảo vệ)
            // =========================================================
            CreateMap<BaoVeChuoiNgay, BaoVeChuoiNgayResponse>()
                // Quan trọng: Chuyển chuỗi "Freeze" trong DB thành Enum
                .ForMember(dest => dest.LoaiBaoVe, opt => opt.MapFrom(src => ParseLoaiBaoVe(src.LoaiBaoVe)));
        }

        // Hàm parse an toàn (Tránh crash nếu DB lưu sai chuỗi)
        private static LoaiBaoVeChuoiEnum ParseLoaiBaoVe(string? value)
        {
            if (string.IsNullOrEmpty(value)) return LoaiBaoVeChuoiEnum.Freeze;

            // Thử parse, nếu lỗi thì mặc định là Freeze
            if (Enum.TryParse<LoaiBaoVeChuoiEnum>(value, true, out var result))
            {
                return result;
            }
            return LoaiBaoVeChuoiEnum.Freeze;
        }
    }
}