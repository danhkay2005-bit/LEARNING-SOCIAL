namespace WinForms.UserControls.Social
{
    partial class NotificationPanel
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // NotificationPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Name = "NotificationPanel";
            Size = new Size(400, 500);
            Load += NotificationPanel_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}