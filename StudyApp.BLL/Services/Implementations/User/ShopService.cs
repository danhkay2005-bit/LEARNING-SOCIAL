using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class ShopService(UserDbContext db, IMapper mapper) : IShopService
    {
        private readonly UserDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<List<VatPhamResponse>> GetShopItemsAsync(CancellationToken cancellationToken = default)
        {
            var items = await _db.VatPhams
                .AsNoTracking()
                .Where(v => v.ConHang == true)
                .Include(v => v.MaDanhMucNavigation)
                .OrderBy(v => v.Gia)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<VatPhamResponse>>(items);
        }

        public async Task<bool> BuyItemAsync(MuaVatPhamRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _db.NguoiDungs.FindAsync([request.MaNguoiDung], cancellationToken);
            var item = await _db.VatPhams.FindAsync([request.MaVatPham], cancellationToken);

            if (user == null || item == null) return false;

            bool hasGold = item.LoaiTienTe == 1 && (user.Vang ?? 0) >= item.Gia;
            bool hasDiamond = item.LoaiTienTe == 2 && (user.KimCuong ?? 0) >= item.Gia;
            if (!hasGold && !hasDiamond) return false;

            if (item.LoaiTienTe == 1) user.Vang -= item.Gia;
            else user.KimCuong -= item.Gia;

            var khoItem = await _db.KhoNguoiDungs.FirstOrDefaultAsync(
                k => k.MaNguoiDung == request.MaNguoiDung && k.MaVatPham == request.MaVatPham,
                cancellationToken);

            if (khoItem != null)
            {
                khoItem.SoLuong = (khoItem.SoLuong ?? 0) + request.SoLuong;
            }
            else
            {
                
                _db.KhoNguoiDungs.AddRange([new KhoNguoiDung
                {
                    MaNguoiDung = request.MaNguoiDung,
                    MaVatPham = request.MaVatPham,
                    SoLuong = request.SoLuong,
                    ThoiGianMua = DateTime.Now
                }]);


            }

            _db.LichSuGiaoDiches.AddRange(new LichSuGiaoDich
            {
                MaNguoiDung = request.MaNguoiDung,
                LoaiGiaoDich = "MuaVatPham",
                LoaiTien = item.LoaiTienTe == 1 ? "Vang" : "KimCuong",
                SoLuong = -item.Gia,
                SoDuTruoc = (int)(item.LoaiTienTe == 1 ? (user.Vang ?? 0) + item.Gia : (user.KimCuong ?? 0) + item.Gia),
                SoDuSau = (int)(item.LoaiTienTe == 1 ? (user.Vang ?? 0) : (user.KimCuong ?? 0)),
                MaVatPham = item.MaVatPham,
                ThoiGian = DateTime.Now
            });

            if (item.TenVatPham.Contains("Freeze")) user.SoStreakFreeze = (user.SoStreakFreeze ?? 0) + request.SoLuong;
            if (item.TenVatPham.Contains("Hồi Sinh")) user.SoStreakHoiSinh = (user.SoStreakHoiSinh ?? 0) + request.SoLuong;

            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<KhoNguoiDungResponse>> GetUserInventoryAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var items = await _db.KhoNguoiDungs
                .AsNoTracking()
                .Where(k => k.MaNguoiDung == maNguoiDung && (k.SoLuong ?? 0) > 0)
                .Include(k => k.MaVatPhamNavigation)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<KhoNguoiDungResponse>>(items);
        }

        public async Task<bool> UseItemAsync(SuDungVatPhamRequest request, CancellationToken cancellationToken = default)
        {
            var khoItem = await _db.KhoNguoiDungs.FirstOrDefaultAsync(
                k => k.MaNguoiDung == request.MaNguoiDung && k.MaVatPham == request.MaVatPham,
                cancellationToken);

            if (khoItem == null || (khoItem.SoLuong ?? 0) <= 0) return false;

            khoItem.SoLuong = (khoItem.SoLuong ?? 0) - 1;
            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}