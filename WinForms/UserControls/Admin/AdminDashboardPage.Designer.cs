namespace WinForms.UserControls.Admin
{
    partial class AdminDashboardPage
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

        private void InitializeComponent()
        {
            pnlStatContainer = new FlowLayoutPanel();
            lblTitle = new Label();
            dgvRecentLogs = new DataGridView();
            pnlGridContainer = new Panel();
            lblLogTitle = new Label();

            // 
            // lblTitle - Tiêu đề trang
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(20, 10, 0, 0);
            lblTitle.Size = new Size(1000, 60);
            lblTitle.Text = "HỆ THỐNG QUẢN TRỊ";

            // 
            // pnlStatContainer - Chứa các thẻ StatCard
            // 
            pnlStatContainer.Dock = DockStyle.Top;
            pnlStatContainer.Height = 160;
            pnlStatContainer.Padding = new Padding(15);
            pnlStatContainer.BackColor = Color.Transparent;

            // 
            // pnlGridContainer - Container cho bảng dữ liệu
            // 
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Padding = new Padding(20);

            // lblLogTitle
            lblLogTitle.Text = "HOẠT ĐỘNG HỆ THỐNG GẦN ĐÂY";
            lblLogTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.FromArgb(193, 225, 127); // Xanh nhạt
            lblLogTitle.Dock = DockStyle.Top;
            lblLogTitle.Height = 35;

            // 
            // dgvRecentLogs - Bảng nhật ký
            // 
            dgvRecentLogs.Dock = DockStyle.Fill;
            dgvRecentLogs.BackgroundColor = Color.FromArgb(25, 45, 50);
            dgvRecentLogs.BorderStyle = BorderStyle.None;
            dgvRecentLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentLogs.ColumnHeadersHeight = 40;
            dgvRecentLogs.RowTemplate.Height = 35;
            dgvRecentLogs.EnableHeadersVisualStyles = false;
            dgvRecentLogs.ReadOnly = true;

            // Thêm các controls vào trang
            pnlGridContainer.Controls.Add(dgvRecentLogs);
            pnlGridContainer.Controls.Add(lblLogTitle);

            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlStatContainer);
            this.Controls.Add(lblTitle);

            this.BackColor = Color.FromArgb(13, 56, 56); // Màu nền chủ đạo của MainForm
            this.Size = new Size(1000, 700);
        }

        // Khai báo các thành phần
        private FlowLayoutPanel pnlStatContainer;
        private Label lblTitle;
        private DataGridView dgvRecentLogs;
        private Panel pnlGridContainer;
        private Label lblLogTitle;
    }
}
