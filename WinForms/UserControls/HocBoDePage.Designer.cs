namespace WinForms.UserControls
{
    partial class HocBoDePage
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
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            lblScore = new Label();
            lblOpponentScore = new Label();
            prgOpponent = new ProgressBar();
            btnExit = new Button();
            lblProgress = new Label();
            prgStatus = new ProgressBar();
            pnlQuestionContent = new Panel();
            prgTimeCountdown = new ProgressBar();
            pnlBottom = new Panel();
            btnNext = new Button();
            btnBack = new Button();
            timerTick = new System.Windows.Forms.Timer(components);

            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();

            // 
            // pnlTop: Thanh trạng thái phía trên
            // 
            pnlTop.BackColor = Color.FromArgb(10, 25, 29);
            pnlTop.Controls.Add(lblScore);
            pnlTop.Controls.Add(lblOpponentScore);
            pnlTop.Controls.Add(prgOpponent);
            pnlTop.Controls.Add(btnExit);
            pnlTop.Controls.Add(lblProgress);
            pnlTop.Controls.Add(prgStatus);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1100, 85);

            // lblProgress: Hiển thị số câu (VD: 1/10)
            lblProgress.AutoSize = true;
            lblProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProgress.ForeColor = Color.DarkGray;
            lblProgress.Location = new Point(25, 12);
            lblProgress.Text = "CÂU HỎI: 0/0";

            // prgStatus: Tiến độ hoàn thành bộ đề
            prgStatus.ForeColor = Color.FromArgb(193, 225, 127);
            prgStatus.Location = new Point(25, 38);
            prgStatus.Size = new Size(300, 8);
            prgStatus.Style = ProgressBarStyle.Continuous;

            // lblScore: Điểm XP của bạn
            lblScore.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
            lblScore.ForeColor = Color.FromArgb(193, 225, 127);
            lblScore.Location = new Point(400, 15);
            lblScore.Size = new Size(300, 30);
            lblScore.Text = "CỦA BẠN: 0 XP";
            lblScore.TextAlign = ContentAlignment.MiddleCenter;

            // lblOpponentScore: Điểm XP đối thủ (Chế độ thách đấu)
            lblOpponentScore.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblOpponentScore.ForeColor = Color.FromArgb(255, 128, 128);
            lblOpponentScore.Location = new Point(780, 12);
            lblOpponentScore.Size = new Size(250, 20);
            lblOpponentScore.Text = "ĐỐI THỦ: 0 XP";
            lblOpponentScore.TextAlign = ContentAlignment.MiddleRight;

            // prgOpponent: Thanh tiến độ đối thủ
            prgOpponent.ForeColor = Color.FromArgb(255, 80, 80);
            prgOpponent.Location = new Point(780, 38);
            prgOpponent.Size = new Size(250, 8);
            prgOpponent.Style = ProgressBarStyle.Continuous;

            // btnExit: Nút thoát nhanh
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(150, 150, 150);
            btnExit.Location = new Point(1050, 5);
            btnExit.Size = new Size(40, 40);
            btnExit.Text = "✕";
            btnExit.Cursor = Cursors.Hand;

            // 
            // prgTimeCountdown: Thanh thời gian đếm ngược câu hỏi
            // 
            prgTimeCountdown.BackColor = Color.FromArgb(18, 38, 44);
            prgTimeCountdown.Dock = DockStyle.Top;
            prgTimeCountdown.ForeColor = Color.LimeGreen;
            prgTimeCountdown.Height = 5;
            prgTimeCountdown.Style = ProgressBarStyle.Continuous;
            prgTimeCountdown.Value = 100;

            // 
            // pnlQuestionContent: Vùng nạp nội dung câu hỏi
            // 
            pnlQuestionContent.BackColor = Color.FromArgb(18, 38, 44);
            pnlQuestionContent.Dock = DockStyle.Fill;
            pnlQuestionContent.Location = new Point(0, 90);
            pnlQuestionContent.Padding = new Padding(50);

            // 
            // pnlBottom: Thanh điều hướng
            // 
            pnlBottom.BackColor = Color.FromArgb(10, 25, 29);
            pnlBottom.Controls.Add(btnNext);
            pnlBottom.Controls.Add(btnBack);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Height = 110;

            btnBack.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI Semibold", 10F);
            btnBack.ForeColor = Color.Gray;
            btnBack.Location = new Point(40, 30);
            btnBack.Size = new Size(130, 50);
            btnBack.Text = "QUAY LẠI";
            btnBack.Cursor = Cursors.Hand;

            btnNext.BackColor = Color.FromArgb(193, 225, 127);
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold);
            btnNext.ForeColor = Color.FromArgb(10, 25, 29);
            btnNext.Location = new Point(880, 25);
            btnNext.Size = new Size(180, 60);
            btnNext.Text = "XÁC NHẬN ➔";
            btnNext.Cursor = Cursors.Hand;
            // Link event đã viết ở code-behind
            btnNext.Click += btnNext_Click;

            // 
            // timerTick: Cấu hình 100ms
            // 
            timerTick.Interval = 100;

            // 
            // HocBoDePage: Tổng thể
            // 
            BackColor = Color.FromArgb(10, 25, 29);
            Controls.Add(pnlQuestionContent);
            Controls.Add(prgTimeCountdown);
            Controls.Add(pnlTop);
            Controls.Add(pnlBottom);
            Size = new Size(1100, 750);

            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlTop;
        private ProgressBar prgStatus;
        private Label lblProgress;
        private Label lblScore;
        private Label lblOpponentScore;
        private ProgressBar prgOpponent;
        private Button btnExit;
        private Panel pnlQuestionContent;
        private ProgressBar prgTimeCountdown;
        private Panel pnlBottom;
        private Button btnNext;
        private Button btnBack;
        private System.Windows.Forms.Timer timerTick;
    }
}