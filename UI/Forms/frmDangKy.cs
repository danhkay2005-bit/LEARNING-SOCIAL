using System;
using System.Windows.Forms;
using BLL;
using BLL.NguoiDung;

namespace UI.Forms
{
    public partial class frmDangKy : Form
    {
        private readonly NguoiDungBLL _nguoiDungBLL;

        public frmDangKy()
        {
            InitializeComponent();
            _nguoiDungBLL = new NguoiDungBLL();
        }

        // ============================================================
        // SỰ KIỆN ĐĂNG KÝ
        // ============================================================

        private void BtnDangKy_Click(object sender, EventArgs e)  // ✅ Viết hoa chữ cái đầu
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string xacNhan = txtXacNhan.Text;

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(username))
            {
                HienThongBao("Vui lòng nhập tên đăng nhập!", true);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                HienThongBao("Vui lòng nhập email!", true);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                HienThongBao("Vui lòng nhập mật khẩu!", true);
                txtPassword.Focus();
                return;
            }

            if (password != xacNhan)
            {
                HienThongBao("Xác nhận mật khẩu không khớp!", true);
                txtXacNhan.Clear();
                txtXacNhan.Focus();
                return;
            }

            btnDangKy.Enabled = false;
            btnDangKy.Text = "Đang xử lý...";

            try
            {
                KetQua ketQua = _nguoiDungBLL.DangKy(username, password, email);

                if (ketQua.ThanhCong)
                {
                    MessageBox.Show("Đăng ký thành công!\nVui lòng đăng nhập.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ChuyenSangDangNhap();
                }
                else
                {
                    HienThongBao(ketQua.ThongBao ?? "Dang ky khong thanh cong", true);
                }
            }
            catch (Exception ex)
            {
                HienThongBao("Lỗi:  " + ex.Message, true);
            }
            finally
            {
                btnDangKy.Enabled = true;
                btnDangKy.Text = "ĐĂNG KÝ";
            }
        }

        // ============================================================
        // HIỆN/ẨN MẬT KHẨU
        // ============================================================

        private void ChkHienMatKhau_CheckedChanged(object sender, EventArgs e)  // ✅ Viết hoa chữ cái đầu
        {
            char c = chkHienMatKhau.Checked ? '\0' : '●';
            txtPassword.PasswordChar = c;
            txtXacNhan.PasswordChar = c;
        }

        // ============================================================
        // CHUYỂN SANG ĐĂNG NHẬP
        // ============================================================

        private void LnkDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)  // ✅ Viết hoa chữ cái đầu
        {
            ChuyenSangDangNhap();
        }

        private void ChuyenSangDangNhap()
        {
            var frm = new frmDangNhap();
            frm.Show();
            this.Close();
        }

        // ============================================================
        // HIỂN THỊ THÔNG BÁO
        // ============================================================

        private void HienThongBao(string thongBao, bool laLoi)
        {
            lblThongBao.Text = thongBao;
            lblThongBao.ForeColor = laLoi ? Color.Red : Color.Green;
        }
    }
}