using StudyApp.BLL.Services.Implementations.User;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DTO.Requests.NguoiDung;
using System;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class frmDangKy : Form
    {
        private readonly NguoiDungService _nguoiDungService;

        public frmDangKy()
        {
            InitializeComponent();
            _nguoiDungService = new NguoiDungService();

            ConfigureForm();
            RegisterEvents();
        }

        private void ConfigureForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "Đăng Ký - StudyApp";

            txtMatKhau.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            Load += (s, e) => txtTenDangNhap.Focus();
        }

        private void RegisterEvents()
        {
            txtTenDangNhap.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtEmail.Focus();
                    e.Handled = true;
                }
            };

            txtEmail.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtHoVaTen.Focus();
                    e.Handled = true;
                }
            };

            txtHoVaTen.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtMatKhau.Focus();
                    e.Handled = true;
                }
            };

            txtMatKhau.KeyPress += (s, e) =>
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
                    Register();
                    e.Handled = true;
                }
            };
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Register()
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                btnDangKy.Enabled = false;
                btnDangKy.Text = "Đang xử lý...";
                Cursor = Cursors.WaitCursor;

                var request = new DangKyNguoiDungRequest
                {
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                    HoVaTen = string.IsNullOrWhiteSpace(txtHoVaTen.Text) ? null : txtHoVaTen.Text.Trim(),
                    MatKhau = txtMatKhau.Text,
                    XacNhanMatKhau = txtXacNhanMatKhau.Text
                };

                var result = _nguoiDungService.Register(request);

                switch (result)
                {
                    case RegisterResult.Success:
                        MessageBox.Show(
                            "✅ Đăng ký thành công!\n\nVui lòng đăng nhập để tiếp tục.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DialogResult = DialogResult.OK;
                        Close();
                        break;

                    case RegisterResult.UsernameExists:
                        MessageBox.Show(
                            "❌ Tên đăng nhập hoặc email đã tồn tại!\n\nVui lòng chọn thông tin khác.",
                            "Lỗi đăng ký",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        txtTenDangNhap.Focus();
                        txtTenDangNhap.SelectAll();
                        break;

                    default:
                        MessageBox.Show(
                            "❌ Đăng ký thất bại. Vui lòng thử lại.",
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
                btnDangKy.Enabled = true;
                btnDangKy.Text = "ĐĂNG KÝ";
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (txtTenDangNhap.Text.Trim().Length < 3 || txtTenDangNhap.Text.Trim().Length > 50)
            {
                MessageBox.Show("⚠️ Tên đăng nhập phải từ 3-50 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text.Trim().Length > 255)
            {
                MessageBox.Show("⚠️ Email quá dài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtHoVaTen.Text) && txtHoVaTen.Text.Trim().Length > 100)
            {
                MessageBox.Show("⚠️ Họ và tên tối đa 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoVaTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (txtMatKhau.Text.Length < 6 || txtMatKhau.Text.Length > 100)
            {
                MessageBox.Show("⚠️ Mật khẩu phải từ 6-100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show("⚠️ Vui lòng xác nhận mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return false;
            }

            if (!string.Equals(txtMatKhau.Text, txtXacNhanMatKhau.Text, StringComparison.Ordinal))
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
