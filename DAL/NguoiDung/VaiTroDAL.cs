using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class VaiTroDAL
    {
        // Lấy tất cả
        public static DataTable LayTatCa()
        {
            string query = "SELECT * FROM VaiTro";
            return DatabaseHelper.ExecuteQuery(query, DatabaseType.NguoiDung);
        }

        // Lấy theo ID
        public static DataTable LayTheoMa(int maVaiTro)
        {
            string query = "SELECT * FROM VaiTro WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[] { new SqlParameter("@MaVaiTro", maVaiTro) };
            return DatabaseHelper.ExecuteQuery(query, DatabaseType.NguoiDung, parameters);
        }

        // Thêm
        public static int Them(string tenVaiTro, string moTa)
        {
            string query = "INSERT INTO VaiTro (TenVaiTro, MoTa) VALUES (@TenVaiTro, @MoTa)";
            var parameters = new[]
            {
                new SqlParameter("@TenVaiTro", tenVaiTro),
                new SqlParameter("@MoTa", (object)moTa ??  DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, DatabaseType.NguoiDung, parameters);
        }

        // Sửa
        public static int Sua(int maVaiTro, string tenVaiTro, string moTa)
        {
            string query = "UPDATE VaiTro SET TenVaiTro = @TenVaiTro, MoTa = @MoTa WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[]
            {
                new SqlParameter("@MaVaiTro", maVaiTro),
                new SqlParameter("@TenVaiTro", tenVaiTro),
                new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, DatabaseType.NguoiDung, parameters);
        }

        // Xóa
        public static int Xoa(int maVaiTro)
        {
            string query = "DELETE FROM VaiTro WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[] { new SqlParameter("@MaVaiTro", maVaiTro) };
            return DatabaseHelper.ExecuteNonQuery(query, DatabaseType.NguoiDung, parameters);
        }
    }
}