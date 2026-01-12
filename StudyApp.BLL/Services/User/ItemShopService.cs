using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Services.User;

public class ItemShopService(UserDbContext _context, IMapper _mapper) : IItemShopService
{
    public async Task<List<DanhMucSanPhamResponse>> GetCategoriesAsync()
    {
        var categories = await _context.DanhMucSanPhams
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<DanhMucSanPhamResponse>>(categories);
    }

    public async Task<List<VatPhamResponse>> GetShopItemsAsync()
    {
        var items = await _context.VatPhams
            .AsNoTracking()
            .Where(vp => vp.ConHang ?? true)
            .ToListAsync();

        return _mapper.Map<List<VatPhamResponse>>(items);
    }

    public async Task<string> BuyItemAsync(Guid userId, int vatPhamId, int soLuong)
    {
        if (soLuong <= 0)
        {
            return "Số lượng không hợp lệ.";
        }

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = await _context.NguoiDungs.FindAsync(userId);
            var item = await _context.VatPhams.FindAsync(vatPhamId);

            if (user == null || item == null)
            {
                return "Lỗi dữ liệu.";
            }

            if ((item.ConHang ?? true) == false)
            {
                return "Vật phẩm đã hết hàng.";
            }

            var tongTien = item.Gia * soLuong;
            var soDuTruoc = 0;
            var soDuSau = 0;
            var loaiTien = LoaiTienGiaoDichEnum.Vang;

            if (item.LoaiTienTe == (int)LoaiTienTeEnum.Vang)
            {
                soDuTruoc = user.Vang ?? 0;
                if (soDuTruoc < tongTien)
                {
                    return "Không đủ vàng!";
                }

                soDuSau = soDuTruoc - tongTien;
                user.Vang = soDuSau;
                loaiTien = LoaiTienGiaoDichEnum.Vang;
            }
            else if (item.LoaiTienTe == (int)LoaiTienTeEnum.KimCuong)
            {
                soDuTruoc = user.KimCuong ?? 0;
                if (soDuTruoc < tongTien)
                {
                    return "Không đủ kim cương!";
                }

                soDuSau = soDuTruoc - tongTien;
                user.KimCuong = soDuSau;
                loaiTien = LoaiTienGiaoDichEnum.KimCuong;
            }
            else
            {
                return "Loại tiền tệ không hợp lệ.";
            }

            var khoItem = await _context.KhoNguoiDungs
                .FirstOrDefaultAsync(k => k.MaNguoiDung == userId && k.MaVatPham == vatPhamId);

            if (khoItem != null)
            {
                khoItem.SoLuong = (khoItem.SoLuong ?? 0) + soLuong;
            }
            else
            {
                var request = new MuaVatPhamRequest
                {
                    MaNguoiDung = userId,
                    MaVatPham = vatPhamId,
                    SoLuong = soLuong
                };

                var entity = _mapper.Map<KhoNguoiDung>(request);
                _context.KhoNguoiDungs.Add(entity);
            }

            _context.LichSuGiaoDiches.Add(new LichSuGiaoDich
            {
                MaNguoiDung = userId,
                MaVatPham = vatPhamId,
                LoaiGiaoDich = LoaiGiaoDichEnum.MuaVatPham.ToString(),
                LoaiTien = loaiTien.ToString(),
                SoLuong = tongTien,
                SoDuTruoc = soDuTruoc,
                SoDuSau = soDuSau,
                ThoiGian = DateTime.Now,
                MoTa = "Mua vật phẩm"
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return "Mua thành công!";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return $"Lỗi: {ex.Message}";
        }
    }

    public async Task<List<KhoNguoiDungResponse>> GetMyInventoryAsync(Guid userId)
    {
        var inventory = await _context.KhoNguoiDungs
            .AsNoTracking()
            .Where(k => k.MaNguoiDung == userId && (k.SoLuong ?? 0) > 0)
            .ToListAsync();

        return _mapper.Map<List<KhoNguoiDungResponse>>(inventory);
    }
}