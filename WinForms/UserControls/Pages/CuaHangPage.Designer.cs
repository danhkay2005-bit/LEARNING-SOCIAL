namespace WinForms.UserControls.Pages
{
    partial class CuaHangPage
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
            pnlHeader = new Panel();
            lblbalance = new Label();
            lblTenShop = new Label();
            flpContainer = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblbalance);
            pnlHeader.Controls.Add(lblTenShop);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1021, 89);
            pnlHeader.TabIndex = 0;
            // 
            // lblbalance
            // 
            lblbalance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblbalance.AutoSize = true;
            lblbalance.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblbalance.Location = new Point(3, 46);
            lblbalance.Name = "lblbalance";
            lblbalance.Size = new Size(103, 21);
            lblbalance.TabIndex = 1;
            lblbalance.Text = "\U0001fa99 0 | 💎 0\"";
            // 
            // lblTenShop
            // 
            lblTenShop.AutoSize = true;
            lblTenShop.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenShop.Location = new Point(0, 0);
            lblTenShop.Name = "lblTenShop";
            lblTenShop.Size = new Size(211, 30);
            lblTenShop.TabIndex = 0;
            lblTenShop.Text = "Cửa Hàng Vật Phẩm";
            // 
            // flpContainer
            // 
            flpContainer.AutoScroll = true;
            flpContainer.BackColor = Color.WhiteSmoke;
            flpContainer.Dock = DockStyle.Fill;
            flpContainer.Location = new Point(0, 89);
            flpContainer.Name = "flpContainer";
            flpContainer.Padding = new Padding(20);
            flpContainer.Size = new Size(1021, 473);
            flpContainer.TabIndex = 1;
            // 
            // CuaHangPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flpContainer);
            Controls.Add(pnlHeader);
            Name = "CuaHangPage";
            Size = new Size(1021, 562);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblbalance;
        private Label lblTenShop;
        private FlowLayoutPanel flpContainer;
    }
}
