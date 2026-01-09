using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.NguoiDung;
using System;
using System.Windows.Forms;

namespace WinForms.UserControls.Author
{
    public partial class DangNhapControl : UserControl
    {
        private readonly INguoiDungService _nguoiDungService;

        public event Action? LoginSuccess;
        public event Action? RequestRegister;
        public event Action? RequestForgotPassword;

        public DangNhapControl(INguoiDungService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;

            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var result = _nguoiDungService.Login(new DangNhapRequest
            {
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MatKhau = txtMatKhau.Text
            });

            if (result != LoginResult.Success)
            {
                MessageBox.Show("❌ Sai tài khoản hoặc mật khẩu");
                return;
            }

            LoginSuccess?.Invoke();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            RequestRegister?.Invoke();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            RequestForgotPassword?.Invoke();
        }
    }
}
