using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Threading.Tasks;


namespace StudyApp.BLL.Services.User;

public class UserProfileService : IUserProfileService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;

    // ✅ Constructor duy nhất với DI
    public UserProfileService(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

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

    public async Task<List<NguoiDungResponse>> TimKiemNguoiDungAsync(string keyword)
    {
        // ✅ THÊM: Debug log
        System.Diagnostics.Debug.WriteLine($"🔍 TimKiemNguoiDungAsync được gọi với keyword: '{keyword}'");

        if (string.IsNullOrWhiteSpace(keyword))
        {
            System.Diagnostics.Debug.WriteLine("⚠️ Keyword rỗng, return empty list");
            return new List<NguoiDungResponse>();
        }

        var users = await _context.Set<NguoiDung>()
            .Where(u =>
                (u.HoVaTen != null && u.HoVaTen.Contains(keyword)) ||
                (u.Email != null && u.Email.Contains(keyword))
            )
            .Take(10)
            .ToListAsync();

        // ✅ THÊM: Debug log kết quả
        System.Diagnostics.Debug.WriteLine($"✅ Tìm thấy {users.Count} users");
        foreach (var u in users)
        {
            System.Diagnostics.Debug.WriteLine($"   - {u.HoVaTen} ({u.Email})");
        }

        return _mapper.Map<List<NguoiDungResponse>>(users);
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

    /// <summary>
    /// ✏️ Cập nhật Avatar
    /// </summary>
    public async Task<bool> UpdateAvatarAsync(Guid userId, string avatarUrl)
    {
        try
        {
            var user = await _context.NguoiDungs.FindAsync(userId);

            if (user == null)
                return false;

            user.HinhDaiDien = avatarUrl;

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 🔍 Lấy Avatar URL
    /// </summary>
    public async Task<string?> GetAvatarUrlAsync(Guid userId)
    {
        try
        {
            var user = await _context.NguoiDungs
                .Where(u => u.MaNguoiDung == userId)
                .Select(u => u.HinhDaiDien)
                .FirstOrDefaultAsync();

            return user;
        }
        catch
        {
            return null;
        }
    }
}