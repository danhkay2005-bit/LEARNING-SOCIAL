namespace WinForms.UserControls.Pages
{
    partial class HocTapPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMainContent = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblRecentlyPublished = new System.Windows.Forms.Label();
            this.flowRecentlyPublished = new System.Windows.Forms.FlowLayoutPanel();

            // Thiết lập pnlMainContent (Chứa tất cả)
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.AutoScroll = true;
            this.pnlMainContent.FlowDirection = FlowDirection.TopDown;
            this.pnlMainContent.WrapContents = false;
            this.pnlMainContent.Padding = new Padding(20);
            this.pnlMainContent.BackColor = Color.WhiteSmoke;

            // 1. Header Section (Create a quiz)
            this.pnlHeader.Size = new Size(1000, 200);
            this.pnlHeader.BackColor = Color.FromArgb(13, 56, 56); // Màu xanh đậm như ảnh
            // Bạn có thể thêm các Label và Button "Create a quiz" vào đây

            // 2. Title "Recently published"
            this.lblRecentlyPublished.Text = "Recently published";
            this.lblRecentlyPublished.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblRecentlyPublished.Margin = new Padding(0, 20, 0, 10);
            this.lblRecentlyPublished.AutoSize = true;

            // 3. FlowLayout chứa các Card (Nằm ngang)
            this.flowRecentlyPublished.Size = new Size(1020, 250);
            this.flowRecentlyPublished.FlowDirection = FlowDirection.LeftToRight;
            this.flowRecentlyPublished.WrapContents = false; // Để nó không xuống dòng
            this.flowRecentlyPublished.AutoScroll = true; // Hiện thanh cuộn ngang

            // Thêm vào Page
            this.pnlMainContent.Controls.Add(pnlHeader);
            this.pnlMainContent.Controls.Add(lblRecentlyPublished);
            this.pnlMainContent.Controls.Add(flowRecentlyPublished);

            this.Controls.Add(pnlMainContent);
            this.Name = "HocTapPage";
            this.Size = new Size(1069, 534);
        }

        private FlowLayoutPanel pnlMainContent;
        private Panel pnlHeader;
        private Label lblRecentlyPublished;
        private FlowLayoutPanel flowRecentlyPublished;
    }
}