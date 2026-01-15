namespace WinForms.UserControls
{
    partial class DangNhapControl
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
            lblSubTitle = new Label();
            lblTenDangNhap = new Label();
            txtTenDangNhap = new TextBox();
            lblMatKhau = new Label();
            txtMatKhau = new TextBox();
            btnQuenMatKhau = new Button();
            btnDangNhap = new Button();
            lblFooter = new Label();
            btnDangKy = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubTitle);
            pnlCard.Controls.Add(lblTenDangNhap);
            pnlCard.Controls.Add(txtTenDangNhap);
            pnlCard.Controls.Add(lblMatKhau);
            pnlCard.Controls.Add(txtMatKhau);
            pnlCard.Controls.Add(btnQuenMatKhau);
            pnlCard.Controls.Add(btnDangNhap);
            pnlCard.Controls.Add(lblFooter);
            pnlCard.Controls.Add(btnDangKy);
            pnlCard.Location = new Point(324, 120);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(370, 420);
            pnlCard.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(370, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📘 LEARNING SOCIAL";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.Dock = DockStyle.Top;
            lblSubTitle.Font = new Font("Segoe UI", 9.5F);
            lblSubTitle.ForeColor = Color.Gray;
            lblSubTitle.Location = new Point(0, 0);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(370, 30);
            lblSubTitle.TabIndex = 1;
            lblSubTitle.Text = "Đăng nhập để tiếp tục học tập";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.Font = new Font("Segoe UI", 9.5F);
            lblTenDangNhap.Location = new Point(30, 110);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(154, 23);
            lblTenDangNhap.TabIndex = 2;
            lblTenDangNhap.Text = "Email hoặc Username";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(30, 135);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "vd: nguyenvana@gmail.com";
            txtTenDangNhap.Size = new Size(300, 23);
            txtTenDangNhap.TabIndex = 3;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Font = new Font("Segoe UI", 9.5F);
            lblMatKhau.Location = new Point(30, 180);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(100, 23);
            lblMatKhau.TabIndex = 4;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(30, 205);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(300, 23);
            txtMatKhau.TabIndex = 5;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // btnQuenMatKhau
            // 
            btnQuenMatKhau.FlatAppearance.BorderSize = 0;
            btnQuenMatKhau.FlatStyle = FlatStyle.Flat;
            btnQuenMatKhau.Font = new Font("Segoe UI", 9F);
            btnQuenMatKhau.ForeColor = Color.FromArgb(70, 130, 180);
            btnQuenMatKhau.Location = new Point(190, 240);
            btnQuenMatKhau.Name = "btnQuenMatKhau";
            btnQuenMatKhau.Size = new Size(140, 34);
            btnQuenMatKhau.TabIndex = 6;
            btnQuenMatKhau.Text = "Quên mật khẩu?";
            btnQuenMatKhau.Click += btnQuenMatKhau_Click;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(70, 130, 180);
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(30, 280);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(300, 40);
            btnDangNhap.TabIndex = 7;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // lblFooter
            // 
            lblFooter.Font = new Font("Segoe UI", 9F);
            lblFooter.Location = new Point(70, 340);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(114, 23);
            lblFooter.TabIndex = 8;
            lblFooter.Text = "Chưa có tài khoản?";
            // 
            // btnDangKy
            // 
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.FromArgb(70, 130, 180);
            btnDangKy.Location = new Point(190, 335);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(85, 28);
            btnDangKy.TabIndex = 9;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.Click += btnDangKy_Click;
            // 
            // DangNhapControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Name = "DangNhapControl";
            Size = new Size(1019, 661);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCard;
        private Label lblTitle;
        private Label lblSubTitle;
        private Label lblTenDangNhap;
        private TextBox txtTenDangNhap;
        private Label lblMatKhau;
        private TextBox txtMatKhau;
        private Button btnQuenMatKhau;
        private Button btnDangNhap;
        private Label lblFooter;
        private Button btnDangKy;
    }
}
