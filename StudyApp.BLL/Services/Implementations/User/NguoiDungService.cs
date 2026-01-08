using System.Security.Cryptography;
using System.Text;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DAL.Repositories;
using StudyApp.DTO;
using StudyApp.DTO.Requests.NguoiDung;

namespace StudyApp.BLL.Services.Implementations.User
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly NguoiDungRepository _repo;

        public NguoiDungService()
        {
            _repo = new NguoiDungRepository();
        }

        public LoginResult Login(DangNhapRequest request)
        {
            var user = _repo.GetUserByUsername(request.TenDangNhap);
            if (user == null) return LoginResult.UserNotFound;

            string inputHash = HashPassword(request.MatKhau);

            if (user.MatKhauMaHoa == inputHash)
            {
                UserSession.CurrentUser = user;
                return LoginResult.Success;
            }

            return LoginResult.InvalidCredentials;
        }

        public RegisterResult Register(DangKyNguoiDungRequest request)
        {
            if (_repo.CheckExists(request.TenDangNhap, request.Email))
            {
                return RegisterResult.UsernameExists;
            }

            NguoiDungDTO newUser = new NguoiDungDTO
            {
                TenDangNhap = request.TenDangNhap,
                MatKhauMaHoa = HashPassword(request.MatKhau),
                HoVaTen = request.HoVaTen,
                Email = request.Email
            };

            return _repo.CreateUser(newUser) ? RegisterResult.Success : RegisterResult.Fail;
        }

        private string HashPassword(string password)
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