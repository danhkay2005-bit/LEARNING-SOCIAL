namespace UI.Forms
{
    partial class frmDangNhap
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkHienMatKhau = new CheckBox();
            btnDangNhap = new Button();
            lblChuaCo = new Label();
            lnkDangKy = new LinkLabel();
            lblThongBao = new Label();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(lblThongBao);
            pnlMain.Controls.Add(lnkDangKy);
            pnlMain.Controls.Add(lblChuaCo);
            pnlMain.Controls.Add(btnDangNhap);
            pnlMain.Controls.Add(chkHienMatKhau);
            pnlMain.Controls.Add(txtPassword);
            pnlMain.Controls.Add(lblPassword);
            pnlMain.Controls.Add(txtUsername);
            pnlMain.Controls.Add(lblUsername);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Location = new Point(200, 60);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(400, 380);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(0, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔐 Đăng Nhập";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 11F);
            lblUsername.Location = new Point(40, 90);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(118, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(40, 115);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(320, 29);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 11F);
            lblPassword.Location = new Point(40, 160);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(76, 20);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(40, 185);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(320, 29);
            txtPassword.TabIndex = 4;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Font = new Font("Segoe UI", 10F);
            chkHienMatKhau.Location = new Point(40, 225);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(117, 23);
            chkHienMatKhau.TabIndex = 5;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += ChkHienMatKhau_CheckedChanged;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(0, 122, 204);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(40, 265);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(320, 45);
            btnDangNhap.TabIndex = 6;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += BtnDangNhap_Click;
            // 
            // lblChuaCo
            // 
            lblChuaCo.AutoSize = true;
            lblChuaCo.Font = new Font("Segoe UI", 10F);
            lblChuaCo.Location = new Point(90, 325);
            lblChuaCo.Name = "lblChuaCo";
            lblChuaCo.Size = new Size(138, 19);
            lblChuaCo.TabIndex = 7;
            lblChuaCo.Text = "Chưa có tài khoản?";
            // 
            // lnkDangKy
            // 
            lnkDangKy.AutoSize = true;
            lnkDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lnkDangKy.Location = new Point(230, 325);
            lnkDangKy.Name = "lnkDangKy";
            lnkDangKy.Size = new Size(64, 19);
            lnkDangKy.TabIndex = 8;
            lnkDangKy.TabStop = true;
            lnkDangKy.Text = "Đăng ký";
            lnkDangKy.LinkClicked += LnkDangKy_LinkClicked;
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI", 9F);
            lblThongBao.ForeColor = Color.Red;
            lblThongBao.Location = new Point(40, 350);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(320, 20);
            lblThongBao.TabIndex = 9;
            lblThongBao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(800, 500);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập - Flashcard App";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkHienMatKhau;
        private Button btnDangNhap;
        private Label lblChuaCo;
        private LinkLabel lnkDangKy;
        private Label lblThongBao;
    }
}