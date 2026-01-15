namespace WinForms.UserControls.Quiz
{
    partial class BoDeItemControl
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
            this.lblTenBoDe = new System.Windows.Forms.Label();
            this.lblSoCauHoi = new System.Windows.Forms.Label();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.btnVaoThi = new System.Windows.Forms.Button();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.panelBorder.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelBorder: Tạo khung viền và màu nền cho thẻ
            // 
            this.panelBorder.BackColor = System.Drawing.Color.White;
            this.panelBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBorder.Controls.Add(this.lblTenBoDe);
            this.panelBorder.Controls.Add(this.lblSoCauHoi);
            this.panelBorder.Controls.Add(this.lblThoiGian);
            this.panelBorder.Controls.Add(this.btnVaoThi);
            this.panelBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBorder.Location = new System.Drawing.Point(0, 0);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Size = new System.Drawing.Size(280, 150);

            // 
            // lblTenBoDe: Tên bộ đề (In đậm)
            // 
            this.lblTenBoDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenBoDe.Location = new System.Drawing.Point(13, 13);
            this.lblTenBoDe.Name = "lblTenBoDe";
            this.lblTenBoDe.Size = new System.Drawing.Size(250, 28);
            this.lblTenBoDe.Text = "Tên Bộ Đề Ở Đây";

            // 
            // lblSoCauHoi
            // 
            this.lblSoCauHoi.AutoSize = true;
            this.lblSoCauHoi.ForeColor = System.Drawing.Color.DimGray;
            this.lblSoCauHoi.Location = new System.Drawing.Point(15, 50);
            this.lblSoCauHoi.Name = "lblSoCauHoi";
            this.lblSoCauHoi.Size = new System.Drawing.Size(100, 20);
            this.lblSoCauHoi.Text = "Số câu: 40 câu";

            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.ForeColor = System.Drawing.Color.DimGray;
            this.lblThoiGian.Location = new System.Drawing.Point(15, 75);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(120, 20);
            this.lblThoiGian.Text = "Thời gian: 45 phút";

            // 
            // btnVaoThi: Nút hành động
            // 
            this.btnVaoThi.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnVaoThi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVaoThi.ForeColor = System.Drawing.Color.White;
            this.btnVaoThi.Location = new System.Drawing.Point(150, 100);
            this.btnVaoThi.Name = "btnVaoThi";
            this.btnVaoThi.Size = new System.Drawing.Size(110, 35);
            this.btnVaoThi.Text = "Vào Thi";
            this.btnVaoThi.UseVisualStyleBackColor = false;

            // 
            // BoDeItemControl
            // 
            this.Controls.Add(this.panelBorder);
            this.Name = "BoDeItemControl";
            this.Size = new System.Drawing.Size(280, 150);
            this.panelBorder.ResumeLayout(false);
            this.panelBorder.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTenBoDe;
        private System.Windows.Forms.Label lblSoCauHoi;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.Button btnVaoThi;
        private System.Windows.Forms.Panel panelBorder;
    }
}
