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

            // Mã hóa pass nhập vào để so sánh
            string inputHash = HashPassword(request.MatKhau);

            if (user.MatKhauMaHoa == inputHash)
            {
                UserSession.CurrentUser = user; // QUAN TRỌNG: Lưu Session ở đây
                return LoginResult.Success;
            }

            return LoginResult.InvalidCredentials;
        }

        public RegisterResult Register(DangKyNguoiDungRequest request)
        {
            // Check trùng
            if (_repo.CheckExists(request.TenDangNhap, request.Email))
            {
                return RegisterResult.UsernameExists;
            }

            // Chuyển đổi Request (Input) -> DTO (Database)
            NguoiDungDTO newUser = new NguoiDungDTO
            {
                TenDangNhap = request.TenDangNhap,
                MatKhauMaHoa = HashPassword(request.MatKhau), // Mã hóa pass
                HoVaTen = request.HoVaTen,
                Email = request.Email
            };

            return _repo.CreateUser(newUser) ? RegisterResult.Success : RegisterResult.Fail;
        }

        private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(password);
                byte[] hash = md5.ComputeHash(input);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));
                return sb.ToString();
            }
        }
    }
}