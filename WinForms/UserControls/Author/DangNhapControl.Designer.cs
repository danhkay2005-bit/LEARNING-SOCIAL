namespace WinForms.UserControls.Author
{
    partial class DangNhapControl
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DangNhapControl));

            lblDangNhapVoiTaiKhoan = new Label();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            btnQuenMatKhau = new Button();
            btnGoogleLogin = new Button();
            lblDangNhapVoi = new Label();
            lblBanCoTaiKhoanChua = new Label();
            btnDangKy = new Button();

            SuspendLayout();

            // lblDangNhapVoiTaiKhoan
            lblDangNhapVoiTaiKhoan.AutoSize = true;
            lblDangNhapVoiTaiKhoan.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold);
            lblDangNhapVoiTaiKhoan.Location = new Point(43, 93);
            lblDangNhapVoiTaiKhoan.Text = "ĐĂNG NHẬP VỚI TÀI KHOẢN";

            // lblTenDangNhap
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Font = new Font("Times New Roman", 14.25F);
            lblTenDangNhap.Location = new Point(30, 163);
            lblTenDangNhap.Text = "Tên Đăng Nhập:";

            // txtTenDangNhap
            txtTenDangNhap.Location = new Point(30, 187);
            txtTenDangNhap.PlaceholderText = "Nhập username hoặc email";
            txtTenDangNhap.Size = new Size(315, 23);

            // lblMatKhau
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Times New Roman", 14.25F);
            lblMatKhau.Location = new Point(30, 223);
            lblMatKhau.Text = "Mật Khẩu:";

            // txtMatKhau
            txtMatKhau.Location = new Point(30, 247);
            txtMatKhau.Size = new Size(315, 23);
            txtMatKhau.UseSystemPasswordChar = true;

            // btnDangNhap
            btnDangNhap.BackColor = Color.FromArgb(255, 128, 0);
            btnDangNhap.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(30, 327);
            btnDangNhap.Size = new Size(315, 37);
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;

            // btnQuenMatKhau
            btnQuenMatKhau.Location = new Point(242, 276);
            btnQuenMatKhau.Size = new Size(103, 23);
            btnQuenMatKhau.Text = "Quên Mật Khẩu";
            btnQuenMatKhau.Click += btnQuenMatKhau_Click;

            // lblBanCoTaiKhoanChua
            lblBanCoTaiKhoanChua.AutoSize = true;
            lblBanCoTaiKhoanChua.Location = new Point(116, 401);
            lblBanCoTaiKhoanChua.Text = "Bạn chưa có tài khoản?";

            // btnDangKy
            btnDangKy.Location = new Point(270, 393);
            btnDangKy.Size = new Size(75, 30);
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.Click += btnDangKy_Click;

            // lblDangNhapVoi
            lblDangNhapVoi.AutoSize = true;
            lblDangNhapVoi.Location = new Point(-9, 425);
            lblDangNhapVoi.Text =
                "-----------------------------Đăng Nhập Với-----------------------------";

            Controls.AddRange(new Control[]
            {
                lblDangNhapVoiTaiKhoan,
                lblTenDangNhap,
                txtTenDangNhap,
                lblMatKhau,
                txtMatKhau,
                btnQuenMatKhau,
                btnDangNhap,
                lblBanCoTaiKhoanChua,
                btnDangKy,
                lblDangNhapVoi,
                btnGoogleLogin
            });

            Name = "DangNhapControl";
            Size = new Size(380, 509);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDangNhapVoiTaiKhoan;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Button btnQuenMatKhau;
        private Button btnGoogleLogin;
        private Label lblDangNhapVoi;
        private Label lblBanCoTaiKhoanChua;
        private Button btnDangKy;
    }
}
