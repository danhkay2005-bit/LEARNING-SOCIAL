using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class VatPhamDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // Lấy tất cả
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM VatPham WHERE ConHang = 1";
            return _dbHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo ID
        public DataTable LayTheoMa(int maVatPham)
        {
            string query = "SELECT * FROM VatPham WHERE MaVatPham = @MaVatPham";
            var parameters = new[] { new SqlParameter("@MaVatPham", maVatPham) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy theo danh mục
        public DataTable LayTheoDanhMuc(int maDanhMuc)
        {
            string query = "SELECT * FROM VatPham WHERE MaDanhMuc = @MaDanhMuc AND ConHang = 1";
            var parameters = new[] { new SqlParameter("@MaDanhMuc", maDanhMuc) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Tăng số lượng bán ra
        public int TangSoLuongBan(int maVatPham)
        {
            string query = "UPDATE VatPham SET SoLuongBanRa = SoLuongBanRa + 1 WHERE MaVatPham = @MaVatPham";
            var parameters = new[] { new SqlParameter("@MaVatPham", maVatPham) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}