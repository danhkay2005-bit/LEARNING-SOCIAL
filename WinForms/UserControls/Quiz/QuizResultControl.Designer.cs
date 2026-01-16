namespace WinForms.UserControls.Quiz
{
    partial class QuizResultControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.lblTitle = new Label();
            this.lblMainStat = new Label();
            this.lblDetails = new Label();
            this.btnHome = new Button();
            this.pnlCircle = new Panel(); // Dùng để vẽ vòng tròn tỉ lệ (tùy chọn)

            // lblTitle: Chúc mừng hoặc Kết quả trận đấu
            this.lblTitle.Font = new Font("Segoe UI Black", 20F);
            this.lblTitle.ForeColor = Color.FromArgb(193, 225, 127);
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 80;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblMainStat: Hiển thị XP hoặc Tỉ lệ % đúng
            this.lblMainStat.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            this.lblMainStat.ForeColor = Color.White;
            this.lblMainStat.Dock = DockStyle.Top;
            this.lblMainStat.Height = 150;
            this.lblMainStat.TextAlign = ContentAlignment.MiddleCenter;

            // lblDetails: Chi tiết Số thẻ đúng/sai, Thời gian
            this.lblDetails.Font = new Font("Segoe UI Semibold", 13F);
            this.lblDetails.ForeColor = Color.Silver;
            this.lblDetails.Dock = DockStyle.Fill;
            this.lblDetails.TextAlign = ContentAlignment.MiddleCenter;

            // btnHome: Nút quay về
            this.btnHome.BackColor = Color.FromArgb(193, 225, 127);
            this.btnHome.FlatStyle = FlatStyle.Flat;
            this.btnHome.Font = new Font("Segoe UI Black", 12F);
            this.btnHome.Size = new Size(250, 60);
            this.btnHome.Location = new Point(425, 550); // Căn giữa
            this.btnHome.Text = "TIẾP TỤC ➔";

            this.BackColor = Color.FromArgb(18, 38, 44);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.lblMainStat);
            this.Controls.Add(this.lblTitle);
            this.Size = new Size(1100, 650);
        }
        private Label lblTitle, lblMainStat, lblDetails;
        private Button btnHome;
        private Panel pnlCircle;
    }
}
