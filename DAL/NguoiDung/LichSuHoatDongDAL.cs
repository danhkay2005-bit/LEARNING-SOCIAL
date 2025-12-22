using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class LichSuHoatDongDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // Lấy lịch sử của người dùng
        public DataTable LayTheoNguoiDung(Guid maNguoiDung, int soLuong)
        {
            string query = $"SELECT TOP {soLuong} * FROM LichSuHoatDong WHERE MaNguoiDung = @MaNguoiDung ORDER BY ThoiGian DESC";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Thêm hoạt động
        public int Them(Guid maNguoiDung, string loaiHoatDong, string moTa, string duLieuThem)
        {
            string query = @"INSERT INTO LichSuHoatDong (MaNguoiDung, LoaiHoatDong, MoTa, DuLieuThem) 
                            VALUES (@MaNguoiDung, @LoaiHoatDong, @MoTa, @DuLieuThem)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiHoatDong", loaiHoatDong),
                new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                new SqlParameter("@DuLieuThem", (object)duLieuThem ??  DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Lấy theo loại
        public DataTable LayTheoLoai(Guid maNguoiDung, string loaiHoatDong, int soLuong)
        {
            string query = $"SELECT TOP {soLuong} * FROM LichSuHoatDong WHERE MaNguoiDung = @MaNguoiDung AND LoaiHoatDong = @LoaiHoatDong ORDER BY ThoiGian DESC";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiHoatDong", loaiHoatDong)
            };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }
    }
}