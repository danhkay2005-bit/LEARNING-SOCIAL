using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinForms.UserControls.Tasks
{
    public partial class ToastForm : Form
    {
        public enum ToastType
        {
            Success,
            Error,
            Info
        }

        private int _toastY;
        private int _toastX;

        public ToastForm(string title, string message, ToastType type)
        {
            InitializeComponent();
            // Setup dữ liệu
            lblThongBao.Text = title;
            lblMassage.Text = message;
            switch (type)
            {
                case ToastType.Success:
                    pnlColor.BackColor = Color.LimeGreen;
                    break;
                case ToastType.Error:
                    pnlColor.BackColor = Color.OrangeRed;
                    lblThongBao.Text = "Thất Bại";
                    break;
                case ToastType.Info:
                    pnlColor.BackColor = Color.DodgerBlue;
                    break;
            }

            // ✅ THÊM NÚT CLOSE
            Button btnClose = new Button
            {
                Text = "✕",
                Size = new Size(25, 25),
                Location = new Point(this.Width - 30, 5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Red,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
            btnClose.BringToFront();
        }

        private void ToastForm_Load(object sender, EventArgs e)
        {
            // ✅ ĐẢM BẢO FORM ĐƯỢC KÍCH THƯỚC ĐÚNG
            this.Width = 350;
            this.Height = 75;

            // Tính toán vị trí góc dưới bên phải màn hình
            var screen = Screen.PrimaryScreen?.WorkingArea
                ?? throw new InvalidOperationException("No primary screen detected.");

            // ✅ TÍNH TOÁN CHÍNH XÁC
            _toastX = screen.Width - this.Width - 20;
            _toastY = screen.Height - this.Height - 20;

            // Đặt vị trí bắt đầu (Thấp hơn đích đến để trượt lên)
            this.Location = new Point(_toastX, _toastY + 50);
            this.Opacity = 0; // Bắt đầu ẩn

            // ✅ CHẠY ANIMATION VÀ TỰ TẮT SAU 3 GIÂY
            AnimateAndClose();
        }

        // ✅ TỐI ƯU: Bỏ Timer, dùng async/await thay thế
        private async void AnimateAndClose()
        {
            // GIAI ĐOẠN 1: Trượt lên & Hiện dần (0 - 500ms)
            for (int i = 0; i < 25; i++)
            {
                if (this.Top > _toastY)
                    this.Top -= 2;
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                
                await Task.Delay(20);
            }

            // GIAI ĐOẠN 2: Dừng lại để đọc (500ms - 3000ms)
            await Task.Delay(2500);

            // GIAI ĐOẠN 3: Mờ dần & Biến mất
            while (this.Opacity > 0)
            {
                this.Opacity -= 0.05;
                await Task.Delay(20);
            }

            this.Close();
        }

        public static void ShowSuccess(string message)
        {
            var toast = new ToastForm("Thành Công 🎉", message, ToastType.Success);
            toast.Show();
        }

        public static void ShowError(string message)
        {
            var toast = new ToastForm("Lỗi 😞", message, ToastType.Error);
            toast.Show();
        }
    }
}
