using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.User
{
    public class ItemShopService : IItemShopService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public ItemShopService(UserDbContext context, IMapper mapper, IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        // ✅ GIỮ CHỈ MỘT BẢN GetCategoriesAsync
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
                    _context.KhoNguoiDungs.Add(new KhoNguoiDung
                    {
                        MaNguoiDung = userId,
                        MaVatPham = vatPhamId,
                        SoLuong = soLuong
                    });
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
                .Join(
                    _context.VatPhams,
                    kho => kho.MaVatPham,
                    vp => vp.MaVatPham,
                    (kho, vp) => new KhoNguoiDungResponse
                    {
                        MaKho = kho.MaKho,
                        MaNguoiDung = kho.MaNguoiDung,
                        MaVatPham = kho.MaVatPham,
                        SoLuong = kho.SoLuong ?? 0,
                        DaTrangBi = kho.DaTrangBi ?? false,
                        ThoiGianMua = kho.ThoiGianMua,
                        ThoiGianHetHan = kho.ThoiGianHetHan,
                        TenVatPham = vp.TenVatPham
                    }
                )
                .ToListAsync();

            return inventory;
        }

        public async Task<string> UseResurrectItemAsync(Guid userId, int khoVatPhamId)
        {
            // ✅ KIỂM TRA ĐIỀU KIỆN NGAY TỪ ĐẦU
            var user = await _context.NguoiDungs.FindAsync(userId);
            if (user == null)
                return "Người dùng không tồn tại!";

            // ✅ CHỈ DÙNG ĐƯỢC KHI CHUỖI BỊ ĐÓNG BĂNG
            if (!(user.IsStreakFrozen))
                return "❌ Không thể sử dụng! Chỉ có thể sử dụng khi chuỗi bị đóng băng.";

            // ✅ KIểm TRA ĐIỀU KIỆN VẬT PHẨM
            var khoItem = await _context.KhoNguoiDungs.FindAsync(khoVatPhamId);
            if (khoItem == null || khoItem.MaNguoiDung != userId)
                return "Vật phẩm không tồn tại!";

            if ((khoItem.SoLuong ?? 0) <= 0)
                return "Không đủ vật phẩm để sử dụng!";

            var item = await _context.VatPhams.FindAsync(khoItem.MaVatPham);
            if (item == null || !item.TenVatPham.Contains("Hồi Sinh"))
                return "Vật phẩm này không phải loại hồi sinh chuỗi!";

            // ✅ MỚI BẮT ĐẦU TRANSACTION SAU KHI KIỂM TRA XONG
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // ✅ GỌI SERVICE HỒI SINH CHUỖI
                using var scope = _serviceProvider.CreateScope();
                var gamificationService = scope.ServiceProvider.GetRequiredService<IGamificationService>();
                var restoreResult = await gamificationService.RestoreStreakAsync(userId);

                // ✅ GIẢM SỐ LƯỢNG
                khoItem.SoLuong = (khoItem.SoLuong ?? 1) - 1;

                // ✅ LƯU LỊCH SỬ
                _context.LichSuGiaoDiches.Add(new LichSuGiaoDich
                {
                    MaNguoiDung = userId,
                    MaVatPham = item.MaVatPham,
                    LoaiGiaoDich = LoaiGiaoDichEnum.SuDungVatPham.ToString(),
                    LoaiTien = LoaiTienGiaoDichEnum.XP.ToString(),
                    SoLuong = 1,
                    ThoiGian = DateTime.Now,
                    MoTa = $"Sử dụng {item.TenVatPham} - Hồi sinh chuỗi"
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // ✅ CẬP NHẬT USERSESSION
                if (StudyApp.DTO.UserSession.CurrentUser != null)
                {
                    var updatedUser = await _context.NguoiDungs.FindAsync(userId);
                    if (updatedUser != null)
                    {
                        StudyApp.DTO.UserSession.CurrentUser.ChuoiNgayHocLienTiep = updatedUser.ChuoiNgayHocLienTiep ?? 0;
                        StudyApp.DTO.UserSession.CurrentUser.ChuoiNgayDaiNhat = updatedUser.ChuoiNgayDaiNhat ?? 0;
                    }
                }

                return restoreResult;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                System.Diagnostics.Debug.WriteLine($"[UseResurrectItem Error] {ex.InnerException?.Message ?? ex.Message}");
                return $"Lỗi: {ex.InnerException?.Message ?? ex.Message}";
            }
        }
    }
}