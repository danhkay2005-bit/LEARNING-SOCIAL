namespace WinForms.Forms
{
    partial class frmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
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
            // 
            // lblDangNhapVoiTaiKhoan
            // 
            lblDangNhapVoiTaiKhoan.AutoSize = true;
            lblDangNhapVoiTaiKhoan.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDangNhapVoiTaiKhoan.Location = new Point(43, 93);
            lblDangNhapVoiTaiKhoan.Name = "lblDangNhapVoiTaiKhoan";
            lblDangNhapVoiTaiKhoan.Size = new Size(302, 24);
            lblDangNhapVoiTaiKhoan.TabIndex = 0;
            lblDangNhapVoiTaiKhoan.Text = "ĐĂNG NHẬP VỚI TÀI KHOẢN";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenDangNhap.Location = new Point(30, 163);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(133, 21);
            lblTenDangNhap.TabIndex = 1;
            lblTenDangNhap.Text = "Tên Đăng Nhập:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(30, 187);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "Nhập username hoặc email";
            txtTenDangNhap.Size = new Size(315, 23);
            txtTenDangNhap.TabIndex = 2;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMatKhau.Location = new Point(30, 223);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(87, 21);
            lblMatKhau.TabIndex = 3;
            lblMatKhau.Text = "Mật Khẩu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(30, 247);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(315, 23);
            txtMatKhau.TabIndex = 4;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(255, 128, 0);
            btnDangNhap.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangNhap.ForeColor = SystemColors.ButtonHighlight;
            btnDangNhap.Location = new Point(30, 327);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(315, 37);
            btnDangNhap.TabIndex = 5;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnQuenMatKhau
            // 
            btnQuenMatKhau.Location = new Point(242, 276);
            btnQuenMatKhau.Name = "btnQuenMatKhau";
            btnQuenMatKhau.Size = new Size(103, 23);
            btnQuenMatKhau.TabIndex = 6;
            btnQuenMatKhau.Text = "Quên Mật Khẩu";
            btnQuenMatKhau.UseVisualStyleBackColor = true;
            btnQuenMatKhau.Click += btnQuenMatKhau_Click;
            // 
            // btnGoogleLogin
            // 
            btnGoogleLogin.FlatStyle = FlatStyle.Flat;
            btnGoogleLogin.Image = (Image)resources.GetObject("btnGoogleLogin.Image");
            btnGoogleLogin.ImageAlign = ContentAlignment.MiddleLeft;
            btnGoogleLogin.Location = new Point(134, 454);
            btnGoogleLogin.Name = "btnGoogleLogin";
            btnGoogleLogin.Padding = new Padding(10, 0, 0, 0);
            btnGoogleLogin.Size = new Size(99, 43);
            btnGoogleLogin.TabIndex = 7;
            btnGoogleLogin.Text = "Google";
            btnGoogleLogin.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGoogleLogin.UseVisualStyleBackColor = true;
            btnGoogleLogin.Click += btnGoogleLogin_Click;
            // 
            // lblDangNhapVoi
            // 
            lblDangNhapVoi.AutoSize = true;
            lblDangNhapVoi.Location = new Point(-9, 425);
            lblDangNhapVoi.Name = "lblDangNhapVoi";
            lblDangNhapVoi.Size = new Size(377, 15);
            lblDangNhapVoi.TabIndex = 8;
            lblDangNhapVoi.Text = "-----------------------------Đăng Nhập Với-----------------------------";
            // 
            // lblBanCoTaiKhoanChua
            // 
            lblBanCoTaiKhoanChua.AutoSize = true;
            lblBanCoTaiKhoanChua.Location = new Point(116, 401);
            lblBanCoTaiKhoanChua.Name = "lblBanCoTaiKhoanChua";
            lblBanCoTaiKhoanChua.Size = new Size(148, 15);
            lblBanCoTaiKhoanChua.TabIndex = 9;
            lblBanCoTaiKhoanChua.Text = "Bạn đã có tài khoản chưa ?";
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(270, 393);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(75, 30);
            btnDangKy.TabIndex = 10;
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 509);
            Controls.Add(btnDangKy);
            Controls.Add(lblBanCoTaiKhoanChua);
            Controls.Add(lblDangNhapVoi);
            Controls.Add(btnGoogleLogin);
            Controls.Add(btnQuenMatKhau);
            Controls.Add(btnDangNhap);
            Controls.Add(txtMatKhau);
            Controls.Add(lblMatKhau);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblTenDangNhap);
            Controls.Add(lblDangNhapVoiTaiKhoan);
            Name = "frmDangNhap";
            Text = "DangNhap";
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