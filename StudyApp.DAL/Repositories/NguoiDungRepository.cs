using Microsoft.Data.SqlClient;
using StudyApp.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DAL.Repositories
{
    public class NguoiDungRepository
    {
        // QUAN TRỌNG: Sửa lại chuỗi kết nối cho đúng máy bạn
        private readonly string _connStr = "Data Source=localhost;Initial Catalog=App_NguoiDung;Integrated Security=True;TrustServerCertificate=True";

        // 1. Lấy user để check đăng nhập
        public NguoiDungDTO? GetUserByUsername(string username)
        {
            NguoiDungDTO? user = null;
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @user AND DaXoa = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new NguoiDungDTO
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
            return user;
        }

        // 2. Check trùng user/email
        public bool CheckExists(string username, string? email)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM NguoiDung WHERE TenDangNhap = @user OR Email = @email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@email", email ?? string.Empty);
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
                string query = @"INSERT INTO NguoiDung (TenDangNhap, MatKhauMaHoa, Email, HoVaTen, SoDienThoai, MaVaiTro, Vang, KimCuong) 
                               VALUES (@user, @pass, @email, @name, @phone, 2, 100, 5)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", newUser.TenDangNhap);
                cmd.Parameters.AddWithValue("@pass", newUser.MatKhauMaHoa);
                cmd.Parameters.AddWithValue("@email", (object?)newUser.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@name", (object?)newUser.HoVaTen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@phone", DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
