namespace WinForms.UserControls.Quiz
{
    partial class DienKhuyetHocControl
    {
        private System.ComponentModel.IContainer components = null;

        // Các control thêm vào
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.FlowLayoutPanel flpContent;
        private System.Windows.Forms.Label lblHuongDan;
        public System.Windows.Forms.Label lblGiaiThich;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.flpContent = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.lblGiaiThich = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.pnlContainer.Controls.Add(this.flpContent);
            this.pnlContainer.Controls.Add(this.lblHuongDan);
            this.pnlContainer.Controls.Add(this.lblGiaiThich);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(20, 20);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContainer.Size = new System.Drawing.Size(760, 460);
            this.pnlContainer.TabIndex = 0;

            // 
            // lblHuongDan
            // 
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHuongDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(225)))), ((int)(((byte)(127)))));
            this.lblHuongDan.Location = new System.Drawing.Point(20, 10);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(250, 23);
            this.lblHuongDan.Text = "HÃY ĐIỀN TỪ CÒN THIẾU:";

            // 
            // flpContent
            // 
            this.flpContent.AutoScroll = true;
            this.flpContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpContent.Location = new System.Drawing.Point(20, 50);
            this.flpContent.Name = "flpContent";
            this.flpContent.Size = new System.Drawing.Size(720, 250);
            this.flpContent.TabIndex = 1;
            this.flpContent.WrapContents = true; // Rất quan trọng để chữ xuống dòng đúng

            // 
            // lblGiaiThich
            // 
            this.lblGiaiThich.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGiaiThich.Font = new System.Drawing.Font("Segoe UI Italics", 11F);
            this.lblGiaiThich.ForeColor = System.Drawing.Color.LightGray;
            this.lblGiaiThich.Location = new System.Drawing.Point(20, 320);
            this.lblGiaiThich.Name = "lblGiaiThich";
            this.lblGiaiThich.Size = new System.Drawing.Size(720, 80);
            this.lblGiaiThich.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblGiaiThich.Text = ""; // Hiển thị sau khi check kết quả

            // 
            // DienKhuyetHocControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.pnlContainer);
            this.Name = "DienKhuyetHocControl";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(800, 500);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
