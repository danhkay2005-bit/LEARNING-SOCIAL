using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Social
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly SocialDbContext _context;
        private readonly IMapper _mapper;

        public ThongBaoService(SocialDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PhanTrangThongBaoResponse> GetDanhSachThongBaoAsync(DanhSachThongBaoRequest request)
        {
            var query = _context.ThongBaos
                .Where(x => x.MaNguoiNhan == request.MaNguoiNhan);

            // Lọc theo trạng thái đọc
            if (request.ChiChuaDoc.HasValue && request.ChiChuaDoc.Value)
            {
                query = query.Where(x => !x.DaDoc);
            }

            // Lọc theo loại thông báo
            if (request.LoaiThongBao.HasValue)
            {
                query = query.Where(x => x.LoaiThongBao == (int)request.LoaiThongBao.Value);
            }

            // Đếm tổng số
            var tongSoDong = await query.CountAsync();

            // Phân trang
            var danhSach = await query
                .OrderByDescending(x => x.ThoiGian)
                .Skip((request.Trang - 1) * request.KichThuocTrang)
                .Take(request.KichThuocTrang)
                .ToListAsync();

            var result = _mapper.Map<List<ThongBaoResponse>>(danhSach);

            // Thêm thời gian tương đối
            foreach (var item in result)
            {
                item.ThoiGianTuongDoi = TinhThoiGianTuongDoi(item.ThoiGian);
            }

            return new PhanTrangThongBaoResponse
            {
                TrangHienTai = request.Trang,
                KichThuocTrang = request.KichThuocTrang,
                TongSoDong = tongSoDong,
                TongSoTrang = (int)Math.Ceiling((double)tongSoDong / request.KichThuocTrang),
                DanhSach = result
            };
        }

        public async Task<ThongBaoResponse?> GetThongBaoByIdAsync(int maThongBao)
        {
            var entity = await _context.ThongBaos.FindAsync(maThongBao);
            
            if (entity == null)
            {
                return null;
            }

            var response = _mapper.Map<ThongBaoResponse>(entity);
            response.ThoiGianTuongDoi = TinhThoiGianTuongDoi(response.ThoiGian);

            return response;
        }

        public async Task<ThongKeThongBaoResponse> GetThongKeThongBaoAsync(Guid maNguoiNhan)
        {
            var thongKe = await _context.ThongBaos
                .Where(x => x.MaNguoiNhan == maNguoiNhan)
                .GroupBy(x => 1)
                .Select(g => new ThongKeThongBaoResponse
                {
                    TongSoThongBao = g.Count(),
                    SoThongBaoChuaDoc = g.Count(x => !x.DaDoc),
                    SoThongBaoDaDoc = g.Count(x => x.DaDoc),
                    ThongBaoMoiNhat = g.Max(x => x.ThoiGian)
                })
                .FirstOrDefaultAsync();

            return thongKe ?? new ThongKeThongBaoResponse
            {
                TongSoThongBao = 0,
                SoThongBaoChuaDoc = 0,
                SoThongBaoDaDoc = 0,
                ThongBaoMoiNhat = null
            };
        }

        public async Task<IEnumerable<ThongBaoSummaryResponse>> GetThongBaoChuaDocAsync(Guid maNguoiNhan, int soLuong = 10)
        {
            var list = await _context.ThongBaos
                .Where(x => x.MaNguoiNhan == maNguoiNhan && !x.DaDoc)
                .OrderByDescending(x => x.ThoiGian)
                .Take(soLuong)
                .ToListAsync();

            var result = _mapper.Map<List<ThongBaoSummaryResponse>>(list);

            foreach (var item in result)
            {
                item.ThoiGianTuongDoi = TinhThoiGianTuongDoi(item.ThoiGian);
            }

            return result;
        }

        public async Task<ThongBaoResponse> TaoThongBaoAsync(TaoThongBaoRequest request)
        {
            var entity = new ThongBao
            {
                MaNguoiNhan = request.MaNguoiNhan,
                LoaiThongBao = (int)request.LoaiThongBao,
                NoiDung = request.NoiDung,
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBaiDang = request.MaBaiDang,
                MaBinhLuan = request.MaBinhLuan,
                MaThachDau = request.MaThachDau,
                MaNguoiGayRa = request.MaNguoiGayRa
            };

            _context.ThongBaos.Add(entity);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<ThongBaoResponse>(entity);
            response.ThoiGianTuongDoi = TinhThoiGianTuongDoi(response.ThoiGian);

            return response;
        }

        public async Task<bool> DanhDauDaDocAsync(int maThongBao)
        {
            var entity = await _context.ThongBaos.FindAsync(maThongBao);

            if (entity == null)
            {
                return false;
            }

            entity.DaDoc = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> DanhDauTatCaDaDocAsync(Guid maNguoiNhan)
        {
            var list = await _context.ThongBaos
                .Where(x => x.MaNguoiNhan == maNguoiNhan && !x.DaDoc)
                .ToListAsync();

            foreach (var item in list)
            {
                item.DaDoc = true;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> XoaThongBaoAsync(int maThongBao, Guid maNguoiNhan)
        {
            var entity = await _context.ThongBaos
                .FirstOrDefaultAsync(x => x.MaThongBao == maThongBao && x.MaNguoiNhan == maNguoiNhan);

            if (entity == null)
            {
                return false;
            }

            _context.ThongBaos.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> XoaTatCaThongBaoDaDocAsync(Guid maNguoiNhan)
        {
            var list = await _context.ThongBaos
                .Where(x => x.MaNguoiNhan == maNguoiNhan && x.DaDoc)
                .ToListAsync();

            _context.ThongBaos.RemoveRange(list);

            return await _context.SaveChangesAsync();
        }

        // ==========================================
        // CÁC HÀM TẠO THÔNG BÁO TỰ ĐỘNG
        // ==========================================

        public async Task TaoThongBaoReactionBaiDangAsync(int maBaiDang, Guid nguoiReaction, Guid nguoiNhan)
        {
            // Không tạo thông báo nếu tự react bài đăng của mình
            if (nguoiReaction == nguoiNhan)
            {
                return;
            }

            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiNhan,
                LoaiThongBao = (int)LoaiThongBaoEnum.ThichBaiDang,
                NoiDung = "đã thích bài đăng của bạn",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBaiDang = maBaiDang,
                MaNguoiGayRa = nguoiReaction
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task TaoThongBaoBinhLuanAsync(int maBaiDang, int maBinhLuan, Guid nguoiBinhLuan, Guid nguoiNhan)
        {
            // Không tạo thông báo nếu tự bình luận bài đăng của mình
            if (nguoiBinhLuan == nguoiNhan)
            {
                return;
            }

            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiNhan,
                LoaiThongBao = (int)LoaiThongBaoEnum.BinhLuanBaiDang,
                NoiDung = "đã bình luận bài đăng của bạn",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBaiDang = maBaiDang,
                MaBinhLuan = maBinhLuan,
                MaNguoiGayRa = nguoiBinhLuan
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task TaoThongBaoChiaSeBaiDangAsync(int maBaiDang, Guid nguoiChiaSe, Guid nguoiNhan)
        {
            // Không tạo thông báo nếu tự chia sẻ bài đăng của mình
            if (nguoiChiaSe == nguoiNhan)
            {
                return;
            }

            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiNhan,
                LoaiThongBao = (int)LoaiThongBaoEnum.ChiaSeBaiDang,
                NoiDung = "đã chia sẻ bài đăng của bạn",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBaiDang = maBaiDang,
                MaNguoiGayRa = nguoiChiaSe
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task TaoThongBaoTheoDoiAsync(Guid nguoiTheoDoi, Guid nguoiDuocTheoDoi)
        {
            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiDuocTheoDoi,
                LoaiThongBao = (int)LoaiThongBaoEnum.TheoDoi,
                NoiDung = "đã bắt đầu theo dõi bạn",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaNguoiGayRa = nguoiTheoDoi
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task TaoThongBaoMentionBaiDangAsync(int maBaiDang, Guid nguoiMention, Guid nguoiDuocMention)
        {
            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiDuocMention,
                LoaiThongBao = (int)LoaiThongBaoEnum.MentionBaiDang,
                NoiDung = "đã nhắc đến bạn trong một bài đăng",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBaiDang = maBaiDang,
                MaNguoiGayRa = nguoiMention
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        public async Task TaoThongBaoMentionBinhLuanAsync(int maBinhLuan, Guid nguoiMention, Guid nguoiDuocMention)
        {
            var thongBao = new ThongBao
            {
                MaNguoiNhan = nguoiDuocMention,
                LoaiThongBao = (int)LoaiThongBaoEnum.MentionBinhLuan,
                NoiDung = "đã nhắc đến bạn trong một bình luận",
                DaDoc = false,
                ThoiGian = DateTime.Now,
                MaBinhLuan = maBinhLuan,
                MaNguoiGayRa = nguoiMention
            };

            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        // ==========================================
        // HÀM HỖ TRỢ
        // ==========================================

        private string TinhThoiGianTuongDoi(DateTime thoiGian)
        {
            var khoangCach = DateTime.Now - thoiGian;

            if (khoangCach.TotalMinutes < 1)
            {
                return "Vừa xong";
            }
            else if (khoangCach.TotalMinutes < 60)
            {
                return $"{(int)khoangCach.TotalMinutes} phút trước";
            }
            else if (khoangCach.TotalHours < 24)
            {
                return $"{(int)khoangCach.TotalHours} giờ trước";
            }
            else if (khoangCach.TotalDays < 7)
            {
                return $"{(int)khoangCach.TotalDays} ngày trước";
            }
            else if (khoangCach.TotalDays < 30)
            {
                return $"{(int)(khoangCach.TotalDays / 7)} tuần trước";
            }
            else if (khoangCach.TotalDays < 365)
            {
                return $"{(int)(khoangCach.TotalDays / 30)} tháng trước";
            }
            else
            {
                return $"{(int)(khoangCach.TotalDays / 365)} năm trước";
            }
        }
    }
}