namespace WinForms.UserControls.MainControl
{
    partial class LeftMenuControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblAppName;
        private FlowLayoutPanel pnlMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblAppName = new Label();
            pnlMenu = new FlowLayoutPanel();

            pnlHeader.SuspendLayout();
            SuspendLayout();

            // ================= HEADER =================
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 80;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblAppName);

            lblAppName.Dock = DockStyle.Fill;
            lblAppName.Text = "📘 LEARNING SOCIAL";
            lblAppName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;

            // ================= MENU =================
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.FlowDirection = FlowDirection.TopDown;
            pnlMenu.WrapContents = false;
            pnlMenu.AutoScroll = true;
            pnlMenu.Padding = new Padding(10);
            pnlMenu.BackColor = Color.WhiteSmoke;

            // ================= CONTROL =================
            BackColor = Color.WhiteSmoke;
            Controls.Add(pnlMenu);
            Controls.Add(pnlHeader);
            Name = "LeftMenuControl";
            Width = 240;

            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
