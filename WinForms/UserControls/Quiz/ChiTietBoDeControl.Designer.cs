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
            btnShareSocial = new Button();
            btnEdit = new Button();
            lblStatus = new Label();
            btnDelete = new Button();
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
            tlpLobby.Controls.Add(btnShareSocial, 0, 5);
            tlpLobby.Controls.Add(lblStatus, 0, 8);
            tlpLobby.Controls.Add(btnDelete, 0, 7);
            tlpLobby.Controls.Add(btnEdit, 0, 6);
            tlpLobby.Dock = DockStyle.Fill;
            tlpLobby.Location = new Point(0, 0);
            tlpLobby.Name = "tlpLobby";
            this.tlpLobby.RowCount = 9;
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Row 0: Back
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));   // Row 1: Title
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));   // Row 2: Code
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));  // Row 3: StartSolo
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));  // Row 4: Create
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));  // Row 5: Share
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));  // Row 6: Edit (Tăng lên 75)
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));  // Row 7: Delete
            this.tlpLobby.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));   // Row 8: Status
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
            btnBack.Click += btnBack_Click;
            // 
            // lblMainTitle
            // 
            lblMainTitle.Dock = DockStyle.Fill;
            lblMainTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblMainTitle.ForeColor = Color.White;
            lblMainTitle.Location = new Point(3, 60);
            lblMainTitle.Name = "lblMainTitle";
            lblMainTitle.Size = new Size(660, 99);
            lblMainTitle.TabIndex = 0;
            lblMainTitle.Text = "TIÊU ĐỀ BỘ ĐỀ";
            lblMainTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblChallengeCode
            // 
            lblChallengeCode.Dock = DockStyle.Fill;
            lblChallengeCode.Font = new Font("Segoe UI Black", 48F, FontStyle.Bold);
            lblChallengeCode.ForeColor = Color.FromArgb(193, 225, 127);
            lblChallengeCode.Location = new Point(3, 159);
            lblChallengeCode.Name = "lblChallengeCode";
            lblChallengeCode.Size = new Size(660, 99);
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
            btnStartSolo.Location = new Point(158, 278);
            btnStartSolo.Margin = new Padding(0, 20, 0, 20);
            btnStartSolo.Name = "btnStartSolo";
            btnStartSolo.Size = new Size(350, 50);
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
            btnCreateChallenge.Location = new Point(158, 353);
            btnCreateChallenge.Margin = new Padding(0, 5, 0, 5);
            btnCreateChallenge.Name = "btnCreateChallenge";
            btnCreateChallenge.Size = new Size(350, 50);
            btnCreateChallenge.TabIndex = 3;
            btnCreateChallenge.Text = "Tạo mã thách đấu";
            btnCreateChallenge.UseVisualStyleBackColor = true;
            // 
            // btnShareSocial
            // 
            btnShareSocial.Anchor = AnchorStyles.None;
            btnShareSocial.BackColor = Color.FromArgb(24, 119, 242);
            btnShareSocial.Cursor = Cursors.Hand;
            btnShareSocial.FlatAppearance.BorderSize = 0;
            btnShareSocial.FlatStyle = FlatStyle.Flat;
            btnShareSocial.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnShareSocial.ForeColor = Color.White;
            btnShareSocial.Location = new Point(158, 415);
            btnShareSocial.Margin = new Padding(0, 5, 0, 5);
            btnShareSocial.Name = "btnShareSocial";
            btnShareSocial.Size = new Size(350, 45);
            btnShareSocial.TabIndex = 6;
            btnShareSocial.Text = "🌐 Chia sẻ mã lên Mạng xã hội";
            btnShareSocial.UseVisualStyleBackColor = false;
            btnShareSocial.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = AnchorStyles.None;
            this.btnEdit.Cursor = Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = Color.FromArgb(255, 193, 7);
            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.Font = new Font("Segoe UI Semibold", 12F);
            this.btnEdit.ForeColor = Color.FromArgb(255, 193, 7);
            this.btnEdit.Location = new Point(158, 485); // Designer tự tính lại, nhưng cứ để đây
            this.btnEdit.Margin = new Padding(0, 10, 0, 10); // GIẢM Top từ 20 xuống 10
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(350, 45); // Chiều cao 45 giờ sẽ hiển thị đầy đủ
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "✎ Chỉnh sửa bộ đề này";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(3, 588);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(660, 66);
            lblStatus.TabIndex = 4;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.None;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderColor = Color.LightCoral;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 12F);
            btnDelete.ForeColor = Color.LightCoral;
            btnDelete.Location = new Point(158, 538);
            btnDelete.Margin = new Padding(0, 5, 0, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(350, 40);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "🗑 Xóa bộ đề này";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Visible = false;
            // 
            // pnlRightSidebar
            // 
            pnlRightSidebar.BackColor = Color.FromArgb(13, 31, 36);
            pnlRightSidebar.Controls.Add(lblSideInfo);
            pnlRightSidebar.Controls.Add(lblSideTitle);
            pnlRightSidebar.Controls.Add(picThumb);
            pnlRightSidebar.Dock = DockStyle.Fill;
            pnlRightSidebar.Location = new Point(695, 23);
            pnlRightSidebar.Name = "pnlRightSidebar";
            pnlRightSidebar.Size = new Size(282, 654);
            pnlRightSidebar.TabIndex = 1;
            // 
            // lblSideInfo
            // 
            lblSideInfo.Dock = DockStyle.Top;
            lblSideInfo.Font = new Font("Segoe UI", 11F);
            lblSideInfo.ForeColor = Color.Gray;
            lblSideInfo.Location = new Point(0, 310);
            lblSideInfo.Name = "lblSideInfo";
            lblSideInfo.Size = new Size(282, 40);
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
            lblSideTitle.Size = new Size(282, 60);
            lblSideTitle.TabIndex = 1;
            lblSideTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picThumb
            // 
            picThumb.Dock = DockStyle.Top;
            picThumb.Location = new Point(0, 0);
            picThumb.Name = "picThumb";
            picThumb.Padding = new Padding(30);
            picThumb.Size = new Size(282, 250);
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

        private Button btnDelete;
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
        private Button btnEdit;
        private Button btnShareSocial;
    }
}