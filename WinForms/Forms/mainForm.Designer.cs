namespace WinForms.Forms
{
    partial class mainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlLeft = new Panel();
            pnlRight = new Panel();
            midControl = new FlowLayoutPanel();
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
            pnlLeft.TabStop = false;
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
            midControl.AutoScroll = true;
            midControl.BackColor = Color.White;
            midControl.Dock = DockStyle.Fill;
            midControl.FlowDirection = FlowDirection.TopDown;
            midControl.Location = new Point(250, 0);
            midControl.Name = "midControl";
            midControl.Size = new Size(631, 492);
            midControl.TabIndex = 2;
            midControl.WrapContents = false;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 492);
            Controls.Add(midControl);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Name = "mainForm";
            Text = "mainForm";
            ResumeLayout(false);
        }
                                                                    
        #endregion

        private Panel pnlLeft;
        private Panel pnlRight;
        private FlowLayoutPanel midControl;
    }
}