using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;

namespace StudyApp.BLL.Services.User;

public class UserProfileService(UserDbContext _context, IMapper _mapper) : IUserProfileService
{
    public async Task<NguoiDungResponse?> GetProfileAsync(Guid userId)
    {
        var user = await _context.NguoiDungs
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.MaNguoiDung == userId);

        if (user == null)
        {
            return null;
        }

        return _mapper.Map<NguoiDungResponse>(user);
    }

    public async Task<bool> UpdateProfileAsync(Guid userId, CapNhatHoSoRequest request)
    {
        var user = await _context.NguoiDungs.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(request.HoVaTen))
        {
            user.HoVaTen = request.HoVaTen;
        }

        if (request.NgaySinh.HasValue)
        {
            user.NgaySinh = request.NgaySinh;
        }

        if (request.GioiTinh.HasValue)
        {
            user.GioiTinh = (byte)request.GioiTinh.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.HinhDaiDien))
        {
            user.HinhDaiDien = request.HinhDaiDien;
        }

        if (!string.IsNullOrWhiteSpace(request.TieuSu))
        {
            user.TieuSu = request.TieuSu;
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string oldPass, string newPass)
    {
        var user = await _context.NguoiDungs.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        // TODO: Verify hash
        if (user.MatKhauMaHoa != oldPass)
        {
            return false;
        }

        // TODO: Hash new password
        user.MatKhauMaHoa = newPass;

        return await _context.SaveChangesAsync() > 0;
    }
}