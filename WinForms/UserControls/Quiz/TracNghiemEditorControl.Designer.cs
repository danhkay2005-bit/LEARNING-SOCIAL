namespace WinForms.UserControls.Quiz
{
    partial class TracNghiemEditorControl
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
            lbl1 = new Label();
            txtCauHoi = new TextBox();
            label1 = new Label();
            panelAnswers = new Panel();
            rbD = new RadioButton();
            txtDapAnD = new TextBox();
            rbC = new RadioButton();
            txtDapAnC = new TextBox();
            rbB = new RadioButton();
            txtDapAnB = new TextBox();
            rbA = new RadioButton();
            txtDapAnA = new TextBox();
            label2 = new Label();
            picCauHoi = new PictureBox();
            btnThemAnh = new Button();
            btnTaoAnhAI = new Button(); // Thêm mới
            panelAnswers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCauHoi).BeginInit();
            SuspendLayout();
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl1.ForeColor = Color.FromArgb(13, 56, 56);
            lbl1.Location = new Point(409, 11);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(279, 46);
            lbl1.TabIndex = 2;
            lbl1.Text = "Tạo trắc nghiệm";
            lbl1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtCauHoi
            // 
            txtCauHoi.Font = new Font("Segoe UI", 12F);
            txtCauHoi.Location = new Point(53, 107);
            txtCauHoi.Multiline = true;
            txtCauHoi.Name = "txtCauHoi";
            txtCauHoi.PlaceholderText = "Nhập nội dung câu hỏi tại đây...";
            txtCauHoi.Size = new Size(635, 107);
            txtCauHoi.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(53, 76);
            label1.Name = "label1";
            label1.Size = new Size(172, 28);
            label1.TabIndex = 4;
            label1.Text = "Nội dung câu hỏi:";
            // 
            // panelAnswers
            // 
            panelAnswers.Controls.Add(rbD);
            panelAnswers.Controls.Add(txtDapAnD);
            panelAnswers.Controls.Add(rbC);
            panelAnswers.Controls.Add(txtDapAnC);
            panelAnswers.Controls.Add(rbB);
            panelAnswers.Controls.Add(txtDapAnB);
            panelAnswers.Controls.Add(rbA);
            panelAnswers.Controls.Add(txtDapAnA);
            panelAnswers.Location = new Point(53, 260);
            panelAnswers.Name = "panelAnswers";
            panelAnswers.Size = new Size(1036, 235);
            panelAnswers.TabIndex = 5;
            // 
            // rbD
            // 
            rbD.AutoSize = true;
            rbD.Location = new Point(14, 185);
            rbD.Name = "rbD";
            rbD.Size = new Size(17, 16);
            rbD.TabIndex = 7;
            rbD.UseVisualStyleBackColor = true;
            // 
            // txtDapAnD
            // 
            txtDapAnD.Location = new Point(47, 178);
            txtDapAnD.Name = "txtDapAnD";
            txtDapAnD.PlaceholderText = "Lựa chọn 4";
            txtDapAnD.Size = new Size(973, 27);
            txtDapAnD.TabIndex = 6;
            // 
            // rbC
            // 
            rbC.AutoSize = true;
            rbC.Location = new Point(14, 131);
            rbC.Name = "rbC";
            rbC.Size = new Size(17, 16);
            rbC.TabIndex = 5;
            rbC.UseVisualStyleBackColor = true;
            // 
            // txtDapAnC
            // 
            txtDapAnC.Location = new Point(47, 124);
            txtDapAnC.Name = "txtDapAnC";
            txtDapAnC.PlaceholderText = "Lựa chọn 3";
            txtDapAnC.Size = new Size(973, 27);
            txtDapAnC.TabIndex = 4;
            // 
            // rbB
            // 
            rbB.AutoSize = true;
            rbB.Location = new Point(14, 78);
            rbB.Name = "rbB";
            rbB.Size = new Size(17, 16);
            rbB.TabIndex = 3;
            rbB.UseVisualStyleBackColor = true;
            // 
            // txtDapAnB
            // 
            txtDapAnB.Location = new Point(47, 71);
            txtDapAnB.Name = "txtDapAnB";
            txtDapAnB.PlaceholderText = "Lựa chọn 2";
            txtDapAnB.Size = new Size(973, 27);
            txtDapAnB.TabIndex = 2;
            // 
            // rbA
            // 
            rbA.AutoSize = true;
            rbA.Checked = true;
            rbA.Location = new Point(14, 25);
            rbA.Name = "rbA";
            rbA.Size = new Size(17, 16);
            rbA.TabIndex = 1;
            rbA.TabStop = true;
            rbA.UseVisualStyleBackColor = true;
            // 
            // txtDapAnA
            // 
            txtDapAnA.Location = new Point(47, 18);
            txtDapAnA.Name = "txtDapAnA";
            txtDapAnA.PlaceholderText = "Lựa chọn 1 (Đáp án mặc định)";
            txtDapAnA.Size = new Size(973, 27);
            txtDapAnA.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(53, 229);
            label2.Name = "label2";
            label2.Size = new Size(393, 28);
            label2.TabIndex = 6;
            label2.Text = "Các lựa chọn (Tích chọn để làm đáp án đúng):";
            // 
            // picCauHoi
            // 
            picCauHoi.BackColor = Color.Gainsboro;
            picCauHoi.BorderStyle = BorderStyle.FixedSingle;
            picCauHoi.Location = new Point(718, 107);
            picCauHoi.Name = "picCauHoi";
            picCauHoi.Size = new Size(204, 107);
            picCauHoi.SizeMode = PictureBoxSizeMode.Zoom;
            picCauHoi.TabIndex = 7;
            picCauHoi.TabStop = false;
            // 
            // btnThemAnh
            // 
            btnThemAnh.BackColor = Color.FromArgb(13, 56, 56);
            btnThemAnh.FlatStyle = FlatStyle.Flat;
            btnThemAnh.ForeColor = Color.White;
            btnThemAnh.Location = new Point(928, 107);
            btnThemAnh.Name = "btnThemAnh";
            btnThemAnh.Size = new Size(94, 52); // Giảm chiều cao từ 107 xuống 52
            btnThemAnh.TabIndex = 8;
            btnThemAnh.Text = "Thêm ảnh";
            btnThemAnh.UseVisualStyleBackColor = false;
            // 
            // btnTaoAnhAI
            // 
            btnTaoAnhAI.BackColor = Color.FromArgb(0, 120, 215); // Màu xanh dương để phân biệt
            btnTaoAnhAI.FlatStyle = FlatStyle.Flat;
            btnTaoAnhAI.ForeColor = Color.White;
            btnTaoAnhAI.Location = new Point(928, 162); // Đặt bên dưới nút Thêm ảnh
            btnTaoAnhAI.Name = "btnTaoAnhAI";
            btnTaoAnhAI.Size = new Size(94, 52);
            btnTaoAnhAI.TabIndex = 9;
            btnTaoAnhAI.Text = "✨ Tạo AI";
            btnTaoAnhAI.UseVisualStyleBackColor = false;
            // 
            // TracNghiemEditorControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnTaoAnhAI); // Thêm vào Controls
            Controls.Add(btnThemAnh);
            Controls.Add(picCauHoi);
            Controls.Add(label2);
            Controls.Add(panelAnswers);
            Controls.Add(label1);
            Controls.Add(txtCauHoi);
            Controls.Add(lbl1);
            Name = "TracNghiemEditorControl";
            Size = new Size(1139, 516);
            panelAnswers.ResumeLayout(false);
            panelAnswers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCauHoi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbl1;
        private TextBox txtCauHoi;
        private Label label1;
        private Panel panelAnswers;
        private RadioButton rbD;
        private TextBox txtDapAnD;
        private RadioButton rbC;
        private TextBox txtDapAnC;
        private RadioButton rbB;
        private TextBox txtDapAnB;
        private RadioButton rbA;
        private TextBox txtDapAnA;
        private Label label2;
        private PictureBox picCauHoi;
        private Button btnThemAnh;
        private Button btnTaoAnhAI; // Khai báo biến
    }
}