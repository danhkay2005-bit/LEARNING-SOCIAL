namespace WinForms.UserControls.Quiz
{
    partial class SapXepEditorControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtCauGoc = new TextBox();
            btnPhanTich = new Button();
            flpPreview = new FlowLayoutPanel();
            lblHuongDan = new Label();
            lblGiaiThich = new Label();
            txtGiaiThich = new TextBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(13, 56, 56);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Text = "Nhập câu hoặc chuỗi cần sắp xếp";
            // 
            // txtCauGoc
            // 
            txtCauGoc.Font = new Font("Segoe UI", 12F);
            txtCauGoc.Location = new Point(25, 60);
            txtCauGoc.Multiline = true;
            txtCauGoc.Name = "txtCauGoc";
            txtCauGoc.Size = new Size(600, 80);
            txtCauGoc.PlaceholderText = "Ví dụ: Học lập trình C# rất thú vị";
            // 
            // btnPhanTich
            // 
            btnPhanTich.BackColor = Color.Gold;
            btnPhanTich.Cursor = Cursors.Hand;
            btnPhanTich.FlatAppearance.BorderSize = 0;
            btnPhanTich.FlatStyle = FlatStyle.Flat;
            btnPhanTich.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnPhanTich.ForeColor = Color.FromArgb(13, 56, 56);
            btnPhanTich.Location = new Point(640, 60);
            btnPhanTich.Size = new Size(130, 80);
            btnPhanTich.Text = "Chia từ";
            btnPhanTich.UseVisualStyleBackColor = false;
            // 
            // flpPreview
            // 
            flpPreview.AutoScroll = true;
            flpPreview.BackColor = Color.FromArgb(245, 245, 245);
            flpPreview.BorderStyle = BorderStyle.FixedSingle;
            flpPreview.Location = new Point(25, 175);
            flpPreview.Name = "flpPreview";
            flpPreview.Padding = new Padding(10);
            flpPreview.Size = new Size(745, 120);
            // 
            // lblHuongDan
            // 
            lblHuongDan.AutoSize = true;
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHuongDan.ForeColor = Color.Gray;
            lblHuongDan.Location = new Point(25, 145);
            lblHuongDan.Text = "* Hệ thống sẽ tự động tách các từ dựa trên khoảng trắng (space).";
            // 
            // lblGiaiThich
            // 
            lblGiaiThich.AutoSize = true;
            lblGiaiThich.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            lblGiaiThich.Location = new Point(20, 310);
            lblGiaiThich.Text = "Giải thích / Dịch nghĩa";
            // 
            // txtGiaiThich
            // 
            txtGiaiThich.Font = new Font("Segoe UI", 10.2F);
            txtGiaiThich.Location = new Point(25, 340);
            txtGiaiThich.Multiline = true;
            txtGiaiThich.Name = "txtGiaiThich";
            txtGiaiThich.Size = new Size(745, 80);
            // 
            // SapXepEditorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(txtGiaiThich);
            Controls.Add(lblGiaiThich);
            Controls.Add(lblHuongDan);
            Controls.Add(flpPreview);
            Controls.Add(btnPhanTich);
            Controls.Add(txtCauGoc);
            Controls.Add(lblTitle);
            Name = "SapXepEditorControl";
            Size = new Size(800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtCauGoc;
        private Button btnPhanTich;
        private FlowLayoutPanel flpPreview;
        private Label lblHuongDan;
        private Label lblGiaiThich;
        private TextBox txtGiaiThich;
    }
}