namespace WinForms.UserControls.Admin
{
    partial class QuanLyBoDeAdminPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabAdmin = new System.Windows.Forms.TabControl();
            this.tpBoDe = new System.Windows.Forms.TabPage();
            this.dgvBoDe = new System.Windows.Forms.DataGridView();
            this.pnlBoDe = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnTogglePublic = new System.Windows.Forms.Button();
            this.btnDeleteBoDe = new System.Windows.Forms.Button();
            this.btnPrevBoDe = new System.Windows.Forms.Button();
            this.lblPageInfoBoDe = new System.Windows.Forms.Label();
            this.btnNextBoDe = new System.Windows.Forms.Button();
            this.tpChuDe = new System.Windows.Forms.TabPage();
            this.dgvChuDe = new System.Windows.Forms.DataGridView();
            this.pnlChuDe = new System.Windows.Forms.Panel();
            this.txtTenChuDe = new System.Windows.Forms.TextBox();
            this.btnAddChuDe = new System.Windows.Forms.Button();
            this.btnEditChuDe = new System.Windows.Forms.Button();
            this.btnDeleteChuDe = new System.Windows.Forms.Button();
            this.tpHashtag = new System.Windows.Forms.TabPage();
            this.dgvHashtags = new System.Windows.Forms.DataGridView();
            this.pnlTag = new System.Windows.Forms.Panel();
            this.txtTagChuan = new System.Windows.Forms.TextBox();
            this.btnMergeTag = new System.Windows.Forms.Button();
            this.btnEditTag = new System.Windows.Forms.Button();
            this.btnDeleteTag = new System.Windows.Forms.Button();
            this.tpHocTap = new System.Windows.Forms.TabPage();
            this.dgvLichSuHoc = new System.Windows.Forms.DataGridView();
            this.tpThachDau = new System.Windows.Forms.TabPage();
            this.dgvLichSuThachDau = new System.Windows.Forms.DataGridView();

