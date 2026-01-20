namespace WinForms.UserControls.Social
{
    partial class UserProfilePage
    {
        private System.ComponentModel.IContainer components = null;

        // Định nghĩa các Controls
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblBio;
        private System.Windows.Forms.Button btnFollow;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblFollowers;
        private System.Windows.Forms.Label lblFollowing;

        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tabPosts;
        private System.Windows.Forms.TabPage tabQuizzes;
        private System.Windows.Forms.TabPage tabHistory;

        private System.Windows.Forms.FlowLayoutPanel flowPosts;
        private System.Windows.Forms.FlowLayoutPanel flowUserQuizzes;
        private System.Windows.Forms.FlowLayoutPanel flowChallengeHistory;

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
            this.SuspendLayout();

            // Form Settings
            this.Name = "UserProfilePage";
            this.Size = new System.Drawing.Size(800, 600);
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ========== HEADER PANEL ==========
            pnlHeader = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Top,
                Height = 200,
                BackColor = System.Drawing.Color.White,
                Padding = new System.Windows.Forms.Padding(20)
            };

            pbAvatar = new System.Windows.Forms.PictureBox { Width = 120, Height = 120, Location = new System.Drawing.Point(20, 20), SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage, BackColor = System.Drawing.Color.LightGray, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
            lblName = new System.Windows.Forms.Label { Location = new System.Drawing.Point(160, 20), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(29, 29, 29) };
            lblEmail = new System.Windows.Forms.Label { Location = new System.Drawing.Point(160, 55), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular), ForeColor = System.Drawing.Color.Gray };
            lblBio = new System.Windows.Forms.Label { Location = new System.Drawing.Point(160, 85), MaximumSize = new System.Drawing.Size(500, 0), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic), ForeColor = System.Drawing.Color.FromArgb(65, 65, 65) };
            btnBack = new System.Windows.Forms.Button { Text = "← Quay lại", Location = new System.Drawing.Point(20, 160), Width = 100, Height = 30, BackColor = System.Drawing.Color.FromArgb(108, 117, 125), ForeColor = System.Drawing.Color.White, FlatStyle = System.Windows.Forms.FlatStyle.Flat, Cursor = System.Windows.Forms.Cursors.Hand };
            btnBack.FlatAppearance.BorderSize = 0;
            btnFollow = new System.Windows.Forms.Button { Text = "➕ Theo dõi", Location = new System.Drawing.Point(160, 120), Width = 120, Height = 35, BackColor = System.Drawing.Color.FromArgb(24, 119, 242), ForeColor = System.Drawing.Color.White, FlatStyle = System.Windows.Forms.FlatStyle.Flat, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold), Cursor = System.Windows.Forms.Cursors.Hand };
            btnFollow.FlatAppearance.BorderSize = 0;
            lblFollowers = new System.Windows.Forms.Label { Text = "0 Followers", Location = new System.Drawing.Point(300, 125), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(24, 119, 242), Cursor = System.Windows.Forms.Cursors.Hand };
            lblFollowing = new System.Windows.Forms.Label { Text = "0 Following", Location = new System.Drawing.Point(420, 125), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(24, 119, 242), Cursor = System.Windows.Forms.Cursors.Hand };

            pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] { pbAvatar, lblName, lblEmail, lblBio, btnBack, btnFollow, lblFollowers, lblFollowing });

            // ========== TAB CONTROL ==========
            this.tabContent = new System.Windows.Forms.TabControl { Dock = System.Windows.Forms.DockStyle.Fill, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };

            // Tab 1: Bài viết
            this.tabPosts = new System.Windows.Forms.TabPage { Text = "📝 Bài viết", BackColor = System.Drawing.Color.White };
            this.flowPosts = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, AutoScroll = true, FlowDirection = System.Windows.Forms.FlowDirection.TopDown, WrapContents = false, Padding = new System.Windows.Forms.Padding(10) };
            this.tabPosts.Controls.Add(this.flowPosts);

            // Tab 2: Bộ đề
            this.tabQuizzes = new System.Windows.Forms.TabPage { Text = "📚 Bộ đề", BackColor = System.Drawing.Color.White };
            this.flowUserQuizzes = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, AutoScroll = true, Padding = new System.Windows.Forms.Padding(10) };
            this.tabQuizzes.Controls.Add(this.flowUserQuizzes);

            // Tab 3: Thách đấu
            this.tabHistory = new System.Windows.Forms.TabPage { Text = "⚔️ Thách đấu", BackColor = System.Drawing.Color.White };
            this.flowChallengeHistory = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Fill, AutoScroll = true, FlowDirection = System.Windows.Forms.FlowDirection.TopDown, WrapContents = false, Padding = new System.Windows.Forms.Padding(10) };
            this.tabHistory.Controls.Add(this.flowChallengeHistory);

            this.tabContent.TabPages.AddRange(new System.Windows.Forms.TabPage[] { tabPosts, tabQuizzes, tabHistory });

            this.Controls.Add(this.tabContent);
            this.Controls.Add(this.pnlHeader);

            this.ResumeLayout(false);
        }
    }
}