namespace WinForms.UserControls.Quiz
{
    partial class GhepCapHocControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.FlowLayoutPanel flpLeft;
        private System.Windows.Forms.FlowLayoutPanel flpRight;
        private System.Windows.Forms.Label lblInstruction;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.flpRight = new System.Windows.Forms.FlowLayoutPanel();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // tlpMain
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.flpLeft, 0, 0);
            this.tlpMain.Controls.Add(this.flpRight, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 50);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(800, 450);

            // flpLeft
            this.flpLeft.AutoScroll = true;
            this.flpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLeft.Padding = new System.Windows.Forms.Padding(10);
            this.flpLeft.WrapContents = false;

            // flpRight
            this.flpRight.AutoScroll = true;
            this.flpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRight.Padding = new System.Windows.Forms.Padding(10);
            this.flpRight.WrapContents = false;

            // lblInstruction
            this.lblInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(193, 225, 127);
            this.lblInstruction.Height = 50;
            this.lblInstruction.Text = "CHỌN CÁC CẶP TƯƠNG ỨNG VỚI NHAU";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // GhepCapHocControl
            this.BackColor = System.Drawing.Color.FromArgb(25, 45, 50);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.lblInstruction);
            this.Size = new System.Drawing.Size(800, 500);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
