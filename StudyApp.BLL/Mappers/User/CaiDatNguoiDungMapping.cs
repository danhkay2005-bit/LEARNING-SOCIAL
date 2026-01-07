using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using System;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho Cài Đặt Người Dùng
    /// </summary>
    public class CaiDatNguoiDungMapping : Profile
    {
        public CaiDatNguoiDungMapping()
        {
            MapRequestToEntity();
            MapEntityToResponse();
        }

        #region Request → Entity

        private void MapRequestToEntity()
        {
            CreateMap<CapNhatCaiDatNguoiDungRequest, CaiDatNguoiDung>()
                // UI
                .ForMember(d => d.CheDoToi, o => o.MapFrom(s => s.CheDoToi))
                .ForMember(d => d.CoHieuUng, o => o.MapFrom(s => s.CoHieuUng))

                // Notification
                .ForMember(d => d.ThongBaoNhacHoc, o => o.MapFrom(s => s.ThongBaoNhacHoc))
                .ForMember(d => d.GioNhacHoc, o => o.MapFrom(s => s.GioNhacHoc))
                .ForMember(d => d.ThongBaoThanhTuu, o => o.MapFrom(s => s.ThongBaoThanhTuu))
                .ForMember(d => d.ThongBaoXaHoi, o => o.MapFrom(s => s.ThongBaoXaHoi))
                .ForMember(d => d.ThongBaoThachDau, o => o.MapFrom(s => s.ThongBaoThachDau))

                // Privacy
                .ForMember(d => d.HienThiTrangThai, o => o.MapFrom(s => s.HienThiTrangThai))
                .ForMember(d => d.HienThiThongKe, o => o.MapFrom(s => s.HienThiThongKe))
                .ForMember(d => d.ChoPhepThachDau, o => o.MapFrom(s => s.ChoPhepThachDau))
                .ForMember(d => d.ChoPhepNhanTin, o => o.MapFrom(s => s.ChoPhepNhanTin))
                .ForMember(d => d.ChoPhepTagTrongBaiDang, o => o.MapFrom(s => s.ChoPhepTagTrongBaiDang))

                // system
                .ForMember(d => d.ThoiGianCapNhat,
                    o => o.MapFrom(_ => DateTime.UtcNow))

                // không cho mapper
                .ForMember(d => d.MaNguoiDung, o => o.Ignore())
                .ForMember(d => d.MaNguoiDungNavigation, o => o.Ignore());
        }

        #endregion

        #region Entity → Response

        private void MapEntityToResponse()
        {
            CreateMap<CaiDatNguoiDung, CaiDatNguoiDungResponse>();
            // mapping 1–1 nên AutoMapper tự xử lý là đủ
        }

        #endregion
    }
}
