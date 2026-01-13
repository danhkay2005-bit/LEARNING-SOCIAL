namespace WinForms.UserControls.Quiz
{
    partial class HeaderEditorControl
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
            pictureBox1 = new PictureBox();
            btnThemAnh = new Button();
            btnTaoAnhAI = new Button();
            lbl1 = new Label();
            txtTenBoDe = new TextBox();
            txtMoTa = new TextBox();
            label2 = new Label();
            cbbChuDe = new ComboBox();
            lbl3 = new Label();
            cmbRiengTu = new ComboBox();
            lbl4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(13, 56, 56);
            pictureBox1.Location = new Point(134, 88);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(343, 327);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnThemAnh
            // 
            btnThemAnh.BackColor = Color.Gold;
            btnThemAnh.Cursor = Cursors.Hand;
            btnThemAnh.FlatAppearance.BorderSize = 0;
            btnThemAnh.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnThemAnh.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnThemAnh.FlatStyle = FlatStyle.Flat;
            btnThemAnh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThemAnh.ForeColor = Color.FromArgb(13, 56, 56);
            btnThemAnh.Location = new Point(241, 219);
            btnThemAnh.Name = "btnThemAnh";
            btnThemAnh.Size = new Size(122, 39);
            btnThemAnh.TabIndex = 6;
            btnThemAnh.Text = "Thêm ảnh";
            btnThemAnh.UseVisualStyleBackColor = false;
            // 
            // btnTaoAnhAI
            // 
            btnTaoAnhAI.BackColor = Color.FromArgb(13, 56, 56);
            btnTaoAnhAI.Cursor = Cursors.Hand;
            btnTaoAnhAI.FlatAppearance.BorderSize = 0;
            btnTaoAnhAI.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnTaoAnhAI.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnTaoAnhAI.FlatStyle = FlatStyle.Flat;
            btnTaoAnhAI.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTaoAnhAI.ForeColor = Color.White;
            btnTaoAnhAI.Location = new Point(185, 277);
            btnTaoAnhAI.Name = "btnTaoAnhAI";
            btnTaoAnhAI.Size = new Size(249, 39);
            btnTaoAnhAI.TabIndex = 7;
            btnTaoAnhAI.Text = "Tạo ảnh bằng AI ở đây";
            btnTaoAnhAI.UseVisualStyleBackColor = false;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl1.ForeColor = Color.Black;
            lbl1.Location = new Point(542, 88);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(180, 31);
            lbl1.TabIndex = 8;
            lbl1.Text = "Nhập tên bộ đề";
            lbl1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtTenBoDe
            // 
            txtTenBoDe.Location = new Point(550, 119);
            txtTenBoDe.Multiline = true;
            txtTenBoDe.Name = "txtTenBoDe";
            txtTenBoDe.Size = new Size(404, 39);
            txtTenBoDe.TabIndex = 9;
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(550, 204);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(404, 139);
            txtMoTa.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(542, 173);
            label2.Name = "label2";
            label2.Size = new Size(140, 31);
            label2.TabIndex = 10;
            label2.Text = "Nhập mô tả";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbbChuDe
            // 
            cbbChuDe.FormattingEnabled = true;
            cbbChuDe.Location = new Point(550, 387);
            cbbChuDe.Name = "cbbChuDe";
            cbbChuDe.Size = new Size(155, 28);
            cbbChuDe.TabIndex = 12;
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl3.ForeColor = Color.Black;
            lbl3.Location = new Point(550, 353);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(148, 31);
            lbl3.TabIndex = 13;
            lbl3.Text = "Chọn chủ đề";
            lbl3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbRiengTu
            // 
            cmbRiengTu.FormattingEnabled = true;
            cmbRiengTu.Location = new Point(777, 387);
            cmbRiengTu.Name = "cmbRiengTu";
            cmbRiengTu.Size = new Size(151, 28);
            cmbRiengTu.TabIndex = 14;
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl4.ForeColor = Color.Black;
            lbl4.Location = new Point(777, 353);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(148, 31);
            lbl4.TabIndex = 15;
            lbl4.Text = "Chọn chế độ";
            lbl4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDoKho
            // 
            this.lblDoKho = new Label();
            this.lblDoKho.AutoSize = true;
            this.lblDoKho.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblDoKho.ForeColor = Color.Black;
            this.lblDoKho.Location = new Point(550, 425); // Nằm dưới cbbChuDe
            this.lblDoKho.Name = "lblDoKho";
            this.lblDoKho.Size = new Size(100, 31);
            this.lblDoKho.TabIndex = 16;
            this.lblDoKho.Text = "Độ khó";
            // 
            // cbbDoKho
            // 
            this.cbbDoKho = new ComboBox();
            this.cbbDoKho.DropDownStyle = ComboBoxStyle.DropDownList; // Chỉ cho phép chọn, không cho gõ
            this.cbbDoKho.FormattingEnabled = true;
            this.cbbDoKho.Location = new Point(550, 460);
            this.cbbDoKho.Name = "cbbDoKho";
            this.cbbDoKho.Size = new Size(155, 28);
            this.cbbDoKho.TabIndex = 17;

            // Thêm vào danh sách Controls của UserControl
            this.Controls.Add(this.lblDoKho);
            this.Controls.Add(this.cbbDoKho);
            // 
            // HeaderEditorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lbl4);
            Controls.Add(cmbRiengTu);
            Controls.Add(lbl3);
            Controls.Add(cbbChuDe);
            Controls.Add(txtMoTa);
            Controls.Add(label2);
            Controls.Add(txtTenBoDe);
            Controls.Add(lbl1);
            Controls.Add(btnTaoAnhAI);
            Controls.Add(btnThemAnh);
            Controls.Add(pictureBox1);
            Name = "HeaderEditorControl";
            Size = new Size(1144, 514);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnThemAnh;
        private Button btnTaoAnhAI;
        private Label lbl1;
        private TextBox txtTenBoDe;
        private TextBox txtMoTa;
        private Label label2;
        private ComboBox cbbChuDe;
        private Label lbl3;
        private ComboBox cmbRiengTu;
        private Label lbl4;
        private Label lblDoKho;
        private ComboBox cbbDoKho;
    }
}
