namespace WinForms.UserControls.Quiz
{
    partial class SapXepHocControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpResult;
        private System.Windows.Forms.FlowLayoutPanel flpBank;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Button btnReset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flpResult = new System.Windows.Forms.FlowLayoutPanel();
            this.flpBank = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblInstruction
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(193, 225, 127);
            this.lblInstruction.Location = new System.Drawing.Point(20, 10);
            this.lblInstruction.Text = "SẮP XẾP CÁC TỪ SAU THÀNH CÂU ĐÚNG:";

            // lblHint (Hiển thị nghĩa/giải thích)
            this.lblHint.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.LightGray;
            this.lblHint.Location = new System.Drawing.Point(20, 40);
            this.lblHint.Size = new System.Drawing.Size(760, 40);
            this.lblHint.Text = "Gợi ý: [Nghĩa của câu]";

            // flpResult (Vùng chứa các từ đã chọn)
            this.flpResult.BackColor = System.Drawing.Color.FromArgb(30, 50, 55);
            this.flpResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpResult.Location = new System.Drawing.Point(20, 90);
            this.flpResult.Name = "flpResult";
            this.flpResult.Padding = new System.Windows.Forms.Padding(10);
            this.flpResult.Size = new System.Drawing.Size(760, 150);

            // flpBank (Kho từ bị xáo trộn)
            this.flpBank.Location = new System.Drawing.Point(20, 260);
            this.flpBank.Name = "flpBank";
            this.flpBank.Padding = new System.Windows.Forms.Padding(10);
            this.flpBank.Size = new System.Drawing.Size(760, 150);

            // btnReset
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(60, 80, 85);
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(680, 420);
            this.btnReset.Size = new System.Drawing.Size(100, 35);
            this.btnReset.Text = "LÀM LẠI";
           // this.btnReset.Click += (s, e) => ResetQuiz();

            // SapXepHocControl
            this.BackColor = System.Drawing.Color.FromArgb(25, 45, 50);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.flpResult);
            this.Controls.Add(this.flpBank);
            this.Controls.Add(this.btnReset);
            this.Size = new System.Drawing.Size(800, 470);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
