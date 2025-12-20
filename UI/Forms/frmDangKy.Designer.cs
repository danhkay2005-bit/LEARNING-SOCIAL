namespace UI.Forms
{
    partial class frmDangKy
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
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblXacNhan = new Label();
            txtXacNhan = new TextBox();
            chkHienMatKhau = new CheckBox();
            btnDangKy = new Button();
            lblDaCo = new Label();
            lnkDangNhap = new LinkLabel();
            lblThongBao = new Label();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(lblThongBao);
            pnlMain.Controls.Add(lnkDangNhap);
            pnlMain.Controls.Add(lblDaCo);
            pnlMain.Controls.Add(btnDangKy);
            pnlMain.Controls.Add(chkHienMatKhau);
            pnlMain.Controls.Add(txtXacNhan);
            pnlMain.Controls.Add(lblXacNhan);
            pnlMain.Controls.Add(txtPassword);
            pnlMain.Controls.Add(lblPassword);
            pnlMain.Controls.Add(txtEmail);
            pnlMain.Controls.Add(lblEmail);
            pnlMain.Controls.Add(txtUsername);
            pnlMain.Controls.Add(lblUsername);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Location = new Point(200, 30);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(400, 500);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(0, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📝 Đăng Ký";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 11F);
            lblUsername.Location = new Point(40, 75);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(118, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập: ";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(40, 98);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(320, 29);
            txtUsername.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 11F);
            lblEmail.Location = new Point(40, 135);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(48, 20);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email: ";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.Location = new Point(40, 158);
            txtEmail.MaxLength = 255;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(320, 29);
            txtEmail.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 11F);
            lblPassword.Location = new Point(40, 195);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(76, 20);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(40, 218);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(320, 29);
            txtPassword.TabIndex = 6;
            // 
            // lblXacNhan
            // 
            lblXacNhan.AutoSize = true;
            lblXacNhan.Font = new Font("Segoe UI", 11F);
            lblXacNhan.Location = new Point(40, 255);
            lblXacNhan.Name = "lblXacNhan";
            lblXacNhan.Size = new Size(143, 20);
            lblXacNhan.TabIndex = 7;
            lblXacNhan.Text = "Xác nhận mật khẩu:";
            // 
            // txtXacNhan
            // 
            txtXacNhan.Font = new Font("Segoe UI", 12F);
            txtXacNhan.Location = new Point(40, 278);
            txtXacNhan.MaxLength = 100;
            txtXacNhan.Name = "txtXacNhan";
            txtXacNhan.PasswordChar = '●';
            txtXacNhan.Size = new Size(320, 29);
            txtXacNhan.TabIndex = 8;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Font = new Font("Segoe UI", 10F);
            chkHienMatKhau.Location = new Point(40, 315);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(117, 23);
            chkHienMatKhau.TabIndex = 9;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += ChkHienMatKhau_CheckedChanged;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(40, 167, 69);
            btnDangKy.Cursor = Cursors.Hand;
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(40, 355);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(320, 45);
            btnDangKy.TabIndex = 10;
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += BtnDangKy_Click;
            // 
            // lblDaCo
            // 
            lblDaCo.AutoSize = true;
            lblDaCo.Font = new Font("Segoe UI", 10F);
            lblDaCo.Location = new Point(95, 415);
            lblDaCo.Name = "lblDaCo";
            lblDaCo.Size = new Size(119, 19);
            lblDaCo.TabIndex = 11;
            lblDaCo.Text = "Đã có tài khoản? ";
            // 
            // lnkDangNhap
            // 
            lnkDangNhap.AutoSize = true;
            lnkDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lnkDangNhap.Location = new Point(218, 415);
            lnkDangNhap.Name = "lnkDangNhap";
            lnkDangNhap.Size = new Size(81, 19);
            lnkDangNhap.TabIndex = 12;
            lnkDangNhap.TabStop = true;
            lnkDangNhap.Text = "Đăng nhập";
            lnkDangNhap.LinkClicked += LnkDangNhap_LinkClicked;
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI", 9F);
            lblThongBao.ForeColor = Color.Red;
            lblThongBao.Location = new Point(40, 450);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(320, 40);
            lblThongBao.TabIndex = 13;
            lblThongBao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmDangKy
            // 
            AcceptButton = btnDangKy;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(800, 570);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmDangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Ký - Flashcard App";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblXacNhan;
        private TextBox txtXacNhan;
        private CheckBox chkHienMatKhau;
        private Button btnDangKy;
        private Label lblDaCo;
        private LinkLabel lnkDangNhap;
        private Label lblThongBao;
    }
}