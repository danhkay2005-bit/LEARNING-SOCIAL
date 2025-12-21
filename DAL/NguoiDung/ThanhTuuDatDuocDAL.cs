using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class ThanhTuuDatDuocDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // Lấy thành tựu đã đạt của người dùng
        public DataTable LayTheoNguoiDung(Guid maNguoiDung)
        {
            string query = @"SELECT td.*, tt.TenThanhTuu, tt.BieuTuong, tt.DoHiem 
                            FROM ThanhTuuDatDuoc td 
                            INNER JOIN ThanhTuu tt ON td. MaThanhTuu = tt.MaThanhTuu 
                            WHERE td.MaNguoiDung = @MaNguoiDung 
                            ORDER BY td.NgayDat DESC";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Kiểm tra đã đạt thành tựu
        public bool KiemTraDaDat(Guid maNguoiDung, int maThanhTuu)
        {
            string query = "SELECT COUNT(*) FROM ThanhTuuDatDuoc WHERE MaNguoiDung = @MaNguoiDung AND MaThanhTuu = @MaThanhTuu";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaThanhTuu", maThanhTuu)
            };
            object? result = _dbHelper.ExecuteScalar(query, _dbType, parameters);
            int count = (result != null) ? Convert.ToInt32(result) : 0;
            return count > 0;
        }

        // Thêm thành tựu đạt được
        public int Them(Guid maNguoiDung, int maThanhTuu)
        {
            string query = "INSERT INTO ThanhTuuDatDuoc (MaNguoiDung, MaThanhTuu) VALUES (@MaNguoiDung, @MaThanhTuu)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaThanhTuu", maThanhTuu)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Đánh dấu đã xem
        public int DanhDauDaXem(Guid maNguoiDung, int maThanhTuu)
        {
            string query = "UPDATE ThanhTuuDatDuoc SET DaXem = 1 WHERE MaNguoiDung = @MaNguoiDung AND MaThanhTuu = @MaThanhTuu";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaThanhTuu", maThanhTuu)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Đếm số thành tựu đã đạt
        public int DemSoThanhTuu(Guid maNguoiDung)
        {
            string query = "SELECT COUNT(*) FROM ThanhTuuDatDuoc WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? result = _dbHelper.ExecuteScalar(query, _dbType, parameters);
            return (result != null) ? Convert.ToInt32(result) : 0;
        }
    }
}