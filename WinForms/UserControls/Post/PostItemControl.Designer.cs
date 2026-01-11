using System.Drawing.Printing;
using System.Windows.Forms;

namespace WinForms.UserControls.Post
{
    partial class PostItemControl: UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlMain;

        // Header
        private Panel pnlHeader;
        private PictureBox picUserAvatar;
        private Panel pnlUserInfo;
        private Label lblUserName;
        private Label lblPostTime;
        private Label lblPrivacy;
        private Button btnMenu;

        // Content
        private Panel pnlContent;
        private Label lblContent;
        private PictureBox picImage;
        private Panel pnlHashtags;

        // Stats
        private Panel pnlStats;
        private Label lblReactionCount;
        private Label lblCommentCount;
        private Label lblViewCount;

        // Actions
        private Panel pnlActions;
        private Button btnLike;
        private Button btnComment;
        private Button btnShare;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlContent = new Panel();
            lblContent = new Label();
            picImage = new PictureBox();
            pnlHashtags = new Panel();
            pnlActions = new Panel();
            btnLike = new Button();
            btnComment = new Button();
            btnShare = new Button();
            pnlStats = new Panel();
            lblReactionCount = new Label();
            lblCommentCount = new Label();
            lblViewCount = new Label();
            pnlHeader = new Panel();
            pnlUserInfo = new Panel();
            lblUserName = new Label();
            lblPostTime = new Label();
            lblPrivacy = new Label();
            picUserAvatar = new PictureBox();
            btnMenu = new Button();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            pnlActions.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlActions);
            pnlMain.Controls.Add(pnlStats);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(15);
            pnlMain.Size = new Size(850, 350);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(lblContent);
            pnlContent.Controls.Add(picImage);
            pnlContent.Controls.Add(pnlHashtags);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(15, 70);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(0, 10, 0, 10);
            pnlContent.Size = new Size(820, 190);
            pnlContent.TabIndex = 0;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Dock = DockStyle.Top;
            lblContent.Font = new Font("Segoe UI", 10F);
            lblContent.ForeColor = Color.FromArgb(51, 51, 51);
            lblContent.Location = new Point(0, 320);
            lblContent.MaximumSize = new Size(800, 0);
            lblContent.Name = "lblContent";
            lblContent.Padding = new Padding(0, 0, 0, 10);
            lblContent.Size = new Size(171, 33);
            lblContent.TabIndex = 0;
            lblContent.Text = "Nội dung bài đăng... ";
            // 
            // picImage
            // 
            picImage.BackColor = Color.FromArgb(240, 242, 245);
            picImage.Dock = DockStyle.Top;
            picImage.Location = new Point(0, 20);
            picImage.Name = "picImage";
            picImage.Size = new Size(820, 300);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 1;
            picImage.TabStop = false;
            picImage.Visible = false;
            // 
            // pnlHashtags
            // 
            pnlHashtags.AutoSize = true;
            pnlHashtags.BackColor = Color.White;
            pnlHashtags.Dock = DockStyle.Top;
            pnlHashtags.Location = new Point(0, 10);
            pnlHashtags.Name = "pnlHashtags";
            pnlHashtags.Padding = new Padding(0, 5, 0, 5);
            pnlHashtags.Size = new Size(820, 10);
            pnlHashtags.TabIndex = 2;
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.White;
            pnlActions.Controls.Add(btnLike);
            pnlActions.Controls.Add(btnComment);
            pnlActions.Controls.Add(btnShare);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(15, 260);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(0, 5, 0, 0);
            pnlActions.Size = new Size(820, 45);
            pnlActions.TabIndex = 1;
            // 
            // btnLike
            // 
            btnLike.BackColor = Color.FromArgb(245, 247, 250);
            btnLike.Cursor = Cursors.Hand;
            btnLike.Dock = DockStyle.Left;
            btnLike.FlatAppearance.BorderSize = 0;
            btnLike.FlatStyle = FlatStyle.Flat;
            btnLike.Font = new Font("Segoe UI", 9F);
            btnLike.ForeColor = Color.FromArgb(101, 103, 107);
            btnLike.Location = new Point(240, 5);
            btnLike.Name = "btnLike";
            btnLike.Size = new Size(120, 40);
            btnLike.TabIndex = 0;
            btnLike.Text = "👍 Thích";
            btnLike.UseVisualStyleBackColor = false;
            // 
            // btnComment
            // 
            btnComment.BackColor = Color.FromArgb(245, 247, 250);
            btnComment.Cursor = Cursors.Hand;
            btnComment.Dock = DockStyle.Left;
            btnComment.FlatAppearance.BorderSize = 0;
            btnComment.FlatStyle = FlatStyle.Flat;
            btnComment.Font = new Font("Segoe UI", 9F);
            btnComment.ForeColor = Color.FromArgb(101, 103, 107);
            btnComment.Location = new Point(120, 5);
            btnComment.Name = "btnComment";
            btnComment.Size = new Size(120, 40);
            btnComment.TabIndex = 1;
            btnComment.Text = "💬 Bình luận";
            btnComment.UseVisualStyleBackColor = false;
            // 
            // btnShare
            // 
            btnShare.BackColor = Color.FromArgb(245, 247, 250);
            btnShare.Cursor = Cursors.Hand;
            btnShare.Dock = DockStyle.Left;
            btnShare.FlatAppearance.BorderSize = 0;
            btnShare.FlatStyle = FlatStyle.Flat;
            btnShare.Font = new Font("Segoe UI", 9F);
            btnShare.ForeColor = Color.FromArgb(101, 103, 107);
            btnShare.Location = new Point(0, 5);
            btnShare.Name = "btnShare";
            btnShare.Size = new Size(120, 40);
            btnShare.TabIndex = 2;
            btnShare.Text = "↗️ Chia sẻ";
            btnShare.UseVisualStyleBackColor = false;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.White;
            pnlStats.Controls.Add(lblReactionCount);
            pnlStats.Controls.Add(lblCommentCount);
            pnlStats.Controls.Add(lblViewCount);
            pnlStats.Dock = DockStyle.Bottom;
            pnlStats.Location = new Point(15, 305);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new Padding(0, 5, 0, 5);
            pnlStats.Size = new Size(820, 30);
            pnlStats.TabIndex = 2;
            // 
            // lblReactionCount
            // 
            lblReactionCount.AutoSize = true;
            lblReactionCount.Dock = DockStyle.Left;
            lblReactionCount.Font = new Font("Segoe UI", 9F);
            lblReactionCount.ForeColor = Color.Gray;
            lblReactionCount.Location = new Point(122, 5);
            lblReactionCount.Name = "lblReactionCount";
            lblReactionCount.Padding = new Padding(0, 3, 15, 0);
            lblReactionCount.Size = new Size(57, 23);
            lblReactionCount.TabIndex = 0;
            lblReactionCount.Text = "❤️ 0";
            // 
            // lblCommentCount
            // 
            lblCommentCount.AutoSize = true;
            lblCommentCount.Dock = DockStyle.Left;
            lblCommentCount.Font = new Font("Segoe UI", 9F);
            lblCommentCount.ForeColor = Color.Gray;
            lblCommentCount.Location = new Point(0, 5);
            lblCommentCount.Name = "lblCommentCount";
            lblCommentCount.Padding = new Padding(0, 3, 15, 0);
            lblCommentCount.Size = new Size(122, 23);
            lblCommentCount.TabIndex = 1;
            lblCommentCount.Text = "💬 0 bình luận";
            // 
            // lblViewCount
            // 
            lblViewCount.AutoSize = true;
            lblViewCount.Dock = DockStyle.Right;
            lblViewCount.Font = new Font("Segoe UI", 9F);
            lblViewCount.ForeColor = Color.Gray;
            lblViewCount.Location = new Point(715, 5);
            lblViewCount.Name = "lblViewCount";
            lblViewCount.Padding = new Padding(0, 3, 0, 0);
            lblViewCount.Size = new Size(105, 23);
            lblViewCount.TabIndex = 2;
            lblViewCount.Text = "👁 0 lượt xem";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(pnlUserInfo);
            pnlHeader.Controls.Add(picUserAvatar);
            pnlHeader.Controls.Add(btnMenu);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(15, 15);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(0, 0, 0, 10);
            pnlHeader.Size = new Size(820, 55);
            pnlHeader.TabIndex = 3;
            // 
            // pnlUserInfo
            // 
            pnlUserInfo.BackColor = Color.White;
            pnlUserInfo.Controls.Add(lblUserName);
            pnlUserInfo.Controls.Add(lblPostTime);
            pnlUserInfo.Controls.Add(lblPrivacy);
            pnlUserInfo.Dock = DockStyle.Fill;
            pnlUserInfo.Location = new Point(45, 0);
            pnlUserInfo.Name = "pnlUserInfo";
            pnlUserInfo.Padding = new Padding(10, 0, 0, 0);
            pnlUserInfo.Size = new Size(735, 45);
            pnlUserInfo.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.FromArgb(51, 51, 51);
            lblUserName.Location = new Point(10, 2);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(137, 23);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "Tên người dùng";
            // 
            // lblPostTime
            // 
            lblPostTime.AutoSize = true;
            lblPostTime.Font = new Font("Segoe UI", 8F);
            lblPostTime.ForeColor = Color.Gray;
            lblPostTime.Location = new Point(10, 22);
            lblPostTime.Name = "lblPostTime";
            lblPostTime.Size = new Size(76, 19);
            lblPostTime.TabIndex = 1;
            lblPostTime.Text = "2 giờ trước";
            // 
            // lblPrivacy
            // 
            lblPrivacy.AutoSize = true;
            lblPrivacy.Font = new Font("Segoe UI", 8F);
            lblPrivacy.ForeColor = Color.Gray;
            lblPrivacy.Location = new Point(80, 22);
            lblPrivacy.Name = "lblPrivacy";
            lblPrivacy.Size = new Size(28, 19);
            lblPrivacy.TabIndex = 2;
            lblPrivacy.Text = "🌍";
            // 
            // picUserAvatar
            // 
            picUserAvatar.BackColor = Color.FromArgb(0, 132, 255);
            picUserAvatar.Dock = DockStyle.Left;
            picUserAvatar.Location = new Point(0, 0);
            picUserAvatar.Name = "picUserAvatar";
            picUserAvatar.Size = new Size(45, 45);
            picUserAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picUserAvatar.TabIndex = 1;
            picUserAvatar.TabStop = false;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.White;
            btnMenu.Cursor = Cursors.Hand;
            btnMenu.Dock = DockStyle.Right;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.Font = new Font("Segoe UI", 12F);
            btnMenu.ForeColor = Color.Gray;
            btnMenu.Location = new Point(780, 0);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(40, 45);
            btnMenu.TabIndex = 2;
            btnMenu.Text = "⋯";
            btnMenu.UseVisualStyleBackColor = false;
            // 
            // PostItemControl
            // 
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(pnlMain);
            Margin = new Padding(0, 0, 0, 15);
            MinimumSize = new Size(400, 200);
            Name = "PostItemControl";
            Size = new Size(850, 350);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            pnlActions.ResumeLayout(false);
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlUserInfo.ResumeLayout(false);
            pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            ResumeLayout(false);
        }
    }
}