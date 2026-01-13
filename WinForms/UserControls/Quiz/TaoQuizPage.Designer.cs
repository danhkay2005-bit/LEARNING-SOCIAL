namespace WinForms.UserControls.Quiz
{
    partial class TaoQuizPage
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
            pnlTop = new Panel();
            btnTaoQuiz = new Button();
            lbl1 = new Label();
            pnlBot = new Panel();
            flpThumbnails = new FlowLayoutPanel();
            btnSetting = new Button();
            pnlMid = new Panel();
            btnThemCauHoi = new Button();
            pnlTop.SuspendLayout();
            pnlBot.SuspendLayout();
            flpThumbnails.SuspendLayout();
            pnlMid.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(13, 56, 56);
            pnlTop.Controls.Add(btnTaoQuiz);
            pnlTop.Controls.Add(lbl1);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1046, 51);
            pnlTop.TabIndex = 1;
            // 
            // btnTaoQuiz
            // 
            btnTaoQuiz.BackColor = Color.Gold;
            btnTaoQuiz.Cursor = Cursors.Hand;
            btnTaoQuiz.FlatAppearance.BorderSize = 0;
            btnTaoQuiz.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnTaoQuiz.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnTaoQuiz.FlatStyle = FlatStyle.Flat;
            btnTaoQuiz.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTaoQuiz.ForeColor = Color.FromArgb(13, 56, 56);
            btnTaoQuiz.Location = new Point(911, 6);
            btnTaoQuiz.Name = "btnTaoQuiz";
            btnTaoQuiz.Size = new Size(122, 39);
            btnTaoQuiz.TabIndex = 4;
            btnTaoQuiz.Text = "XONG";
            btnTaoQuiz.UseVisualStyleBackColor = false;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl1.ForeColor = Color.WhiteSmoke;
            lbl1.Location = new Point(0, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(235, 46);
            lbl1.TabIndex = 1;
            lbl1.Text = "Tạo một Quiz";
            // 
            // pnlBot
            // 
            pnlBot.Controls.Add(flpThumbnails);
            pnlBot.Dock = DockStyle.Bottom;
            pnlBot.Location = new Point(0, 419);
            pnlBot.Name = "pnlBot";
            pnlBot.Size = new Size(1046, 111);
            pnlBot.TabIndex = 2;
            // 
            // flpThumbnails
            // 
            flpThumbnails.AutoScroll = true;
            flpThumbnails.BackColor = Color.FromArgb(13, 56, 56);
            flpThumbnails.Controls.Add(btnSetting);
            flpThumbnails.Dock = DockStyle.Fill;
            flpThumbnails.Location = new Point(0, 0);
            flpThumbnails.Name = "flpThumbnails";
            flpThumbnails.Size = new Size(1046, 111);
            flpThumbnails.TabIndex = 0;
            flpThumbnails.WrapContents = false;
            // 
            // btnSetting
            // 
            btnSetting.BackColor = Color.FromArgb(192, 255, 255);
            btnSetting.Cursor = Cursors.Hand;
            btnSetting.FlatAppearance.BorderSize = 0;
            btnSetting.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnSetting.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnSetting.FlatStyle = FlatStyle.Flat;
            btnSetting.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSetting.ForeColor = Color.FromArgb(13, 56, 56);
            btnSetting.Location = new Point(3, 3);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(140, 108);
            btnSetting.TabIndex = 5;
            btnSetting.Text = "Setting";
            btnSetting.UseVisualStyleBackColor = false;
            btnSetting.Click += btnSetting_Click;
            // 
            // pnlMid
            // 
            pnlMid.Controls.Add(btnThemCauHoi);
            pnlMid.Dock = DockStyle.Fill;
            pnlMid.Location = new Point(0, 51);
            pnlMid.Name = "pnlMid";
            pnlMid.Size = new Size(1046, 368);
            pnlMid.TabIndex = 3;
            // 
            // btnThemCauHoi
            // 
            btnThemCauHoi.BackColor = Color.Gold;
            btnThemCauHoi.Cursor = Cursors.Hand;
            btnThemCauHoi.FlatAppearance.BorderSize = 0;
            btnThemCauHoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnThemCauHoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnThemCauHoi.FlatStyle = FlatStyle.Flat;
            btnThemCauHoi.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThemCauHoi.ForeColor = Color.FromArgb(13, 56, 56);
            btnThemCauHoi.Location = new Point(984, 321);
            btnThemCauHoi.Name = "btnThemCauHoi";
            btnThemCauHoi.Size = new Size(59, 44);
            btnThemCauHoi.TabIndex = 5;
            btnThemCauHoi.Text = "+";
            btnThemCauHoi.TextAlign = ContentAlignment.TopCenter;
            btnThemCauHoi.UseVisualStyleBackColor = false;
            // 
            // TaoQuizPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMid);
            Controls.Add(pnlBot);
            Controls.Add(pnlTop);
            Name = "TaoQuizPage";
            Size = new Size(1046, 530);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBot.ResumeLayout(false);
            flpThumbnails.ResumeLayout(false);
            pnlMid.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Panel pnlBot;
        private Panel pnlMid;
        private FlowLayoutPanel flpThumbnails;
        private Label lbl1;
        private Button btnTaoQuiz;
        private Button btnSetting;
        private Button btnThemCauHoi;
    }
}
