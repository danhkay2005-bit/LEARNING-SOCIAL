using AutoMapper;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.NguoiDung;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using StudyApp.DTO.Enums;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly UserDbContext _db;
        private readonly IMapper _mapper;

        public NguoiDungService(UserDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public LoginResult Login(DangNhapRequest request)
        {
            string username = request.TenDangNhap.Trim();

            var user = _db.NguoiDungs.FirstOrDefault(x => x.TenDangNhap == username && x.DaXoa == false);
            if (user == null)
            {
                return LoginResult.UserNotFound;
            }

            string inputHash = HashPassword(request.MatKhau);
            if (!string.Equals(user.MatKhauMaHoa, inputHash, StringComparison.Ordinal))
            {
                return LoginResult.InvalidCredentials;
            }

            UserSession.Login(new NguoiDungDTO
            {
                MaNguoiDung = user.MaNguoiDung,
                TenDangNhap = user.TenDangNhap,
                HoVaTen = user.HoVaTen,
                Email = user.Email,
                SoDienThoai = user.SoDienThoai,
                MaVaiTro = user.MaVaiTro ?? (int)VaiTroEnum.Member,
                Vang = user.Vang ?? 0,
                KimCuong = user.KimCuong ?? 0
            });

            return LoginResult.Success;
        }

        public RegisterResult Register(DangKyNguoiDungRequest request)
        {
            string username = request.TenDangNhap.Trim();
            string? email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();

            bool exists = _db.NguoiDungs.Any(x =>
                x.DaXoa == false &&
                (x.TenDangNhap == username || (email != null && x.Email == email)));

            if (exists)
            {
                return RegisterResult.UsernameExists;
            }

            // Map Request -> Entity (theo NguoiDungMapping)
            NguoiDung entity = _mapper.Map<NguoiDung>(request);

            // Normalize + hash (SHA256)
            entity.TenDangNhap = username;
            entity.Email = email;
            entity.MatKhauMaHoa = HashPassword(request.MatKhau);

            // Ensure MaVaiTro is valid (avoid FK violation when DB isn't seeded with expected default)
            if (entity.MaVaiTro == null)
            {
                const int preferredDefaultRoleId = 2;

                int? roleId = _db.VaiTros
                    .Where(v => v.MaVaiTro == preferredDefaultRoleId)
                    .Select(v => (int?)v.MaVaiTro)
                    .FirstOrDefault();

                roleId ??= _db.VaiTros
                    .OrderBy(v => v.MaVaiTro)
                    .Select(v => (int?)v.MaVaiTro)
                    .FirstOrDefault();

                if (roleId == null)
                {
                    return RegisterResult.Fail;
                }

                entity.MaVaiTro = roleId;
            }

            // MaCapDo / Vang / KimCuong sẽ lấy default từ DB (DbContext mapping đã HasDefaultValue)
            _db.NguoiDungs.Add(entity);

            return _db.SaveChanges() > 0 ? RegisterResult.Success : RegisterResult.Fail;
        }

        public ResetPasswordResult ResetPassword(string email, string newPassword)
        {
            string normalizedEmail = email.Trim();

            var user = _db.NguoiDungs.FirstOrDefault(x => x.Email == normalizedEmail && x.DaXoa == false);
            if (user == null)
            {
                return ResetPasswordResult.EmailNotFound;
            }

            user.MatKhauMaHoa = HashPassword(newPassword);

            return _db.SaveChanges() > 0 ? ResetPasswordResult.Success : ResetPasswordResult.Fail;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}