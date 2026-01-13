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
            pnlBot = new Panel();
            pnlMid = new Panel();
            flpThumbnails = new FlowLayoutPanel();
            lbl1 = new Label();
            btnTaoQuiz = new Button();
            pnlTop.SuspendLayout();
            pnlBot.SuspendLayout();
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
            // pnlBot
            // 
            pnlBot.Controls.Add(flpThumbnails);
            pnlBot.Dock = DockStyle.Bottom;
            pnlBot.Location = new Point(0, 419);
            pnlBot.Name = "pnlBot";
            pnlBot.Size = new Size(1046, 111);
            pnlBot.TabIndex = 2;
            // 
            // pnlMid
            // 
            pnlMid.Dock = DockStyle.Fill;
            pnlMid.Location = new Point(0, 51);
            pnlMid.Name = "pnlMid";
            pnlMid.Size = new Size(1046, 368);
            pnlMid.TabIndex = 3;
            // 
            // flpThumbnails
            // 
            flpThumbnails.AutoScroll = true;
            flpThumbnails.BackColor = Color.FromArgb(13, 56, 56);
            flpThumbnails.Dock = DockStyle.Fill;
            flpThumbnails.Location = new Point(0, 0);
            flpThumbnails.Name = "flpThumbnails";
            flpThumbnails.Size = new Size(1046, 111);
            flpThumbnails.TabIndex = 0;
            flpThumbnails.WrapContents = false;
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
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Panel pnlBot;
        private Panel pnlMid;
        private FlowLayoutPanel flpThumbnails;
        private Label lbl1;
        private Button btnTaoQuiz;
    }
}
