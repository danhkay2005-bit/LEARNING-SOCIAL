using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO.Enums;
using System;
using System.Text.RegularExpressions; // Thư viện để dùng Regex
using System.Windows.Forms;

namespace WinForms.UserControls
{
    public partial class QuenMatKhauControl : UserControl
    {
        private readonly IAuthService _nguoiDungService;
        public event Action? QuayVeDangNhap;

        public QuenMatKhauControl(IAuthService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;

            // 1. Mặc định ẩn mật khẩu
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // 2. Focus vào ô Email khi mở form
            this.Load += (s, e) => txtEmail.Focus();

            // 3. Cài đặt các tính năng
            RegisterEvents();         // Sự kiện phím Enter
            SetupPasswordVisibility(); // Sự kiện soi mật khẩu
        }

        // --- TÍNH NĂNG: ẤN GIỮ ĐỂ XEM MẬT KHẨU ---
        private void SetupPasswordVisibility()
        {
            // A. Nút xem Mật Khẩu Mới
            btnXemMatKhauMoi.MouseDown += (s, e) => txtMatKhauMoi.UseSystemPasswordChar = false;
            btnXemMatKhauMoi.MouseUp += (s, e) => txtMatKhauMoi.UseSystemPasswordChar = true;
            btnXemMatKhauMoi.MouseLeave += (s, e) => txtMatKhauMoi.UseSystemPasswordChar = true;

            // B. Nút xem Xác Nhận
            btnXemXacNhan.MouseDown += (s, e) => txtXacNhanMatKhau.UseSystemPasswordChar = false;
            btnXemXacNhan.MouseUp += (s, e) => txtXacNhanMatKhau.UseSystemPasswordChar = true;
            btnXemXacNhan.MouseLeave += (s, e) => txtXacNhanMatKhau.UseSystemPasswordChar = true;
        }

        // --- TÍNH NĂNG: XỬ LÝ PHÍM ENTER ---
        private void RegisterEvents()
        {
            txtEmail.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter) { txtMatKhauMoi.Focus(); e.Handled = true; }
            };

            txtMatKhauMoi.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter) { txtXacNhanMatKhau.Focus(); e.Handled = true; }
            };

            txtXacNhanMatKhau.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter) { btnDatLaiMatKhau.PerformClick(); e.Handled = true; }
            };
        }

        // --- TÍNH NĂNG: KIỂM TRA RÀNG BUỘC (REGEX) ---
        private bool ValidateInput()
        {
            // 1. Kiểm tra trống
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhauMoi.Text) ||
                string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra Email
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // 3. Kiểm tra Mật khẩu xác nhận
            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("❌ Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return false;
            }

            // 4. Kiểm tra độ mạnh mật khẩu (Theo yêu cầu của bạn)
            // ^              : Bắt đầu chuỗi
            // (?=.*[A-Za-z]) : Phải chứa ít nhất 1 chữ cái
            // (?=.*\d)       : Phải chứa ít nhất 1 số
            // [A-Za-z\d]     : Chỉ chấp nhận chữ và số (Không ký tự đặc biệt)
            // {8,}           : Độ dài tối thiểu 8 ký tự
            // $              : Kết thúc chuỗi
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!Regex.IsMatch(txtMatKhauMoi.Text, passwordPattern))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ và số, KHÔNG chứa ký tự đặc biệt.",
                               "Mật khẩu yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return false;
            }

            return true;
        }

        private async void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            btnDatLaiMatKhau.Enabled = false;
            btnDatLaiMatKhau.Text = "Đang xử lý...";
            Cursor = Cursors.WaitCursor;

            try
            {
                var result = await _nguoiDungService.ResetPasswordAsync(txtEmail.Text.Trim(), txtMatKhauMoi.Text);

                switch (result)
                {
                    case ResetPasswordResult.Success:
                        MessageBox.Show("✅ Đặt lại mật khẩu thành công!\n\nVui lòng đăng nhập lại.",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        QuayVeDangNhap?.Invoke();
                        break;

                    case ResetPasswordResult.EmailNotFound:
                        MessageBox.Show("❌ Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmail.Focus();
                        break;

                    default:
                        MessageBox.Show("❌ Lỗi hệ thống. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}");
            }
            finally
            {
                btnDatLaiMatKhau.Enabled = true;
                btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
                Cursor = Cursors.Default;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => QuayVeDangNhap?.Invoke();
    }
}