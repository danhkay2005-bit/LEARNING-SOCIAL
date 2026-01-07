using AutoMapper;
using StudyApp.BLL.Mappers;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// AutoMapper cho Cuộc Trò Chuyện (Chat)
    /// </summary>
    public class CuocTroChuyenMapping : Profile
    {
        public CuocTroChuyenMapping()
        {
            MapEntityToResponse();
        }

        #region Entity → Response

        private void MapEntityToResponse()
        {
            // ===== CHI TIẾT CUỘC TRÒ CHUYỆN =====
            CreateMap<CuocTroChuyen, CuocTroChuyenResponse>()
                .ForMember(d => d.LoaiCuocTroChuyen,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiCuocTroChuyenEnum>(s.LoaiCuocTroChuyen)))
                .ForMember(d => d.TinNhanCuoi,
                    o => o.Ignore()) // map từ TinNhan
                .ForMember(d => d.ThoiGianTinCuoi,
                    o => o.MapFrom(s => s.ThoiGianTinCuoi))
                .ForMember(d => d.SoThanhVien,
                    o => o.MapFrom(s => s.ThanhVienCuocTroChuyens.Count))

                // user-context
                .ForMember(d => d.SoTinChuaDoc, o => o.Ignore())
                .ForMember(d => d.TatThongBao, o => o.Ignore())
                .ForMember(d => d.GhimCuocTro, o => o.Ignore())
                .ForMember(d => d.BiDanhCuaToi, o => o.Ignore())
                .ForMember(d => d.VaiTroCuaToi, o => o.Ignore())

                // navigation
                .ForMember(d => d.NguoiTaoNhom, o => o.Ignore())
                .ForMember(d => d.ThanhViens, o => o.Ignore())

                // chat cá nhân
                .ForMember(d => d.NguoiNhan, o => o.Ignore())
                .ForMember(d => d.NguoiNhanOnline, o => o.Ignore())
                .ForMember(d => d.ThoiGianOnlineCuoi, o => o.Ignore());

            // ===== TÓM TẮT (DANH SÁCH) =====
            CreateMap<CuocTroChuyen, CuocTroChuyenTomTatResponse>()
                .ForMember(d => d.LoaiCuocTroChuyen,
                    o => o.MapFrom(s =>
                        MappingHelpers.ParseEnum<LoaiCuocTroChuyenEnum>(s.LoaiCuocTroChuyen)))
                .ForMember(d => d.NoiDungTinCuoi,
                    o => o.MapFrom(s => s.NoiDungTinCuoi))
                .ForMember(d => d.ThoiGianTinCuoi,
                    o => o.MapFrom(s => s.ThoiGianTinCuoi))

                // hiển thị (tùy loại chat)
                .ForMember(d => d.TenHienThi, o => o.Ignore())
                .ForMember(d => d.AnhHienThi, o => o.Ignore())

                // user-context
                .ForMember(d => d.SoTinChuaDoc, o => o.Ignore())
                .ForMember(d => d.TatThongBao, o => o.Ignore())
                .ForMember(d => d.GhimCuocTro, o => o.Ignore())
                .ForMember(d => d.Online, o => o.Ignore());
        }

        #endregion
    }
}
