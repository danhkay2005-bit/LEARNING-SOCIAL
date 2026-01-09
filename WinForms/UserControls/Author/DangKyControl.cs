using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DTO.Requests.NguoiDung;
using System;
using System.Windows.Forms;

namespace WinForms.UserControls.Author
{
    public partial class DangKyControl : UserControl
    {
        private readonly INguoiDungService _nguoiDungService;

        public event Action? RequestBackToLogin;

        public DangKyControl(INguoiDungService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            _nguoiDungService.Register(new DangKyNguoiDungRequest
            {
                TenDangNhap = txtTenDangNhap.Text,
                MatKhau = txtMatKhau.Text,
                XacNhanMatKhau = txtXacNhanMatKhau.Text
            });

            MessageBox.Show("✅ Đăng ký thành công");
            RequestBackToLogin?.Invoke();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RequestBackToLogin?.Invoke();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            RequestBackToLogin?.Invoke();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
        }
    }
}
