namespace WinForms.UserControls.Pages
{
    partial class HocTapPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMainContent = new FlowLayoutPanel();
            pnlHeader = new Panel();
            btnTaoQuiz = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            lbl1 = new Label();
            lblRecentlyPublished = new Label();
            flowRecentlyPublished = new FlowLayoutPanel();
            label2 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pnlMainContent.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.AutoScroll = true;
            pnlMainContent.BackColor = Color.WhiteSmoke;
            pnlMainContent.Controls.Add(pnlHeader);
            pnlMainContent.Controls.Add(lblRecentlyPublished);
            pnlMainContent.Controls.Add(flowRecentlyPublished);
            pnlMainContent.Controls.Add(label2);
            pnlMainContent.Controls.Add(flowLayoutPanel1);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.FlowDirection = FlowDirection.TopDown;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Padding = new Padding(20);
            pnlMainContent.Size = new Size(1069, 534);
            pnlMainContent.TabIndex = 0;
            pnlMainContent.WrapContents = false;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(13, 56, 56);
            pnlHeader.Controls.Add(btnTaoQuiz);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Controls.Add(lbl1);
            pnlHeader.Location = new Point(23, 23);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1000, 200);
            pnlHeader.TabIndex = 0;
            // 
            // btnTaoQuiz
            // 
            btnTaoQuiz.BackColor = Color.Gold;
            btnTaoQuiz.Cursor = Cursors.Hand;
            btnTaoQuiz.FlatAppearance.BorderSize = 0;
            btnTaoQuiz.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnTaoQuiz.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnTaoQuiz.FlatStyle = FlatStyle.Flat;
            btnTaoQuiz.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTaoQuiz.ForeColor = Color.FromArgb(13, 56, 56);
            btnTaoQuiz.Location = new Point(557, 136);
            btnTaoQuiz.Name = "btnTaoQuiz";
            btnTaoQuiz.Size = new Size(263, 49);
            btnTaoQuiz.TabIndex = 3;
            btnTaoQuiz.Text = "TẠO QUIZ NGAY";
            btnTaoQuiz.UseVisualStyleBackColor = false;
            btnTaoQuiz.Click += btnTaoQuiz_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(339, 74);
            label1.Name = "label1";
            label1.Size = new Size(648, 46);
            label1.TabIndex = 2;
            label1.Text = "Thỏa sức sáng tạo với bộ quiz của mình";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.unnamed_removebg_preview;
            pictureBox1.Location = new Point(63, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(255, 182);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl1.ForeColor = Color.White;
            lbl1.Location = new Point(568, 15);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(235, 46);
            lbl1.TabIndex = 0;
            lbl1.Text = "Tạo một Quiz";
            // 
            // lblRecentlyPublished
            // 
            lblRecentlyPublished.AutoSize = true;
            lblRecentlyPublished.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblRecentlyPublished.Location = new Point(20, 246);
            lblRecentlyPublished.Margin = new Padding(0, 20, 0, 10);
            lblRecentlyPublished.Name = "lblRecentlyPublished";
            lblRecentlyPublished.Size = new Size(164, 32);
            lblRecentlyPublished.TabIndex = 1;
            lblRecentlyPublished.Text = "Bộ đề của tôi";
            // 
            // flowRecentlyPublished
            // 
            flowRecentlyPublished.AutoScroll = true;
            flowRecentlyPublished.Location = new Point(23, 291);
            flowRecentlyPublished.Name = "flowRecentlyPublished";
            flowRecentlyPublished.Size = new Size(1020, 250);
            flowRecentlyPublished.TabIndex = 2;
            flowRecentlyPublished.WrapContents = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(20, 564);
            label2.Margin = new Padding(0, 20, 0, 10);
            label2.Name = "label2";
            label2.Size = new Size(114, 32);
            label2.TabIndex = 3;
            label2.Text = "Thư mục";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(23, 609);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1020, 250);
            flowLayoutPanel1.TabIndex = 3;
            flowLayoutPanel1.WrapContents = false;
            // 
            // HocTapPage
            // 
            Controls.Add(pnlMainContent);
            Name = "HocTapPage";
            Size = new Size(1069, 534);
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private FlowLayoutPanel pnlMainContent;
        private Panel pnlHeader;
        private Label lblRecentlyPublished;
        private FlowLayoutPanel flowRecentlyPublished;
        private Label lbl1;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private Button btnTaoQuiz;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}