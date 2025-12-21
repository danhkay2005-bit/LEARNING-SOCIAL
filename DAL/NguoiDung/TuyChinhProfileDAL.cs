using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class TuyChinhProfileDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // ============================================================
        // TẠO MẶC ĐỊNH
        // ============================================================

        public int TaoMacDinh(Guid maNguoiDung)
        {
            string query = @"INSERT INTO TuyChinhProfile (MaNguoiDung) 
                            VALUES (@MaNguoiDung)";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // LẤY DỮ LIỆU
        // ============================================================

        public DataTable LayTheoNguoiDung(Guid maNguoiDung)
        {
            string query = "SELECT * FROM TuyChinhProfile WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT AVATAR
        // ============================================================

        public int CapNhatAvatar(Guid maNguoiDung, int? maAvatar)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaAvatarDangDung = @MaAvatar, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaAvatar", (object?)maAvatar ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT KHUNG
        // ============================================================

        public int CapNhatKhung(Guid maNguoiDung, int? maKhung)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaKhungDangDung = @MaKhung, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaKhung", (object?)maKhung ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT HÌNH NỀN
        // ============================================================

        public int CapNhatHinhNen(Guid maNguoiDung, int? maHinhNen)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaHinhNenDangDung = @MaHinhNen, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaHinhNen", (object?)maHinhNen ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT HIỆU ỨNG
        // ============================================================

        public int CapNhatHieuUng(Guid maNguoiDung, int? maHieuUng)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaHieuUngDangDung = @MaHieuUng, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaHieuUng", (object?)maHieuUng ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT THEME
        // ============================================================

        public int CapNhatTheme(Guid maNguoiDung, int? maTheme)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaThemeDangDung = @MaTheme, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaTheme", (object?)maTheme ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT NHẠC NỀN
        // ============================================================

        public int CapNhatNhacNen(Guid maNguoiDung, int? maNhacNen)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaNhacNenDangDung = @MaNhacNen, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaNhacNen", (object?)maNhacNen ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT BADGE
        // ============================================================

        public int CapNhatBadge(Guid maNguoiDung, int? maBadge)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaBadgeDangDung = @MaBadge, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaBadge", (object?)maBadge ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT HUY HIỆU HIỂN THỊ
        // ============================================================

        public int CapNhatHuyHieu(Guid maNguoiDung, int? maHuyHieu)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET MaHuyHieuHienThi = @MaHuyHieu, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MaHuyHieu", (object?)maHuyHieu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT CÂU CHÂM NGÔN
        // ============================================================

        public int CapNhatCauChamNgon(Guid maNguoiDung, string cauChamNgon)
        {
            string query = @"UPDATE TuyChinhProfile 
                            SET CauChamNgon = @CauChamNgon, 
                                ThoiGianCapNhat = GETDATE()
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@CauChamNgon", (object)cauChamNgon ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }
    }
}