using StudyApp.DAL.Repositories;
using StudyApp.DTO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly NguoiDungRepository _nguoiDungRepo;

        public frmDangNhap()
        {
            InitializeComponent();
            _nguoiDungRepo = new NguoiDungRepository();
            
            ConfigureForm();
            RegisterEvents();
        }

        private void ConfigureForm()
        {
            // Cấu hình form 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Đăng Nhập - StudyApp";

            // Cấu hình textbox mật khẩu
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.UseSystemPasswordChar = true;

            this.Load += (s, e) => txtTenDangNhap.Focus();
        }

        private void RegisterEvents()
        {
            // Enter ở username → chuyển sang password
            txtTenDangNhap.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtMatKhau.Focus();
                    e.Handled = true;
                }
            };

            // Enter ở password → đăng nhập luôn
            txtMatKhau.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    btnDangNhap_Click(s, e);
                    e.Handled = true;
                }
            };
        }
            
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                {
                    return;
                }

                // Disable button
                btnDangNhap.Enabled = false;
                btnDangNhap.Text = "Đang xử lý...";
                this.Cursor = Cursors.WaitCursor;

                // Lấy thông tin
                string username = txtTenDangNhap.Text.Trim();
                string password = txtMatKhau.Text;

                // ✅ TÌM USER
                var user = _nguoiDungRepo.GetUserByUsername(username);

                if (user == null)
                {
                    MessageBox.Show(
                        "❌ Tên đăng nhập không tồn tại!\n\n" +
                        "Vui lòng kiểm tra lại hoặc đăng ký tài khoản mới.",
                        "Lỗi đăng nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtTenDangNhap.Focus();
                    txtTenDangNhap.SelectAll();
                    return;
                }

                // ✅ KIỂM TRA MẬT KHẨU
                string hashedPassword = HashPassword(password);

                if (user.MatKhauMaHoa != hashedPassword)
                {
                    MessageBox.Show(
                        "❌ Mật khẩu không chính xác!\n\n" +
                        "Vui lòng thử lại hoặc click 'Quên Mật Khẩu'.",
                        "Lỗi đăng nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                // ✅ ĐĂNG NHẬP THÀNH CÔNG
                UserSession.CurrentUser = user;
                _nguoiDungRepo.UpdateOnlineStatus(user.MaNguoiDung, true);

                MessageBox.Show(
                    $"✅ Đăng nhập thành công!\n\n" +
                    $"Chào mừng {user.HoVaTen ?? user.TenDangNhap}!\n\n" +
                    $"💰 Vàng: {user.Vang:N0}\n" +
                    $"💎 Kim cương: {user.KimCuong:N0}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
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
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP";
                this.Cursor = Cursors.Default;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                // Ẩn form đăng nhập
                this.Hide();

                // Mở form đăng ký
                using (var formDangKy = new frmDangKy())
                {
                    var result = formDangKy.ShowDialog();

                    // Hiển thị lại
                    this.Show();

                    if (result == DialogResult.OK)
                    {
                        // Đăng ký thành công
                        txtTenDangNhap.Clear();
                        txtMatKhau.Clear();
                        txtTenDangNhap.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Show();
                MessageBox.Show($"⚠️ Lỗi:\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ✅ QUÊN MẬT KHẨU - MỞ FORM frmQuenMatKhau
            try
            {
                // ✅ ẨN FORM ĐĂNG NHẬP
                this.Hide();

                // ✅ MỞ FORM QUÊN MẬT KHẨU
                using (var formQuenMatKhau = new frmQuenMatKhau())
                {
                    var result = formQuenMatKhau.ShowDialog();

                    // ✅ HIỂN THỊ LẠI FORM ĐĂNG NHẬP
                    this.Show();

                    if (result == DialogResult.OK)
                    {
                        // ✅ ĐẶT LẠI MẬT KHẨU THÀNH CÔNG
                        MessageBox.Show(
                            "✅ Đặt lại mật khẩu thành công!\n\n" +
                            "Vui lòng đăng nhập lại với mật khẩu mới.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        txtMatKhau.Clear();
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                        // User hủy đặt lại mật khẩu
                        txtTenDangNhap.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ Lỗi:\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoogleLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "🔑 Chức năng đăng nhập Google đang được phát triển!\n\n" +
                "Vui lòng sử dụng tài khoản thường.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #region Helpers

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!UserSession.IsLoggedIn && this.DialogResult != DialogResult.OK)
            {
                var result = MessageBox.Show(
                    "❓ Bạn chưa đăng nhập.\n\nBạn có chắc muốn thoát?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.OnFormClosing(e);
        }
    }
}
