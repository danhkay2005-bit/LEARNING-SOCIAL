using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using System;

namespace StudyApp.BLL.Services.User;

public class AuthService(UserDbContext _context, IMapper _mapper) : IAuthService
{
    public async Task<(LoginResult Result, NguoiDungDTO? User)> LoginAsync(DangNhapRequest request)
    {
        /*var user = await _context.NguoiDungs
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.TenDangNhap == request.TenDangNhap &&
                (u.DaXoa ?? false) == false);

        if (user == null)
        {
            return (LoginResult.UserNotFound, null);
        }

        // TODO: Hash password check (BCrypt)
        if (user.MatKhauMaHoa != request.MatKhau)
        {
            return (LoginResult.InvalidCredentials, null);
        }

        var userDto = _mapper.Map<NguoiDungDTO>(user);
        return (LoginResult.Success, userDto);*/
        var user = await _context.NguoiDungs
        .AsNoTracking()
        .FirstOrDefaultAsync(u =>
            u.TenDangNhap == request.TenDangNhap &&
            (u.DaXoa ?? false) == false);

        if (user == null)
        {
            return (LoginResult.UserNotFound, null);
        }

        // --- THAY ĐỔI Ở ĐÂY ---
        // 1. Mã hóa cái mật khẩu người dùng vừa nhập vào
        string inputHash = StudyApp.BLL.Helpers.SecurityHelper.HashPassword(request.MatKhau) ?? string.Empty;

        // 2. So sánh 2 chuỗi đã mã hóa với nhau
        if (user.MatKhauMaHoa != inputHash)
        {
            return (LoginResult.InvalidCredentials, null);
        }
        // ----------------------

        var userDto = _mapper.Map<NguoiDungDTO>(user);
        return (LoginResult.Success, userDto);
    }

    public async Task<RegisterResult> RegisterAsync(DangKyNguoiDungRequest request)
    {
        /*if (await _context.NguoiDungs.AnyAsync(u => u.TenDangNhap == request.TenDangNhap))
        {
            return RegisterResult.UsernameExists;
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == request.Email))
            {
                return RegisterResult.EmailExists;
            }
        }

        var newUser = new DAL.Entities.User.NguoiDung
        {
            MaNguoiDung = Guid.NewGuid(),
            TenDangNhap = request.TenDangNhap,
            MatKhauMaHoa = request.MatKhau, // TODO: Hash
            Email = request.Email,
            SoDienThoai = request.SoDienThoai,
            HoVaTen = request.HoVaTen,
            NgaySinh = request.NgaySinh,
            GioiTinh = request.GioiTinh.HasValue ? (byte?)request.GioiTinh.Value : null,
            MaVaiTro = 2,
            MaCapDo = 1,
            Vang = 100,
            KimCuong = 5,
            ChuoiNgayHocLienTiep = 0,
            SoStreakFreeze = 2,
            ThoiGianTao = DateTime.Now,
            DaXoa = false
        };

        _context.NguoiDungs.Add(newUser);
        await _context.SaveChangesAsync();

        return RegisterResult.Success;*/
        if (await _context.NguoiDungs.AnyAsync(u => u.TenDangNhap == request.TenDangNhap))
        {
            return RegisterResult.UsernameExists;
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == request.Email))
            {
                return RegisterResult.EmailExists;
            }
        }

        // --- THAY ĐỔI Ở ĐÂY ---
        // Mã hóa mật khẩu trước khi tạo đối tượng
        string passwordHash = StudyApp.BLL.Helpers.SecurityHelper.HashPassword(request.MatKhau) ?? string.Empty;

        var newUser = new DAL.Entities.User.NguoiDung
        {
            MaNguoiDung = Guid.NewGuid(),
            TenDangNhap = request.TenDangNhap,

            MatKhauMaHoa = passwordHash, // <--- Lưu mật khẩu đã mã hóa

            Email = request.Email,
            SoDienThoai = request.SoDienThoai,
            HoVaTen = request.HoVaTen,
            NgaySinh = request.NgaySinh,
            GioiTinh = request.GioiTinh.HasValue ? (byte?)request.GioiTinh.Value : null,
            MaVaiTro = 2,
            MaCapDo = 1,
            Vang = 100,
            KimCuong = 5,
            ChuoiNgayHocLienTiep = 0,
            SoStreakFreeze = 2,
            ThoiGianTao = DateTime.Now,
            DaXoa = false
        };
        // ----------------------

        _context.NguoiDungs.Add(newUser);
        await _context.SaveChangesAsync();

        return RegisterResult.Success;
    }

    public async Task<ResetPasswordResult> ResetPasswordAsync(string email, string newPassword)
    {
        /* var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
         if (user == null)
         {
             return ResetPasswordResult.EmailNotFound;
         }

         // TODO: Hash new password
         user.MatKhauMaHoa = newPassword;

         await _context.SaveChangesAsync();
         return ResetPasswordResult.Success;*/
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return ResetPasswordResult.EmailNotFound;
        }

        // --- THAY ĐỔI Ở ĐÂY ---
        // Mã hóa mật khẩu mới
        user.MatKhauMaHoa = StudyApp.BLL.Helpers.SecurityHelper.HashPassword(newPassword) ?? string.Empty;
        // ----------------------

        await _context.SaveChangesAsync();
        return ResetPasswordResult.Success;
    }

    public async Task<string?> GetTieuSuAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
    {
        return await _context.NguoiDungs
            .AsNoTracking()
            .Where(x => x.MaNguoiDung == maNguoiDung)
            .Select(x => x.TieuSu)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateTieuSuAsync(Guid maNguoiDung, string? tieuSu, CancellationToken cancellationToken = default)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.MaNguoiDung == maNguoiDung, cancellationToken);
        if (user == null)
        {
            return false;
        }

        user.TieuSu = tieuSu;
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<string?> GetAvatarPathAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
    {
        return await _context.NguoiDungs
            .AsNoTracking()
            .Where(x => x.MaNguoiDung == maNguoiDung)
            .Select(x => x.HinhDaiDien)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateAvatarPathAsync(Guid maNguoiDung, string? avatarPath, CancellationToken cancellationToken = default)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.MaNguoiDung == maNguoiDung, cancellationToken);
        if (user == null)
        {
            return false;
        }

        user.HinhDaiDien = avatarPath;
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}