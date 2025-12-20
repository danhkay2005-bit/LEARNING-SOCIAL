using System;
using System.Windows.Forms;
using BLL;
using BLL.NguoiDung;
using Common;

namespace UI.Forms
{
    public partial class frmDangNhap : Form
    {
        private readonly NguoiDungBLL _nguoiDungBLL;

        public frmDangNhap()
        {
            InitializeComponent();
            _nguoiDungBLL = new NguoiDungBLL();
        }

        // ============================================================
        // SỰ KIỆN ĐĂNG NHẬP
        // ============================================================

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(username))
            {
                HienThongBao("Vui lòng nhập tên đăng nhập!", true);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                HienThongBao("Vui lòng nhập mật khẩu!", true);
                txtPassword.Focus();
                return;
            }

            // Disable nút
            btnDangNhap.Enabled = false;
            btnDangNhap.Text = "Đang xử lý...";

            try
            {
                KetQua ketQua = _nguoiDungBLL.DangNhap(username, password);

                if (ketQua.ThanhCong)
                {
                    HienThongBao("Đăng nhập thành công!", false);

                    // Mở form chính
                    // frmMain frmMain = new frmMain();
                    // frmMain.Show();
                    // this.Hide();

                    MessageBox.Show($"Xin chào {SessionManager.CurrentHoTen}!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HienThongBao(ketQua.ThongBao ?? "Dang nhap that bai", true);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                HienThongBao("Lỗi:  " + ex.Message, true);
            }
            finally
            {
                btnDangNhap.Enabled = true;
                btnDangNhap.Text = "ĐĂNG NHẬP";
            }
        }

        // ============================================================
        // HIỆN/ẨN MẬT KHẨU
        // ============================================================

        private void ChkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkHienMatKhau.Checked ? '\0' : '●';
        }

        // ============================================================
        // CHUYỂN FORM ĐĂNG KÝ
        // ============================================================

        private void LnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frmDangKy = new frmDangKy();
            frmDangKy.Show();
            this.Hide();
        }

        // ============================================================
        // HIỂN THỊ THÔNG BÁO
        // ============================================================

        private void HienThongBao(string thongBao, bool laLoi)
        {
            lblThongBao.Text = thongBao;
            lblThongBao.ForeColor = laLoi ? Color.Red : Color.Green;
        }

        // ============================================================
        // ĐÓNG FORM
        // ============================================================

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}