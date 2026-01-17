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
            btnBack = new Button();
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
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1000, 700);
            tlpMain.TabIndex = 0;
            // 
            // pnlLeftLobby
            // 
            pnlLeftLobby.BackColor = Color.FromArgb(18, 38, 44);
            pnlLeftLobby.Controls.Add(tlpLobby);
            pnlLeftLobby.Dock = DockStyle.Fill;
            pnlLeftLobby.Location = new Point(23, 23);
            pnlLeftLobby.Name = "pnlLeftLobby";
            pnlLeftLobby.Size = new Size(666, 654);
            pnlLeftLobby.TabIndex = 0;
            // 
            // tlpLobby
            // 
            tlpLobby.ColumnCount = 1;
            tlpLobby.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLobby.Controls.Add(btnBack, 0, 0);
            tlpLobby.Controls.Add(lblMainTitle, 0, 1);
            tlpLobby.Controls.Add(lblChallengeCode, 0, 2);
            tlpLobby.Controls.Add(btnStartSolo, 0, 3);
            tlpLobby.Controls.Add(btnCreateChallenge, 0, 4);
            tlpLobby.Controls.Add(lblStatus, 0, 5);
            tlpLobby.Dock = DockStyle.Fill;
            tlpLobby.Location = new Point(0, 0);
            tlpLobby.Name = "tlpLobby";
            tlpLobby.RowCount = 6;
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Hàng nút Back
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));  // Tiêu đề
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));  // Mã PIN
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F)); // Nút Solo
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F)); // Nút Thách đấu
            tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));  // Trạng thái
            tlpLobby.Size = new Size(666, 654);
            tlpLobby.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Left;
            btnBack.BackColor = Color.Transparent;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 50, 55);
            btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(25, 45, 50);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(15, 10);
            btnBack.Margin = new Padding(15, 0, 0, 0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 40);
            btnBack.TabIndex = 5;
            btnBack.Text = "← Quay lại";
            btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblMainTitle
            // 
            lblMainTitle.Dock = DockStyle.Fill;
            lblMainTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblMainTitle.ForeColor = Color.White;
            lblMainTitle.Location = new Point(3, 60);
            lblMainTitle.Name = "lblMainTitle";
            lblMainTitle.Size = new Size(660, 108);
            lblMainTitle.TabIndex = 0;
            lblMainTitle.Text = "TIÊU ĐỀ BỘ ĐỀ";
            lblMainTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChallengeCode
            // 
            lblChallengeCode.Dock = DockStyle.Fill;
            lblChallengeCode.Font = new Font("Segoe UI Black", 48F, FontStyle.Bold);
            lblChallengeCode.ForeColor = Color.FromArgb(193, 225, 127);
            lblChallengeCode.Location = new Point(3, 168);
            lblChallengeCode.Name = "lblChallengeCode";
            lblChallengeCode.Size = new Size(660, 108);
            lblChallengeCode.TabIndex = 1;
            lblChallengeCode.Text = "000000";
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
            btnStartSolo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnStartSolo.ForeColor = Color.FromArgb(10, 25, 29);
            btnStartSolo.Location = new Point(158, 283);
            btnStartSolo.Name = "btnStartSolo";
            btnStartSolo.Size = new Size(350, 75);
            btnStartSolo.TabIndex = 2;
            btnStartSolo.Text = "HỌC MỘT MÌNH";
            btnStartSolo.UseVisualStyleBackColor = false;
            // 
            // btnCreateChallenge
            // 
            btnCreateChallenge.Anchor = AnchorStyles.None;
            btnCreateChallenge.Cursor = Cursors.Hand;
            btnCreateChallenge.FlatAppearance.BorderColor = Color.FromArgb(193, 225, 127);
            btnCreateChallenge.FlatAppearance.BorderSize = 2;
            btnCreateChallenge.FlatStyle = FlatStyle.Flat;
            btnCreateChallenge.Font = new Font("Segoe UI Semibold", 14F);
            btnCreateChallenge.ForeColor = Color.White;
            btnCreateChallenge.Location = new Point(158, 375);
            btnCreateChallenge.Name = "btnCreateChallenge";
            btnCreateChallenge.Size = new Size(350, 55);
            btnCreateChallenge.TabIndex = 3;
            btnCreateChallenge.Text = "Tạo mã thách đấu";
            btnCreateChallenge.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(3, 436);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(660, 44);
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
            pnlRightSidebar.Location = new Point(715, 23);
            pnlRightSidebar.Name = "pnlRightSidebar";
            pnlRightSidebar.Size = new Size(262, 654);
            pnlRightSidebar.TabIndex = 1;
            // 
            // lblSideInfo
            // 
            lblSideInfo.Dock = DockStyle.Top;
            lblSideInfo.Font = new Font("Segoe UI", 11F);
            lblSideInfo.ForeColor = Color.Gray;
            lblSideInfo.Location = new Point(0, 310);
            lblSideInfo.Name = "lblSideInfo";
            lblSideInfo.Size = new Size(262, 40);
            lblSideInfo.TabIndex = 0;
            lblSideInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSideTitle
            // 
            lblSideTitle.Dock = DockStyle.Top;
            lblSideTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSideTitle.ForeColor = Color.White;
            lblSideTitle.Location = new Point(0, 250);
            lblSideTitle.Name = "lblSideTitle";
            lblSideTitle.Size = new Size(262, 60);
            lblSideTitle.TabIndex = 1;
            lblSideTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picThumb
            // 
            picThumb.Dock = DockStyle.Top;
            picThumb.Location = new Point(0, 0);
            picThumb.Name = "picThumb";
            picThumb.Padding = new Padding(30);
            picThumb.Size = new Size(262, 250);
            picThumb.SizeMode = PictureBoxSizeMode.Zoom;
            picThumb.TabIndex = 2;
            picThumb.TabStop = false;
            // 
            // ChiTietBoDeControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Name = "ChiTietBoDeControl";
            Size = new Size(1000, 700);
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
        private Button btnBack;
    }
}