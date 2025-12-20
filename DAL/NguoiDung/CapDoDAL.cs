using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class CapDoDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        // Lấy tất cả
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM CapDo ORDER BY MucXPToiThieu";
            return DatabaseHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo ID
        public DataTable LayTheoMa(int maCapDo)
        {
            string query = "SELECT * FROM CapDo WHERE MaCapDo = @MaCapDo";
            var parameters = new[] { new SqlParameter("@MaCapDo", maCapDo) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy cấp độ theo XP
        public DataTable LayTheoXP(int xp)
        {
            string query = "SELECT TOP 1 * FROM CapDo WHERE MucXPToiThieu <= @XP ORDER BY MucXPToiThieu DESC";
            var parameters = new[] { new SqlParameter("@XP", xp) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Thêm
        public int Them(string tenCapDo, string bieuTuong, int mucXPToiThieu, int mucXPToiDa, string mauSacKhung, string moKhoaTinhNang)
        {
            string query = @"INSERT INTO CapDo (TenCapDo, BieuTuong, MucXPToiThieu, MucXPToiDa, MauSacKhung, MoKhoaTinhNang) 
                            VALUES (@TenCapDo, @BieuTuong, @MucXPToiThieu, @MucXPToiDa, @MauSacKhung, @MoKhoaTinhNang)";
            var parameters = new[] {
                new SqlParameter("@TenCapDo", tenCapDo),
                new SqlParameter("@BieuTuong", (object)bieuTuong ?? DBNull.Value),
                new SqlParameter("@MucXPToiThieu", mucXPToiThieu),
                new SqlParameter("@MucXPToiDa", mucXPToiDa),
                new SqlParameter("@MauSacKhung", (object)mauSacKhung ?? DBNull. Value),
                new SqlParameter("@MoKhoaTinhNang", (object)moKhoaTinhNang ??  DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}