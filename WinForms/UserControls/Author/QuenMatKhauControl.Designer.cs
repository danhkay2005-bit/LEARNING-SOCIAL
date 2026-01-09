using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.UserControls.Author
{
    partial class QuenMatKhauControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            labelTitle = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();
            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();
            btnDatLaiMatKhau = new Button();
            btnHuy = new Button();

            SuspendLayout();

            // labelTitle
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.Location = new Point(60, 20);
            labelTitle.Text = "Quên Mật Khẩu";

            // lblEmail
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 95);
            lblEmail.Text = "Email:";

            // txtEmail
            txtEmail.Location = new Point(155, 92);
            txtEmail.Size = new Size(161, 23);

            // lblMatKhauMoi
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Location = new Point(12, 148);
            lblMatKhauMoi.Text = "Mật Khẩu Mới(*):";

            // txtMatKhauMoi
            txtMatKhauMoi.Location = new Point(155, 140);
            txtMatKhauMoi.Size = new Size(161, 23);
            txtMatKhauMoi.UseSystemPasswordChar = true;

            // lblXacNhanMatKhau
            lblXacNhanMatKhau.AutoSize = true;
            lblXacNhanMatKhau.Location = new Point(12, 198);
            lblXacNhanMatKhau.Text = "Xác Nhận Mật Khẩu(*):";

            // txtXacNhanMatKhau
            txtXacNhanMatKhau.Location = new Point(155, 190);
            txtXacNhanMatKhau.Size = new Size(161, 23);
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // btnDatLaiMatKhau
            btnDatLaiMatKhau.BackColor = Color.FromArgb(255, 128, 0);
            btnDatLaiMatKhau.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDatLaiMatKhau.ForeColor = Color.White;
            btnDatLaiMatKhau.Location = new Point(50, 250);
            btnDatLaiMatKhau.Size = new Size(220, 45);
            btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
            btnDatLaiMatKhau.UseVisualStyleBackColor = false;
            btnDatLaiMatKhau.Click += btnDatLaiMatKhau_Click;

            // btnHuy
            btnHuy.Location = new Point(241, 320);
            btnHuy.Size = new Size(75, 23);
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;

            // UserControl
            Controls.AddRange(new Control[]
            {
                labelTitle,
                lblEmail, txtEmail,
                lblMatKhauMoi, txtMatKhauMoi,
                lblXacNhanMatKhau, txtXacNhanMatKhau,
                btnDatLaiMatKhau,
                btnHuy
            });

            Name = "QuenMatKhauControl";
            Size = new Size(328, 380);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitle;
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
