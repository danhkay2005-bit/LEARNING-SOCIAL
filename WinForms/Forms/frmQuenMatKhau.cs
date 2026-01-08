using StudyApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class frmQuenMatKhau : Form
    {
        private readonly NguoiDungRepository _nguoiDungRepo;
        public frmQuenMatKhau()
        {
            InitializeComponent();
            _nguoiDungRepo = new NguoiDungRepository();
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
                    btnDatLaiMatKhau_Click(sender: btnDatLaiMatKhau, e: EventArgs.Empty);
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
                var user = _nguoiDungRepo.GetUserByEmail(email);

                if (user == null)
                {
                    MessageBox.Show(
                        "❌ Email không tồn tại trong hệ thống!\n\nVui lòng kiểm tra lại.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return;
                }

                string hashedPassword = HashPasswordSha256(txtMatKhauMoi.Text);

                bool changed = _nguoiDungRepo.ChangePassword(email, hashedPassword);
                if (!changed)
                {
                    MessageBox.Show(
                        "❌ Không thể đặt lại mật khẩu.\n\nVui lòng thử lại sau.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(
                    "✅ Đặt lại mật khẩu thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
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

            if (!IsValidEmail(email))
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

            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("⚠️ Vui lòng xác nhận mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
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

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static string HashPasswordSha256(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
