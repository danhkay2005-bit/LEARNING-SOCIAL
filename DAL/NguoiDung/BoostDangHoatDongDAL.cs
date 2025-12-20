using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class BoostDangHoatDongDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        // ============================================================
        // LẤY DỮ LIỆU
        // ============================================================

        // Lấy tất cả boost của người dùng
        public DataTable LayTheoNguoiDung(Guid maNguoiDung)
        {
            string query = "SELECT * FROM BoostDangHoatDong WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy boost đang hoạt động của người dùng
        public DataTable LayBoostDangHoatDong(Guid maNguoiDung)
        {
            string query = @"SELECT * FROM BoostDangHoatDong 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND ConHieuLuc = 1 
                            AND ThoiGianKetThuc > GETDATE()";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy boost theo loại (XP hoặc Vang)
        public DataTable LayBoostTheoLoai(Guid maNguoiDung, string loaiBoost)
        {
            string query = @"SELECT * FROM BoostDangHoatDong 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND LoaiBoost = @LoaiBoost
                            AND ConHieuLuc = 1 
                            AND ThoiGianKetThuc > GETDATE()";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiBoost", loaiBoost)
            };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy theo ID
        public DataTable LayTheoMa(int maBoost)
        {
            string query = "SELECT * FROM BoostDangHoatDong WHERE MaBoost = @MaBoost";
            var parameters = new[] { new SqlParameter("@MaBoost", maBoost) };
            return DatabaseHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // ============================================================
        // KIỂM TRA
        // ============================================================

        // Kiểm tra đang có boost loại này không
        public bool KiemTraDangCoBoost(Guid maNguoiDung, string loaiBoost)
        {
            string query = @"SELECT COUNT(*) FROM BoostDangHoatDong 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND LoaiBoost = @LoaiBoost
                            AND ConHieuLuc = 1 
                            AND ThoiGianKetThuc > GETDATE()";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiBoost", loaiBoost)
            };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            int count = result != DBNull.Value ? Convert.ToInt32(result) : 0;
            return count > 0;
        }

        // Lấy hệ số nhân hiện tại
        public double LayHeSoNhan(Guid maNguoiDung, string loaiBoost)
        {
            string query = @"SELECT ISNULL(MAX(HeSoNhan), 1) FROM BoostDangHoatDong 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND LoaiBoost = @LoaiBoost
                            AND ConHieuLuc = 1 
                            AND ThoiGianKetThuc > GETDATE()";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@LoaiBoost", loaiBoost)
            };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            return result != DBNull.Value ? Convert.ToDouble(result) : 1.0;
        }

        // ============================================================
        // THÊM / CẬP NHẬT / XÓA
        // ============================================================

        // Kích hoạt boost mới
        public int KichHoatBoost(Guid maNguoiDung, int maVatPham, string loaiBoost, double heSoNhan, int thoiHanPhut)
        {
            string query = @"INSERT INTO BoostDangHoatDong 
                            (MaNguoiDung, MaVatPham, LoaiBoost, HeSoNhan, ThoiGianKetThuc) 
                            VALUES 
                            (@MaNguoiDung, @MaVatPham, @LoaiBoost, @HeSoNhan, DATEADD(MINUTE, @ThoiHanPhut, GETDATE()))";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaVatPham", maVatPham),
                new SqlParameter("@LoaiBoost", loaiBoost),
                new SqlParameter("@HeSoNhan", heSoNhan),
                new SqlParameter("@ThoiHanPhut", thoiHanPhut)
            };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Tắt boost
        public int TatBoost(int maBoost)
        {
            string query = "UPDATE BoostDangHoatDong SET ConHieuLuc = 0 WHERE MaBoost = @MaBoost";
            var parameters = new[] { new SqlParameter("@MaBoost", maBoost) };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Tắt tất cả boost của người dùng
        public int TatTatCaBoost(Guid maNguoiDung)
        {
            string query = "UPDATE BoostDangHoatDong SET ConHieuLuc = 0 WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật boost hết hạn
        public int CapNhatBoostHetHan()
        {
            string query = @"UPDATE BoostDangHoatDong 
                            SET ConHieuLuc = 0 
                            WHERE ConHieuLuc = 1 AND ThoiGianKetThuc <= GETDATE()";
            return DatabaseHelper.ExecuteNonQuery(query, _dbType);
        }

        // Xóa boost
        public int Xoa(int maBoost)
        {
            string query = "DELETE FROM BoostDangHoatDong WHERE MaBoost = @MaBoost";
            var parameters = new[] { new SqlParameter("@MaBoost", maBoost) };
            return DatabaseHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // THỐNG KÊ
        // ============================================================

        // Đếm số boost đang hoạt động
        public int DemBoostDangHoatDong(Guid maNguoiDung)
        {
            string query = @"SELECT COUNT(*) FROM BoostDangHoatDong 
                            WHERE MaNguoiDung = @MaNguoiDung 
                            AND ConHieuLuc = 1 
                            AND ThoiGianKetThuc > GETDATE()";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        // Lấy thời gian còn lại của boost (phút)
        public int LayThoiGianConLai(int maBoost)
        {
            string query = @"SELECT DATEDIFF(MINUTE, GETDATE(), ThoiGianKetThuc) 
                            FROM BoostDangHoatDong 
                            WHERE MaBoost = @MaBoost AND ConHieuLuc = 1";
            var parameters = new[] { new SqlParameter("@MaBoost", maBoost) };
            object? result = DatabaseHelper.ExecuteScalar(query, _dbType, parameters);
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
    }
}