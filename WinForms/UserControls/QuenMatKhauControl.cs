using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO.Enums;
using System;
using System.Text.RegularExpressions;
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
            ConfigureControl();
            RegisterEvents();
        }

        public void ConfigureControl()
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

            txtXacNhanMatKhau.KeyPress += async (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    await DatLaiMatKhauAsync();
                    e.Handled = true;
                }
            };
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
        {
            _ = DatLaiMatKhauAsync();
        }

        private bool ValidateInput()
        {
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email!");
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return false;
            }

            if (txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu tối thiểu 6 ký tự!");
                txtMatKhauMoi.Focus();
                return false;
            }

            if (!txtMatKhauMoi.Text.Equals(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return false;
            }

            return true;
        }

        private async System.Threading.Tasks.Task DatLaiMatKhauAsync()
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                btnDatLaiMatKhau.Enabled = false;
                btnDatLaiMatKhau.Text = "Đang xử lý...";
                Cursor = Cursors.WaitCursor;

                var email = txtEmail.Text.Trim();
                var newPassword = txtMatKhauMoi.Text;

                var result = await _nguoiDungService.ResetPasswordAsync(email, newPassword);

                switch (result)
                {
                    case ResetPasswordResult.Success:
                        MessageBox.Show(
                            "Đặt lại mật khẩu thành công!\n\nVui lòng đăng nhập lại.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        QuayVeDangNhap?.Invoke();
                        break;

                    case ResetPasswordResult.EmailNotFound:
                        MessageBox.Show(
                            "Email không tồn tại!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        break;

                    default:
                        MessageBox.Show(
                            "Không thể đặt lại mật khẩu.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
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
            QuayVeDangNhap?.Invoke();
        }
    }
}
