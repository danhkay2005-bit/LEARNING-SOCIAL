namespace WinForms.UserControls
{
    partial class DangKyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlCard = new Panel();
            lblTitle = new Label();
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
            btnDangNhap = new Button();
            btnHuy = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblTenDangNhap);
            pnlCard.Controls.Add(txtTenDangNhap);
            pnlCard.Controls.Add(lblEmail);
            pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(lblHoVaTen);
            pnlCard.Controls.Add(txtHoVaTen);
            pnlCard.Controls.Add(lblMatKhau);
            pnlCard.Controls.Add(txtMatKhau);
            pnlCard.Controls.Add(lblXacNhanMatKhau);
            pnlCard.Controls.Add(txtXacNhanMatKhau);
            pnlCard.Controls.Add(btnDangKy);
            pnlCard.Controls.Add(btnDangNhap);
            pnlCard.Controls.Add(btnHuy);
            pnlCard.Location = new Point(335, 80);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(387, 505);
            pnlCard.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(387, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📝 ĐĂNG KÝ TÀI KHOẢN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.Location = new Point(30, 80);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(129, 23);
            lblTenDangNhap.TabIndex = 1;
            lblTenDangNhap.Text = "Tên đăng nhập (*):";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(30, 105);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "username hoặc email";
            txtTenDangNhap.Size = new Size(320, 23);
            txtTenDangNhap.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(30, 145);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email (*):";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(30, 170);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "abc@gmail.com";
            txtEmail.Size = new Size(320, 23);
            txtEmail.TabIndex = 4;
            // 
            // lblHoVaTen
            // 
            lblHoVaTen.Location = new Point(30, 210);
            lblHoVaTen.Name = "lblHoVaTen";
            lblHoVaTen.Size = new Size(100, 23);
            lblHoVaTen.TabIndex = 5;
            lblHoVaTen.Text = "Họ và tên:";
            // 
            // txtHoVaTen
            // 
            txtHoVaTen.Location = new Point(30, 235);
            txtHoVaTen.Name = "txtHoVaTen";
            txtHoVaTen.PlaceholderText = "Không bắt buộc";
            txtHoVaTen.Size = new Size(320, 23);
            txtHoVaTen.TabIndex = 6;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Location = new Point(30, 275);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(100, 23);
            lblMatKhau.TabIndex = 7;
            lblMatKhau.Text = "Mật khẩu (*):";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(30, 300);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(320, 23);
            txtMatKhau.TabIndex = 8;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.Location = new Point(30, 340);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(129, 23);
            lblXacNhanMatKhau.TabIndex = 9;
            lblXacNhanMatKhau.Text = "Xác nhận mật khẩu (*):";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(30, 365);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(320, 23);
            txtXacNhanMatKhau.TabIndex = 10;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(70, 130, 180);
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(30, 405);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(320, 42);
            btnDangKy.TabIndex = 11;
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // btnDangNhap
            // 
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.ForeColor = Color.Black;
            btnDangNhap.Location = new Point(30, 453);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(141, 27);
            btnDangNhap.TabIndex = 13;
            btnDangNhap.Text = "Bạn đã có tài khoản ?";
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnHuy
            // 
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.Gray;
            btnHuy.Location = new Point(275, 452);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(75, 28);
            btnHuy.TabIndex = 14;
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            // 
            // DangKyControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Name = "DangKyControl";
            Size = new Size(1057, 665);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCard;
        private Label lblTitle;
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
        private Button btnDangNhap;
        private Button btnHuy;
    }
}
