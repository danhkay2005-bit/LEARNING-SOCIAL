namespace WinForms.UserControls.Achievements
{
    partial class AchievementItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tblMain = new TableLayoutPanel();
            pnlRarity = new Panel();
            lblIcon = new Label();
            flpContent = new FlowLayoutPanel();
            lblTenThanhTuu = new Label();
            lblMoTa = new Label();
            flpRewards = new FlowLayoutPanel();
            lblNgayDat = new Label();
            tblMain.SuspendLayout();
            flpContent.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.ColumnCount = 3;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5955048F));
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.4044952F));
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 472F));
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tblMain.Controls.Add(pnlRarity, 0, 0);
            tblMain.Controls.Add(lblIcon, 1, 0);
            tblMain.Controls.Add(flpContent, 2, 0);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 1;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblMain.Size = new Size(720, 110);
            tblMain.TabIndex = 0;
            // 
            // pnlRarity
            // 
            pnlRarity.BackColor = Color.LightGray;
            pnlRarity.Dock = DockStyle.Fill;
            pnlRarity.Location = new Point(0, 0);
            pnlRarity.Margin = new Padding(0);
            pnlRarity.Name = "pnlRarity";
            pnlRarity.Size = new Size(58, 110);
            pnlRarity.TabIndex = 0;
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.Dock = DockStyle.Fill;
            lblIcon.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIcon.Location = new Point(61, 0);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(183, 110);
            lblIcon.TabIndex = 1;
            lblIcon.Text = "🏆";
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpContent
            // 
            flpContent.Controls.Add(lblTenThanhTuu);
            flpContent.Controls.Add(lblMoTa);
            flpContent.Controls.Add(flpRewards);
            flpContent.Controls.Add(lblNgayDat);
            flpContent.Dock = DockStyle.Fill;
            flpContent.FlowDirection = FlowDirection.TopDown;
            flpContent.Location = new Point(250, 3);
            flpContent.Name = "flpContent";
            flpContent.Padding = new Padding(5, 5, 0, 0);
            flpContent.Size = new Size(467, 104);
            flpContent.TabIndex = 2;
            flpContent.WrapContents = false;
            // 
            // lblTenThanhTuu
            // 
            lblTenThanhTuu.AutoSize = true;
            lblTenThanhTuu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenThanhTuu.Location = new Point(8, 5);
            lblTenThanhTuu.Name = "lblTenThanhTuu";
            lblTenThanhTuu.Size = new Size(127, 21);
            lblTenThanhTuu.TabIndex = 0;
            lblTenThanhTuu.Text = "Tên Thành Tựu:";
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.ForeColor = Color.DimGray;
            lblMoTa.Location = new Point(5, 26);
            lblMoTa.Margin = new Padding(0, 0, 0, 5);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(50, 15);
            lblMoTa.TabIndex = 1;
            lblMoTa.Text = "Mô Tả...";
            // 
            // flpRewards
            // 
            flpRewards.AutoSize = true;
            flpRewards.Location = new Point(8, 49);
            flpRewards.Name = "flpRewards";
            flpRewards.Size = new Size(0, 0);
            flpRewards.TabIndex = 2;
            // 
            // lblNgayDat
            // 
            lblNgayDat.AutoSize = true;
            lblNgayDat.ForeColor = Color.Green;
            lblNgayDat.Location = new Point(5, 62);
            lblNgayDat.Margin = new Padding(0, 10, 0, 0);
            lblNgayDat.Name = "lblNgayDat";
            lblNgayDat.Size = new Size(65, 15);
            lblNgayDat.TabIndex = 3;
            lblNgayDat.Text = "Ngày Đạt...";
            // 
            // AchievementItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tblMain);
            Name = "AchievementItem";
            Size = new Size(720, 110);
            tblMain.ResumeLayout(false);
            tblMain.PerformLayout();
            flpContent.ResumeLayout(false);
            flpContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tblMain;
        private Panel pnlRarity;
        private Label lblIcon;
        private FlowLayoutPanel flpContent;
        private Label lblTenThanhTuu;
        private Label lblMoTa;
        private FlowLayoutPanel flpRewards;
        private Label lblNgayDat;
    }
}
