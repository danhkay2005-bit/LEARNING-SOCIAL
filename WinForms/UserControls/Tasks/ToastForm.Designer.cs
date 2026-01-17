namespace WinForms.UserControls.Tasks
{
    partial class ToastForm
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
            pnlColor = new Panel();
            lblThongBao = new Label();
            lblMassage = new Label();
            tmrAnimation = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // pnlColor
            // 
            pnlColor.BackColor = Color.LimeGreen;
            pnlColor.Dock = DockStyle.Left;
            pnlColor.Location = new Point(0, 0);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(10, 90);
            pnlColor.TabIndex = 0;
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongBao.Location = new Point(20, 10);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(86, 17);
            lblThongBao.TabIndex = 1;
            lblThongBao.Text = "\"Thông Báo\"";
            // 
            // lblMassage
            // 
            lblMassage.AutoSize = true;
            lblMassage.BackColor = Color.Gray;
            lblMassage.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMassage.Location = new Point(20, 35);
            lblMassage.Name = "lblMassage";
            lblMassage.Size = new Size(123, 15);
            lblMassage.TabIndex = 2;
            lblMassage.Text = "\"Nội dung tin nhắn...\"";
            // 
            // tmrAnimation
            // 
            tmrAnimation.Enabled = true;
            tmrAnimation.Interval = 20;
            // 
            // ToastForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(619, 90);
            Controls.Add(lblMassage);
            Controls.Add(lblThongBao);
            Controls.Add(pnlColor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ToastForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ToastForm";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlColor;
        private Label lblThongBao;
        private Label lblMassage;
        private System.Windows.Forms.Timer tmrAnimation;
    }
}