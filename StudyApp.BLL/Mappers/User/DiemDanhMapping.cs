using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Responses.NguoiDung;

namespace StudyApp.BLL.Mappers.User
{
    /// <summary>
    /// Mapping cho module Điểm Danh
    /// </summary>
    public class DiemDanhMapping : Profile
    {
        public DiemDanhMapping()
        {
            MapCauHinhDiemDanh();
            MapDiemDanhHangNgay();
        }

        #region Cấu hình điểm danh

        private void MapCauHinhDiemDanh()
        {
            CreateMap<CauHinhDiemDanh, CauHinhDiemDanhResponse>()
                .ForMember(d => d.NgayThu,
                    o => o.MapFrom(s => s.NgayThu))
                .ForMember(d => d.ThuongVang,
                    o => o.MapFrom(s => s.ThuongVang))
                .ForMember(d => d.ThuongXp,
                    o => o.MapFrom(s => s.ThuongXp))
                .ForMember(d => d.ThuongDacBiet,
                    o => o.MapFrom(s => s.ThuongDacBiet))

                // runtime value – Service sẽ set
                .ForMember(d => d.DaDiemDanh,
                    o => o.Ignore());
        }

        #endregion

        #region Lịch sử điểm danh

        private void MapDiemDanhHangNgay()
        {
            CreateMap<DiemDanhHangNgay, LichSuDiemDanhResponse>()
                .ForMember(d => d.NgayDiemDanh,
                    o => o.MapFrom(s => s.NgayDiemDanh))
                .ForMember(d => d.NgayThuMay,
                    o => o.MapFrom(s => s.NgayThuMay))
                .ForMember(d => d.ThuongVang,
                    o => o.MapFrom(s => s.ThuongVang))
                .ForMember(d => d.ThuongXp,
                    o => o.MapFrom(s => s.ThuongXp))
                .ForMember(d => d.ThuongDacBiet,
                    o => o.MapFrom(s => s.ThuongDacBiet));
        }

        #endregion
    }
}
