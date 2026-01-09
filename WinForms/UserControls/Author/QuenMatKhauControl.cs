using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Interfaces.User;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.UserControls.Author
{
    public partial class QuenMatKhauControl : UserControl
    {
        private readonly INguoiDungService _nguoiDungService;

        // ⭐ Event để mainForm điều hướng
        public event Action? RequestBackToLogin;

        public QuenMatKhauControl(INguoiDungService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;

            ConfigureControl();
            RegisterEvents();
        }

        private void ConfigureControl()
        {
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            Load += (s, e) => txtEmail.Focus();
        }

        private void RegisterEvents()
        {
            txtEmail.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtMatKhauMoi.Focus();
                    e.Handled = true;
                }
            };

            txtMatKhauMoi.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtXacNhanMatKhau.Focus();
                    e.Handled = true;
                }
            };

            txtXacNhanMatKhau.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    DatLaiMatKhau();
                    e.Handled = true;
                }
            };
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            DatLaiMatKhau();
        }

        private void DatLaiMatKhau()
        {
            try
            {
                if (!ValidateInput())
                    return;

                btnDatLaiMatKhau.Enabled = false;
                btnDatLaiMatKhau.Text = "Đang xử lý...";
                Cursor = Cursors.WaitCursor;

                var email = txtEmail.Text.Trim();
                var newPassword = txtMatKhauMoi.Text;

                var result = _nguoiDungService.ResetPassword(email, newPassword);

                switch (result)
                {
                    case ResetPasswordResult.Success:
                        MessageBox.Show(
                            "✅ Đặt lại mật khẩu thành công!\n\nVui lòng đăng nhập lại.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        RequestBackToLogin?.Invoke();
                        break;

                    case ResetPasswordResult.EmailNotFound:
                        MessageBox.Show(
                            "❌ Email không tồn tại!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        break;

                    default:
                        MessageBox.Show(
                            "❌ Không thể đặt lại mật khẩu.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"⚠️ Lỗi hệ thống:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnDatLaiMatKhau.Enabled = true;
                btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
                Cursor = Cursors.Default;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RequestBackToLogin?.Invoke();
        }

        private bool ValidateInput()
        {
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("⚠️ Vui lòng nhập email!");
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("⚠️ Email không hợp lệ!");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return false;
            }

            if (txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("⚠️ Mật khẩu tối thiểu 6 ký tự!");
                txtMatKhauMoi.Focus();
                return false;
            }

            if (!txtMatKhauMoi.Text.Equals(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("⚠️ Mật khẩu xác nhận không khớp!");
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return false;
            }

            return true;
        }
    }
}
