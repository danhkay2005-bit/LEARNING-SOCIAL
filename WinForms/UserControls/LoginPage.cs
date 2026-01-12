using StudyApp.DTO;
using System;
using System.Windows.Forms;

namespace WinForms.UserControls.Pages
{
    public partial class LoginPage : UserControl
    {
        public event Action<NguoiDungDTO>? LoginSuccess;

        public LoginPage()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Nhập tên đăng nhập");
                return;
            }

            // DEMO LOGIN
            var user = new NguoiDungDTO
            {
                TenDangNhap = txtUsername.Text,
                HoVaTen = "Người dùng " + txtUsername.Text,
                Vang = 100,
                KimCuong = 10
            };

            LoginSuccess?.Invoke(user);
        }
    }
}
