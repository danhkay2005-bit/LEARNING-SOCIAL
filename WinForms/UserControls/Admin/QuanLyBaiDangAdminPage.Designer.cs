namespace WinForms.UserControls.Admin
{
    partial class QuanLyBaiDangAdminPage
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
            pnlInput = new Panel();
            lblNoiDung = new Label();
            lblTieuDe = new Label();
            lblNguoiDang = new Label();
            lblMaBaiViet = new Label();
            txtNoiDung = new RichTextBox();
            txtTieuDe = new TextBox();
            txtNguoiDang = new TextBox();
            txtMaBaiViet = new TextBox();
            txtTimKiem = new TextBox();
            btnTim = new Button();
            label1 = new Label();
            btnLamMoi = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            dgvBaiViet = new DataGridView();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBaiViet).BeginInit();
            SuspendLayout();
            // 
            // pnlInput
            // 
            pnlInput.Controls.Add(lblNoiDung);
            pnlInput.Controls.Add(lblTieuDe);
            pnlInput.Controls.Add(lblNguoiDang);
            pnlInput.Controls.Add(lblMaBaiViet);
            pnlInput.Controls.Add(txtNoiDung);
            pnlInput.Controls.Add(txtTieuDe);
            pnlInput.Controls.Add(txtNguoiDang);
            pnlInput.Controls.Add(txtMaBaiViet);
            pnlInput.Controls.Add(txtTimKiem);
            pnlInput.Controls.Add(btnTim);
            pnlInput.Controls.Add(label1);
            pnlInput.Controls.Add(btnLamMoi);
            pnlInput.Controls.Add(btnXoa);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.Location = new Point(0, 0);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1121, 251);
            pnlInput.TabIndex = 0;
            // 
            // lblNoiDung
            // 
            lblNoiDung.AutoSize = true;
            lblNoiDung.Location = new Point(419, 74);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(61, 15);
            lblNoiDung.TabIndex = 28;
            lblNoiDung.Text = "Nội Dung:";
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(36, 144);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(50, 15);
            lblTieuDe.TabIndex = 27;
            lblTieuDe.Text = "Tiêu Đề:";
            // 
            // lblNguoiDang
            // 
            lblNguoiDang.AutoSize = true;
            lblNguoiDang.Location = new Point(36, 105);
            lblNguoiDang.Name = "lblNguoiDang";
            lblNguoiDang.Size = new Size(74, 15);
            lblNguoiDang.TabIndex = 26;
            lblNguoiDang.Text = "Người Đăng:";
            // 
            // lblMaBaiViet
            // 
            lblMaBaiViet.AutoSize = true;
            lblMaBaiViet.Location = new Point(36, 74);
            lblMaBaiViet.Name = "lblMaBaiViet";
            lblMaBaiViet.Size = new Size(69, 15);
            lblMaBaiViet.TabIndex = 25;
            lblMaBaiViet.Text = "Mã Bài Viết:";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Location = new Point(499, 66);
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.Size = new Size(323, 93);
            txtNoiDung.TabIndex = 24;
            txtNoiDung.Text = "";
            // 
            // txtTieuDe
            // 
            txtTieuDe.Location = new Point(130, 136);
            txtTieuDe.Name = "txtTieuDe";
            txtTieuDe.Size = new Size(176, 23);
            txtTieuDe.TabIndex = 23;
            // 
            // txtNguoiDang
            // 
            txtNguoiDang.Location = new Point(130, 97);
            txtNguoiDang.Name = "txtNguoiDang";
            txtNguoiDang.ReadOnly = true;
            txtNguoiDang.Size = new Size(176, 23);
            txtNguoiDang.TabIndex = 22;
            // 
            // txtMaBaiViet
            // 
            txtMaBaiViet.Location = new Point(130, 66);
            txtMaBaiViet.Name = "txtMaBaiViet";
            txtMaBaiViet.ReadOnly = true;
            txtMaBaiViet.Size = new Size(176, 23);
            txtMaBaiViet.TabIndex = 21;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(310, 34);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Tìm nội dung hoặc người đăng...";
            txtTimKiem.Size = new Size(435, 23);
            txtTimKiem.TabIndex = 20;
            // 
            // btnTim
            // 
            btnTim.BackColor = Color.DodgerBlue;
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnTim.Location = new Point(180, 180);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(105, 42);
            btnTim.TabIndex = 19;
            btnTim.Text = "🔍 TÌM";
            btnTim.UseVisualStyleBackColor = false;
            btnTim.Click += btnTim_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 25);
            label1.Name = "label1";
            label1.Size = new Size(243, 32);
            label1.TabIndex = 18;
            label1.Text = "QUẢN LÝ BÀI ĐĂNG";
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Gray;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnLamMoi.Location = new Point(776, 182);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(131, 40);
            btnLamMoi.TabIndex = 17;
            btnLamMoi.Text = "🔄 LÀM MỚI";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnXoa.Location = new Point(580, 182);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(106, 40);
            btnXoa.TabIndex = 16;
            btnXoa.Text = "🗑 XÓA";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Coral;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnSua.Location = new Point(374, 182);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(106, 40);
            btnSua.TabIndex = 15;
            btnSua.Text = "✏️ SỬA";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // dgvBaiViet
            // 
            dgvBaiViet.AllowUserToAddRows = false;
            dgvBaiViet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaiViet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaiViet.Dock = DockStyle.Fill;
            dgvBaiViet.Location = new Point(0, 251);
            dgvBaiViet.Name = "dgvBaiViet";
            dgvBaiViet.ReadOnly = true;
            dgvBaiViet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaiViet.Size = new Size(1121, 399);
            dgvBaiViet.TabIndex = 1;
            dgvBaiViet.CellClick += dgvBaiViet_CellClick;
            // 
            // QuanLyBaiDangAdminPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvBaiViet);
            Controls.Add(pnlInput);
            Name = "QuanLyBaiDangAdminPage";
            Size = new Size(1121, 650);
            Load += QuanLyBaiDangAdminPage_Load;
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBaiViet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlInput;
        private Button btnLamMoi;
        private Button btnXoa;
        private Button btnSua;
        private Button btnTim;
        private Label label1;
        private TextBox txtTimKiem;
        private DataGridView dgvBaiViet;
        private Label lblNoiDung;
        private Label lblTieuDe;
        private Label lblNguoiDang;
        private Label lblMaBaiViet;
        private RichTextBox txtNoiDung;
        private TextBox txtTieuDe;
        private TextBox txtNguoiDang;
        private TextBox txtMaBaiViet;
    }
}
