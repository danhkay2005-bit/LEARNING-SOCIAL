using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.UserControls
{
    public partial class DangKyControl : UserControl
    {
        private readonly IAuthService _nguoiDungService;
        public event Action? QuayVeDangNhap;

        public DangKyControl(IAuthService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;
            txtMatKhau.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // 2. Kích hoạt tính năng "Ấn giữ để xem"
            SetupPasswordVisibility();
        }
        

        private void SetupPasswordVisibility()
        {
            // A. Cho ô Mật khẩu chính (btnXemMatKhau)
            // Khi nhấn chuột xuống -> Hiện chữ
            btnXemMatKhau.MouseDown += (s, e) => {
                txtMatKhau.UseSystemPasswordChar = false;
            };

            // Khi nhả chuột ra -> Ẩn thành dấu chấm
            btnXemMatKhau.MouseUp += (s, e) => {
                txtMatKhau.UseSystemPasswordChar = true;
            };

            // [An toàn] Khi kéo chuột ra khỏi nút mà chưa nhả -> Cũng ẩn luôn
            btnXemMatKhau.MouseLeave += (s, e) => {
                txtMatKhau.UseSystemPasswordChar = true;
            };

            // B. Cho ô Xác nhận mật khẩu (btnXemXacNhan)
            btnXemXacNhan.MouseDown += (s, e) => {
                txtXacNhanMatKhau.UseSystemPasswordChar = false;
            };

            btnXemXacNhan.MouseUp += (s, e) => {
                txtXacNhanMatKhau.UseSystemPasswordChar = true;
            };

            btnXemXacNhan.MouseLeave += (s, e) => {
                txtXacNhanMatKhau.UseSystemPasswordChar = true;
            };
        }

        private bool ValidateInput()
        {
            // Kiểm tra trống
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtHoVaTen.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra khớp mật khẩu
            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("❌ Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtXacNhanMatKhau.Focus();
                return false;
            }

            // Kiểm tra độ mạnh (Ít nhất 8 ký tự, Chữ + Số, KHÔNG ký tự đặc biệt)
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!Regex.IsMatch(txtMatKhau.Text, passwordPattern))
            {
                MessageBox.Show("⚠️ Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ và số, KHÔNG chứa ký tự đặc biệt.",
                                "Mật khẩu yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            return true;
        }


        private async void btnDangKy_Click(object sender, EventArgs e)
        {

            if (!ValidateInput())
            {
                return;
            }

            btnDangKy.Enabled = false;

            try
            {
                var result = await _nguoiDungService.RegisterAsync(new DangKyNguoiDungRequest
                {
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    HoVaTen = txtHoVaTen.Text,
                    MatKhau = txtMatKhau.Text,
                    XacNhanMatKhau = txtXacNhanMatKhau.Text
                });

                if (result != RegisterResult.Success)
                {
                    string msg = "❌ Đăng ký không thành công!";
                    if (result == RegisterResult.UsernameExists) msg = "Tên đăng nhập đã tồn tại.";
                    else if (result == RegisterResult.EmailExists) msg = "Email đã tồn tại.";

                    MessageBox.Show(msg);
                    return;
                }

                MessageBox.Show("✅ Đăng ký thành công! Vui lòng đăng nhập.");
                QuayVeDangNhap?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌  Lỗi hệ thống: {ex.Message}");
            }
            finally
            {
                btnDangKy.Enabled = true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            QuayVeDangNhap?.Invoke();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            QuayVeDangNhap?.Invoke();
        }
    }
}
