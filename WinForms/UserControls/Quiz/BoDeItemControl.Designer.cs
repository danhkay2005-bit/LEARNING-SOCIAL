namespace WinForms.UserControls.Quiz
{
    partial class BoDeItemControl
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
            this.lblTenBoDe = new System.Windows.Forms.Label();
            this.lblSoCauHoi = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.btnVaoThi = new System.Windows.Forms.Button();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.panelBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelBorder
            // 
            this.panelBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.panelBorder.Controls.Add(this.pnlInfo);
            this.panelBorder.Controls.Add(this.lblTenBoDe);
            this.panelBorder.Controls.Add(this.picCover);
            this.panelBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBorder.Location = new System.Drawing.Point(0, 0);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Padding = new System.Windows.Forms.Padding(1);
            this.panelBorder.Size = new System.Drawing.Size(280, 240); // Tăng chiều cao để chứa ảnh

            // 
            // picCover (Ảnh bìa từ AI)
            // 
            this.picCover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.picCover.Dock = System.Windows.Forms.DockStyle.Top;
            this.picCover.Location = new System.Drawing.Point(1, 1);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(278, 130);
            this.picCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCover.TabIndex = 0;
            this.picCover.TabStop = false;

            // 
            // lblTenBoDe
            // 
            this.lblTenBoDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenBoDe.ForeColor = System.Drawing.Color.White;
            this.lblTenBoDe.Location = new System.Drawing.Point(12, 140);
            this.lblTenBoDe.Name = "lblTenBoDe";
            this.lblTenBoDe.Size = new System.Drawing.Size(255, 30);
            this.lblTenBoDe.Text = "Tên Bộ Đề";
            this.lblTenBoDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // pnlInfo (Khu vực thông số & Nút bấm)
            // 
            this.pnlInfo.Controls.Add(this.lblSoCauHoi);
            this.pnlInfo.Controls.Add(this.lblThoiGian);
            this.pnlInfo.Controls.Add(this.btnVaoThi);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(1, 175);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(278, 64);

            // 
            // lblSoCauHoi
            // 
            this.lblSoCauHoi.AutoSize = true;
            this.lblSoCauHoi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSoCauHoi.ForeColor = System.Drawing.Color.Silver;
            this.lblSoCauHoi.Location = new System.Drawing.Point(12, 5);
            this.lblSoCauHoi.Name = "lblSoCauHoi";
            this.lblSoCauHoi.Size = new System.Drawing.Size(55, 20);
            this.lblSoCauHoi.Text = "40 câu";

            // 
            // lblThoiGian (Đã bỏ chữ phút)
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblThoiGian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblThoiGian.Location = new System.Drawing.Point(12, 28);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(30, 23);
            this.lblThoiGian.Text = "45"; // Chỉ hiển thị số

            // 
            // btnVaoThi
            // 
            this.btnVaoThi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(225)))), ((int)(((byte)(127)))));
            this.btnVaoThi.FlatAppearance.BorderSize = 0;
            this.btnVaoThi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVaoThi.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.btnVaoThi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(25)))), ((int)(((byte)(29)))));
            this.btnVaoThi.Location = new System.Drawing.Point(155, 12);
            this.btnVaoThi.Name = "btnVaoThi";
            this.btnVaoThi.Size = new System.Drawing.Size(110, 40);
            this.btnVaoThi.Text = "BẮT ĐẦU";
            this.btnVaoThi.UseVisualStyleBackColor = false;

            // 
            // BoDeItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBorder);
            this.Name = "BoDeItemControl";
            this.Size = new System.Drawing.Size(280, 240);
            this.panelBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTenBoDe;
        private System.Windows.Forms.Label lblSoCauHoi;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Button btnVaoThi;
        private System.Windows.Forms.Panel panelBorder;
        private System.Windows.Forms.PictureBox picCover; // Thành phần mới
        private System.Windows.Forms.Panel pnlInfo; // Panel phụ để cân đối thông tin
    }
}