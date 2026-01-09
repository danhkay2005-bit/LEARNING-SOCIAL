namespace WinForms.UserControls.MainControl
{
    partial class LeftMenuControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblAppName;

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
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1193, 80);
            pnlHeader.TabIndex = 5;
            // 
            // lblAppName
            // 
            lblAppName.Dock = DockStyle.Fill;
            lblAppName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAppName.Location = new Point(0, 0);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(1193, 80);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "LEARNING SOCIAL";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LeftMenuControl
            // 
            BackColor = Color.WhiteSmoke;
            Controls.Add(pnlHeader);
            Name = "LeftMenuControl";
            Size = new Size(1193, 578);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void InitButton(Button btn, string text)
        {
            btn.Text = text;
            btn.Dock = DockStyle.Top;
            btn.Height = 45;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
            btn.Font = new Font("Segoe UI", 10);
            btn.BackColor = Color.WhiteSmoke;
            btn.Cursor = Cursors.Hand;
        }
    }
}
