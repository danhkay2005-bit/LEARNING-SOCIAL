namespace WinForms.UserControls.Pages
{
    partial class HocTapPage
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
            pnlMainContent = new FlowLayoutPanel();
            pnlHeader = new Panel();
            label2 = new Label();
            label3 = new Label();
            btnThamGia = new Button();
            btnTaoQuiz = new Button();
            pictureBox1 = new PictureBox();
            lblMyQuizzes = new Label();
            flowBoDeCuaToi = new FlowLayoutPanel();
            pnlMyNavigation = new FlowLayoutPanel();
            btnPrevMy = new Button();
            btnNextMy = new Button();
            pnlFilterContainer = new FlowLayoutPanel();
            lblFilterTitle = new Label();
            cbbLocChuDe = new ComboBox();
            lblPublicQuizzes = new Label();
            flowBoDeCongKhai = new FlowLayoutPanel();
            pnlPublicNavigation = new FlowLayoutPanel();
            btnPrevPublic = new Button();
            btnNextPublic = new Button();
            pnlMainContent.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlMyNavigation.SuspendLayout();
            pnlFilterContainer.SuspendLayout();
            pnlPublicNavigation.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMainContent
            // 
            pnlMainContent.AutoScroll = true;
            pnlMainContent.BackColor = Color.WhiteSmoke;
            pnlMainContent.Controls.Add(pnlHeader);
            pnlMainContent.Controls.Add(lblMyQuizzes);
            pnlMainContent.Controls.Add(flowBoDeCuaToi);
            pnlMainContent.Controls.Add(pnlMyNavigation);
            pnlMainContent.Controls.Add(pnlFilterContainer);
            pnlMainContent.Controls.Add(lblPublicQuizzes);
            pnlMainContent.Controls.Add(flowBoDeCongKhai);
            pnlMainContent.Controls.Add(pnlPublicNavigation);
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
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(label3);
            pnlHeader.Controls.Add(btnThamGia);
            pnlHeader.Controls.Add(btnTaoQuiz);
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Location = new Point(33, 23);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1000, 215);
            pnlHeader.TabIndex = 0;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(450, 0);
            label2.Name = "label2";
            label2.Size = new Size(374, 57);
            label2.TabIndex = 0;
            label2.Text = "Học tập cùng nhau";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 14F);
            label3.ForeColor = Color.FromArgb(200, 220, 220);
            label3.Location = new Point(450, 57);
            label3.Name = "label3";
            label3.Size = new Size(406, 64);
            label3.TabIndex = 1;
            label3.Text = "Thử thách bạn bè hoặc tự ôn luyện kiến thức";
            // 
            // btnThamGia
            // 
            btnThamGia.BackColor = Color.Transparent;
            btnThamGia.FlatAppearance.BorderColor = Color.Gold;
            btnThamGia.FlatAppearance.BorderSize = 2;
            btnThamGia.FlatStyle = FlatStyle.Flat;
            btnThamGia.ForeColor = Color.Gold;
            btnThamGia.Location = new Point(440, 140);
            btnThamGia.Name = "btnThamGia";
            btnThamGia.Size = new Size(240, 50);
            btnThamGia.TabIndex = 2;
            btnThamGia.Text = "NHẬP MÃ THAM GIA";
            btnThamGia.UseVisualStyleBackColor = false;
            // 
            // btnTaoQuiz
            // 
            btnTaoQuiz.BackColor = Color.Gold;
            btnTaoQuiz.FlatStyle = FlatStyle.Flat;
            btnTaoQuiz.ForeColor = Color.FromArgb(13, 56, 56);
            btnTaoQuiz.Location = new Point(700, 140);
            btnTaoQuiz.Name = "btnTaoQuiz";
            btnTaoQuiz.Size = new Size(240, 50);
            btnTaoQuiz.TabIndex = 3;
            btnTaoQuiz.Text = "TẠO QUIZ MỚI";
            btnTaoQuiz.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.unnamed_removebg_preview;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(380, 205);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // lblMyQuizzes
            // 
            lblMyQuizzes.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMyQuizzes.Location = new Point(33, 241);
            lblMyQuizzes.Name = "lblMyQuizzes";
            lblMyQuizzes.Size = new Size(100, 42);
            lblMyQuizzes.TabIndex = 1;
            lblMyQuizzes.Text = "Bộ đề của tôi";
            // 
            // flowBoDeCuaToi
            // 
            flowBoDeCuaToi.AutoScroll = true;
            flowBoDeCuaToi.AutoSize = true;
            flowBoDeCuaToi.Location = new Point(33, 286);
            flowBoDeCuaToi.Name = "flowBoDeCuaToi";
            flowBoDeCuaToi.Size = new Size(0, 0);
            flowBoDeCuaToi.TabIndex = 2;
            flowBoDeCuaToi.WrapContents = false;
            // 
            // pnlMyNavigation
            // 
            pnlMyNavigation.AutoSize = true;
            pnlMyNavigation.Controls.Add(btnPrevMy);
            pnlMyNavigation.Controls.Add(btnNextMy);
            pnlMyNavigation.Location = new Point(33, 292);
            pnlMyNavigation.Name = "pnlMyNavigation";
            pnlMyNavigation.Size = new Size(92, 46);
            pnlMyNavigation.TabIndex = 3;
            // 
            // btnPrevMy
            // 
            btnPrevMy.Location = new Point(3, 3);
            btnPrevMy.Name = "btnPrevMy";
            btnPrevMy.Size = new Size(40, 40);
            btnPrevMy.TabIndex = 0;
            btnPrevMy.Text = "◀";
            btnPrevMy.Visible = false;
            // 
            // btnNextMy
            // 
            btnNextMy.Location = new Point(49, 3);
            btnNextMy.Name = "btnNextMy";
            btnNextMy.Size = new Size(40, 40);
            btnNextMy.TabIndex = 1;
            btnNextMy.Text = "▶";
            btnNextMy.Visible = false;
            // 
            // pnlFilterContainer
            // 
            pnlFilterContainer.AutoSize = true;
            pnlFilterContainer.Controls.Add(lblFilterTitle);
            pnlFilterContainer.Controls.Add(cbbLocChuDe);
            pnlFilterContainer.Location = new Point(30, 371);
            pnlFilterContainer.Margin = new Padding(0, 30, 0, 10);
            pnlFilterContainer.Name = "pnlFilterContainer";
            pnlFilterContainer.Size = new Size(512, 47);
            pnlFilterContainer.TabIndex = 4;
            // 
            // lblFilterTitle
            // 
            lblFilterTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblFilterTitle.Location = new Point(3, 0);
            lblFilterTitle.Name = "lblFilterTitle";
            lblFilterTitle.Size = new Size(100, 47);
            lblFilterTitle.TabIndex = 0;
            lblFilterTitle.Text = "Lọc nhanh chủ đề";
            // 
            // cbbLocChuDe
            // 
            cbbLocChuDe.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbLocChuDe.Font = new Font("Segoe UI", 12F);
            cbbLocChuDe.Location = new Point(109, 3);
            cbbLocChuDe.Name = "cbbLocChuDe";
            cbbLocChuDe.Size = new Size(400, 36);
            cbbLocChuDe.TabIndex = 1;
            // 
            // lblPublicQuizzes
            // 
            lblPublicQuizzes.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPublicQuizzes.Location = new Point(33, 428);
            lblPublicQuizzes.Name = "lblPublicQuizzes";
            lblPublicQuizzes.Size = new Size(100, 44);
            lblPublicQuizzes.TabIndex = 5;
            lblPublicQuizzes.Text = "Bộ đề công khai";
            // 
            // flowBoDeCongKhai
            // 
            flowBoDeCongKhai.AutoScroll = true;
            flowBoDeCongKhai.AutoSize = true;
            flowBoDeCongKhai.Location = new Point(33, 475);
            flowBoDeCongKhai.Name = "flowBoDeCongKhai";
            flowBoDeCongKhai.Size = new Size(0, 0);
            flowBoDeCongKhai.TabIndex = 6;
            flowBoDeCongKhai.WrapContents = false;
            // 
            // pnlPublicNavigation
            // 
            pnlPublicNavigation.AutoSize = true;
            pnlPublicNavigation.Controls.Add(btnPrevPublic);
            pnlPublicNavigation.Controls.Add(btnNextPublic);
            pnlPublicNavigation.Location = new Point(33, 481);
            pnlPublicNavigation.Name = "pnlPublicNavigation";
            pnlPublicNavigation.Size = new Size(92, 46);
            pnlPublicNavigation.TabIndex = 7;
            // 
            // btnPrevPublic
            // 
            btnPrevPublic.Location = new Point(3, 3);
            btnPrevPublic.Name = "btnPrevPublic";
            btnPrevPublic.Size = new Size(40, 40);
            btnPrevPublic.TabIndex = 0;
            btnPrevPublic.Text = "◀";
            btnPrevPublic.Visible = false;
            // 
            // btnNextPublic
            // 
            btnNextPublic.Location = new Point(49, 3);
            btnNextPublic.Name = "btnNextPublic";
            btnNextPublic.Size = new Size(40, 40);
            btnNextPublic.TabIndex = 1;
            btnNextPublic.Text = "▶";
            btnNextPublic.Visible = false;
            // 
            // HocTapPage
            // 
            Controls.Add(pnlMainContent);
            Name = "HocTapPage";
            Size = new Size(1100, 800);
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlMyNavigation.ResumeLayout(false);
            pnlFilterContainer.ResumeLayout(false);
            pnlPublicNavigation.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel pnlMainContent;
        private Panel pnlHeader;
        private PictureBox pictureBox1;
        private Button btnTaoQuiz;
        private Button btnThamGia;
        private Label label2;
        private Label label3;

        private Label lblMyQuizzes;
        private FlowLayoutPanel flowBoDeCuaToi;
        private FlowLayoutPanel pnlMyNavigation;
        private Button btnPrevMy;
        private Button btnNextMy;

        private FlowLayoutPanel pnlFilterContainer;
        private Label lblFilterTitle;
        private ComboBox cbbLocChuDe;

        private Label lblPublicQuizzes;
        private FlowLayoutPanel flowBoDeCongKhai;
        private FlowLayoutPanel pnlPublicNavigation;
        private Button btnPrevPublic;
        private Button btnNextPublic;
    }
}
