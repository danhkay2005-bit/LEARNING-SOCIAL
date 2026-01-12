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
        var user = await _context.NguoiDungs
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.TenDangNhap == request.TenDangNhap &&
                (u.DaXoa ?? false) == false);

        if (user == null)
        {
            return (LoginResult.InvalidCredentials, null);
        }

        // TODO: Hash password check (BCrypt)
        if (user.MatKhauMaHoa != request.MatKhau)
        {
            return (LoginResult.InvalidCredentials, null);
        }

        var userDto = _mapper.Map<NguoiDungDTO>(user);
        return (LoginResult.Success, userDto);
    }

    public async Task<RegisterResult> RegisterAsync(DangKyNguoiDungRequest request)
    {
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
            ThoiGianTao = DateTime.Now
        };

        _context.NguoiDungs.Add(newUser);
        await _context.SaveChangesAsync();

        return RegisterResult.Success;
    }

    public async Task<ResetPasswordResult> ForgotPasswordAsync(string email)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return ResetPasswordResult.EmailNotFound;
        }

        return ResetPasswordResult.Success;
    }

    public async Task<ResetPasswordResult> ResetPasswordAsync(DoiMatKhauRequest request)
    {
        return ResetPasswordResult.Fail;
    }
}