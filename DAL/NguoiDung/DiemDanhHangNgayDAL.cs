using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class DiemDanhHangNgayDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        // Kiểm tra đã điểm danh hôm nay
        public bool KiemTraDiemDanhHomNay(Guid maNguoiDung)
        {
            string query = "SELECT COUNT(*) FROM DiemDanhHangNgay WHERE MaNguoiDung = @MaNguoiDung AND NgayDiemDanh = CAST(GETDATE() AS DATE)";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
            return count > 0;
        }

        // Lấy ngày thứ mấy trong chuỗi
        public int LayNgayThuMay(Guid maNguoiDung)
        {
            string query = @"SELECT ISNULL(MAX(NgayThuMay), 0) + 1 
                            FROM DiemDanhHangNgay 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND NgayDiemDanh = DATEADD(DAY, -1, CAST(GETDATE() AS DATE))";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            int ngay = result != DBNull.Value ? Convert.ToInt32(result) : 1;
            return ngay > 7 ? 1 : ngay;
        }

        // Điểm danh
        public int DiemDanh(Guid maNguoiDung, int ngayThuMay, int thuongVang, int thuongXP, string thuongDacBiet)
        {
            string query = @"INSERT INTO DiemDanhHangNgay (MaNguoiDung, NgayDiemDanh, NgayThuMay, ThuongVang, ThuongXP, ThuongDacBiet) 
                            VALUES (@MaNguoiDung, CAST(GETDATE() AS DATE), @NgayThuMay, @ThuongVang, @ThuongXP, @ThuongDacBiet)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@NgayThuMay", ngayThuMay),
                new SqlParameter("@ThuongVang", thuongVang),
                new SqlParameter("@ThuongXP", thuongXP),
                new SqlParameter("@ThuongDacBiet", (object)thuongDacBiet ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Lấy lịch sử điểm danh
        public DataTable LayLichSu(Guid maNguoiDung, int soNgay)
        {
            string query = $"SELECT TOP {soNgay} * FROM DiemDanhHangNgay WHERE MaNguoiDung = @MaNguoiDung ORDER BY NgayDiemDanh DESC";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }
    }
}