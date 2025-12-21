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
            lblThongBao = new Label();
            lnkDangKy = new LinkLabel();
            lblChuaCo = new Label();
            btnDangNhap = new Button();
            chkHienMatKhau = new CheckBox();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblTitle = new Label();
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
            pnlMain.Location = new Point(229, 80);
            pnlMain.Margin = new Padding(3, 4, 3, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(457, 506);
            pnlMain.TabIndex = 0;
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI", 9F);
            lblThongBao.ForeColor = Color.Red;
            lblThongBao.Location = new Point(46, 467);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(366, 27);
            lblThongBao.TabIndex = 9;
            lblThongBao.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lnkDangKy
            // 
            lnkDangKy.AutoSize = true;
            lnkDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lnkDangKy.Location = new Point(263, 433);
            lnkDangKy.Name = "lnkDangKy";
            lnkDangKy.Size = new Size(77, 23);
            lnkDangKy.TabIndex = 8;
            lnkDangKy.TabStop = true;
            lnkDangKy.Text = "Đăng ký";
            lnkDangKy.LinkClicked += LnkDangKy_LinkClicked;
            // 
            // lblChuaCo
            // 
            lblChuaCo.AutoSize = true;
            lblChuaCo.Font = new Font("Segoe UI", 10F);
            lblChuaCo.Location = new Point(103, 433);
            lblChuaCo.Name = "lblChuaCo";
            lblChuaCo.Size = new Size(157, 23);
            lblChuaCo.TabIndex = 7;
            lblChuaCo.Text = "Chưa có tài khoản?";
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(0, 122, 204);
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(46, 353);
            btnDangNhap.Margin = new Padding(3, 4, 3, 4);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(366, 60);
            btnDangNhap.TabIndex = 6;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += BtnDangNhap_Click;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Font = new Font("Segoe UI", 10F);
            chkHienMatKhau.Location = new Point(46, 300);
            chkHienMatKhau.Margin = new Padding(3, 4, 3, 4);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(144, 27);
            chkHienMatKhau.TabIndex = 5;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += ChkHienMatKhau_CheckedChanged;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(46, 247);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(365, 34);
            txtPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 11F);
            lblPassword.Location = new Point(46, 213);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(95, 25);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(46, 153);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(365, 34);
            txtUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 11F);
            lblUsername.Location = new Point(46, 120);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(141, 25);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(0, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(457, 67);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔐 Đăng Nhập";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(914, 667);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập - Flashcard App";
            Load += frmDangNhap_Load;
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