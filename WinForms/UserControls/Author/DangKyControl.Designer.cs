using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.UserControls.Author
{
    partial class DangKyControl : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblDangKyTaiKhoan = new Label();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblHoVaTen = new Label();
            txtHoVaTen = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();
            btnDangKy = new Button();
            btnHuy = new Button();
            lblDaCoTaiKhoan = new Label();
            btnDangNhap = new Button();

            SuspendLayout();

            // lblDangKyTaiKhoan
            lblDangKyTaiKhoan.AutoSize = true;
            lblDangKyTaiKhoan.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDangKyTaiKhoan.Location = new Point(55, 28);
            lblDangKyTaiKhoan.Text = "ĐĂNG KÝ TÀI KHOẢN";

            // lblTenDangNhap
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(12, 112);
            lblTenDangNhap.Text = "Tên Đăng Nhập(*):";

            // txtTenDangNhap
            txtTenDangNhap.Location = new Point(154, 104);
            txtTenDangNhap.PlaceholderText = "Nhập Username / Email";
            txtTenDangNhap.Size = new Size(164, 23);

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 145);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(154, 133);
            txtEmail.PlaceholderText = "abc123@gmail.com";
            txtEmail.Size = new Size(164, 23);

            // lblHoVaTen
            lblHoVaTen.AutoSize = true;
            lblHoVaTen.Location = new Point(12, 179);
            lblHoVaTen.Text = "Họ và Tên:";

            // txtHoVaTen
            txtHoVaTen.Location = new Point(154, 171);
            txtHoVaTen.PlaceholderText = "Nhập họ và tên (không bắt buộc)";
            txtHoVaTen.Size = new Size(164, 23);

            // lblMatKhau
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(12, 212);
            lblMatKhau.Text = "Mật Khẩu(*):";

            // txtMatKhau
            txtMatKhau.Location = new Point(154, 204);
            txtMatKhau.Size = new Size(164, 23);
            txtMatKhau.UseSystemPasswordChar = true;

            // lblXacNhanMatKhau
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Location = new Point(12, 246);
            lblXacNhanMatKhau.Text = "Xác Nhận Mật Khẩu(*):";

            // txtXacNhanMatKhau
            txtXacNhanMatKhau.Location = new Point(154, 238);
            txtXacNhanMatKhau.Size = new Size(164, 23);
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // btnDangKy
            btnDangKy.BackColor = Color.FromArgb(255, 128, 0);
            btnDangKy.Font = new Font("Segoe UI", 14.25F);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(116, 295);
            btnDangKy.Size = new Size(138, 48);
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;

            // lblDaCoTaiKhoan
            lblDaCoTaiKhoan.AutoSize = true;
            lblDaCoTaiKhoan.Location = new Point(83, 393);
            lblDaCoTaiKhoan.Text = "Đã Có Tài Khoản?";

            // btnDangNhap
            btnDangNhap.Location = new Point(188, 389);
            btnDangNhap.Size = new Size(75, 23);
            btnDangNhap.Text = "Đăng Nhập";
            btnDangNhap.Click += btnDangNhap_Click;

            // btnHuy
            btnHuy.Location = new Point(301, 389);
            btnHuy.Size = new Size(59, 23);
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;

            // UserControl
            Controls.AddRange(new Control[]
            {
                lblDangKyTaiKhoan, lblTenDangNhap, txtTenDangNhap,
                lblEmail, txtEmail, lblHoVaTen, txtHoVaTen,
                lblMatKhau, txtMatKhau, lblXacNhanMatKhau, txtXacNhanMatKhau,
                btnDangKy, lblDaCoTaiKhoan, btnDangNhap, btnHuy
            });

            Size = new Size(368, 434);
            Name = "DangKy";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDangKyTaiKhoan;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblHoVaTen;
        private TextBox txtHoVaTen;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Label lblXacNhanMatKhau;
        private TextBox txtXacNhanMatKhau;
        private Button btnDangKy;
        private Button btnHuy;
        private Label lblDaCoTaiKhoan;
        private Button btnDangNhap;
    }
}
