namespace WinForms.UserControls.Tasks
{
    partial class FireworksForm
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
            components = new System.ComponentModel.Container();
            tmrRender = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // tmrRender
            // 
            tmrRender.Interval = 20;
            // 
            // FireworksForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            DoubleBuffered = true;
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FireworksForm";
            ShowInTaskbar = false;
            Text = "FireworksForm";
            TopMost = true;
            TransparencyKey = Color.Black;
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer tmrRender;
    }
}