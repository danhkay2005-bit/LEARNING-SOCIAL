using Common;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static Common.DatabaseHelper;

namespace DAL.NguoiDung
{
    public class NguoiDungDAL
    {
        private readonly DatabaseType _dbType = DatabaseType.NguoiDung;
        private readonly DatabaseHelper _dbHelper = new();
        // ============================================================
        // LẤY DỮ LIỆU
        // ============================================================

        // Lấy tất cả
        public DataTable LayTatCa()
        {
            string query = "SELECT * FROM NguoiDung WHERE DaXoa = 0";
            return _dbHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy theo ID
        public DataTable LayTheoMa(Guid maNguoiDung)
        {
            string query = "SELECT * FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung AND DaXoa = 0";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy theo Username
        public DataTable LayTheoUsername(string tenDangNhap)
        {
            string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND DaXoa = 0";
            var parameters = new[] { new SqlParameter("@TenDangNhap", tenDangNhap) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Lấy theo Email
        public DataTable LayTheoEmail(string email)
        {
            string query = "SELECT * FROM NguoiDung WHERE Email = @Email AND DaXoa = 0";
            var parameters = new[] { new SqlParameter("@Email", email) };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // ============================================================
        // ĐĂNG NHẬP / ĐĂNG KÝ
        // ============================================================

        // Đăng nhập
        public DataTable DangNhap(string tenDangNhap, string matKhauMaHoa)
        {
            string query = @"SELECT * FROM NguoiDung 
                            WHERE TenDangNhap = @TenDangNhap 
                            AND MatKhauMaHoa = @MatKhauMaHoa 
                            AND DaXoa = 0";
            var parameters = new[] {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhauMaHoa", matKhauMaHoa)
            };
            return _dbHelper.ExecuteQuery(query, _dbType, parameters);
        }

        // Đăng ký
        public int DangKy(string tenDangNhap, string matKhauMaHoa, string email)
        {
            string query = @"INSERT INTO NguoiDung (TenDangNhap, MatKhauMaHoa, Email, MaVaiTro, MaCapDo) 
                            VALUES (@TenDangNhap, @MatKhauMaHoa, @Email, @MaVaiTro, @MaCapDo)";
            var parameters = new[] {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhauMaHoa", matKhauMaHoa),
                new SqlParameter("@Email", email),
                new SqlParameter("@MaVaiTro", 2),
                new SqlParameter("@MaCapDo", 1)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Kiểm tra Username tồn tại
        public bool KiemTraUsername(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
            var parameters = new[] { new SqlParameter("@TenDangNhap", tenDangNhap) };
            object? result = _dbHelper.ExecuteScalar(query, _dbType, parameters);
            int count = result is int i ? i : Convert.ToInt32(result ?? 0);
            return count > 0;
        }

        // Kiểm tra Email tồn tại
        public bool KiemTraEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
            var parameters = new[] { new SqlParameter("@Email", email) };
            object? result = _dbHelper.ExecuteScalar(query, _dbType, parameters);
            int count = result is int i ? i : Convert.ToInt32(result ?? 0);
            return count > 0;
        }

        // ============================================================
        // CẬP NHẬT THÔNG TIN
        // ============================================================

        // Cập nhật hồ sơ
        public int CapNhatHoSo(Guid maNguoiDung, string hoVaTen, DateTime? ngaySinh, byte? gioiTinh, string tieuSu)
        {
            string query = @"UPDATE NguoiDung 
                            SET HoVaTen = @HoVaTen, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                TieuSu = @TieuSu 
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@HoVaTen", (object?)hoVaTen ?? DBNull.Value),
                new SqlParameter("@NgaySinh", (object?)ngaySinh ?? DBNull.Value),
                new SqlParameter("@GioiTinh", (object?)gioiTinh ?? DBNull.Value),
                new SqlParameter("@TieuSu", (object?)tieuSu ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật avatar
        public int CapNhatAvatar(Guid maNguoiDung, string hinhDaiDien)
        {
            string query = "UPDATE NguoiDung SET HinhDaiDien = @HinhDaiDien WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@HinhDaiDien", (object)hinhDaiDien ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Đổi mật khẩu
        public int DoiMatKhau(Guid maNguoiDung, string matKhauMaHoaMoi)
        {
            string query = "UPDATE NguoiDung SET MatKhauMaHoa = @MatKhauMaHoa WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@MatKhauMaHoa", matKhauMaHoaMoi)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT TÀI CHÍNH
        // ============================================================

        // Cập nhật XP
        public int CapNhatXP(Guid maNguoiDung, int xpThem)
        {
            string query = "UPDATE NguoiDung SET TongDiemXP = TongDiemXP + @XP WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@XP", xpThem)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật Vàng
        public int CapNhatVang(Guid maNguoiDung, int vangThem)
        {
            string query = "UPDATE NguoiDung SET Vang = Vang + @Vang WHERE MaNguoiDung = @MaNguoiDung AND Vang + @Vang >= 0";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Vang", vangThem)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật Kim Cương
        public int CapNhatKimCuong(Guid maNguoiDung, int kimCuongThem)
        {
            string query = "UPDATE NguoiDung SET KimCuong = KimCuong + @KimCuong WHERE MaNguoiDung = @MaNguoiDung AND KimCuong + @KimCuong >= 0";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@KimCuong", kimCuongThem)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT STREAK
        // ============================================================

        // Cập nhật streak
        public int CapNhatStreak(Guid maNguoiDung, int chuoiNgay)
        {
            string query = @"UPDATE NguoiDung 
                            SET ChuoiNgayHocLienTiep = @ChuoiNgay,
                                ChuoiNgayDaiNhat = CASE WHEN @ChuoiNgay > ChuoiNgayDaiNhat THEN @ChuoiNgay ELSE ChuoiNgayDaiNhat END,
                                NgayHoatDongCuoi = CAST(GETDATE() AS DATE)
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@ChuoiNgay", chuoiNgay)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Reset streak
        public int ResetStreak(Guid maNguoiDung)
        {
            string query = "UPDATE NguoiDung SET ChuoiNgayHocLienTiep = 0 WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật Freeze
        public int CapNhatStreakFreeze(Guid maNguoiDung, int soFreeze)
        {
            string query = "UPDATE NguoiDung SET SoStreakFreeze = @SoFreeze WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@SoFreeze", soFreeze)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // CẬP NHẬT THỐNG KÊ
        // ============================================================

        // Cập nhật thống kê học
        public int CapNhatThongKeHoc(Guid maNguoiDung, int soTheHocThem, int soTheDungThem, int thoiGianPhutThem)
        {
            string query = @"UPDATE NguoiDung 
                            SET TongSoTheHoc = TongSoTheHoc + @SoTheHoc,
                                TongSoTheDung = TongSoTheDung + @SoTheDung,
                                TongThoiGianHocPhut = TongThoiGianHocPhut + @ThoiGian
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@SoTheHoc", soTheHocThem),
                new SqlParameter("@SoTheDung", soTheDungThem),
                new SqlParameter("@ThoiGian", thoiGianPhutThem)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Cập nhật thống kê thách đấu
        public int CapNhatThongKeThachDau(Guid maNguoiDung, bool thang)
        {
            string query = thang
                ? "UPDATE NguoiDung SET SoTranThachDau = SoTranThachDau + 1, SoTranThang = SoTranThang + 1 WHERE MaNguoiDung = @MaNguoiDung"
                : "UPDATE NguoiDung SET SoTranThachDau = SoTranThachDau + 1, SoTranThua = SoTranThua + 1 WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // TRẠNG THÁI
        // ============================================================

        // Cập nhật trạng thái online
        public int CapNhatTrangThaiOnline(Guid maNguoiDung, bool online)
        {
            string query = @"UPDATE NguoiDung 
                            SET TrangThaiOnline = @Online, LanOnlineCuoi = GETDATE() 
                            WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@Online", online)
            };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Xác thực email
        public int XacThucEmail(Guid maNguoiDung)
        {
            string query = "UPDATE NguoiDung SET DaXacThucEmail = 1 WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // Xóa mềm
        public int XoaMem(Guid maNguoiDung)
        {
            string query = "UPDATE NguoiDung SET DaXoa = 1 WHERE MaNguoiDung = @MaNguoiDung";
            var parameters = new[] { new SqlParameter("@MaNguoiDung", maNguoiDung) };
            return _dbHelper.ExecuteNonQuery(query, _dbType, parameters);
        }

        // ============================================================
        // THỐNG KÊ
        // ============================================================

        // Đếm tổng người dùng
        public int DemTongNguoiDung()
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE DaXoa = 0";
            object? result = _dbHelper.ExecuteScalar(query, _dbType);
            return result is int i ? i : Convert.ToInt32(result ?? 0);
        }

        // Lấy top XP
        public DataTable LayTopXP(int top)
        {
            string query = $"SELECT TOP {top} * FROM NguoiDung WHERE DaXoa = 0 ORDER BY TongDiemXP DESC";
            return _dbHelper.ExecuteQuery(query, _dbType);
        }

        // Lấy top Streak
        public DataTable LayTopStreak(int top)
        {
            string query = $"SELECT TOP {top} * FROM NguoiDung WHERE DaXoa = 0 ORDER BY ChuoiNgayHocLienTiep DESC";
            return _dbHelper.ExecuteQuery(query, _dbType);
        }
    }
}