namespace WinForms.UserControls.Author
{
    partial class QuenMatKhauControl
    {
        private System.ComponentModel.IContainer components = null;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlCard = new Panel();
            lblTitle = new Label();

            lblEmail = new Label();
            txtEmail = new TextBox();

            lblMatKhauMoi = new Label();
            txtMatKhauMoi = new TextBox();

            lblXacNhanMatKhau = new Label();
            txtXacNhanMatKhau = new TextBox();

            btnDatLaiMatKhau = new Button();
            btnHuy = new Button();

            SuspendLayout();

            // ================= ROOT =================
            BackColor = Color.FromArgb(245, 247, 250);
            Size = new Size(600, 520);

            // ================= CARD =================
            pnlCard.BackColor = Color.White;
            pnlCard.Size = new Size(360, 400);
            pnlCard.Anchor = AnchorStyles.None;

            // ================= TITLE =================
            lblTitle.Text = "🔒 QUÊN MẬT KHẨU";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;

            // ================= EMAIL =================
            lblEmail.Text = "Email";
            lblEmail.Location = new Point(30, 90);

            txtEmail.Location = new Point(30, 115);
            txtEmail.Size = new Size(300, 27);
            txtEmail.PlaceholderText = "abc@gmail.com";

            // ================= NEW PASSWORD =================
            lblMatKhauMoi.Text = "Mật khẩu mới (*)";
            lblMatKhauMoi.Location = new Point(30, 155);

            txtMatKhauMoi.Location = new Point(30, 180);
            txtMatKhauMoi.Size = new Size(300, 27);
            txtMatKhauMoi.UseSystemPasswordChar = true;

            // ================= CONFIRM =================
            lblXacNhanMatKhau.Text = "Xác nhận mật khẩu (*)";
            lblXacNhanMatKhau.Location = new Point(30, 220);

            txtXacNhanMatKhau.Location = new Point(30, 245);
            txtXacNhanMatKhau.Size = new Size(300, 27);
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // ================= RESET =================
            btnDatLaiMatKhau.Text = "ĐẶT LẠI MẬT KHẨU";
            btnDatLaiMatKhau.Size = new Size(300, 42);
            btnDatLaiMatKhau.Location = new Point(30, 295);
            btnDatLaiMatKhau.BackColor = Color.FromArgb(70, 130, 180);
            btnDatLaiMatKhau.ForeColor = Color.White;
            btnDatLaiMatKhau.FlatStyle = FlatStyle.Flat;
            btnDatLaiMatKhau.FlatAppearance.BorderSize = 0;
            btnDatLaiMatKhau.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnDatLaiMatKhau.Click += btnDatLaiMatKhau_Click;

            // ================= CANCEL =================
            btnHuy.Text = "Hủy";
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.ForeColor = Color.Gray;
            btnHuy.Location = new Point(260, 345);
            btnHuy.Click += btnHuy_Click;

            // ================= ADD =================
            pnlCard.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblEmail, txtEmail,
                lblMatKhauMoi, txtMatKhauMoi,
                lblXacNhanMatKhau, txtXacNhanMatKhau,
                btnDatLaiMatKhau,
                btnHuy
            });

            Controls.Add(pnlCard);

            ResumeLayout(false);
        }
    }
}
