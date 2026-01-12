using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using StudyApp.DTO.Responses.User;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class NguoiDungService(UserDbContext db, IMapper mapper) : INguoiDungService
    {
        private readonly UserDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        public LoginResult Login(DangNhapRequest request)
        {
            string username = request.TenDangNhap.Trim();

            var user = _db.NguoiDungs
                .Include(u => u.MaVaiTroNavigation)
                .FirstOrDefault(x => x.TenDangNhap == username && x.DaXoa == false);

            if (user == null) return LoginResult.UserNotFound;

            string inputHash = HashPassword(request.MatKhau);
            if (!string.Equals(user.MatKhauMaHoa, inputHash, StringComparison.Ordinal))
            {
                return LoginResult.InvalidCredentials;
            }

            return LoginResult.Success;
        }

        public RegisterResult Register(DangKyNguoiDungRequest request)
        {
            string username = request.TenDangNhap.Trim();
            string? email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();

            bool exists = _db.NguoiDungs.Any(x => x.DaXoa == false && (x.TenDangNhap == username || (email != null && x.Email == email)));

            if (exists) return RegisterResult.UsernameExists;

            NguoiDung entity = _mapper.Map<NguoiDung>(request);
            entity.TenDangNhap = username;
            entity.Email = email;
            entity.MatKhauMaHoa = HashPassword(request.MatKhau);

            string? hoVaTen = string.IsNullOrWhiteSpace(request.HoVaTen) ? null : request.HoVaTen.Trim();
            entity.HoVaTen = hoVaTen ?? "Thành Viên Mới";

            entity.MaCapDo = 1;
            entity.Vang = 100;
            entity.KimCuong = 5;
            entity.ThoiGianTao = DateTime.Now;
            entity.SoStreakHoiSinh = 1;
            entity.SoStreakFreeze = 2;

            if (entity.MaVaiTro == null)
            {
                const int preferredDefaultRoleId = 2;
                int? roleId = _db.VaiTros.Where(v => v.MaVaiTro == preferredDefaultRoleId).Select(v => (int?)v.MaVaiTro).FirstOrDefault();
                roleId ??= _db.VaiTros.OrderBy(v => v.MaVaiTro).Select(v => (int?)v.MaVaiTro).FirstOrDefault();

                if (roleId == null) return RegisterResult.Fail;
                entity.MaVaiTro = roleId;
            }

            _db.NguoiDungs.Add(entity);
            return _db.SaveChanges() > 0 ? RegisterResult.Success : RegisterResult.Fail;
        }

        public ResetPasswordResult ResetPassword(string email, string newPassword)
        {
            string normalizedEmail = email.Trim();
            var user = _db.NguoiDungs.FirstOrDefault(x => x.Email == normalizedEmail && x.DaXoa == false);

            if (user == null) return ResetPasswordResult.EmailNotFound;

            user.MatKhauMaHoa = HashPassword(newPassword);
            return _db.SaveChanges() > 0 ? ResetPasswordResult.Success : ResetPasswordResult.Fail;
        }

        public async Task<string?> GetTieuSuAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            return await _db.NguoiDungs.AsNoTracking()
                .Where(x => x.MaNguoiDung == maNguoiDung && x.DaXoa == false)
                .Select(x => x.TieuSu)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateTieuSuAsync(Guid maNguoiDung, string? tieuSu, CancellationToken cancellationToken = default)
        {
            string? normalized = string.IsNullOrWhiteSpace(tieuSu)
                ? null
                : (tieuSu.Length > 500 ? tieuSu[..500] : tieuSu.Trim());

            var user = await _db.NguoiDungs.FirstOrDefaultAsync(x => x.MaNguoiDung == maNguoiDung && x.DaXoa == false, cancellationToken);

            if (user == null) return false;
            user.TieuSu = normalized;
            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<string?> GetAvatarPathAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            return await _db.NguoiDungs.AsNoTracking()
                .Where(x => x.MaNguoiDung == maNguoiDung && x.DaXoa == false)
                .Select(x => x.HinhDaiDien)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateAvatarPathAsync(Guid maNguoiDung, string? avatarPath, CancellationToken cancellationToken = default)
        {
            string? normalized = string.IsNullOrWhiteSpace(avatarPath) ? null : avatarPath.Trim();
            var user = await _db.NguoiDungs.FirstOrDefaultAsync(x => x.MaNguoiDung == maNguoiDung && x.DaXoa == false, cancellationToken);

            if (user == null) return false;
            user.HinhDaiDien = normalized;
            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<UserStatsResponses?> GetUserStatsAsync(Guid maNguoiDung, CancellationToken cancellationToken = default)
        {
            var user = await _db.NguoiDungs.AsNoTracking()
                .FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung, cancellationToken);

            if (user == null) return null;

            int tongHoc = user.TongSoTheHoc ?? 0;
            int tongDung = user.TongSoTheDung ?? 0;

            double tyLe = tongHoc <= 0 ? 0 : (double)tongDung / tongHoc * 100;

            return new UserStatsResponses
            {
                ChuoiNgayHoc = user.ChuoiNgayHocLienTiep ?? 0,
                TongSoTheDaHoc = tongHoc,
                TongThoiGianHocPhut = user.TongThoiGianHocPhut ?? 0,
                TyLeChinhXac = tyLe
            };
        }

        public async Task<bool> AddXPAsync(Guid maNguoiDung, int amount, CancellationToken cancellationToken = default)
        {
            var user = await _db.NguoiDungs
                .Include(u => u.MaCapDoNavigation)
                .FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung, cancellationToken);

            if (user == null) return false;

            user.TongDiemXp = (user.TongDiemXp ?? 0) + amount;

            var capDo = user.MaCapDoNavigation;
            var tongDiemXp = user.TongDiemXp ?? 0;

            if (capDo != null && tongDiemXp >= capDo.MucXptoiDa)
            {
                var nextLevel = await _db.CapDos
                    .OrderBy(c => c.MucXptoiThieu)
                    .FirstOrDefaultAsync(c => c.MucXptoiThieu > tongDiemXp, cancellationToken);

                if (nextLevel != null)
                {
                    user.MaCapDo = nextLevel.MaCapDo;
                }
            }

            return await _db.SaveChangesAsync(cancellationToken) > 0;
        }

        private static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}