using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.User;
using System;
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
        }

        private async void btnDangKy_Click(object sender, EventArgs e)
        {
            var result = await _nguoiDungService.RegisterAsync(new DangKyNguoiDungRequest
            {
                TenDangNhap = txtTenDangNhap.Text,
                Email = txtEmail.Text,
                HoVaTen = txtHoVaTen.Text,
                MatKhau = txtMatKhau.Text,
                XacNhanMatKhau = txtXacNhanMatKhau.Text
            });
                
            if (result != RegisterResult.Success)
            {
                MessageBox.Show("❌ Đăng ký không thành công!");
                return;
            }

            MessageBox.Show("✅ Đăng ký thành công!");
            QuayVeDangNhap?.Invoke();
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
