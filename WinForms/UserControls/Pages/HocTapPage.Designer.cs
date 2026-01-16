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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlMainContent = new FlowLayoutPanel();
            pnlHeader = new Panel();
            label3 = new Label();
            label2 = new Label();
            btnThamGia = new Button(); // Nút mới
            btnTaoQuiz = new Button();
            pictureBox1 = new PictureBox();
            lblMyQuizzes = new Label();
            flowBoDeCuaToi = new FlowLayoutPanel();
            lblTopics = new Label();
            flowChuDe = new FlowLayoutPanel();
            lblPublicQuizzes = new Label();
            flowBoDeCongKhai = new FlowLayoutPanel();
            lblFolders = new Label();
            flowThuMuc = new FlowLayoutPanel();

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
            pnlMainContent.Controls.Add(lblMyQuizzes);
            pnlMainContent.Controls.Add(flowBoDeCuaToi);
            pnlMainContent.Controls.Add(lblTopics);
            pnlMainContent.Controls.Add(flowChuDe);
            pnlMainContent.Controls.Add(lblPublicQuizzes);
            pnlMainContent.Controls.Add(flowBoDeCongKhai);
            pnlMainContent.Controls.Add(lblFolders);
            pnlMainContent.Controls.Add(flowThuMuc);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.FlowDirection = FlowDirection.TopDown;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Padding = new Padding(30, 20, 30, 20);
            pnlMainContent.Size = new Size(1100, 800);
            pnlMainContent.TabIndex = 0;
            pnlMainContent.WrapContents = false;

            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(13, 56, 56);
            pnlHeader.Controls.Add(label3);
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(btnThamGia);
            pnlHeader.Controls.Add(btnTaoQuiz);
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Location = new Point(33, 23);
            pnlHeader.Margin = new Padding(3, 3, 3, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1000, 215);
            pnlHeader.TabIndex = 0;

            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(440, 30);
            label2.Name = "label2";
            label2.Size = new Size(245, 46);
            label2.Text = "Học tập cùng AI";

            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.ForeColor = Color.FromArgb(200, 220, 220);
            label3.Location = new Point(440, 80);
            label3.Name = "label3";
            label3.Size = new Size(500, 32);
            label3.Text = "Thử thách bạn bè hoặc tự ôn luyện kiến thức";

            // 
            // btnThamGia (MỚI)
            // 
            btnThamGia.BackColor = Color.Transparent;
            btnThamGia.Cursor = Cursors.Hand;
            btnThamGia.FlatAppearance.BorderColor = Color.Gold;
            btnThamGia.FlatAppearance.BorderSize = 2;
            btnThamGia.FlatStyle = FlatStyle.Flat;
            btnThamGia.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnThamGia.ForeColor = Color.Gold;
            btnThamGia.Location = new Point(440, 140);
            btnThamGia.Name = "btnThamGia";
            btnThamGia.Size = new Size(240, 50);
            btnThamGia.TabIndex = 1;
            btnThamGia.Text = "NHẬP MÃ THAM GIA";
            btnThamGia.UseVisualStyleBackColor = false;
            btnThamGia.Click += btnThamGia_Click;

            // 
            // btnTaoQuiz
            // 
            btnTaoQuiz.BackColor = Color.Gold;
            btnTaoQuiz.Cursor = Cursors.Hand;
            btnTaoQuiz.FlatAppearance.BorderSize = 0;
            btnTaoQuiz.FlatStyle = FlatStyle.Flat;
            btnTaoQuiz.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnTaoQuiz.ForeColor = Color.FromArgb(13, 56, 56);
            btnTaoQuiz.Location = new Point(700, 140);
            btnTaoQuiz.Name = "btnTaoQuiz";
            btnTaoQuiz.Size = new Size(240, 50);
            btnTaoQuiz.TabIndex = 0;
            btnTaoQuiz.Text = "TẠO QUIZ MỚI";
            btnTaoQuiz.UseVisualStyleBackColor = false;
            btnTaoQuiz.Click += btnTaoQuiz_Click;

            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(20, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(380, 205);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;

            // 
            // lblMyQuizzes
            // 
            lblMyQuizzes.AutoSize = true;
            lblMyQuizzes.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMyQuizzes.Location = new Point(30, 268);
            lblMyQuizzes.Name = "lblMyQuizzes";
            lblMyQuizzes.Size = new Size(184, 37);
            lblMyQuizzes.Text = "Bộ đề của tôi";

            // 
            // flowBoDeCuaToi
            // 
            flowBoDeCuaToi.AutoScroll = true;
            flowBoDeCuaToi.Location = new Point(33, 313);
            flowBoDeCuaToi.Name = "flowBoDeCuaToi";
            flowBoDeCuaToi.Size = new Size(1020, 260);
            flowBoDeCuaToi.WrapContents = false;

            // 
            // lblTopics
            // 
            lblTopics.AutoSize = true;
            lblTopics.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTopics.Location = new Point(30, 600);
            lblTopics.Name = "lblTopics";
            lblTopics.Size = new Size(303, 37);
            lblTopics.Text = "Khám phá theo chủ đề";

            // 
            // flowChuDe
            // 
            flowChuDe.AutoScroll = true;
            flowChuDe.Location = new Point(33, 645);
            flowChuDe.Name = "flowChuDe";
            flowChuDe.Size = new Size(1020, 260);
            flowChuDe.WrapContents = false;

            // 
            // HocTapPage
            // 
            Controls.Add(pnlMainContent);
            Name = "HocTapPage";
            Size = new Size(1100, 800);
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel pnlMainContent;
        private Panel pnlHeader;
        private PictureBox pictureBox1;
        private Button btnTaoQuiz;
        private Button btnThamGia; // Khai báo nút mới
        private Label lblMyQuizzes;
        private FlowLayoutPanel flowBoDeCuaToi;
        private Label lblTopics;
        private FlowLayoutPanel flowChuDe;
        private Label lblPublicQuizzes;
        private FlowLayoutPanel flowBoDeCongKhai;
        private Label lblFolders;
        private FlowLayoutPanel flowThuMuc;
        private Label label3;
        private Label label2;
    }
}