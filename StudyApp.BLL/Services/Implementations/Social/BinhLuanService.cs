using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Social;
using StudyApp.DTO;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Implementations.Social
{
    public class BinhLuanService : IBinhLuanService
    {
        private readonly IMapper _mapper;
        private readonly SocialDbContext _db;

        public BinhLuanService(
            IMapper mapper,
            SocialDbContext dbContext 
        )
        {
            _mapper = mapper;
            _db = dbContext;
        }

        // =====================================================
        // ================= GET COMMENTS ======================
        // =====================================================
        public DanhSachBinhLuanResponse LayDanhSachBinhLuan(LayBinhLuanRequest request)
        {
            var query = _db.Set<BinhLuanBaiDang>()
                .AsNoTracking()
                .Where(x =>
                    x.MaBaiDang == request.MaBaiDang &&
                    x.DaXoa != true &&
                    x.MaBinhLuanCha == request.MaBinhLuanCha);

            var total = query.Count();

            var binhLuans = query
                .OrderBy(x => x.ThoiGianTao)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(x => x.InverseMaBinhLuanChaNavigation)
                .Include(x => x.ThichBinhLuans)
                .ToList();

            return new DanhSachBinhLuanResponse
            {
                BinhLuans = binhLuans.Select(MapUserContext).ToList(),
                TongSo = total,
                TrangHienTai = request.PageNumber,
                TongSoTrang = (int)Math.Ceiling(total / (double)request.PageSize),
                CoTrangTiep = request.PageNumber * request.PageSize < total,
                CoTrangTruoc = request.PageNumber > 1
            };
        }

        // =====================================================
        // ================= CREATE ============================
        // =====================================================
        public TaoBinhLuanResponse TaoBinhLuan(TaoBinhLuanRequest request)
        {
            if (!UserSession.IsLoggedIn)
                throw new Exception("Chưa đăng nhập");

            var baiDangTonTai = _db.Set<BaiDang>()
                .Any(x => x.MaBaiDang == request.MaBaiDang && x.DaXoa != true);

            if (!baiDangTonTai)
                throw new Exception("Bài đăng không tồn tại");

            var entity = _mapper.Map<BinhLuanBaiDang>(request);
            entity.MaNguoiDung = UserSession.CurrentUser!.MaNguoiDung;

            _db.Add(entity);

            // tăng counter bình luận
            var baiDang = _db.Set<BaiDang>()
                .First(x => x.MaBaiDang == request.MaBaiDang);

            baiDang.SoBinhLuan = (baiDang.SoBinhLuan ?? 0) + 1;

            _db.SaveChanges();

            return new TaoBinhLuanResponse
            {
                ThanhCong = true,
                BinhLuan = MapUserContext(entity)
            };
        }

        // =====================================================
        // ================= UPDATE ============================
        // =====================================================
        public void CapNhatBinhLuan(CapNhatBinhLuanRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var entity = _db.Set<BinhLuanBaiDang>()
                .FirstOrDefault(x =>
                    x.MaBinhLuan == request.MaBinhLuan &&
                    x.MaNguoiDung == userId &&
                    x.DaXoa != true)
                ?? throw new Exception("Không có quyền sửa bình luận");

            _mapper.Map(request, entity);
            _db.SaveChanges();
        }

        // =====================================================
        // ================= DELETE ============================
        // =====================================================
        public void XoaBinhLuan(XoaBinhLuanRequest request)
        {
            var userId = UserSession.CurrentUser?.MaNguoiDung
                ?? throw new Exception("Chưa đăng nhập");

            var entity = _db.Set<BinhLuanBaiDang>()
                .FirstOrDefault(x =>
                    x.MaBinhLuan == request.MaBinhLuan &&
                    x.MaNguoiDung == userId)
                ?? throw new Exception("Không có quyền xóa");

            entity.DaXoa = true;

            // giảm counter
            var baiDang = _db.Set<BaiDang>()
                .First(x => x.MaBaiDang == entity.MaBaiDang);

            baiDang.SoBinhLuan = Math.Max(0, (baiDang.SoBinhLuan ?? 1) - 1);

            _db.SaveChanges();
        }

        // =====================================================
        // ================= PRIVATE ===========================
        // =====================================================
        private BinhLuanResponse MapUserContext(BinhLuanBaiDang entity)
        {
            var response = _mapper.Map<BinhLuanResponse>(entity);

            if (UserSession.IsLoggedIn)
            {
                response.DaThich = entity.ThichBinhLuans
                    .Any(x => x.MaNguoiDung == UserSession.CurrentUser!.MaNguoiDung);
            }

            return response;
        }
    }
}
