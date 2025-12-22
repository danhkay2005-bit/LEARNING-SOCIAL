using Common;
using DAL.NguoiDung;
using DTO.NguoiDung;
using System;
using System.Data;

namespace BLL.NguoiDung
{
    public class NguoiDungBLL
    {
        // ============================================================
        // KHAI BÁO DAL
        // ============================================================

        private readonly NguoiDungDAL _nguoiDungDAL;
        private readonly CaiDatNguoiDungDAL _caiDatDAL;
        private readonly TuyChinhProfileDAL _tuyChinhDAL;
        private readonly LichSuHoatDongDAL _lichSuDAL;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public NguoiDungBLL()
        {
            _nguoiDungDAL = new NguoiDungDAL();
            _caiDatDAL = new CaiDatNguoiDungDAL();
            _tuyChinhDAL = new TuyChinhProfileDAL();
            _lichSuDAL = new LichSuHoatDongDAL();
        }

        // ============================================================
        // ĐĂNG KÝ
        // ============================================================

        public KetQua DangKy(string tenDangNhap, string matKhau, string email)
        {
            try
            {
                // 1. Validate Username
                if (!Validators.IsValidUsername(tenDangNhap))
                {
                    return new KetQua(false, "Username phải từ 3-50 ký tự, chỉ gồm chữ, số và dấu gạch dưới");
                }

                // 2. Validate Password
                if (!Validators.IsValidPassword(matKhau))
                {
                    return new KetQua(false, "Mật khẩu phải từ 6-100 ký tự");
                }

                // 3. Validate Email
                if (!Validators.IsValidEmail(email))
                {
                    return new KetQua(false, "Email không hợp lệ");
                }

                // 4. Kiểm tra Username đã tồn tại
                if (_nguoiDungDAL.KiemTraUsername(tenDangNhap))
                {
                    return new KetQua(false, "Tên đăng nhập đã tồn tại");
                }

                // 5. Kiểm tra Email đã tồn tại
                if (_nguoiDungDAL.KiemTraEmail(email))
                {
                    return new KetQua(false, "Email đã được sử dụng");
                }

                // 6. Mã hóa mật khẩu
                string matKhauMaHoa = Helpers.HashSHA256(matKhau);

                // 7. Thực hiện đăng ký
                int result = _nguoiDungDAL.DangKy(tenDangNhap, matKhauMaHoa, email);

                if (result > 0)
                {
                    // 8. Lấy thông tin user vừa tạo
                    DataTable dt = _nguoiDungDAL.LayTheoUsername(tenDangNhap);

                    if (dt.Rows.Count > 0)
                    {
                        Guid maNguoiDung = (Guid)dt.Rows[0]["MaNguoiDung"];

                        // 9. Tạo cài đặt mặc định
                        _caiDatDAL.TaoMacDinh(maNguoiDung);

                        // 10. Tạo tùy chỉnh profile mặc định
                        _tuyChinhDAL.TaoMacDinh(maNguoiDung);

                        // 11. Ghi log hoạt động
                        _lichSuDAL.Them(maNguoiDung, "Đăng ký", "Đăng ký tài khoản thành công", string.Empty);
                    }

                    return new KetQua(true, "Đăng ký thành công!");
                }

                return new KetQua(false, "Đăng ký thất bại, vui lòng thử lại");
            }
            catch (Exception ex)
            {
                return new KetQua(false, "Lỗi hệ thống:  " + ex.Message);
            }
        }

        // ============================================================
        // ĐĂNG NHẬP
        // ============================================================

        public KetQua DangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                // 1. Validate input
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    return new KetQua(false, "Vui lòng nhập tên đăng nhập");
                }

                if (string.IsNullOrWhiteSpace(matKhau))
                {
                    return new KetQua(false, "Vui lòng nhập mật khẩu");
                }

                // 2. Mã hóa mật khẩu
                string matKhauMaHoa = Helpers.HashSHA256(matKhau);

