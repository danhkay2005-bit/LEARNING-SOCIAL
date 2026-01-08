using Microsoft.Data.SqlClient;
using StudyApp.DTO;
using System;
using System.Data;

namespace StudyApp.DAL.Repositories
{
    public class NguoiDungRepository
    {
        // QUAN TRỌNG: Sửa lại chuỗi kết nối cho đúng máy bạn
        private readonly string _connStr = "Data Source=localhost;Initial Catalog=App_NguoiDung;Integrated Security=True;TrustServerCertificate=True";

        // 1. Lấy user để check đăng nhập
        public NguoiDungDTO? GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            NguoiDungDTO? user = null;

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                const string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @user AND DaXoa = 0";
                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = username.Trim();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = MapNguoiDung(reader);
                }
            }

            return user;
        }

        // 1.1 Lấy user theo email (dùng cho Quên mật khẩu)
        public NguoiDungDTO? GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            NguoiDungDTO? user = null;

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                const string query = "SELECT * FROM NguoiDung WHERE Email = @email AND DaXoa = 0";
                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = email.Trim();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = MapNguoiDung(reader);
                }
            }

            return user;
        }

        // 2. Check trùng user/email
        public bool CheckExists(string username, string? email)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return true;
            }

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                const string query = "SELECT COUNT(1) FROM NguoiDung WHERE TenDangNhap = @user OR Email = @email";
                using SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = username.Trim();
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = (email ?? string.Empty).Trim();

                return (int)cmd.ExecuteScalar()! > 0;
            }
        }

        // 3. Thêm mới user (Đăng ký)
        public bool CreateUser(NguoiDungDTO newUser)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                // Insert các giá trị mặc định: Vang=100, KimCuong=5, MaVaiTro=2 (Member)
                const string query = @"
INSERT INTO NguoiDung (TenDangNhap, MatKhauMaHoa, Email, HoVaTen, SoDienThoai, MaVaiTro, Vang, KimCuong) 
VALUES (@user, @pass, @email, @name, @phone, 2, 100, 5)";

                using SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = newUser.TenDangNhap.Trim();
                cmd.Parameters.Add("@pass", SqlDbType.VarChar, 255).Value = newUser.MatKhauMaHoa;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = (object?)newUser.Email ?? DBNull.Value;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = (object?)newUser.HoVaTen ?? DBNull.Value;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 20).Value = DBNull.Value;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Cập nhật trạng thái online
        public void UpdateOnlineStatus(Guid maNguoiDung, bool isOnline)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                const string query = @"
UPDATE NguoiDung
SET TrangThaiOnline = @online,
    LanOnlineCuoi = GETDATE()
WHERE MaNguoiDung = @id";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@online", SqlDbType.Bit).Value = isOnline;
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = maNguoiDung;

                cmd.ExecuteNonQuery();
            }
        }

        // 5. Đổi mật khẩu theo email (đã hash sẵn)
        public bool ChangePassword(string email, string newHashedPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newHashedPassword))
            {
                return false;
            }

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                const string query = @"
UPDATE NguoiDung
SET MatKhauMaHoa = @pass
WHERE Email = @email AND DaXoa = 0";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@pass", SqlDbType.VarChar, 255).Value = newHashedPassword;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = email.Trim();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private static NguoiDungDTO MapNguoiDung(SqlDataReader reader)
        {
            return new NguoiDungDTO
            {
                MaNguoiDung = (Guid)reader["MaNguoiDung"],
                TenDangNhap = reader["TenDangNhap"]?.ToString() ?? string.Empty,
                MatKhauMaHoa = reader["MatKhauMaHoa"]?.ToString() ?? string.Empty,
                HoVaTen = reader["HoVaTen"] != DBNull.Value ? reader["HoVaTen"]?.ToString() : null,
                Email = reader["Email"] != DBNull.Value ? reader["Email"]?.ToString() : null,
                MaVaiTro = reader["MaVaiTro"] != DBNull.Value ? (int)reader["MaVaiTro"] : 3,
                Vang = reader["Vang"] != DBNull.Value ? (int)reader["Vang"] : 0,
                KimCuong = reader["KimCuong"] != DBNull.Value ? (int)reader["KimCuong"] : 0
            };
        }
    }
}
