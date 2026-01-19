using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.User;
using WinForms.UserControls.Tasks;

namespace WinForms.UserControls.Pages
{
    public partial class DiemDanhPage : UserControl
    {
        // Service điểm danh
        private readonly IDailyStreakService _dailyStreakService;

        // Các control giao diện
        private Label? lblTitle;
        private Panel? panelStats;
        private Label? lblStreakLabel;
        private Label? lblStreakCount;
        private FlowLayoutPanel? flowDaysContainer;
        private Button? btnDiemDanh;
        private Label? lblMessage;

        // Hàm khởi tạo, truyền service vào
        public DiemDanhPage(IDailyStreakService dailyStreakService)
        {
            _dailyStreakService = dailyStreakService;
            KhoiTaoGiaoDien();
            this.Load += DiemDanhPage_Load;
        }

        // Khởi tạo giao diện bằng code
        private void KhoiTaoGiaoDien()
        {
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(30, 30, 30);

            lblTitle = new Label
            {
                Text = "🎁 ĐIỂM DANH NHẬN QUÀ",
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.Yellow
            };

            panelStats = new Panel
            {
                Size = new Size(300, 150),
                Location = new Point((this.Width - 300) / 2, 100),
                BackColor = Color.FromArgb(50, 50, 55)
            };
            lblStreakLabel = new Label
            {
                Text = "Chuỗi ngày học liên tiếp",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.LightGray
            };
            lblStreakCount = new Label
            {
                Text = "0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 48F, FontStyle.Bold),
                ForeColor = Color.Cyan
            };
            panelStats.Controls.Add(lblStreakCount);
            panelStats.Controls.Add(lblStreakLabel);

            flowDaysContainer = new FlowLayoutPanel
            {
                Size = new Size(700, 120),
                Location = new Point((this.Width - 700) / 2, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Padding = new Padding(35, 0, 0, 0)
            };

            btnDiemDanh = new Button
            {
                Text = "NHẬN THƯỞNG NGAY",
                Size = new Size(280, 60),
                Location = new Point((this.Width - 280) / 2, 450),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 128, 0),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDiemDanh.FlatAppearance.BorderSize = 0;
            btnDiemDanh.Click += btnDiemDanh_Click;

            lblMessage = new Label
            {
                Text = "",
                AutoSize = false,
                Size = new Size(800, 30),
                Location = new Point(0, 520),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Yellow,
                Visible = false
            };

            this.Controls.Add(lblMessage);
            this.Controls.Add(btnDiemDanh);
            this.Controls.Add(flowDaysContainer);
            this.Controls.Add(panelStats);
            this.Controls.Add(lblTitle);
        }

        // Sự kiện load trang
        private void DiemDanhPage_Load(object? sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null) return;

            lblStreakCount?.Text = UserSession.CurrentUser.ChuoiNgayHocLienTiep.ToString();
            VeLichTuan();
            CapNhatTrangThaiNut();
        }

        // Xử lý khi bấm nút nhận thưởng
        private async void btnDiemDanh_Click(object? sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null) return;

            try
            {
                btnDiemDanh?.Enabled = false;
                btnDiemDanh?.Text = "Đang mở quà...";

                var request = new DiemDanhHangNgayRequest
                {
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung
                };

                var response = await _dailyStreakService.CheckInDailyAsync(request);

                // Cập nhật session
                UserSession.CurrentUser.Vang += response.ThuongVang ?? 0;
                UserSession.CurrentUser.TongDiemXp += response.ThuongXp ?? 0;

                // Gọi sự kiện để Trang Chủ cập nhật
              //  AppEvents.RaiseUserStatsChanged();
              // Thêm thưởng cuối tuần
                int thuTrongTuan = (int)DateTime.Now.DayOfWeek;
                string msg = $"🎁 NHẬN QUÀ THÀNH CÔNG!\n\n" +
                             $"🪙 Vàng: +{response.ThuongVang}\n" +
                             $"⭐ XP: +{response.ThuongXp}";

                if(thuTrongTuan == 0)
                {
                    UserSession.CurrentUser.KimCuong += 10;
                    msg += $"\n💎 Kim Cương: +10 (Thưởng cuối tuần)";

                }    

                AppEvents.RaiseUserStatsChanged();
                MessageBox.Show(msg, "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                VoHieuHoaNut("ĐÃ NHẬN THƯỞNG");
                VeLichTuan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");

                if (ex.Message.Contains("đã nhận"))
                    VoHieuHoaNut("ĐÃ NHẬN THƯỞNG");
                else
                    KichHoatNut();
            }
        }

        // Vẽ lịch tuần điểm danh
        private void VeLichTuan()
        {
            flowDaysContainer?.Controls.Clear();
            bool daNhanHomNay = btnDiemDanh != null && btnDiemDanh.Enabled;

            for (int i = 13; i >= 0; i--)
            {
                bool laHomNay = (i == 0);
                Panel p = new Panel { Size = new Size(80, 100), Margin = new Padding(5) };
                Panel circle = new Panel { Size = new Size(50, 50), Location = new Point(15, 10) };

                if (laHomNay) circle.BackColor = daNhanHomNay ? Color.Orange : Color.DimGray;
                else circle.BackColor = Color.SeaGreen;

                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0, 0, 50, 50);
                circle.Region = new Region(gp);

                Label lbl = new Label
                {
                    Text = laHomNay ? "Hôm nay" : DateTime.Now.AddDays(-i).ToString("dd/MM"),
                    Dock = DockStyle.Bottom,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                p.Controls.Add(circle);
                p.Controls.Add(lbl);
                flowDaysContainer?.Controls.Add(p);
            }
        }

        // Vô hiệu hóa nút sau khi nhận thưởng
        private void VoHieuHoaNut(string msg)
        {
            btnDiemDanh?.Enabled = false;
            btnDiemDanh?.BackColor = Color.Gray;
            btnDiemDanh?.Text = msg;
            lblMessage?.Text = msg;
            lblMessage?.Visible = true;
        }

        // Kích hoạt lại nút nếu chưa nhận
        private void KichHoatNut()
        {
            btnDiemDanh?.Enabled = true;
            btnDiemDanh?.BackColor = Color.FromArgb(255, 128, 0);
            btnDiemDanh?.Text = "NHẬN THƯỞNG NGAY";
            lblMessage?.Visible = false;
        }

        // Kiểm tra trạng thái nút khi load trang
        private void CapNhatTrangThaiNut()
        {
            // Kiểm tra xem hôm nay đã nhận chưa (có thể kiểm tra qua service hoặc session)
            // Ở đây đơn giản: nếu đã nhận thì vô hiệu hóa, chưa nhận thì kích hoạt
            // Nếu cần kiểm tra chính xác, nên gọi service kiểm tra trạng thái điểm danh hôm nay
            KichHoatNut();
        }
    }
}
