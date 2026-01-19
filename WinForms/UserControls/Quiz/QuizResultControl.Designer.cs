namespace WinForms.UserControls.Quiz
{
    partial class QuizResultControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblMainStat = new Label();
            lblDetails = new Label();
            btnFinish = new Button();
            btnShowDetails = new Button();// Khai báo nút mới
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(800, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "KẾT QUẢ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMainStat
            // 
            lblMainStat.Dock = DockStyle.Top;
            lblMainStat.Font = new Font("Segoe UI", 60F, FontStyle.Bold);
            lblMainStat.ForeColor = Color.White;
            lblMainStat.Location = new Point(0, 110);
            lblMainStat.Name = "lblMainStat";
            lblMainStat.Size = new Size(800, 150);
            lblMainStat.TabIndex = 1;
            lblMainStat.Text = "0%";
            lblMainStat.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDetails
            // 
            lblDetails.Dock = DockStyle.Top;
            lblDetails.Font = new Font("Segoe UI", 14F);
            lblDetails.ForeColor = Color.FromArgb(200, 200, 200);
            lblDetails.Location = new Point(0, 260);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new Size(800, 150);
            lblDetails.TabIndex = 2;
            lblDetails.Text = "Chi tiết kết quả...";
            lblDetails.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFinish
            // 
            btnFinish.BackColor = Color.FromArgb(193, 225, 127);
            btnFinish.Cursor = Cursors.Hand;
            btnFinish.FlatAppearance.BorderSize = 0;
            btnFinish.FlatStyle = FlatStyle.Flat;
            btnFinish.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnFinish.ForeColor = Color.FromArgb(13, 56, 56);
            btnFinish.Location = new Point(275, 450);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(250, 60);
            btnFinish.TabIndex = 3;
            btnFinish.Text = "HOÀN THÀNH";
            btnFinish.UseVisualStyleBackColor = false;

            btnShowDetails.BackColor = Color.FromArgb(40, 70, 80); // Màu tối hơn nút Hoàn thành
            btnShowDetails.Cursor = Cursors.Hand;
            btnShowDetails.FlatAppearance.BorderSize = 1;
            btnShowDetails.FlatAppearance.BorderColor = Color.FromArgb(193, 225, 127);
            btnShowDetails.FlatStyle = FlatStyle.Flat;
            btnShowDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnShowDetails.ForeColor = Color.White;
            btnShowDetails.Location = new Point(275, 520); // Nằm dưới nút Finish
            btnShowDetails.Name = "btnShowDetails";
            btnShowDetails.Size = new Size(250, 50);
            btnShowDetails.TabIndex = 4;
            btnShowDetails.Text = "XEM CHI TIẾT CÂU TRẢ LỜI";
            btnShowDetails.UseVisualStyleBackColor = false;
            // 
            // QuizResultControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 56, 56);
            Controls.Add(btnFinish);
            Controls.Add(lblDetails);
            Controls.Add(lblMainStat);
            Controls.Add(lblTitle);
            this.Controls.Add(btnShowDetails);
            Name = "QuizResultControl";
            Padding = new Padding(0, 50, 0, 0);
            Size = new Size(800, 600);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblMainStat;
        private Label lblDetails;
        private Button btnFinish; 
       
            
            private Button btnShowDetails;// Khai báo nút
    }
}