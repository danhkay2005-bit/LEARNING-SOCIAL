namespace WinForms.UserControls.Admin
{
    partial class QuanLyBoDeAdminPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabAdmin = new TabControl();
            tpBoDe = new TabPage();
            tpChuDe = new TabPage();
            tpHashtag = new TabPage();

            dgvBoDe = new DataGridView();
            dgvChuDe = new DataGridView();
            dgvHashtags = new DataGridView();

            txtTenChuDe = new TextBox();
            txtTagChuan = new TextBox();

            btnRestore = new Button();
            btnDeleteBoDe = new Button();
            btnTogglePublic = new Button(); // Nút Sửa trạng thái Bộ đề

            btnAddChuDe = new Button();
            btnEditChuDe = new Button();    // Nút Sửa Chủ đề
            btnDeleteChuDe = new Button();

            btnMergeTag = new Button();
            btnEditTag = new Button();      // Nút Sửa tên Tag
            btnDeleteTag = new Button();

            btnPrevBoDe = new Button();
            btnNextBoDe = new Button();
            lblPageInfoBoDe = new Label();

            // ===== TabControl =====
            tabAdmin.Dock = DockStyle.Fill;
            tabAdmin.Appearance = TabAppearance.FlatButtons;
            tabAdmin.TabPages.AddRange(new TabPage[] { tpBoDe, tpChuDe, tpHashtag });

            // ================= TAB 1: BỘ ĐỀ =================
            tpBoDe.Text = "📦 Quản lý Bộ đề";
            tpBoDe.BackColor = Color.FromArgb(25, 45, 50);
            dgvBoDe.Dock = DockStyle.Fill;
            dgvBoDe.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgvBoDe.ReadOnly = true;
            dgvBoDe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBoDe.AllowUserToAddRows = false;

            Panel pnlBoDe = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };

            btnRestore.Text = "♻️ Khôi phục";
            btnRestore.Size = new Size(110, 40);
            btnRestore.FlatStyle = FlatStyle.Flat;
            btnRestore.BackColor = Color.FromArgb(0, 122, 204);
            btnRestore.ForeColor = Color.White;
            btnRestore.Click += btnRestore_Click;

            btnTogglePublic.Text = "👁️ Hiện/Ẩn";
            btnTogglePublic.Location = new Point(130, 10);
            btnTogglePublic.Size = new Size(110, 40);
            btnTogglePublic.FlatStyle = FlatStyle.Flat;
            btnTogglePublic.BackColor = Color.FromArgb(104, 33, 122); // Màu tím quản trị
            btnTogglePublic.ForeColor = Color.White;
            btnTogglePublic.Click += btnTogglePublic_Click;

            btnDeleteBoDe.Text = "🗑️ Xóa";
            btnDeleteBoDe.Location = new Point(250, 10);
            btnDeleteBoDe.Size = new Size(110, 40);
            btnDeleteBoDe.FlatStyle = FlatStyle.Flat;
            btnDeleteBoDe.BackColor = Color.FromArgb(183, 28, 28);
            btnDeleteBoDe.ForeColor = Color.White;
            btnDeleteBoDe.Click += btnDeleteBoDe_Click;

            // Cấu hình Nút QUAY LẠI
            btnPrevBoDe.Text = "◀";
            btnPrevBoDe.Location = new Point(380, 10); // Đặt sau nút Xóa
            btnPrevBoDe.Size = new Size(40, 40);
            btnPrevBoDe.FlatStyle = FlatStyle.Flat;
            btnPrevBoDe.BackColor = Color.FromArgb(45, 45, 48);
            btnPrevBoDe.ForeColor = Color.White;
            btnPrevBoDe.Click += btnPrevBoDe_Click; // Liên kết sự kiện

            // Cấu hình NHÃN THÔNG TIN TRANG
            lblPageInfoBoDe.Text = "Trang 1 / 1";
            lblPageInfoBoDe.Location = new Point(430, 20);
            lblPageInfoBoDe.AutoSize = true;
            lblPageInfoBoDe.ForeColor = Color.White;
            lblPageInfoBoDe.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Cấu hình Nút TIẾP THEO
            btnNextBoDe.Text = "▶";
            btnNextBoDe.Location = new Point(530, 10);
            btnNextBoDe.Size = new Size(40, 40);
            btnNextBoDe.FlatStyle = FlatStyle.Flat;
            btnNextBoDe.BackColor = Color.FromArgb(45, 45, 48);
            btnNextBoDe.ForeColor = Color.White;
            btnNextBoDe.Click += btnNextBoDe_Click; // Liên kết sự kiện

            pnlBoDe.Controls.AddRange(new Control[] {
        btnRestore,
        btnTogglePublic,
        btnDeleteBoDe,
        btnPrevBoDe,
        lblPageInfoBoDe,
        btnNextBoDe
    });
            tpBoDe.Controls.AddRange(new Control[] { dgvBoDe, pnlBoDe });

            // ================= TAB 2: CHỦ ĐỀ =================
            tpChuDe.Text = "📁 Chủ đề hệ thống";
            tpChuDe.BackColor = Color.FromArgb(25, 45, 50);
            dgvChuDe.Dock = DockStyle.Fill;
            dgvChuDe.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgvChuDe.ReadOnly = true;
            dgvChuDe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChuDe.AllowUserToAddRows = false;

            Panel pnlChuDe = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };
            txtTenChuDe.Size = new Size(180, 30);
            txtTenChuDe.PlaceholderText = "Tên chủ đề...";

            btnAddChuDe.Text = "➕ Thêm";
            btnAddChuDe.Location = new Point(200, 10);
            btnAddChuDe.Size = new Size(90, 35);
            btnAddChuDe.FlatStyle = FlatStyle.Flat;
            btnAddChuDe.BackColor = Color.FromArgb(46, 125, 50);
            btnAddChuDe.ForeColor = Color.White;
            btnAddChuDe.Click += btnAddChuDe_Click;

            btnEditChuDe.Text = "📝 Sửa";
            btnEditChuDe.Location = new Point(300, 10);
            btnEditChuDe.Size = new Size(90, 35);
            btnEditChuDe.FlatStyle = FlatStyle.Flat;
            btnEditChuDe.BackColor = Color.FromArgb(0, 122, 204);
            btnEditChuDe.ForeColor = Color.White;
            btnEditChuDe.Click += btnEditChuDe_Click;

            btnDeleteChuDe.Text = "🗑️ Xóa";
            btnDeleteChuDe.Location = new Point(400, 10);
            btnDeleteChuDe.Size = new Size(90, 35);
            btnDeleteChuDe.FlatStyle = FlatStyle.Flat;
            btnDeleteChuDe.BackColor = Color.FromArgb(183, 28, 28);
            btnDeleteChuDe.ForeColor = Color.White;
            btnDeleteChuDe.Click += btnDeleteChuDe_Click;

            pnlChuDe.Controls.AddRange(new Control[] { txtTenChuDe, btnAddChuDe, btnEditChuDe, btnDeleteChuDe });
            tpChuDe.Controls.AddRange(new Control[] { dgvChuDe, pnlChuDe });

            // ================= TAB 3: HASHTAG =================
            tpHashtag.Text = "🏷️ Kiểm soát Hashtag";
            tpHashtag.BackColor = Color.FromArgb(25, 45, 50);
            dgvHashtags.Dock = DockStyle.Fill;
            dgvHashtags.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgvHashtags.ReadOnly = true;
            dgvHashtags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHashtags.AllowUserToAddRows = false;

            Panel pnlTag = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10) };
            txtTagChuan.Size = new Size(180, 30);
            txtTagChuan.PlaceholderText = "Tên hashtag...";

            btnMergeTag.Text = "🔗 Gộp";
            btnMergeTag.Location = new Point(200, 10);
            btnMergeTag.Size = new Size(90, 35);
            btnMergeTag.FlatStyle = FlatStyle.Flat;
            btnMergeTag.BackColor = Color.FromArgb(204, 102, 0);
            btnMergeTag.ForeColor = Color.White;
            btnMergeTag.Click += btnMergeTag_Click;

            btnEditTag.Text = "📝 Sửa";
            btnEditTag.Location = new Point(300, 10);
            btnEditTag.Size = new Size(90, 35);
            btnEditTag.FlatStyle = FlatStyle.Flat;
            btnEditTag.BackColor = Color.FromArgb(0, 122, 204);
            btnEditTag.ForeColor = Color.White;
            btnEditTag.Click += btnEditTag_Click;

            btnDeleteTag.Text = "🗑️ Xóa";
            btnDeleteTag.Location = new Point(400, 10);
            btnDeleteTag.Size = new Size(90, 35);
            btnDeleteTag.FlatStyle = FlatStyle.Flat;
            btnDeleteTag.BackColor = Color.FromArgb(183, 28, 28);
            btnDeleteTag.ForeColor = Color.White;
            btnDeleteTag.Click += btnDeleteTag_Click;

            pnlTag.Controls.AddRange(new Control[] { txtTagChuan, btnMergeTag, btnEditTag, btnDeleteTag });
            tpHashtag.Controls.AddRange(new Control[] { dgvHashtags, pnlTag });

            Controls.Add(tabAdmin);
            Size = new Size(1000, 700);
            BackColor = Color.FromArgb(13, 56, 56);
        }

        private TabControl tabAdmin;
        private TabPage tpBoDe, tpChuDe, tpHashtag;
        private DataGridView dgvBoDe, dgvChuDe, dgvHashtags;
        private TextBox txtTenChuDe, txtTagChuan;
        private Button btnRestore, btnDeleteBoDe, btnTogglePublic, btnAddChuDe, btnEditChuDe, btnDeleteChuDe, btnMergeTag, btnEditTag, btnDeleteTag;
        private System.Windows.Forms.Button btnPrevBoDe;
        private System.Windows.Forms.Button btnNextBoDe;
        private System.Windows.Forms.Label lblPageInfoBoDe;
    }
}