using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class VaiTroDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();   
        // Lấy tất cả
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM VaiTro";
            return _dbHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo ID
        public DataTable LayTheoMa(int maVaiTro)
        {
            string query = "SELECT * FROM VaiTro WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[] { new SqlParameter("@MaVaiTro", maVaiTro) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Thêm
        public int Them(string tenVaiTro, string moTa)
        {
            string query = "INSERT INTO VaiTro (TenVaiTro, MoTa) VALUES (@TenVaiTro, @MoTa)";
            var parameters = new[]
            {
                new SqlParameter("@TenVaiTro", tenVaiTro),
                new SqlParameter("@MoTa", (object)moTa ??  DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Sửa
        public int Sua(int maVaiTro, string tenVaiTro, string moTa)
        {
            string query = "UPDATE VaiTro SET TenVaiTro = @TenVaiTro, MoTa = @MoTa WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[]
            {
                new SqlParameter("@MaVaiTro", maVaiTro),
                new SqlParameter("@TenVaiTro", tenVaiTro),
                new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Xóa
        public int Xoa(int maVaiTro)
        {
            string query = "DELETE FROM VaiTro WHERE MaVaiTro = @MaVaiTro";
            var parameters = new[] { new SqlParameter("@MaVaiTro", maVaiTro) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}