            this.tabAdmin.SuspendLayout();
            this.tpBoDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoDe)).BeginInit();
            this.pnlBoDe.SuspendLayout();
            this.tpChuDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuDe)).BeginInit();
            this.pnlChuDe.SuspendLayout();
            this.tpHashtag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHashtags)).BeginInit();
            this.pnlTag.SuspendLayout();
            this.tpHocTap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuHoc)).BeginInit();
            this.tpThachDau.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuThachDau)).BeginInit();
            this.SuspendLayout();

            // --- tabAdmin (5 Tab chuyên nghiệp) ---
            this.tabAdmin.Controls.AddRange(new System.Windows.Forms.Control[] { this.tpBoDe, this.tpChuDe, this.tpHashtag, this.tpHocTap, this.tpThachDau });
            this.tabAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAdmin.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.tabAdmin.ItemSize = new System.Drawing.Size(180, 45);
            this.tabAdmin.Location = new System.Drawing.Point(0, 0);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;

            // ================= TAB 1: BỘ ĐỀ (Có phân trang) =================
            this.tpBoDe.Controls.Add(this.dgvBoDe);
            this.tpBoDe.Controls.Add(this.pnlBoDe);
            this.tpBoDe.Text = "📦 Bộ đề học";
            this.tpBoDe.BackColor = System.Drawing.Color.White;
            this.dgvBoDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoDe.BackgroundColor = System.Drawing.Color.White;
            this.dgvBoDe.BorderStyle = BorderStyle.None;

            this.pnlBoDe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBoDe.Height = 70;
            this.pnlBoDe.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlBoDe.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnRestore, this.btnTogglePublic, this.btnDeleteBoDe,
                this.btnPrevBoDe, this.lblPageInfoBoDe, this.btnNextBoDe
            });

            this.btnRestore.Location = new System.Drawing.Point(15, 15);
            this.btnRestore.Size = new System.Drawing.Size(120, 38);
            this.btnRestore.Text = "♻️ Khôi phục";
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);

            this.btnTogglePublic.Location = new System.Drawing.Point(145, 15);
            this.btnTogglePublic.Size = new System.Drawing.Size(120, 38);
            this.btnTogglePublic.Text = "👁️ Hiện/Ẩn";
            this.btnTogglePublic.BackColor = System.Drawing.Color.FromArgb(108, 92, 231);
            this.btnTogglePublic.ForeColor = System.Drawing.Color.White;
            this.btnTogglePublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePublic.Click += new System.EventHandler(this.btnTogglePublic_Click);

            this.btnDeleteBoDe.Location = new System.Drawing.Point(275, 15);
            this.btnDeleteBoDe.Size = new System.Drawing.Size(120, 38);
            this.btnDeleteBoDe.Text = "🗑️ Xóa";
            this.btnDeleteBoDe.BackColor = System.Drawing.Color.FromArgb(255, 118, 117);
            this.btnDeleteBoDe.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBoDe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBoDe.Click += new System.EventHandler(this.btnDeleteBoDe_Click);

            // Nút Phân trang (Căn phải)
            this.btnPrevBoDe.Location = new System.Drawing.Point(450, 15);
            this.btnPrevBoDe.Size = new System.Drawing.Size(45, 38);
            this.btnPrevBoDe.Text = "◀";
            this.btnPrevBoDe.Click += new System.EventHandler(this.btnPrevBoDe_Click);

            this.lblPageInfoBoDe.Location = new System.Drawing.Point(505, 24);
            this.lblPageInfoBoDe.Size = new System.Drawing.Size(100, 20);
            this.lblPageInfoBoDe.Text = "Trang 1 / 1";
            this.lblPageInfoBoDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnNextBoDe.Location = new System.Drawing.Point(615, 15);
            this.btnNextBoDe.Size = new System.Drawing.Size(45, 38);
            this.btnNextBoDe.Text = "▶";
            this.btnNextBoDe.Click += new System.EventHandler(this.btnNextBoDe_Click);

            // ================= TAB 2: CHỦ ĐỀ =================
            this.tpChuDe.Controls.Add(this.dgvChuDe);
            this.tpChuDe.Controls.Add(this.pnlChuDe);
            this.tpChuDe.Text = "📁 Chủ đề";
            this.tpChuDe.BackColor = System.Drawing.Color.White;
            this.dgvChuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChuDe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlChuDe.Height = 70;
            this.pnlChuDe.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlChuDe.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtTenChuDe, this.btnAddChuDe, this.btnEditChuDe, this.btnDeleteChuDe });

            this.txtTenChuDe.Location = new System.Drawing.Point(15, 20);
            this.txtTenChuDe.Size = new System.Drawing.Size(200, 30);
            this.btnAddChuDe.Location = new System.Drawing.Point(230, 15);
            this.btnAddChuDe.Size = new System.Drawing.Size(100, 38);
            this.btnAddChuDe.Text = "➕ Thêm";
            this.btnAddChuDe.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnAddChuDe.ForeColor = System.Drawing.Color.White;
            this.btnAddChuDe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddChuDe.Click += new System.EventHandler(this.btnAddChuDe_Click);

            this.btnEditChuDe.Location = new System.Drawing.Point(340, 15);
            this.btnEditChuDe.Size = new System.Drawing.Size(100, 38);
            this.btnEditChuDe.Text = "📝 Sửa";
            this.btnEditChuDe.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnEditChuDe.ForeColor = System.Drawing.Color.White;
            this.btnEditChuDe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditChuDe.Click += new System.EventHandler(this.btnEditChuDe_Click);

            this.btnDeleteChuDe.Location = new System.Drawing.Point(450, 15);
            this.btnDeleteChuDe.Size = new System.Drawing.Size(100, 38);
            this.btnDeleteChuDe.Text = "🗑️ Xóa";
            this.btnDeleteChuDe.BackColor = System.Drawing.Color.FromArgb(255, 118, 117);
            this.btnDeleteChuDe.ForeColor = System.Drawing.Color.White;
            this.btnDeleteChuDe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteChuDe.Click += new System.EventHandler(this.btnDeleteChuDe_Click);

            // ================= TAB 3: HASHTAG =================
            this.tpHashtag.Controls.Add(this.dgvHashtags);
            this.tpHashtag.Controls.Add(this.pnlTag);
            this.tpHashtag.Text = "🏷️ Hashtag";
            this.tpHashtag.BackColor = System.Drawing.Color.White;
            this.dgvHashtags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTag.Height = 70;
            this.pnlTag.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlTag.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtTagChuan, this.btnMergeTag, this.btnEditTag, this.btnDeleteTag });

            this.txtTagChuan.Location = new System.Drawing.Point(15, 20);
            this.txtTagChuan.Size = new System.Drawing.Size(200, 30);
            this.btnMergeTag.Location = new System.Drawing.Point(230, 15);
            this.btnMergeTag.Size = new System.Drawing.Size(100, 38);
            this.btnMergeTag.Text = "🔗 Gộp";
            this.btnMergeTag.BackColor = System.Drawing.Color.FromArgb(253, 150, 68);
            this.btnMergeTag.ForeColor = System.Drawing.Color.White;
            this.btnMergeTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMergeTag.Click += new System.EventHandler(this.btnMergeTag_Click);

            this.btnEditTag.Location = new System.Drawing.Point(340, 15);
            this.btnEditTag.Size = new System.Drawing.Size(100, 38);
            this.btnEditTag.Text = "📝 Sửa";
            this.btnEditTag.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnEditTag.ForeColor = System.Drawing.Color.White;
            this.btnEditTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTag.Click += new System.EventHandler(this.btnEditTag_Click);

            this.btnDeleteTag.Location = new System.Drawing.Point(450, 15);
            this.btnDeleteTag.Size = new System.Drawing.Size(100, 38);
            this.btnDeleteTag.Text = "🗑️ Xóa";
            this.btnDeleteTag.BackColor = System.Drawing.Color.FromArgb(255, 118, 117);
            this.btnDeleteTag.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTag.Click += new System.EventHandler(this.btnDeleteTag_Click);

            // ================= TAB 4 & 5 (Log) =================
            this.tpHocTap.Controls.Add(this.dgvLichSuHoc);
            this.tpHocTap.Text = "🔥 Log Học";
            this.tpHocTap.BackColor = System.Drawing.Color.White;
            this.dgvLichSuHoc.Dock = System.Windows.Forms.DockStyle.Fill;

            this.tpThachDau.Controls.Add(this.dgvLichSuThachDau);
            this.tpThachDau.Text = "⚔️ Log Thách đấu";
            this.tpThachDau.BackColor = System.Drawing.Color.White;
            this.dgvLichSuThachDau.Dock = System.Windows.Forms.DockStyle.Fill;

            // Finalize
            this.Controls.Add(this.tabAdmin);
            this.Size = new System.Drawing.Size(1100, 750);
            this.tabAdmin.ResumeLayout(false);
            this.tpBoDe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoDe)).EndInit();
            this.pnlBoDe.ResumeLayout(false);
            this.pnlBoDe.PerformLayout();
            this.tpChuDe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuDe)).EndInit();
            this.pnlChuDe.ResumeLayout(false);
            this.pnlChuDe.PerformLayout();
            this.tpHashtag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHashtags)).EndInit();
            this.pnlTag.ResumeLayout(false);
            this.pnlTag.PerformLayout();
            this.tpHocTap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuHoc)).EndInit();
            this.tpThachDau.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuThachDau)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tpBoDe, tpChuDe, tpHashtag, tpHocTap, tpThachDau;
        private System.Windows.Forms.DataGridView dgvBoDe, dgvChuDe, dgvHashtags, dgvLichSuHoc, dgvLichSuThachDau;
        private System.Windows.Forms.Panel pnlBoDe, pnlChuDe, pnlTag;
        private System.Windows.Forms.TextBox txtTenChuDe, txtTagChuan;
        private System.Windows.Forms.Button btnRestore, btnDeleteBoDe, btnTogglePublic;
        private System.Windows.Forms.Button btnAddChuDe, btnEditChuDe, btnDeleteChuDe;
        private System.Windows.Forms.Button btnMergeTag, btnEditTag, btnDeleteTag;
        private System.Windows.Forms.Button btnPrevBoDe, btnNextBoDe;
        private System.Windows.Forms.Label lblPageInfoBoDe;
    }
}