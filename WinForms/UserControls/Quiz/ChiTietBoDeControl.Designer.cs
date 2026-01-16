namespace WinForms.UserControls.Quiz
{
    partial class ChiTietBoDeControl
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
            tlpMain = new TableLayoutPanel();
            pnlLeftLobby = new Panel();
            tlpLobby = new TableLayoutPanel();
            lblMainTitle = new Label();
            lblChallengeCode = new Label();
            btnStartSolo = new Button();
            btnCreateChallenge = new Button();
            lblStatus = new Label();
            pnlRightSidebar = new Panel();
            lblSideInfo = new Label();
            lblSideTitle = new Label();
            picThumb = new PictureBox();
            tlpMain.SuspendLayout();
            pnlLeftLobby.SuspendLayout();
            tlpLobby.SuspendLayout();
            pnlRightSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picThumb).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.BackColor = Color.FromArgb(10, 25, 29);
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpMain.Controls.Add(pnlLeftLobby, 0, 0);
            tlpMain.Controls.Add(pnlRightSidebar, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(20);
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpMain.Size = new Size(150, 150);
            tlpMain.TabIndex = 0;
            // 
            // pnlLeftLobby
            // 
            pnlLeftLobby.BackColor = Color.FromArgb(18, 38, 44);
            pnlLeftLobby.Controls.Add(tlpLobby);
            pnlLeftLobby.Dock = DockStyle.Fill;
            pnlLeftLobby.Location = new Point(23, 23);
            pnlLeftLobby.Name = "pnlLeftLobby";
            pnlLeftLobby.Size = new Size(71, 104);
            pnlLeftLobby.TabIndex = 0;
            // 
            // tlpLobby
            // 
            tlpLobby.ColumnCount = 1;
            tlpLobby.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLobby.Controls.Add(lblMainTitle, 0, 0);
            tlpLobby.Controls.Add(lblChallengeCode, 0, 1);
            tlpLobby.Controls.Add(btnStartSolo, 0, 2);
            tlpLobby.Controls.Add(btnCreateChallenge, 0, 3);
            tlpLobby.Controls.Add(lblStatus, 0, 4);
            tlpLobby.Dock = DockStyle.Fill;
            tlpLobby.Location = new Point(0, 0);
            tlpLobby.Name = "tlpLobby";
            tlpLobby.RowCount = 5;
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpLobby.Size = new Size(71, 104);
            tlpLobby.TabIndex = 0;
            // 
            // lblMainTitle
            // 
            lblMainTitle.Dock = DockStyle.Fill;
            lblMainTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblMainTitle.ForeColor = Color.White;
            lblMainTitle.Location = new Point(3, 0);
            lblMainTitle.Name = "lblMainTitle";
            lblMainTitle.Size = new Size(65, 1);
            lblMainTitle.TabIndex = 0;
            lblMainTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChallengeCode
            // 
            lblChallengeCode.Dock = DockStyle.Fill;
            lblChallengeCode.Font = new Font("Segoe UI Black", 48F, FontStyle.Bold);
            lblChallengeCode.ForeColor = Color.FromArgb(193, 225, 127);
            lblChallengeCode.Location = new Point(3, -15);
            lblChallengeCode.Name = "lblChallengeCode";
            lblChallengeCode.Size = new Size(65, 1);
            lblChallengeCode.TabIndex = 1;
            lblChallengeCode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStartSolo
            // 
            btnStartSolo.Anchor = AnchorStyles.None;
            btnStartSolo.BackColor = Color.FromArgb(193, 225, 127);
            btnStartSolo.Cursor = Cursors.Hand;
            btnStartSolo.FlatAppearance.BorderSize = 0;
            btnStartSolo.FlatAppearance.MouseDownBackColor = Color.FromArgb(160, 200, 100);
            btnStartSolo.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 240, 150);
            btnStartSolo.FlatStyle = FlatStyle.Flat;
            btnStartSolo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnStartSolo.ForeColor = Color.FromArgb(10, 25, 29);
            btnStartSolo.Location = new Point(190, 280); // Vị trí sẽ tự căn giữa nhờ Anchor
            btnStartSolo.Name = "btnStartSolo";
            btnStartSolo.Size = new Size(350, 75); // Tăng chiều rộng để chữ "thở" được
            btnStartSolo.TabIndex = 2;
            btnStartSolo.Text = "HỌC MỘT MÌNH";
            btnStartSolo.TextAlign = ContentAlignment.MiddleCenter;
            btnStartSolo.UseVisualStyleBackColor = false;

            // 
            // btnCreateChallenge (Nút phụ - Viền Lime)
            // 
            btnCreateChallenge.Anchor = AnchorStyles.None;
            btnCreateChallenge.Cursor = Cursors.Hand;
            btnCreateChallenge.FlatAppearance.BorderColor = Color.FromArgb(193, 225, 127);
            btnCreateChallenge.FlatAppearance.BorderSize = 2;
            btnCreateChallenge.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 60, 65);
            btnCreateChallenge.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 50, 55);
            btnCreateChallenge.FlatStyle = FlatStyle.Flat;
            btnCreateChallenge.Font = new Font("Segoe UI Semibold", 12F);
            btnCreateChallenge.ForeColor = Color.White;
            btnCreateChallenge.Location = new Point(190, 370);
            btnCreateChallenge.Name = "btnCreateChallenge";
            btnCreateChallenge.Size = new Size(350, 55); // Kích thước cân đối với nút trên
            btnCreateChallenge.TabIndex = 3;
            btnCreateChallenge.Text = "Tạo mã thách đấu";
            btnCreateChallenge.TextAlign = ContentAlignment.MiddleCenter;
            btnCreateChallenge.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 110);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(65, 1);
            lblStatus.TabIndex = 4;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlRightSidebar
            // 
            pnlRightSidebar.BackColor = Color.FromArgb(13, 31, 36);
            pnlRightSidebar.Controls.Add(lblSideInfo);
            pnlRightSidebar.Controls.Add(lblSideTitle);
            pnlRightSidebar.Controls.Add(picThumb);
            pnlRightSidebar.Dock = DockStyle.Fill;
            pnlRightSidebar.Location = new Point(100, 23);
            pnlRightSidebar.Name = "pnlRightSidebar";
            pnlRightSidebar.Size = new Size(27, 104);
            pnlRightSidebar.TabIndex = 1;
            // 
            // lblSideInfo
            // 
            lblSideInfo.Dock = DockStyle.Top;
            lblSideInfo.ForeColor = Color.Gray;
            lblSideInfo.Location = new Point(0, 250);
            lblSideInfo.Name = "lblSideInfo";
            lblSideInfo.Size = new Size(27, 23);
            lblSideInfo.TabIndex = 0;
            lblSideInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSideTitle
            // 
            lblSideTitle.Dock = DockStyle.Top;
            lblSideTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSideTitle.ForeColor = Color.White;
            lblSideTitle.Location = new Point(0, 200);
            lblSideTitle.Name = "lblSideTitle";
            lblSideTitle.Size = new Size(27, 50);
            lblSideTitle.TabIndex = 1;
            lblSideTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picThumb
            // 
            picThumb.Dock = DockStyle.Top;
            picThumb.Location = new Point(0, 0);
            picThumb.Name = "picThumb";
            picThumb.Padding = new Padding(20);
            picThumb.Size = new Size(27, 200);
            picThumb.SizeMode = PictureBoxSizeMode.Zoom;
            picThumb.TabIndex = 2;
            picThumb.TabStop = false;
            // 
            // ChiTietBoDeControl
            // 
            Controls.Add(tlpMain);
            Name = "ChiTietBoDeControl";
            tlpMain.ResumeLayout(false);
            pnlLeftLobby.ResumeLayout(false);
            tlpLobby.ResumeLayout(false);
            pnlRightSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picThumb).EndInit();
            ResumeLayout(false);
        }

        private Panel pnlLeftLobby;
        private Label lblMainTitle;
        private Label lblChallengeCode;
        private Label lblStatus;
        private Button btnStartSolo;
        private Button btnCreateChallenge;
        private Panel pnlRightSidebar;
        private PictureBox picThumb;
        private Label lblSideTitle;
        private Label lblSideInfo;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpLobby;
    }
}