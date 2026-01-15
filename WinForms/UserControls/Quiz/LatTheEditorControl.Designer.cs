namespace WinForms.UserControls.Quiz
{
    partial class LatTheEditorControl
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
            lblFront = new Label();
            txtMatTruoc = new TextBox();
            lblBack = new Label();
            txtMatSau = new TextBox();
            lblGiaiThich = new Label();
            txtGiaiThich = new TextBox();
            picFront = new PictureBox();
            picBack = new PictureBox();
            btnPickFront = new Button();
            btnPickBack = new Button();
            ((System.ComponentModel.ISupportInitialize)picFront).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBack).BeginInit();
            SuspendLayout();
            // 
            // lblFront (Mặt trước)
            // 
            lblFront.AutoSize = true;
            lblFront.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFront.ForeColor = Color.FromArgb(13, 56, 56);
            lblFront.Location = new Point(25, 20);
            lblFront.Text = "Mặt trước (Câu hỏi / Thuật ngữ)";
            // 
            // txtMatTruoc
            // 
            txtMatTruoc.Location = new Point(30, 55);
            txtMatTruoc.Multiline = true;
            txtMatTruoc.Name = "txtMatTruoc";
            txtMatTruoc.Size = new Size(450, 100);
            // 
            // picFront
            // 
            picFront.BackColor = Color.Gainsboro;
            picFront.BorderStyle = BorderStyle.FixedSingle;
            picFront.Location = new Point(500, 55);
            picFront.Size = new Size(120, 100);
            picFront.SizeMode = PictureBoxSizeMode.Zoom;
            // 
            // btnPickFront
            // 
            btnPickFront.BackColor = Color.Gold;
            btnPickFront.FlatStyle = FlatStyle.Flat;
            btnPickFront.Location = new Point(500, 160);
            btnPickFront.Size = new Size(120, 30);
            btnPickFront.Text = "Ảnh mặt trước";
            // 
            // lblBack (Mặt sau)
            // 
            lblBack.AutoSize = true;
            lblBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBack.ForeColor = Color.FromArgb(13, 56, 56);
            lblBack.Location = new Point(25, 180);
            lblBack.Text = "Mặt sau (Đáp án / Định nghĩa)";
            // 
            // txtMatSau
            // 
            txtMatSau.Location = new Point(30, 215);
            txtMatSau.Multiline = true;
            txtMatSau.Name = "txtMatSau";
            txtMatSau.Size = new Size(450, 100);
            // 
            // picBack
            // 
            picBack.BackColor = Color.Gainsboro;
            picBack.BorderStyle = BorderStyle.FixedSingle;
            picBack.Location = new Point(500, 215);
            picBack.Size = new Size(120, 100);
            picBack.SizeMode = PictureBoxSizeMode.Zoom;
            // 
            // btnPickBack
            // 
            btnPickBack.BackColor = Color.Gold;
            btnPickBack.FlatStyle = FlatStyle.Flat;
            btnPickBack.Location = new Point(500, 320);
            btnPickBack.Size = new Size(120, 30);
            btnPickBack.Text = "Ảnh mặt sau";
            // 
            // lblGiaiThich
            // 
            lblGiaiThich.AutoSize = true;
            lblGiaiThich.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblGiaiThich.Location = new Point(25, 330);
            lblGiaiThich.Text = "Giải thích thêm (Không bắt buộc)";
            // 
            // txtGiaiThich
            // 
            txtGiaiThich.Location = new Point(30, 360);
            txtGiaiThich.Multiline = true;
            txtGiaiThich.Name = "txtGiaiThich";
            txtGiaiThich.Size = new Size(590, 60);
            // 
            // LatTheEditorControl
            // 
            BackColor = Color.White;
            Controls.Add(btnPickBack);
            Controls.Add(picBack);
            Controls.Add(btnPickFront);
            Controls.Add(picFront);
            Controls.Add(txtGiaiThich);
            Controls.Add(lblGiaiThich);
            Controls.Add(txtMatSau);
            Controls.Add(lblBack);
            Controls.Add(txtMatTruoc);
            Controls.Add(lblFront);
            Size = new Size(700, 450);
            ((System.ComponentModel.ISupportInitialize)picFront).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBack).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblFront;
        private TextBox txtMatTruoc;
        private Label lblBack;
        private TextBox txtMatSau;
        private Label lblGiaiThich;
        private TextBox txtGiaiThich;
        private PictureBox picFront;
        private PictureBox picBack;
        private Button btnPickFront;
        private Button btnPickBack;
    }
}