namespace WinForms.UserControls.Quiz
{
    partial class TracNghiemHocControl
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
            lblMatTruoc = new Label();
            flpDapAn = new FlowLayoutPanel();
            picHinhAnh = new PictureBox();
            SuspendLayout();
            // 
            // lblMatTruoc (Câu hỏi - Mặt trước thẻ)
            // 
            lblMatTruoc.Dock = DockStyle.Top;
            lblMatTruoc.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold); // Giảm font size một chút nếu có ảnh
            lblMatTruoc.ForeColor = Color.White;
            lblMatTruoc.Location = new Point(0, 0);
            lblMatTruoc.Name = "lblMatTruoc";
            lblMatTruoc.Size = new Size(900, 100); // Giảm chiều cao mặc định
            lblMatTruoc.TabIndex = 0;
            lblMatTruoc.TextAlign = ContentAlignment.MiddleCenter;

            picHinhAnh.Dock = DockStyle.Top;
            picHinhAnh.Location = new Point(0, 100);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(900, 200); // Chiều cao cho ảnh
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom; // Giữ tỉ lệ ảnh
            picHinhAnh.TabIndex = 2;
            picHinhAnh.TabStop = false;
            picHinhAnh.Visible = false; // Mặc định ẩn, chỉ hiện khi có ảnh
            // 
            // flpDapAn (Danh sách đáp án nạp động)
            // 
            flpDapAn.AutoScroll = true;
            flpDapAn.Dock = DockStyle.Fill;
            flpDapAn.FlowDirection = FlowDirection.TopDown;
            flpDapAn.Location = new Point(0, 180);
            flpDapAn.Name = "flpDapAn";
            flpDapAn.Padding = new Padding(150, 0, 150, 0); // Đẩy đáp án vào giữa cho đẹp
            flpDapAn.Size = new Size(900, 320);
            flpDapAn.TabIndex = 1;
            flpDapAn.WrapContents = false;
            // 
            // TracNghiemHocControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 38, 44); // Màu tối đồng bộ với HocBoDePage
            Controls.Add(flpDapAn);
            Controls.Add(picHinhAnh);
            Controls.Add(lblMatTruoc);
            Name = "TracNghiemHocControl";
            Size = new Size(900, 500);
            ResumeLayout(false);
        }

        #endregion

        private Label lblMatTruoc;
        private FlowLayoutPanel flpDapAn;
        private PictureBox picHinhAnh;
    }
}