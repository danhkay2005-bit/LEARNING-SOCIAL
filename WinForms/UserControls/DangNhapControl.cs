using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using System;
using System.Windows.Forms;

namespace WinForms.UserControls
{
    public partial class DangNhapControl : UserControl
    {
        private readonly IAuthService _nguoiDungService;

        public event Action? DangNhapThanhCong;
        public event Action? YeuCauDangKy;
        public event Action? QuenMatKhau;

        public DangNhapControl(IAuthService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            btnDangNhap.Enabled = false;

            try
            {
                var (result, user) = await _nguoiDungService.LoginAsync(new DangNhapRequest
                {
                    TenDangNhap = txtTenDangNhap.Text,
                    MatKhau = txtMatKhau.Text
                });

                if (result == LoginResult.Success && user != null)
                {
                    UserSession.Login(user);
                    DangNhapThanhCong?.Invoke();
                    return;
                }

                if (result == LoginResult.InvalidCredentials)
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                    return;
                }

                if (result == LoginResult.UserNotFound)
                {
                    MessageBox.Show("Tài khoản không tồn tại.");
                    return;
                }

                MessageBox.Show("Đăng nhập thất bại.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            YeuCauDangKy?.Invoke();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            QuenMatKhau?.Invoke();
        }
    }
}
