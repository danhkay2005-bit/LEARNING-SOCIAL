namespace WinForms.UserControls
{
    partial class QuenMatKhauControl
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
            btnXemXacNhan = new Button();
            btnXemMatKhauMoi = new Button();
            lblTitle = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();
            btnDatLaiMatKhau = new Button();
            btnHuy = new Button();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCard
            // 
            pnlCard.Anchor = AnchorStyles.None;
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(btnXemXacNhan);
            pnlCard.Controls.Add(btnXemMatKhauMoi);
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblEmail);
            pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(lblMatKhauMoi);
            pnlCard.Controls.Add(txtMatKhauMoi);
            pnlCard.Controls.Add(lblXacNhanMatKhau);
            pnlCard.Controls.Add(txtXacNhanMatKhau);
            pnlCard.Controls.Add(btnDatLaiMatKhau);
            pnlCard.Controls.Add(btnHuy);
            pnlCard.Location = new Point(337, 132);
            pnlCard.Name = "pnlCard";
            pnlCard.Size = new Size(360, 400);
            pnlCard.TabIndex = 2;
            // 
            // btnXemXacNhan
            // 
            btnXemXacNhan.FlatAppearance.BorderSize = 0;
            btnXemXacNhan.FlatStyle = FlatStyle.Flat;
            btnXemXacNhan.Location = new Point(300, 245);
            btnXemXacNhan.Name = "btnXemXacNhan";
            btnXemXacNhan.Size = new Size(30, 28);
            btnXemXacNhan.TabIndex = 12;
            btnXemXacNhan.Text = "👁️";
            btnXemXacNhan.UseVisualStyleBackColor = true;
            // 
            // btnXemMatKhauMoi
            // 
            btnXemMatKhauMoi.FlatAppearance.BorderSize = 0;
            btnXemMatKhauMoi.FlatStyle = FlatStyle.Flat;
            btnXemMatKhauMoi.Location = new Point(300, 172);
            btnXemMatKhauMoi.Name = "btnXemMatKhauMoi";
            btnXemMatKhauMoi.Size = new Size(30, 28);
            btnXemMatKhauMoi.TabIndex = 11;
            btnXemMatKhauMoi.Text = "👁️";
            btnXemMatKhauMoi.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔒 QUÊN MẬT KHẨU";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(30, 90);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(30, 115);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "abc@gmail.com";
            txtEmail.Size = new Size(300, 23);
            txtEmail.TabIndex = 2;
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.Location = new Point(30, 155);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(100, 23);
            lblMatKhauMoi.TabIndex = 3;
            lblMatKhauMoi.Text = "Mật khẩu mới (*):";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(30, 180);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(300, 23);
            txtMatKhauMoi.TabIndex = 4;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.Location = new Point(30, 220);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(137, 23);
            lblXacNhanMatKhau.TabIndex = 5;
            lblXacNhanMatKhau.Text = "Xác nhận mật khẩu (*):";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(30, 245);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(300, 23);
            txtXacNhanMatKhau.TabIndex = 6;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDatLaiMatKhau
            // 
            btnDatLaiMatKhau.BackColor = Color.FromArgb(70, 130, 180);
            btnDatLaiMatKhau.FlatAppearance.BorderSize = 0;
            btnDatLaiMatKhau.FlatStyle = FlatStyle.Flat;
            btnDatLaiMatKhau.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDatLaiMatKhau.ForeColor = Color.White;
            btnDatLaiMatKhau.Location = new Point(30, 295);
            btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
            btnDatLaiMatKhau.Size = new Size(300, 42);
            btnDatLaiMatKhau.TabIndex = 7;
            btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
            btnDatLaiMatKhau.UseVisualStyleBackColor = false;
            btnDatLaiMatKhau.Click += btnDatLaiMatKhau_Click;
            // 
            // btnHuy
            // 
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.Gray;
            btnHuy.Location = new Point(260, 345);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(75, 23);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            // 
            // QuenMatKhauControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Name = "QuenMatKhauControl";
            Size = new Size(1035, 665);
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCard;
        private Label lblTitle;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Label lblXacNhanMatKhau;
        private TextBox txtXacNhanMatKhau;
        private Button btnDatLaiMatKhau;
        private Button btnHuy;
        private Button btnXemXacNhan;
        private Button btnXemMatKhauMoi;
    }
}
