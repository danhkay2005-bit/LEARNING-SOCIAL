namespace WinForms.UserControls.Pages
{
    partial class FeedPageControl
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnRefresh;

        // Create Post Area
        private Panel pnlCreatePost;
        private Panel pnlCreatePostInner;
        private PictureBox picAvatar;
        private TextBox txtCreatePost;
        private Panel pnlPostActions;
        private Button btnAddImage;
        private Button btnAddVideo;
        private ComboBox cboPrivacy;
        private Button btnPost;

        // Feed Area
        private Panel pnlFeedContainer;
        private FlowLayoutPanel flowFeed;

        // Loading
        private Label lblLoading;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            btnRefresh = new Button();

            pnlCreatePost = new Panel();
            pnlCreatePostInner = new Panel();
            picAvatar = new PictureBox();
            txtCreatePost = new TextBox();
            pnlPostActions = new Panel();
            btnAddImage = new Button();
            btnAddVideo = new Button();
            cboPrivacy = new ComboBox();
            btnPost = new Button();

            pnlFeedContainer = new Panel();
            flowFeed = new FlowLayoutPanel();
            lblLoading = new Label();

            pnlHeader.SuspendLayout();
            pnlCreatePost.SuspendLayout();
            pnlCreatePostInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            pnlPostActions.SuspendLayout();
            pnlFeedContainer.SuspendLayout();
            SuspendLayout();

            // ========================================
            // pnlHeader
            // ========================================
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(900, 60);

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(51, 51, 51);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "📰 Bảng tin";

            // btnRefresh
            btnRefresh.BackColor = Color.FromArgb(240, 242, 245);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F);
            btnRefresh.ForeColor = Color.FromArgb(51, 51, 51);
            btnRefresh.Location = new Point(800, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.Text = "🔄 Tải lại";

            // ========================================
            // pnlCreatePost
            // ========================================
            pnlCreatePost.BackColor = Color.FromArgb(240, 242, 245);
            pnlCreatePost.Controls.Add(pnlCreatePostInner);
            pnlCreatePost.Dock = DockStyle.Top;
            pnlCreatePost.Location = new Point(0, 60);
            pnlCreatePost.Name = "pnlCreatePost";
            pnlCreatePost.Padding = new Padding(20, 15, 20, 15);
            pnlCreatePost.Size = new Size(900, 180);

            // pnlCreatePostInner - Card trắng chứa form tạo bài
            pnlCreatePostInner.BackColor = Color.White;
            pnlCreatePostInner.Controls.Add(txtCreatePost);
            pnlCreatePostInner.Controls.Add(picAvatar);
            pnlCreatePostInner.Controls.Add(pnlPostActions);
            pnlCreatePostInner.Dock = DockStyle.Fill;
            pnlCreatePostInner.Padding = new Padding(15);
            pnlCreatePostInner.Name = "pnlCreatePostInner";

            // picAvatar
            picAvatar.BackColor = Color.FromArgb(70, 130, 180);
            picAvatar.Location = new Point(15, 15);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(45, 45);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picAvatar.TabStop = false;

            // txtCreatePost
            txtCreatePost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCreatePost.BorderStyle = BorderStyle.None;
            txtCreatePost.Font = new Font("Segoe UI", 11F);
            txtCreatePost.ForeColor = Color.FromArgb(51, 51, 51);
            txtCreatePost.Location = new Point(70, 15);
            txtCreatePost.Multiline = true;
            txtCreatePost.Name = "txtCreatePost";
            txtCreatePost.PlaceholderText = "Bạn đang nghĩ gì?  Chia sẻ với mọi người... ";
            txtCreatePost.Size = new Size(755, 70);
            txtCreatePost.MaxLength = 5000;

            // pnlPostActions
            pnlPostActions.BackColor = Color.White;
            pnlPostActions.Controls.Add(btnAddImage);
            pnlPostActions.Controls.Add(btnAddVideo);
            pnlPostActions.Controls.Add(cboPrivacy);
            pnlPostActions.Controls.Add(btnPost);
            pnlPostActions.Dock = DockStyle.Bottom;
            pnlPostActions.Location = new Point(15, 95);
            pnlPostActions.Name = "pnlPostActions";
            pnlPostActions.Padding = new Padding(55, 10, 0, 5);
            pnlPostActions.Size = new Size(830, 50);

            // btnAddImage
            btnAddImage.BackColor = Color.FromArgb(240, 242, 245);
            btnAddImage.Cursor = Cursors.Hand;
            btnAddImage.Dock = DockStyle.Left;
            btnAddImage.FlatAppearance.BorderSize = 0;
            btnAddImage.FlatStyle = FlatStyle.Flat;
            btnAddImage.Font = new Font("Segoe UI", 9F);
            btnAddImage.ForeColor = Color.FromArgb(45, 134, 89);
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Size = new Size(100, 35);
            btnAddImage.Text = "🖼️ Ảnh";

            // btnAddVideo
            btnAddVideo.BackColor = Color.FromArgb(240, 242, 245);
            btnAddVideo.Cursor = Cursors.Hand;
            btnAddVideo.Dock = DockStyle.Left;
            btnAddVideo.FlatAppearance.BorderSize = 0;
            btnAddVideo.FlatStyle = FlatStyle.Flat;
            btnAddVideo.Font = new Font("Segoe UI", 9F);
            btnAddVideo.ForeColor = Color.FromArgb(214, 41, 118);
            btnAddVideo.Name = "btnAddVideo";
            btnAddVideo.Size = new Size(100, 35);
            btnAddVideo.Text = "🎬 Video";

            // cboPrivacy
            cboPrivacy.BackColor = Color.FromArgb(240, 242, 245);
            cboPrivacy.Dock = DockStyle.Left;
            cboPrivacy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPrivacy.FlatStyle = FlatStyle.Flat;
            cboPrivacy.Font = new Font("Segoe UI", 9F);
            cboPrivacy.Name = "cboPrivacy";
            cboPrivacy.Size = new Size(130, 35);

            // btnPost
            btnPost.BackColor = Color.FromArgb(0, 132, 255);
            btnPost.Cursor = Cursors.Hand;
            btnPost.Dock = DockStyle.Right;
            btnPost.FlatAppearance.BorderSize = 0;
            btnPost.FlatStyle = FlatStyle.Flat;
            btnPost.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPost.ForeColor = Color.White;
            btnPost.Name = "btnPost";
            btnPost.Size = new Size(100, 35);
            btnPost.Text = "Đăng bài";

            // ========================================
            // pnlFeedContainer
            // ========================================
            pnlFeedContainer.BackColor = Color.FromArgb(240, 242, 245);
            pnlFeedContainer.Controls.Add(flowFeed);
            pnlFeedContainer.Controls.Add(lblLoading);
            pnlFeedContainer.Dock = DockStyle.Fill;
            pnlFeedContainer.Location = new Point(0, 240);
            pnlFeedContainer.Name = "pnlFeedContainer";
            pnlFeedContainer.Padding = new Padding(20, 10, 20, 10);

            // flowFeed
            flowFeed.AutoScroll = true;
            flowFeed.BackColor = Color.FromArgb(240, 242, 245);
            flowFeed.Dock = DockStyle.Fill;
            flowFeed.FlowDirection = FlowDirection.TopDown;
            flowFeed.Name = "flowFeed";
            flowFeed.Padding = new Padding(0);
            flowFeed.WrapContents = false;

            // lblLoading
            lblLoading.BackColor = Color.FromArgb(240, 242, 245);
            lblLoading.Dock = DockStyle.Fill;
            lblLoading.Font = new Font("Segoe UI", 12F);
            lblLoading.ForeColor = Color.Gray;
            lblLoading.Name = "lblLoading";
            lblLoading.Text = "⏳ Đang tải... ";
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            lblLoading.Visible = false;

            // ========================================
            // FeedPageControl
            // ========================================
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            Controls.Add(pnlFeedContainer);
            Controls.Add(pnlCreatePost);
            Controls.Add(pnlHeader);
            Name = "FeedPageControl";
            Size = new Size(900, 600);

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlCreatePost.ResumeLayout(false);
            pnlCreatePostInner.ResumeLayout(false);
            pnlCreatePostInner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            pnlPostActions.ResumeLayout(false);
            pnlFeedContainer.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}