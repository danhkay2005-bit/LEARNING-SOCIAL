namespace WinForms.UserControls.Pages
{
    partial class KhoVatPhamPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #pragma warning disable CS0414
        private System.ComponentModel.IContainer components = null;
        #pragma warning restore CS0414

        // ✅ XÓA METHOD DISPOSE VÌ ĐÃ ĐỊNH NGHĨA Ở FILE .cs

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            flpInventory = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(45, 45, 48);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(1000, 102);
            pnlHeader.TabIndex = 1;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.LightGray;
            lblSubtitle.Location = new Point(20, 60);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(960, 27);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Quản lý các vật phẩm bạn đã mua";
            lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(960, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Kho Vật Phẩm";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpInventory
            // 
            flpInventory.AutoScroll = true;
            flpInventory.BackColor = Color.FromArgb(30, 30, 30);
            flpInventory.Dock = DockStyle.Fill;
            flpInventory.Location = new Point(0, 102);
            flpInventory.Name = "flpInventory";
            flpInventory.Padding = new Padding(20);
            flpInventory.Size = new Size(1000, 498);
            flpInventory.TabIndex = 0;
            // 
            // KhoVatPhamPage
            // 
            BackColor = Color.FromArgb(30, 30, 30);
            Controls.Add(flpInventory);
            Controls.Add(pnlHeader);
            Name = "KhoVatPhamPage";
            Size = new Size(1000, 600);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ===== DECLARATIONS =====
        private System.Windows.Forms.FlowLayoutPanel flpInventory;
        private Panel pnlHeader;
        private Label lblSubtitle;
        private Label lblTitle;
    }
}
