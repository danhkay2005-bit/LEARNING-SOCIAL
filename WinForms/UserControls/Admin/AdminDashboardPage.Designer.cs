using System.Reflection;

namespace WinForms.UserControls.Admin
{
    partial class AdminDashboardPage
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

        private void InitializeComponent()
        {
            pnlStatContainer = new FlowLayoutPanel();
            lblTitle = new Label();
            dgvRecentLogs = new DataGridView();
            pnlGridContainer = new Panel();
            lblLogTitle = new Label();
            pnlPagination = new Panel();
            btnPrevLog = new Button();
            btnNextLog = new Button();
            lblLogPageInfo = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvRecentLogs).BeginInit();
            pnlGridContainer.SuspendLayout();
            pnlPagination.SuspendLayout();
            SuspendLayout();

            // --- TIÊU ĐỀ TRANG ---
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(25, 20, 0, 0);
            lblTitle.Size = new Size(1000, 80);
            lblTitle.Text = "HỆ THỐNG QUẢN TRỊ";

            // --- PANEL THỐNG KÊ (Stat Cards) ---
            pnlStatContainer.Dock = DockStyle.Top;
            pnlStatContainer.Height = 160;
            pnlStatContainer.Padding = new Padding(20, 5, 20, 5);
            pnlStatContainer.BackColor = Color.Transparent;

            // --- CONTAINER BẢNG DỮ LIỆU (Có Bo Góc Giả) ---
            pnlGridContainer.Dock = DockStyle.Fill;
            pnlGridContainer.Padding = new Padding(30, 10, 30, 20);
            pnlGridContainer.BackColor = Color.Transparent;

            // Tiêu đề bảng Log
            lblLogTitle.Text = "⚡ HOẠT ĐỘNG HỆ THỐNG MỚI NHẤT";
            lblLogTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.FromArgb(193, 225, 127); // Màu Xanh Chanh nhấn bài
            lblLogTitle.Dock = DockStyle.Top;
            lblLogTitle.Height = 40;

            // ========================================================
            // 🔥 THIẾT KẾ GRIDVIEW HIỆN ĐẠI (CUSTOM STYLE) 🔥
            // ========================================================
            dgvRecentLogs.Dock = DockStyle.Fill;
            dgvRecentLogs.BackgroundColor = Color.FromArgb(20, 45, 50); // Nền tối hơn Panel
            dgvRecentLogs.BorderStyle = BorderStyle.None;
            dgvRecentLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecentLogs.GridColor = Color.FromArgb(35, 70, 75); // Đường kẻ ngang mờ
            dgvRecentLogs.EnableHeadersVisualStyles = false; // QUAN TRỌNG: Để đổi màu Header
            dgvRecentLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecentLogs.MultiSelect = false;
            dgvRecentLogs.ReadOnly = true;
            dgvRecentLogs.RowHeadersVisible = false; // Ẩn cột thừa bên trái
            dgvRecentLogs.AllowUserToAddRows = false;
            dgvRecentLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- Style cho Header (Dòng tiêu đề) ---
            dgvRecentLogs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRecentLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(13, 56, 56);
            dgvRecentLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(193, 225, 127);
            dgvRecentLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Bold", 10F);
            dgvRecentLogs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRecentLogs.ColumnHeadersHeight = 50;

            // --- Style cho Dòng dữ liệu ---
            dgvRecentLogs.DefaultCellStyle.BackColor = Color.FromArgb(25, 55, 60);
            dgvRecentLogs.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvRecentLogs.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvRecentLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 85, 90); // Màu khi chọn dòng
            dgvRecentLogs.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvRecentLogs.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            // --- Style cho Dòng xen kẽ (Zebra Stripes) ---
            dgvRecentLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 50, 55);

            pnlPagination.Dock = DockStyle.Bottom;
            pnlPagination.Height = 60;
            pnlPagination.BackColor = Color.FromArgb(13, 56, 56);
            pnlPagination.Controls.Add(btnPrevLog);
            pnlPagination.Controls.Add(lblLogPageInfo);
            pnlPagination.Controls.Add(btnNextLog);

            // Button Trước
            btnPrevLog.Text = "Trước";
            btnPrevLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevLog.Size = new Size(80, 35);
            btnPrevLog.Location = new Point(10, 12);
            btnPrevLog.FlatStyle = FlatStyle.Flat;
            btnPrevLog.FlatAppearance.BorderSize = 1;
            btnPrevLog.FlatAppearance.BorderColor = Color.FromArgb(35, 75, 80);
            btnPrevLog.ForeColor = Color.White;
            btnPrevLog.Cursor = Cursors.Hand;
            btnPrevLog.Click += btnPrevLog_Click;

            // Info Trang
            lblLogPageInfo.Text = "Trang 1 / 1";
            lblLogPageInfo.Font = new Font("Segoe UI Semibold", 9.5F);
            lblLogPageInfo.ForeColor = Color.FromArgb(193, 225, 127);
            lblLogPageInfo.AutoSize = false;
            lblLogPageInfo.Size = new Size(150, 35);
            lblLogPageInfo.Location = new Point(95, 12);
            lblLogPageInfo.TextAlign = ContentAlignment.MiddleCenter;

            // Button Sau
            btnNextLog.Text = "Tiếp";
            btnNextLog.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNextLog.Size = new Size(80, 35);
            btnNextLog.Location = new Point(250, 12);
            btnNextLog.FlatStyle = FlatStyle.Flat;
            btnNextLog.FlatAppearance.BorderSize = 0;
            btnNextLog.BackColor = Color.FromArgb(35, 75, 80);
            btnNextLog.ForeColor = Color.White;
            btnNextLog.Cursor = Cursors.Hand;
            btnNextLog.Click += btnNextLog_Click;

            // --- SẮP XẾP VÀ THÊM VÀO TRANG ---
            pnlGridContainer.Controls.Add(dgvRecentLogs);
            pnlGridContainer.Controls.Add(pnlPagination);
            pnlGridContainer.Controls.Add(lblLogTitle);

            this.Controls.Add(pnlGridContainer);
            this.Controls.Add(pnlStatContainer);
            this.Controls.Add(lblTitle);

            this.BackColor = Color.FromArgb(13, 56, 56);
            this.Size = new Size(1000, 750);

            ((System.ComponentModel.ISupportInitialize)dgvRecentLogs).EndInit();
            pnlGridContainer.ResumeLayout(false);
            pnlPagination.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Khai báo các thành phần
        private FlowLayoutPanel pnlStatContainer;
        private Label lblTitle;
        private DataGridView dgvRecentLogs;
        private Panel pnlGridContainer;
        private Label lblLogTitle;

        // Khai báo các thành phần điều hướng mới
        private Panel pnlPagination;
        private Button btnPrevLog;
        private Button btnNextLog;
        private Label lblLogPageInfo;
    }
}