                // 3. Kiểm tra đăng nhập
                DataTable dt = _nguoiDungDAL.DangNhap(tenDangNhap, matKhauMaHoa);

                if (dt.Rows.Count == 0)
                {
                    return new KetQua(false, "Sai tên đăng nhập hoặc mật khẩu");
                }

                DataRow user = dt.Rows[0];

                // 4. Kiểm tra tài khoản bị khóa
                if ((bool)user["DaXoa"])
                {
                    return new KetQua(false, "Tài khoản đã bị khóa");
                }

                Guid maNguoiDung = (Guid)user["MaNguoiDung"];

                // 5. Kiểm tra & cập nhật streak
                KiemTraVaCapNhatStreak(maNguoiDung, user);

                // 6. Lưu thông tin vào Session
                SessionManager.Login(
                    maNguoiDung,
                    user["TenDangNhap"]?.ToString() ?? string.Empty,
                    user["HoVaTen"]?.ToString() ?? "Thành Viên Mới",
                    (VaiTro)(int)user["MaVaiTro"],
                    (int)user["MaCapDo"],
                    (int)user["TongDiemXP"],
                    (int)user["Vang"],
                    (int)user["KimCuong"],
                    (int)user["ChuoiNgayHocLienTiep"]
                );

                // 7. Cập nhật trạng thái online
                _nguoiDungDAL.CapNhatTrangThaiOnline(maNguoiDung, true);

                // 8. Ghi log hoạt động
                _lichSuDAL.Them(maNguoiDung, "Đăng nhập", "Đăng nhập thành công", string.Empty);

