namespace WinForms.UserControls.Admin
{
    partial class QuanLyCuaHangAdminPage
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
            dgvVatPham = new DataGridView();
            lblID = new Label();
            txtMaVatPham = new TextBox();
            lblName = new Label();
            txtTenVatPham = new TextBox();
            blPrice = new Label();
            numGia = new NumericUpDown();
            lblCurrency = new Label();
            cboLoaiTien = new ComboBox();
            lblDesc = new Label();
            txtMoTa = new TextBox();
            chkConHang = new CheckBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVatPham).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGia).BeginInit();
            SuspendLayout();
            // 
            // pnlInput
            // 
            pnlInput.Controls.Add(btnLamMoi);
            pnlInput.Controls.Add(btnXoa);
            pnlInput.Controls.Add(btnSua);
            pnlInput.Controls.Add(btnThem);
            pnlInput.Controls.Add(chkConHang);
            pnlInput.Controls.Add(txtMoTa);
            pnlInput.Controls.Add(lblDesc);
            pnlInput.Controls.Add(cboLoaiTien);
            pnlInput.Controls.Add(lblCurrency);
            pnlInput.Controls.Add(numGia);
            pnlInput.Controls.Add(blPrice);
            pnlInput.Controls.Add(txtTenVatPham);
            pnlInput.Controls.Add(lblName);
            pnlInput.Controls.Add(txtMaVatPham);
            pnlInput.Controls.Add(lblID);
            pnlInput.Dock = DockStyle.Top;
            pnlInput.Location = new Point(0, 0);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1206, 208);
            pnlInput.TabIndex = 0;
            // 
            // dgvVatPham
            // 
            dgvVatPham.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVatPham.Dock = DockStyle.Fill;
            dgvVatPham.Location = new Point(0, 208);
            dgvVatPham.Name = "dgvVatPham";
            dgvVatPham.Size = new Size(1206, 447);
            dgvVatPham.TabIndex = 1;
            dgvVatPham.CellClick += dgvVatPham_CellClick;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(12, 14);
            lblID.Name = "lblID";
            lblID.Size = new Size(44, 15);
            lblID.TabIndex = 0;
            lblID.Text = "Mã VP:";
            // 
            // txtMaVatPham
            // 
            txtMaVatPham.Location = new Point(121, 6);
            txtMaVatPham.Name = "txtMaVatPham";
            txtMaVatPham.ReadOnly = true;
            txtMaVatPham.Size = new Size(154, 23);
            txtMaVatPham.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 42);
            lblName.Name = "lblName";
            lblName.Size = new Size(83, 15);
            lblName.TabIndex = 2;
            lblName.Text = "Tên Vật Phẩm:";
            // 
            // txtTenVatPham
            // 
            txtTenVatPham.Location = new Point(121, 34);
            txtTenVatPham.Name = "txtTenVatPham";
            txtTenVatPham.Size = new Size(154, 23);
            txtTenVatPham.TabIndex = 3;
            // 
            // blPrice
            // 
            blPrice.AutoSize = true;
            blPrice.Location = new Point(12, 70);
            blPrice.Name = "blPrice";
            blPrice.Size = new Size(50, 15);
            blPrice.TabIndex = 4;
            blPrice.Text = "Giá Bán:";
            // 
            // numGia
            // 
            numGia.Location = new Point(121, 62);
            numGia.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numGia.Name = "numGia";
            numGia.Size = new Size(154, 23);
            numGia.TabIndex = 5;
            // 
            // lblCurrency
            // 
            lblCurrency.AutoSize = true;
            lblCurrency.Location = new Point(352, 14);
            lblCurrency.Name = "lblCurrency";
            lblCurrency.Size = new Size(58, 15);
            lblCurrency.TabIndex = 6;
            lblCurrency.Text = "Loại Tiền:";
            // 
            // cboLoaiTien
            // 
            cboLoaiTien.FormattingEnabled = true;
            cboLoaiTien.Location = new Point(461, 6);
            cboLoaiTien.Name = "cboLoaiTien";
            cboLoaiTien.Size = new Size(154, 23);
            cboLoaiTien.TabIndex = 7;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(349, 42);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(44, 15);
            lblDesc.TabIndex = 8;
            lblDesc.Text = "Mô Tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(461, 35);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(154, 23);
            txtMoTa.TabIndex = 9;
            // 
            // chkConHang
            // 
            chkConHang.AutoSize = true;
            chkConHang.Location = new Point(461, 70);
            chkConHang.Name = "chkConHang";
            chkConHang.Size = new Size(140, 19);
            chkConHang.TabIndex = 10;
            chkConHang.Text = "Đang bán (Còn hàng)";
            chkConHang.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.LimeGreen;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnThem.Location = new Point(64, 146);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(106, 40);
            btnThem.TabIndex = 11;
            btnThem.Text = "➕ THÊM";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Coral;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnSua.Location = new Point(255, 146);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(106, 40);
            btnSua.TabIndex = 12;
            btnSua.Text = "✏️ SỬA";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnXoa.Location = new Point(461, 146);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(106, 40);
            btnXoa.TabIndex = 13;
            btnXoa.Text = "🗑 XÓA";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Gray;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnLamMoi.Location = new Point(657, 146);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(131, 40);
            btnLamMoi.TabIndex = 14;
            btnLamMoi.Text = "🔄 LÀM MỚI";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // QuanLyCuaHangAdminPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvVatPham);
            Controls.Add(pnlInput);
            Name = "QuanLyCuaHangAdminPage";
            Size = new Size(1206, 655);
            Load += QuanLyCuaHangAdminPage_Load;
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVatPham).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGia).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlInput;
        private TextBox txtMaVatPham;
        private Label lblID;
        private DataGridView dgvVatPham;
        private TextBox txtMoTa;
        private Label lblDesc;
        private ComboBox cboLoaiTien;
        private Label lblCurrency;
        private NumericUpDown numGia;
        private Label blPrice;
        private TextBox txtTenVatPham;
        private Label lblName;
        private Button btnLamMoi;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
        private CheckBox chkConHang;
    }
}
