namespace WinForms.Forms
{
    partial class frmDangKy
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
            // 
            // lblDangKyTaiKhoan
            // 
            lblDangKyTaiKhoan.AutoSize = true;
            lblDangKyTaiKhoan.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDangKyTaiKhoan.Location = new Point(55, 28);
            lblDangKyTaiKhoan.Name = "lblDangKyTaiKhoan";
            lblDangKyTaiKhoan.Size = new Size(263, 32);
            lblDangKyTaiKhoan.TabIndex = 0;
            lblDangKyTaiKhoan.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(12, 112);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(105, 15);
            lblTenDangNhap.TabIndex = 1;
            lblTenDangNhap.Text = "Tên Đăng Nhập(*):";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(154, 104);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.PlaceholderText = "Nhập Username / Email";
            txtTenDangNhap.Size = new Size(164, 23);
            txtTenDangNhap.TabIndex = 2;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 145);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(154, 133);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "abc123@gmail.com";
            txtEmail.Size = new Size(164, 23);
            txtEmail.TabIndex = 4;
            // 
            // lblHoVaTen
            // 
            lblHoVaTen.AutoSize = true;
            lblHoVaTen.Location = new Point(12, 179);
            lblHoVaTen.Name = "lblHoVaTen";
            lblHoVaTen.Size = new Size(63, 15);
            lblHoVaTen.TabIndex = 5;
            lblHoVaTen.Text = "Họ và Tên:";
            // 
            // txtHoVaTen
            // 
            txtHoVaTen.Location = new Point(154, 171);
            txtHoVaTen.Name = "txtHoVaTen";
            txtHoVaTen.PlaceholderText = "Nhập họ và tên (không bắt buộc)";
            txtHoVaTen.Size = new Size(164, 23);
            txtHoVaTen.TabIndex = 6;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(12, 212);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(74, 15);
            lblMatKhau.TabIndex = 7;
            lblMatKhau.Text = "Mật Khẩu(*):";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(154, 204);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(164, 23);
            txtMatKhau.TabIndex = 8;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Location = new Point(12, 246);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(128, 15);
            lblXacNhanMatKhau.TabIndex = 9;
            lblXacNhanMatKhau.Text = "Xác Nhận Mật Khẩu(*):";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(154, 238);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(164, 23);
            txtXacNhanMatKhau.TabIndex = 10;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.FromArgb(255, 128, 0);
            btnDangKy.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDangKy.ForeColor = Color.White;
            btnDangKy.Location = new Point(116, 295);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(138, 48);
            btnDangKy.TabIndex = 11;
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.FlatStyle = FlatStyle.System;
            btnHuy.ForeColor = Color.Silver;
            btnHuy.Location = new Point(301, 389);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(59, 23);
            btnHuy.TabIndex = 12;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // lblDaCoTaiKhoan
            // 
            lblDaCoTaiKhoan.AutoSize = true;
            lblDaCoTaiKhoan.Location = new Point(83, 393);
            lblDaCoTaiKhoan.Name = "lblDaCoTaiKhoan";
            lblDaCoTaiKhoan.Size = new Size(99, 15);
            lblDaCoTaiKhoan.TabIndex = 13;
            lblDaCoTaiKhoan.Text = "Đã Có Tài Khoản?";
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(188, 389);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(75, 23);
            btnDangNhap.TabIndex = 14;
            btnDangNhap.Text = "Đăng Nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            // 
            // frmDangKy
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(368, 434);
            Controls.Add(btnDangNhap);
            Controls.Add(lblDaCoTaiKhoan);
            Controls.Add(btnHuy);
            Controls.Add(btnDangKy);
            Controls.Add(txtXacNhanMatKhau);
            Controls.Add(lblXacNhanMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(lblMatKhau);
            Controls.Add(txtHoVaTen);
            Controls.Add(lblHoVaTen);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblTenDangNhap);
            Controls.Add(lblDangKyTaiKhoan);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmDangKy";
            Text = "Đăng Ký ";
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