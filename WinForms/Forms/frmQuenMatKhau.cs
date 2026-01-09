using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Services.Interfaces.User;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class frmQuenMatKhau : Form
    {
        private readonly INguoiDungService _nguoiDungService;

        public frmQuenMatKhau()
        {
            InitializeComponent();
            _nguoiDungService = Program.ServiceProvider.GetRequiredService<INguoiDungService>();

            ConfigureForm();
            RegisterEvents();
        }

        private void ConfigureForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "Quên Mật Khẩu - StudyApp";

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
                    btnDatLaiMatKhau_Click(btnDatLaiMatKhau, EventArgs.Empty);
                    e.Handled = true;
                }
            };
        }

        private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
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

                string email = txtEmail.Text.Trim();
                string newPassword = txtMatKhauMoi.Text;

                var result = _nguoiDungService.ResetPassword(email, newPassword);

                switch (result)
                {
                    case ResetPasswordResult.Success:
                        MessageBox.Show("✅ Đặt lại mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                        break;

                    case ResetPasswordResult.EmailNotFound:
                        MessageBox.Show("❌ Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        break;

                    default:
                        MessageBox.Show("❌ Không thể đặt lại mật khẩu. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ Lỗi:\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInput()
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("⚠️ Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("⚠️ Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                txtEmail.SelectAll();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return false;
            }

            if (txtMatKhauMoi.Text.Length < 6 || txtMatKhauMoi.Text.Length > 100)
            {
                MessageBox.Show("⚠️ Mật khẩu phải từ 6-100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return false;
            }

            if (!string.Equals(txtMatKhauMoi.Text, txtXacNhanMatKhau.Text, StringComparison.Ordinal))
            {
                MessageBox.Show("⚠️ Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Clear();
                txtXacNhanMatKhau.Focus();
                return false;
            }

            return true;
        }
    }
}
