using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class ThanhTuuDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        // Lấy tất cả
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM ThanhTuu";
            return DatabaseHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo loại
        public DataTable LayTheoLoai(string loaiThanhTuu)
        {
            string query = "SELECT * FROM ThanhTuu WHERE LoaiThanhTuu = @LoaiThanhTuu";
            var parameters = new[] { new SqlParameter("@LoaiThanhTuu", loaiThanhTuu) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy thành tựu chưa đạt của người dùng
        public DataTable LayChuaDat(Guid maNguoiDung)
        {
            string query = @"SELECT * FROM ThanhTuu 
                            WHERE MaThanhTuu NOT IN (SELECT MaThanhTuu FROM ThanhTuuDatDuoc WHERE MaNguoiDung = @MaNguoiDung)";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }
    }
}