                return new KetQua(true, "Đăng nhập thành công!");
            }
            catch (Exception ex)
            {
                return new KetQua(false, "Lỗi hệ thống: " + ex.Message);
            }
        }

        // ============================================================
        // ĐĂNG XUẤT
        // ============================================================

        public KetQua DangXuat()
        {
            try
            {
                if (!SessionManager.IsLoggedIn)
                {
                    return new KetQua(false, "Chưa đăng nhập");
                }

                if (!SessionManager.CurrentUserId.HasValue)
                {
                    return new KetQua(false, "Không xác định được người dùng hiện tại");
                }

                Guid maNguoiDung = SessionManager.CurrentUserId.Value;

                // 1. Cập nhật trạng thái offline
                _nguoiDungDAL.CapNhatTrangThaiOnline(maNguoiDung, false);

                // 2. Ghi log
                _lichSuDAL.Them(maNguoiDung, "Đăng xuất", "Đăng xuất", string.Empty);

                // 3. Xóa session
                SessionManager.Logout();

                return new KetQua(true, "Đăng xuất thành công!");
            }
            catch (Exception ex)
            {
                return new KetQua(false, "Lỗi hệ thống: " + ex.Message);
            }
        }

        // ============================================================
        // ĐỔI MẬT KHẨU
        // ============================================================

        public KetQua DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            try
            {
                if (!SessionManager.IsLoggedIn)
                {
                    return new KetQua(false, "Chưa đăng nhập");
                }

                // 1. Validate mật khẩu mới
                if (!Validators.IsValidPassword(matKhauMoi))
                {
                    return new KetQua(false, "Mật khẩu mới phải từ 6-100 ký tự");
                }

                // 2. Kiểm tra xác nhận mật khẩu
                if (matKhauMoi != xacNhanMatKhau)
                {
                    return new KetQua(false, "Xác nhận mật khẩu không khớp");
                }

                // 3. Kiểm tra mật khẩu cũ
                if (!SessionManager.CurrentUserId.HasValue)
                {
                    return new KetQua(false, "Không xác định được người dùng hiện tại");
                }
                Guid maNguoiDung = SessionManager.CurrentUserId.Value;
                string matKhauCuMaHoa = Helpers.HashSHA256(matKhauCu);

                DataTable dt = _nguoiDungDAL.LayTheoMa(maNguoiDung);
                if (dt.Rows.Count == 0)
                {
                    return new KetQua(false, "Không tìm thấy tài khoản");
                }

                string? matKhauHienTai = dt.Rows[0]["MatKhauMaHoa"].ToString();
                if (matKhauCuMaHoa != matKhauHienTai)
                {
                    return new KetQua(false, "Mật khẩu cũ không đúng");
                }

                // 4. Cập nhật mật khẩu mới
                string matKhauMoiMaHoa = Helpers.HashSHA256(matKhauMoi);
                int result = _nguoiDungDAL.DoiMatKhau(maNguoiDung, matKhauMoiMaHoa);

                if (result > 0)
                {
                    // 5. Ghi log
                    _lichSuDAL.Them(maNguoiDung, "Đổi mật khẩu", "Đổi mật khẩu thành công", string.Empty);
                    return new KetQua(true, "Đổi mật khẩu thành công!");
                }

                return new KetQua(false, "Đổi mật khẩu thất bại");
            }
            catch (Exception ex)
            {
                return new KetQua(false, "Lỗi hệ thống: " + ex.Message);
            }
        }

        // ============================================================
        // KIỂM TRA & CẬP NHẬT STREAK
        // ============================================================

        private void KiemTraVaCapNhatStreak(Guid maNguoiDung, DataRow user)
        {
            try
            {
                DateTime? ngayHoatDongCuoi = user["NgayHoatDongCuoi"] as DateTime?;
                int chuoiNgay = (int)user["ChuoiNgayHocLienTiep"];
                int soFreeze = (int)user["SoStreakFreeze"];

                if (ngayHoatDongCuoi == null)
                {
                    return;
                }

                DateTime homNay = DateTime.Today;
                DateTime ngayCuoi = ngayHoatDongCuoi.Value.Date;
                int soNgayNghi = (homNay - ngayCuoi).Days;

                // Nếu nghỉ 1 ngày → Dùng Freeze hoặc Reset
                if (soNgayNghi == 2)  // Hôm qua không học
                {
                    if (soFreeze > 0)
                    {
                        // Dùng Freeze
                        _nguoiDungDAL.CapNhatStreakFreeze(maNguoiDung, soFreeze - 1);

                        // Ghi log
                        _lichSuDAL.Them(maNguoiDung, "Chuỗi ngày", "Tự động sử dụng Streak Freeze", string.Empty);
                    }
                    else
                    {
                        // Reset streak
                        _nguoiDungDAL.ResetStreak(maNguoiDung);

                        // Cập nhật session
                        SessionManager.UpdateStreak(0);

                        // Ghi log
                        _lichSuDAL.Them(maNguoiDung, "Chuỗi ngày", "Mất chuỗi ngày học", string.Empty);
                    }
                }
                // Nếu nghỉ > 1 ngày → Reset
                else if (soNgayNghi > 2)
                {
                    _nguoiDungDAL.ResetStreak(maNguoiDung);
                    SessionManager.UpdateStreak(0);
                    _lichSuDAL.Them(maNguoiDung, "Chuỗi ngày", "Mất chuỗi ngày học", string.Empty);
                }
            }
            catch
            {
                // Bỏ qua lỗi streak
            }
        }

        // ============================================================
        // KIỂM TRA TỒN TẠI
        // ============================================================

        public bool KiemTraUsernameConTrong(string tenDangNhap)
        {
            return !_nguoiDungDAL.KiemTraUsername(tenDangNhap);
        }

        public bool KiemTraEmailConTrong(string email)
        {
            return !_nguoiDungDAL.KiemTraEmail(email);
        }

        // ============================================================
        // LẤY THÔNG TIN
        // ============================================================

        public DataTable LayThongTinNguoiDung(Guid maNguoiDung)
        {
            return _nguoiDungDAL.LayTheoMa(maNguoiDung);
        }

        public DataTable? LayThongTinHienTai()
        {
            if (!SessionManager.IsLoggedIn || !SessionManager.CurrentUserId.HasValue)
                return null;

            return _nguoiDungDAL.LayTheoMa(SessionManager.CurrentUserId.Value);
        }
    }
}