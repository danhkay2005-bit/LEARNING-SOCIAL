namespace WinForms.UserControls
{
    partial class HocBoDePage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblOpponentScore = new System.Windows.Forms.Label();
            this.prgOpponent = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.prgTimeCountdown = new System.Windows.Forms.ProgressBar();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlScoreFeed = new System.Windows.Forms.Panel();
            this.lblFeedContent = new System.Windows.Forms.Label();
            this.lblFeedTitle = new System.Windows.Forms.Label();
            this.pnlQuestionContent = new System.Windows.Forms.Panel();
            this.timerTick = new System.Windows.Forms.Timer(this.components);

            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlScoreFeed.SuspendLayout();
            this.SuspendLayout();

            // pnlTop: Khu vực tiêu đề và điểm số
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(10, 25, 29);
            this.pnlTop.Controls.Add(this.lblScore);
            this.pnlTop.Controls.Add(this.lblOpponentScore);
            this.pnlTop.Controls.Add(this.prgOpponent);
            this.pnlTop.Controls.Add(this.lblProgress);
            this.pnlTop.Controls.Add(this.prgStatus);
            this.pnlTop.Controls.Add(this.btnExit);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 85;

            this.lblScore.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.FromArgb(193, 225, 127);
            this.lblScore.Location = new System.Drawing.Point(350, 15);
            this.lblScore.Size = new System.Drawing.Size(400, 35);
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScore.Text = "BẠN: 0 XP";

            this.lblOpponentScore.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblOpponentScore.ForeColor = System.Drawing.Color.FromArgb(255, 128, 128);
            this.lblOpponentScore.Location = new System.Drawing.Point(880, 12);
            this.lblOpponentScore.Size = new System.Drawing.Size(200, 20);
            this.lblOpponentScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.prgOpponent.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.prgOpponent.Location = new System.Drawing.Point(880, 38);
            this.prgOpponent.Size = new System.Drawing.Size(200, 8);

            // prgTimeCountdown: Thanh thời gian chạy dưới Top
            this.prgTimeCountdown.Dock = System.Windows.Forms.DockStyle.Top;
            this.prgTimeCountdown.Height = 6;
            this.prgTimeCountdown.ForeColor = System.Drawing.Color.LimeGreen;

            // pnlBottom: Khu vực nút bấm điều hướng
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(10, 25, 29);
            this.pnlBottom.Controls.Add(this.lblStatusMessage);
            this.pnlBottom.Controls.Add(this.btnNext);
            this.pnlBottom.Controls.Add(this.btnBack);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 100;

            this.lblStatusMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(255, 255, 128);
            this.lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);

            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.Width = 180;
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(193, 225, 127);
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Black", 12F);

            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.Width = 150;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.Gray;

            // pnlScoreFeed: Nhật ký bên phải
            this.pnlScoreFeed.BackColor = System.Drawing.Color.FromArgb(14, 30, 36);
            this.pnlScoreFeed.Controls.Add(this.lblFeedContent);
            this.pnlScoreFeed.Controls.Add(this.lblFeedTitle);
            this.pnlScoreFeed.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlScoreFeed.Width = 260;
            this.pnlScoreFeed.Padding = new System.Windows.Forms.Padding(10);

            this.lblFeedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFeedTitle.Height = 35;
            this.lblFeedTitle.ForeColor = System.Drawing.Color.FromArgb(193, 225, 127);
            this.lblFeedTitle.Text = "📊 NHẬT KÝ";

            this.lblFeedContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeedContent.ForeColor = System.Drawing.Color.Silver;

            // pnlQuestionContent: Nơi hiện câu hỏi (QUAN TRỌNG: DOCK FILL)
            this.pnlQuestionContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuestionContent.BackColor = System.Drawing.Color.FromArgb(18, 38, 44);
            this.pnlQuestionContent.Padding = new System.Windows.Forms.Padding(40);

            // HocBoDePage
            this.Controls.Add(this.pnlQuestionContent); // Add đầu tiên để Fill phần còn lại
            this.Controls.Add(this.pnlScoreFeed);
            this.Controls.Add(this.prgTimeCountdown);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);

            this.Size = new System.Drawing.Size(1200, 800);
            this.pnlTop.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlScoreFeed.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblOpponentScore;
        private System.Windows.Forms.ProgressBar prgOpponent;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar prgStatus;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ProgressBar prgTimeCountdown;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlScoreFeed;
        private System.Windows.Forms.Label lblFeedTitle;
        private System.Windows.Forms.Label lblFeedContent;
        private System.Windows.Forms.Panel pnlQuestionContent;
        private System.Windows.Forms.Timer timerTick;
    }
}