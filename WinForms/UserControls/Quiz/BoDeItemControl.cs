using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace WinForms.UserControls.Quiz
{
    public partial class BoDeItemControl : UserControl
    {
        private int _maBoDe;
        private int _soCauHoi;
        private int _thoiGian;
        private string _urlAnhBia = string.Empty;

        [Category("Quiz Data")]
        public int MaBoDe { get => _maBoDe; set => _maBoDe = value; }

        [Category("Quiz Data")]
        public string TenBoDe
        {
            get => lblTenBoDe.Text;
            set => lblTenBoDe.Text = value;
        }

        [Category("Quiz Data")]
        public int SoCauHoi
        {
            get => _soCauHoi;
            set { _soCauHoi = value; lblSoCauHoi.Text = $"📝 {value} câu"; } // Thêm icon nhỏ cho cân đối
        }

        [Category("Quiz Data")]
        public int ThoiGian
        {
            get => _thoiGian;
            set { _thoiGian = value; lblThoiGian.Text = $"⏱ {value}"; } // Dùng icon thay cho chữ "phút"
        }

        [Category("Quiz Data")]
        public string UrlAnhBia
        {
            get => _urlAnhBia;
            set
            {
                _urlAnhBia = value;
                if (!string.IsNullOrEmpty(value) && value.StartsWith("http"))
                {
                    picCover.ImageLocation = value;
                }
            }
        }

        public event EventHandler? OnVaoThiClick;

        public BoDeItemControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Chống giật lag khi render ảnh AI
            SetupStyles();
            RegisterEvents();
        }

        private void SetupStyles()
        {
            this.BackColor = Color.FromArgb(25, 40, 45); // Màu nền tối
            btnVaoThi.BackColor = Color.FromArgb(193, 225, 127); // Xanh lime
            btnVaoThi.FlatAppearance.BorderSize = 0;

            lblTenBoDe.ForeColor = Color.White;
            lblSoCauHoi.ForeColor = Color.Silver;
            lblThoiGian.ForeColor = Color.FromArgb(255, 128, 128); // Màu đỏ nhạt hiện đại
        }

        private void RegisterEvents()
        {
            btnVaoThi.Click += (s, e) => OnVaoThiClick?.Invoke(this, e);

            // Gán sự kiện Hover cho tất cả thành phần bên trong để đồng nhất hiệu ứng
            AssignHoverRecursive(this);
        }

        private void AssignHoverRecursive(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button) continue; // Nút bấm có hiệu ứng riêng của nó
                c.MouseEnter += Control_MouseEnter;
                c.MouseLeave += Control_MouseLeave;
                AssignHoverRecursive(c);
            }
            parent.MouseEnter += Control_MouseEnter;
            parent.MouseLeave += Control_MouseLeave;
        }

        private void Control_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(35, 55, 60); // Sáng lên một chút
            this.Cursor = Cursors.Hand;
        }

        private void Control_MouseLeave(object? sender, EventArgs e)
        {
            // Kiểm tra chuột thực sự đã ra khỏi thẻ chưa (tránh flickering)
            if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                this.BackColor = Color.FromArgb(25, 40, 45);
                this.Cursor = Cursors.Default;
            }
        }

        public void SetData(int ma, string ten, int soCau, int thoiGian, string urlAnh = "")
        {
            this.MaBoDe = ma;
            this.TenBoDe = ten;
            this.SoCauHoi = soCau;
            this.ThoiGian = thoiGian;
            this.UrlAnhBia = urlAnh;
        }
    }
}