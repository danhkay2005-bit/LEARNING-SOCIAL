using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyApp.DTO.Enums.StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;

namespace StudyApp.BLL.Interfaces.Social
{
    public interface IThongBaoService
    {
        /// <summary>
        /// Lấy danh sách thông báo có phân trang
        /// </summary>
        Task<PhanTrangThongBaoResponse> GetDanhSachThongBaoAsync(DanhSachThongBaoRequest request);

        /// <summary>
        /// Lấy thông báo theo ID
        /// </summary>
        Task<ThongBaoResponse?> GetThongBaoByIdAsync(int maThongBao);

        /// <summary>
        /// Lấy thống kê thông báo của người dùng
        /// </summary>
        Task<ThongKeThongBaoResponse> GetThongKeThongBaoAsync(Guid maNguoiNhan);

        /// <summary>
        /// Lấy danh sách thông báo chưa đọc (rút gọn)
        /// </summary>
        Task<IEnumerable<ThongBaoSummaryResponse>> GetThongBaoChuaDocAsync(Guid maNguoiNhan, int soLuong = 10);

        /// <summary>
        /// Tạo thông báo mới (hệ thống)
        /// </summary>
        Task<ThongBaoResponse> TaoThongBaoAsync(TaoThongBaoRequest request);

        /// <summary>
        /// Đánh dấu một thông báo là đã đọc
        /// </summary>
        Task<bool> DanhDauDaDocAsync(int maThongBao);

        /// <summary>
        /// Đánh dấu tất cả thông báo là đã đọc
        /// </summary>
        Task<int> DanhDauTatCaDaDocAsync(Guid maNguoiNhan);

        /// <summary>
        /// Xóa thông báo
        /// </summary>
        Task<bool> XoaThongBaoAsync(int maThongBao, Guid maNguoiNhan);

        /// <summary>
        /// Xóa tất cả thông báo đã đọc
        /// </summary>
        Task<int> XoaTatCaThongBaoDaDocAsync(Guid maNguoiNhan);

        /// <summary>
        /// Tạo thông báo khi có người reaction bài đăng
        /// </summary>
        Task TaoThongBaoReactionBaiDangAsync(int maBaiDang, Guid nguoiReaction, Guid nguoiNhan);

        /// <summary>
        /// Tạo thông báo khi có người bình luận
        /// </summary>
        Task TaoThongBaoBinhLuanAsync(int maBaiDang, int maBinhLuan, Guid nguoiBinhLuan, Guid nguoiNhan);

        /// <summary>
        /// Tạo thông báo khi có người chia sẻ bài đăng
        /// </summary>
        Task TaoThongBaoChiaSeBaiDangAsync(int maBaiDang, Guid nguoiChiaSe, Guid nguoiNhan);

        /// <summary>
        /// Tạo thông báo khi có người theo dõi
        /// </summary>
        Task TaoThongBaoTheoDoiAsync(Guid nguoiTheoDoi, Guid nguoiDuocTheoDoi);

        /// <summary>
        /// Tạo thông báo khi được mention trong bài đăng
        /// </summary>
        Task TaoThongBaoMentionBaiDangAsync(int maBaiDang, Guid nguoiMention, Guid nguoiDuocMention);

        /// <summary>
        /// Tạo thông báo khi được mention trong bình luận
        /// </summary>
        Task TaoThongBaoMentionBinhLuanAsync(int maBinhLuan, Guid nguoiMention, Guid nguoiDuocMention);
    }
}