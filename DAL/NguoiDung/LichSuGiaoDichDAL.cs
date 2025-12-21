using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class LichSuGiaoDichDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // Lấy lịch sử của người dùng
        public DataTable LayTheoNguoiDung(Guid maNguoiDung, int soLuong)
        {
            string query = $"SELECT TOP {soLuong} * FROM LichSuGiaoDich WHERE MaNguoiDung = @MaNguoiDung ORDER BY ThoiGian DESC";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Thêm giao dịch
        public int Them(Guid maNguoiDung, string loaiGiaoDich, string loaiTien, int soLuong, int soDuTruoc, int soDuSau, string moTa, int? maVatPham, Guid? maNguoiLienQuan)
        {
            string query = @"INSERT INTO LichSuGiaoDich 
                            (MaNguoiDung, LoaiGiaoDich, LoaiTien, SoLuong, SoDuTruoc, SoDuSau, MoTa, MaVatPham, MaNguoiLienQuan) 
                            VALUES (@MaNguoiDung, @LoaiGiaoDich, @LoaiTien, @SoLuong, @SoDuTruoc, @SoDuSau, @MoTa, @MaVatPham, @MaNguoiLienQuan)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiGiaoDich", loaiGiaoDich),
                new SqlParameter("@LoaiTien", loaiTien),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@SoDuTruoc", soDuTruoc),
                new SqlParameter("@SoDuSau", soDuSau),
                new SqlParameter("@MoTa", (object?)moTa ?? DBNull.Value),
                new SqlParameter("@MaVatPham", (object?)maVatPham ?? DBNull.Value),
                new SqlParameter("@MaNguoiLienQuan", (object?)maNguoiLienQuan ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}