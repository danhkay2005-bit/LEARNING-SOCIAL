using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class CauHinhDiemDanhDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        // Lấy tất cả cấu hình
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM CauHinhDiemDanh ORDER BY NgayThu";
            return DatabaseHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo ngày thứ
        public DataTable LayTheoNgay(int ngayThu)
        {
            string query = "SELECT * FROM CauHinhDiemDanh WHERE NgayThu = @NgayThu";
            var parameters = new[] { new SqlParameter("@NgayThu", ngayThu) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }
    }
}