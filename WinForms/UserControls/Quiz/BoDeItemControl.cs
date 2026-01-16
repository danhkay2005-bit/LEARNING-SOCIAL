using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class BoDeItemControl : UserControl
    {
        // 1. Khai báo các thuộc tính để lưu trữ dữ liệu
        public int MaBoDe { get; set; }

        [Category("Quiz Data")]
        public string TenBoDe
        {
            get => lblTenBoDe.Text;
            set => lblTenBoDe.Text = value;
        }

        [Category("Quiz Data")]
        public int SoCauHoi
        {
            get => int.Parse(lblSoCauHoi.Text.Replace("Số câu: ", "").Replace(" câu", ""));
            set => lblSoCauHoi.Text = $"Số câu: {value} câu";
        }

        [Category("Quiz Data")]
        public int ThoiGian
        {
            get => int.Parse(lblThoiGian.Text.Replace("Thời gian: ", "").Replace(" phút", ""));
            set => lblThoiGian.Text = $"Thời gian: {value} phút";
        }

        // 2. Tạo sự kiện để Form cha có thể đăng ký xử lý khi nhấn nút
        public event EventHandler? OnVaoThiClick;

        public BoDeItemControl()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            // Gán sự kiện click cho nút "Vào Thi"
            btnVaoThi.Click += (s, e) => OnVaoThiClick?.Invoke(this, e);

            // Hiệu ứng Hover (Tùy chọn) - Đổi màu khi di chuột vào
            panelBorder.MouseEnter += (s, e) => panelBorder.BackColor = Color.AliceBlue;
            panelBorder.MouseLeave += (s, e) => panelBorder.BackColor = Color.White;
        }

        /// <summary>
        /// Hàm tiện ích để nạp nhanh dữ liệu vào control
        /// </summary>
        public void SetData(int ma, string ten, int soCau, int thoiGian)
        {
            this.MaBoDe = ma;
            this.TenBoDe = ten;
            this.SoCauHoi = soCau;
            this.ThoiGian = thoiGian;
        }
    }
}