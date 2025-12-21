using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class KhoNguoiDungDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // Lấy kho của người dùng
        public DataTable LayKhoNguoiDung(Guid maNguoiDung)
        {
            string query = "SELECT * FROM KhoNguoiDung WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Kiểm tra đã sở hữu vật phẩm
        public bool KiemTraSoHuu(Guid maNguoiDung, int maVatPham)
        {
            string query = "SELECT COUNT(*) FROM KhoNguoiDung WHERE MaNguoiDung = @MaNguoiDung AND MaVatPham = @MaVatPham";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaVatPham", maVatPham)
            };
            object? result = _dbHelper.ExecuteScalar(query, _dbType, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;
            return count > 0;
        }

        // Thêm vật phẩm vào kho
        public int ThemVaoKho(Guid maNguoiDung, int maVatPham, int soLuong, DateTime? thoiGianHetHan)
        {
            string query = @"INSERT INTO KhoNguoiDung (MaNguoiDung, MaVatPham, SoLuong, ThoiGianHetHan) 
                            VALUES (@MaNguoiDung, @MaVatPham, @SoLuong, @ThoiGianHetHan)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaVatPham", maVatPham),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@ThoiGianHetHan", (object?)thoiGianHetHan ?? DBNull. Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật số lượng
        public int CapNhatSoLuong(Guid maNguoiDung, int maVatPham, int soLuongThem)
        {
            string query = @"UPDATE KhoNguoiDung 
                            SET SoLuong = SoLuong + @SoLuong 
                            WHERE MaNguoiDung = @MaNguoiDung AND MaVatPham = @MaVatPham";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaVatPham", maVatPham),
                new SqlParameter("@SoLuong", soLuongThem)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Trang bị vật phẩm
        public int TrangBi(Guid maNguoiDung, int maVatPham, bool trangBi)
        {
            string query = "UPDATE KhoNguoiDung SET DaTrangBi = @DaTrangBi WHERE MaNguoiDung = @MaNguoiDung AND MaVatPham = @MaVatPham";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaVatPham", maVatPham),
                new SqlParameter("@DaTrangBi", trangBi)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}