namespace WinForms.UserControls.Quiz
{
    partial class DienKhuyetEditorControl
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
            lblTitle = new Label();
            txtSentence = new TextBox();
            btnHideWord = new Button();
            lblAnswers = new Label();
            txtAnswers = new TextBox();
            lblGiaiThich = new Label();
            txtGiaiThich = new TextBox();
            lblHuongDan = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(13, 56, 56);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(244, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nội dung câu hỏi gốc";
            // 
            // txtSentence
            // 
            txtSentence.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSentence.Location = new Point(25, 60);
            txtSentence.Multiline = true;
            txtSentence.Name = "txtSentence";
            txtSentence.ScrollBars = ScrollBars.Vertical;
            txtSentence.Size = new Size(750, 140);
            txtSentence.TabIndex = 1;
            // 
            // btnHideWord
            // 
            btnHideWord.BackColor = Color.Gold;
            btnHideWord.Cursor = Cursors.Hand;
            btnHideWord.FlatAppearance.BorderSize = 0;
            btnHideWord.FlatStyle = FlatStyle.Flat;
            btnHideWord.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHideWord.ForeColor = Color.FromArgb(13, 56, 56);
            btnHideWord.Location = new Point(575, 20);
            btnHideWord.Name = "btnHideWord";
            btnHideWord.Size = new Size(200, 35);
            btnHideWord.TabIndex = 2;
            btnHideWord.Text = "Ẩn từ đang chọn [...]";
            btnHideWord.UseVisualStyleBackColor = false;
            // 
            // lblAnswers
            // 
            lblAnswers.AutoSize = true;
            lblAnswers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAnswers.ForeColor = Color.FromArgb(13, 56, 56);
            lblAnswers.Location = new Point(20, 220);
            lblAnswers.Name = "lblAnswers";
            lblAnswers.Size = new Size(208, 28);
            lblAnswers.TabIndex = 3;
            lblAnswers.Text = "Các từ cần điền (Key)";
            // 
            // txtAnswers
            // 
            txtAnswers.BackColor = Color.FromArgb(240, 240, 240);
            txtAnswers.BorderStyle = BorderStyle.FixedSingle;
            txtAnswers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            txtAnswers.ForeColor = Color.DarkRed;
            txtAnswers.Location = new Point(25, 255);
            txtAnswers.Name = "txtAnswers";
            txtAnswers.ReadOnly = true;
            txtAnswers.Size = new Size(750, 34);
            txtAnswers.TabIndex = 4;
            // 
            // lblGiaiThich
            // 
            lblGiaiThich.AutoSize = true;
            lblGiaiThich.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblGiaiThich.Location = new Point(20, 310);
            lblGiaiThich.Name = "lblGiaiThich";
            lblGiaiThich.Size = new Size(98, 28);
            lblGiaiThich.TabIndex = 5;
            lblGiaiThich.Text = "Giải thích";
            // 
            // txtGiaiThich
            // 
            txtGiaiThich.Font = new Font("Segoe UI", 10.8F);
            txtGiaiThich.Location = new Point(25, 345);
            txtGiaiThich.Multiline = true;
            txtGiaiThich.Name = "txtGiaiThich";
            txtGiaiThich.Size = new Size(750, 80);
            txtGiaiThich.TabIndex = 6;
            // 
            // lblHuongDan
            // 
            lblHuongDan.AutoSize = true;
            lblHuongDan.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblHuongDan.ForeColor = Color.Gray;
            lblHuongDan.Location = new Point(25, 203);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(428, 20);
            lblHuongDan.TabIndex = 7;
            lblHuongDan.Text = "* Mẹo: Bôi đen từ cần ẩn rồi nhấn nút \"Ẩn từ đang chọn\" ở trên.";
            // 
            // DienKhuyetEditorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblHuongDan);
            Controls.Add(txtGiaiThich);
            Controls.Add(lblGiaiThich);
            Controls.Add(txtAnswers);
            Controls.Add(lblAnswers);
            Controls.Add(btnHideWord);
            Controls.Add(txtSentence);
            Controls.Add(lblTitle);
            Name = "DienKhuyetEditorControl";
            Size = new Size(800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtSentence;
        private Button btnHideWord;
        private Label lblAnswers;
        private TextBox txtAnswers;
        private Label lblGiaiThich;
        private TextBox txtGiaiThich;
        private Label lblHuongDan;
    }
}