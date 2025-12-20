using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class VatPhamDAL
    {
        // Lấy tất cả
        public static DataTable LayTatCa()
        {
            string query = "SELECT * FROM VatPham WHERE ConHang = 1";
            return DatabaseHelper.ExecuteQuery(query, DatabaseType.NguoiDung);
        }

        // Lấy theo ID
        public static DataTable LayTheoMa(int maVatPham)
        {
            string query = "SELECT * FROM VatPham WHERE MaVatPham = @MaVatPham";
            var parameters = new[] { new SqlParameter("@MaVatPham", maVatPham) };
            return DatabaseHelper.ExecuteQuery(query, DatabaseType.NguoiDung, parameters);
        }

        // Lấy theo danh mục
        public static DataTable LayTheoDanhMuc(int maDanhMuc)
        {
            string query = "SELECT * FROM VatPham WHERE MaDanhMuc = @MaDanhMuc AND ConHang = 1";
            var parameters = new[] { new SqlParameter("@MaDanhMuc", maDanhMuc) };
            return DatabaseHelper.ExecuteQuery(query, DatabaseType.NguoiDung, parameters);
        }

        // Tăng số lượng bán ra
        public static int TangSoLuongBan(int maVatPham)
        {
            string query = "UPDATE VatPham SET SoLuongBanRa = SoLuongBanRa + 1 WHERE MaVatPham = @MaVatPham";
            var parameters = new[] { new SqlParameter("@MaVatPham", maVatPham) };
            return DatabaseHelper.ExecuteNonQuery(query, DatabaseType.NguoiDung, parameters);
        }
    }
}