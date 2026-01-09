namespace WinForms.Forms
{
    partial class mainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlLeft = new Panel();
            pnlRight = new Panel();
            midControl = new Panel();   // ✅ PANEL, KHÔNG PHẢI FlowLayoutPanel
            SuspendLayout();

            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = Color.WhiteSmoke;
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Size = new Size(250, 492);
            pnlLeft.TabIndex = 0;

            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.WhiteSmoke;
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(881, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Size = new Size(250, 492);
            pnlRight.TabIndex = 1;

            // 
            // midControl
            // 
            midControl.BackColor = Color.White;
            midControl.Dock = DockStyle.Fill;
            midControl.Location = new Point(250, 0);
            midControl.Name = "midControl";
            midControl.Size = new Size(631, 492);
            midControl.TabIndex = 2;

            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 492);

            // ❗ THỨ TỰ ADD RẤT QUAN TRỌNG
            Controls.Add(midControl);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);

            Name = "mainForm";
            Text = "StudyApp";
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private Panel pnlRight;
        private Panel midControl;
    }
}
