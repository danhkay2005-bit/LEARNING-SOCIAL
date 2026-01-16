using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class TheoDoiNguoiDungService : ITheoDoiNguoiDungService
    {
        private readonly SocialDbContext _socialContext;
        private readonly UserDbContext _userContext;
        private readonly IMapper _mapper;

        public TheoDoiNguoiDungService(
            SocialDbContext socialContext,
            UserDbContext userContext,
            IMapper mapper)
        {
            _socialContext = socialContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<TheoDoiNguoiDungResponse> TheoDoiAsync(TheoDoiNguoiDungRequest request)
        {
            // Kiểm tra không thể tự theo dõi bản thân
            if (request.MaNguoiTheoDoi == request.MaNguoiDuocTheoDoi)
            {
                throw new Exception("Không thể tự theo dõi bản thân.");
            }

            // Kiểm tra người dùng có tồn tại không
            var nguoiTheoDoi = await _userContext.NguoiDungs.FindAsync(request.MaNguoiTheoDoi);
            var nguoiDuocTheoDoi = await _userContext.NguoiDungs.FindAsync(request.MaNguoiDuocTheoDoi);

            if (nguoiTheoDoi == null || nguoiDuocTheoDoi == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            // Kiểm tra đã theo dõi chưa
            var daTonTai = await _socialContext.TheoDoiNguoiDungs
                .AnyAsync(x => x.MaNguoiTheoDoi == request.MaNguoiTheoDoi 
                            && x.MaNguoiDuocTheoDoi == request.MaNguoiDuocTheoDoi);

            if (daTonTai)
            {
                throw new Exception("Đã theo dõi người dùng này rồi.");
            }

            // Tạo mới theo dõi
            var theoDoi = new TheoDoiNguoiDung
            {
                MaNguoiTheoDoi = request.MaNguoiTheoDoi,
                MaNguoiDuocTheoDoi = request.MaNguoiDuocTheoDoi,
                ThoiGian = DateTime.Now
            };

            _socialContext.TheoDoiNguoiDungs.Add(theoDoi);
            await _socialContext.SaveChangesAsync();

            // Tạo thông báo (nếu có hệ thống thông báo)
            try
            {
                var thongBao = new ThongBao
                {
                    // Sửa lỗi: MaThongBao là int, không phải Guid
                    // Nếu bạn cần tạo giá trị tự động, hãy bỏ dòng này (giả sử DB tự tăng)
                    MaNguoiNhan = request.MaNguoiDuocTheoDoi,
                    LoaiThongBao = 0, // hoặc giá trị phù hợp với "TheoDoi"
                    NoiDung = $"{nguoiTheoDoi.HoVaTen ?? nguoiTheoDoi.TenDangNhap} đã bắt đầu theo dõi bạn.",
                    MaNguoiGayRa = request.MaNguoiTheoDoi,
                    ThoiGian = DateTime.Now,
                    DaDoc = false
                };

                _socialContext.ThongBaos.Add(thongBao);
                await _socialContext.SaveChangesAsync();
            }
            catch
            {
                // Không ảnh hưởng đến chức năng chính nếu tạo thông báo thất bại
            }

            return _mapper.Map<TheoDoiNguoiDungResponse>(theoDoi);
        }

        public async Task<bool> BoTheoDoiAsync(BoTheoDoiNguoiDungRequest request)
        {
            var theoDoi = await _socialContext.TheoDoiNguoiDungs
                .FirstOrDefaultAsync(x => x.MaNguoiTheoDoi == request.MaNguoiTheoDoi 
                                       && x.MaNguoiDuocTheoDoi == request.MaNguoiDuocTheoDoi);

            if (theoDoi == null)
            {
                throw new Exception("Chưa theo dõi người dùng này.");
            }

            _socialContext.TheoDoiNguoiDungs.Remove(theoDoi);
            await _socialContext.SaveChangesAsync();

            return true;
        }

        public async Task<TrangThaiTheoDoiResponse> KiemTraTheoDoiAsync(KiemTraTheoDoiRequest request)
        {
            var theoDoi = await _socialContext.TheoDoiNguoiDungs
                .FirstOrDefaultAsync(x => x.MaNguoiTheoDoi == request.MaNguoiTheoDoi 
                                       && x.MaNguoiDuocTheoDoi == request.MaNguoiDuocTheoDoi);

            return new TrangThaiTheoDoiResponse
            {
                MaNguoiTheoDoi = request.MaNguoiTheoDoi,
                MaNguoiDuocTheoDoi = request.MaNguoiDuocTheoDoi,
                DangTheoDoi = theoDoi != null,
                ThoiGianTheoDoi = theoDoi?.ThoiGian
            };
        }

        public async Task<IEnumerable<NguoiTheoDoiResponse>> LayDanhSachNguoiTheoDoiAsync(
            Guid maNguoiDung, 
            Guid? nguoiXemId = null)
        {
            var danhSachTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .Where(x => x.MaNguoiDuocTheoDoi == maNguoiDung)
                .Select(x => new
                {
                    x.MaNguoiTheoDoi,
                    x.ThoiGian
                })
                .ToListAsync();

            var danhSachMaNguoiDung = danhSachTheoDoi.Select(x => x.MaNguoiTheoDoi).ToList();

            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => danhSachMaNguoiDung.Contains(x.MaNguoiDung))
                .Select(x => new
                {
                    x.MaNguoiDung,
                    x.TenDangNhap,
                    x.HoVaTen,
                    x.HinhDaiDien,
                    x.TieuSu
                })
                .ToListAsync();

            // Kiểm tra người xem có đang theo dõi lại không (nếu có nguoiXemId)
            List<Guid> danhSachTheoDoiLai = new List<Guid>();
            if (nguoiXemId.HasValue)
            {
                danhSachTheoDoiLai = await _socialContext.TheoDoiNguoiDungs
                    .Where(x => x.MaNguoiTheoDoi == nguoiXemId.Value 
                             && danhSachMaNguoiDung.Contains(x.MaNguoiDuocTheoDoi))
                    .Select(x => x.MaNguoiDuocTheoDoi)
                    .ToListAsync();
            }

            var result = from theoDoi in danhSachTheoDoi
                         join nguoiDung in thongTinNguoiDung on theoDoi.MaNguoiTheoDoi equals nguoiDung.MaNguoiDung
                         select new NguoiTheoDoiResponse
                         {
                             MaNguoiDung = nguoiDung.MaNguoiDung,
                             TenDangNhap = nguoiDung.TenDangNhap,
                             HoVaTen = nguoiDung.HoVaTen,
                             HinhDaiDien = nguoiDung.HinhDaiDien,
                             TieuSu = nguoiDung.TieuSu,
                             ThoiGianTheoDoi = theoDoi.ThoiGian,
                             DangTheoDoiLai = danhSachTheoDoiLai.Contains(nguoiDung.MaNguoiDung)
                         };

            return result.OrderByDescending(x => x.ThoiGianTheoDoi).ToList();
        }

        public async Task<IEnumerable<NguoiDangTheoDoiResponse>> LayDanhSachDangTheoDoiAsync(
            Guid maNguoiDung, 
            Guid? nguoiXemId = null)
        {
            var danhSachTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .Where(x => x.MaNguoiTheoDoi == maNguoiDung)
                .Select(x => new
                {
                    x.MaNguoiDuocTheoDoi,
                    x.ThoiGian
                })
                .ToListAsync();

            var danhSachMaNguoiDung = danhSachTheoDoi.Select(x => x.MaNguoiDuocTheoDoi).ToList();

            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => danhSachMaNguoiDung.Contains(x.MaNguoiDung))
                .Select(x => new
                {
                    x.MaNguoiDung,
                    x.TenDangNhap,
                    x.HoVaTen,
                    x.HinhDaiDien,
                    x.TieuSu
                })
                .ToListAsync();

            // Kiểm tra người đó có theo dõi lại không
            var danhSachTheoDoiLai = await _socialContext.TheoDoiNguoiDungs
                .Where(x => danhSachMaNguoiDung.Contains(x.MaNguoiTheoDoi) 
                         && x.MaNguoiDuocTheoDoi == maNguoiDung)
                .Select(x => x.MaNguoiTheoDoi)
                .ToListAsync();

            var result = from theoDoi in danhSachTheoDoi
                         join nguoiDung in thongTinNguoiDung on theoDoi.MaNguoiDuocTheoDoi equals nguoiDung.MaNguoiDung
                         select new NguoiDangTheoDoiResponse
                         {
                             MaNguoiDung = nguoiDung.MaNguoiDung,
                             TenDangNhap = nguoiDung.TenDangNhap,
                             HoVaTen = nguoiDung.HoVaTen,
                             HinhDaiDien = nguoiDung.HinhDaiDien,
                             TieuSu = nguoiDung.TieuSu,
                             ThoiGianTheoDoi = theoDoi.ThoiGian,
                             TheoDoiLai = danhSachTheoDoiLai.Contains(nguoiDung.MaNguoiDung)
                         };

            return result.OrderByDescending(x => x.ThoiGianTheoDoi).ToList();
        }

        public async Task<ThongKeTheoDoiResponse> LayThongKeTheoDoiAsync(Guid maNguoiDung)
        {
            var soNguoiTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .CountAsync(x => x.MaNguoiDuocTheoDoi == maNguoiDung);

            var soDangTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .CountAsync(x => x.MaNguoiTheoDoi == maNguoiDung);

            return new ThongKeTheoDoiResponse
            {
                MaNguoiDung = maNguoiDung,
                SoNguoiTheoDoi = soNguoiTheoDoi,
                SoDangTheoDoi = soDangTheoDoi
            };
        }

        public async Task<IEnumerable<GoiYTheoDoiResponse>> LayGoiYTheoDoiAsync(Guid maNguoiDung, int soLuong = 10)
        {
            // Lấy danh sách người mình đang theo dõi
            var danhSachDangTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .Where(x => x.MaNguoiTheoDoi == maNguoiDung)
                .Select(x => x.MaNguoiDuocTheoDoi)
                .ToListAsync();

            // Lấy danh sách người mà bạn bè của mình đang theo dõi
            var goiY = await _socialContext.TheoDoiNguoiDungs
                .Where(x => danhSachDangTheoDoi.Contains(x.MaNguoiTheoDoi) // Những người mình theo dõi
                         && x.MaNguoiDuocTheoDoi != maNguoiDung // Không phải bản thân
                         && !danhSachDangTheoDoi.Contains(x.MaNguoiDuocTheoDoi)) // Mình chưa theo dõi
                .GroupBy(x => x.MaNguoiDuocTheoDoi)
                .Select(g => new
                {
                    MaNguoiDung = g.Key,
                    SoNguoiTheoDoiChung = g.Count()
                })
                .OrderByDescending(x => x.SoNguoiTheoDoiChung)
                .Take(soLuong)
                .ToListAsync();

            var danhSachMaGoiY = goiY.Select(x => x.MaNguoiDung).ToList();

            // Lấy thông tin người dùng
            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => danhSachMaGoiY.Contains(x.MaNguoiDung))
                .Select(x => new
                {
                    x.MaNguoiDung,
                    x.TenDangNhap,
                    x.HoVaTen,
                    x.HinhDaiDien,
                    x.TieuSu
                })
                .ToListAsync();

            // Đếm tổng số người theo dõi của mỗi người
            var thongKeTheoDoi = await _socialContext.TheoDoiNguoiDungs
                .Where(x => danhSachMaGoiY.Contains(x.MaNguoiDuocTheoDoi))
                .GroupBy(x => x.MaNguoiDuocTheoDoi)
                .Select(g => new
                {
                    MaNguoiDung = g.Key,
                    TongSoNguoiTheoDoi = g.Count()
                })
                .ToListAsync();

            var result = from gy in goiY
                         join nguoiDung in thongTinNguoiDung on gy.MaNguoiDung equals nguoiDung.MaNguoiDung
                         join thongKe in thongKeTheoDoi on gy.MaNguoiDung equals thongKe.MaNguoiDung into thongKeGroup
                         from thongKe in thongKeGroup.DefaultIfEmpty()
                         select new GoiYTheoDoiResponse
                         {
                             MaNguoiDung = nguoiDung.MaNguoiDung,
                             TenDangNhap = nguoiDung.TenDangNhap,
                             HoVaTen = nguoiDung.HoVaTen,
                             HinhDaiDien = nguoiDung.HinhDaiDien,
                             TieuSu = nguoiDung.TieuSu,
                             SoNguoiTheoDoiChung = gy.SoNguoiTheoDoiChung,
                             TongSoNguoiTheoDoi = thongKe?.TongSoNguoiTheoDoi ?? 0
                         };

            return result.ToList();
        }

        public async Task<IEnumerable<NguoiTheoDoiResponse>> LayDanhSachBanChungAsync(
            Guid maNguoiDung1, 
            Guid maNguoiDung2)
        {
            // Lấy danh sách người mà cả hai đều theo dõi
            var danhSachTheoDoi1 = await _socialContext.TheoDoiNguoiDungs
                .Where(x => x.MaNguoiTheoDoi == maNguoiDung1)
                .Select(x => x.MaNguoiDuocTheoDoi)
                .ToListAsync();

            var danhSachTheoDoi2 = await _socialContext.TheoDoiNguoiDungs
                .Where(x => x.MaNguoiTheoDoi == maNguoiDung2)
                .Select(x => x.MaNguoiDuocTheoDoi)
                .ToListAsync();

            var banChung = danhSachTheoDoi1.Intersect(danhSachTheoDoi2).ToList();

            if (!banChung.Any())
            {
                return new List<NguoiTheoDoiResponse>();
            }

            // Lấy thông tin người dùng
            var thongTinNguoiDung = await _userContext.NguoiDungs
                .Where(x => banChung.Contains(x.MaNguoiDung))
                .Select(x => new NguoiTheoDoiResponse
                {
                    MaNguoiDung = x.MaNguoiDung,
                    TenDangNhap = x.TenDangNhap,
                    HoVaTen = x.HoVaTen,
                    HinhDaiDien = x.HinhDaiDien,
                    TieuSu = x.TieuSu,
                    ThoiGianTheoDoi = null,
                    DangTheoDoiLai = false
                })
                .ToListAsync();

            return thongTinNguoiDung;
        }
    }
}