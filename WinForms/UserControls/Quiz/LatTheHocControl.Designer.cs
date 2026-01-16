namespace WinForms.UserControls.Quiz
{
    partial class LatTheHocControl
    {   /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnLat;
        private System.Windows.Forms.Button btnDung;
        private System.Windows.Forms.Button btnSai;
        private System.Windows.Forms.Label lblSideIndicator;
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


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblContent = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnLat = new System.Windows.Forms.Button();
            this.btnDung = new System.Windows.Forms.Button();
            this.btnSai = new System.Windows.Forms.Button();
            this.lblSideIndicator = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.picImage);
            this.pnlCard.Controls.Add(this.lblContent);
            this.pnlCard.Controls.Add(this.lblSideIndicator);
            this.pnlCard.Location = new System.Drawing.Point(100, 40);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(600, 320);
            this.pnlCard.TabIndex = 0;

            // 
            // lblSideIndicator
            // 
            this.lblSideIndicator.AutoSize = true;
            this.lblSideIndicator.ForeColor = System.Drawing.Color.Gray;
            this.lblSideIndicator.Location = new System.Drawing.Point(10, 10);
            this.lblSideIndicator.Text = "MẶT TRƯỚC";

            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(600, 180);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.Visible = false;

            // 
            // lblContent
            // 
            this.lblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblContent.ForeColor = System.Drawing.Color.White;
            this.lblContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContent.Text = "Nội dung thẻ";

            // 
            // btnLat
            // 
            this.btnLat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(85)))));
            this.btnLat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLat.ForeColor = System.Drawing.Color.White;
            this.btnLat.Location = new System.Drawing.Point(325, 380);
            this.btnLat.Size = new System.Drawing.Size(150, 45);
            this.btnLat.Text = "LẬT THẺ";

            // 
            // btnDung (Tương đương nút "Tôi đã thuộc")
            // 
            this.btnDung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnDung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDung.ForeColor = System.Drawing.Color.White;
            this.btnDung.Location = new System.Drawing.Point(410, 380);
            this.btnDung.Size = new System.Drawing.Size(120, 45);
            this.btnDung.Text = "ĐÃ THUỘC";
            this.btnDung.Visible = false;

            // 
            // btnSai (Tương đương nút "Cần học lại")
            // 
            this.btnSai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSai.ForeColor = System.Drawing.Color.White;
            this.btnSai.Location = new System.Drawing.Point(270, 380);
            this.btnSai.Size = new System.Drawing.Size(120, 45);
            this.btnSai.Text = "CHƯA THUỘC";
            this.btnSai.Visible = false;

            // Control Setting
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(50)))));
            this.Size = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.btnLat);
            this.Controls.Add(this.btnDung);
            this.Controls.Add(this.btnSai);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
