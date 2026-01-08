namespace WinForms.Forms
{
    partial class frmQuenMatKhau
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
            label1 = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();
            btnDatLaiMatKhau = new Button();
            btnHuy = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(63, 20);
            label1.Name = "label1";
            label1.Size = new Size(192, 32);
            label1.TabIndex = 0;
            label1.Text = "Quên Mật Khẩu";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 95);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(155, 92);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(161, 23);
            txtEmail.TabIndex = 2;
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Location = new Point(12, 148);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(98, 15);
            lblMatKhauMoi.TabIndex = 3;
            lblMatKhauMoi.Text = "Mật Khẩu Mới(*):";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Location = new Point(155, 140);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(161, 23);
            txtMatKhauMoi.TabIndex = 4;
            // 
            // lblXacNhanMatKhau
            // 
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Location = new Point(12, 198);
            lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            lblXacNhanMatKhau.Size = new Size(128, 15);
            lblXacNhanMatKhau.TabIndex = 5;
            lblXacNhanMatKhau.Text = "Xác Nhận Mật Khẩu(*):";
            // 
            // txtXacNhanMatKhau
            // 
            txtXacNhanMatKhau.Location = new Point(155, 190);
            txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            txtXacNhanMatKhau.Size = new Size(161, 23);
            txtXacNhanMatKhau.TabIndex = 6;
            // 
            // btnDatLaiMatKhau
            // 
            btnDatLaiMatKhau.BackColor = Color.FromArgb(255, 128, 0);
            btnDatLaiMatKhau.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDatLaiMatKhau.ForeColor = SystemColors.ButtonHighlight;
            btnDatLaiMatKhau.Location = new Point(63, 250);
            btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
            btnDatLaiMatKhau.Size = new Size(194, 63);
            btnDatLaiMatKhau.TabIndex = 7;
            btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
            btnDatLaiMatKhau.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(241, 373);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(75, 23);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "HỦY";
            btnHuy.UseVisualStyleBackColor = true;
            // 
            // frmQuenMatKhau
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(328, 408);
            Controls.Add(btnHuy);
            Controls.Add(btnDatLaiMatKhau);
            Controls.Add(txtXacNhanMatKhau);
            Controls.Add(lblXacNhanMatKhau);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(lblMatKhauMoi);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(label1);
            Name = "frmQuenMatKhau";
            Text = "Quên Mật Khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Label lblXacNhanMatKhau;
        private TextBox txtXacNhanMatKhau;
        private Button btnDatLaiMatKhau;
        private Button btnHuy;
    }
}