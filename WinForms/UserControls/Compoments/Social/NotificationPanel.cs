using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.UserControls.Social
{
    public partial class NotificationPanel : UserControl
    {
        private readonly INotificationService? _notificationService;

        private List<ThongBaoResponse> _notifications = new List<ThongBaoResponse>();
        private bool _showUnreadOnly = false;

        // Event khi có notification được đọc
        public event Action? OnNotificationRead;

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Button? btnClose;

        private Panel? pnlTabs;
        private Button? btnTabAll;
        private Button? btnTabUnread;
        private Button? btnMarkAllRead;

        private FlowLayoutPanel? flowNotifications;

        // Constructor mặc định
        public NotificationPanel()
        {
            InitializeComponent();
        }

        // Constructor với DI
        public NotificationPanel(INotificationService notificationService) : this()
        {
            _notificationService = notificationService;
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            lblTitle = new Label
            {
                Text = "Thông báo",
                Location = new Point(15, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            btnClose = new Button
            {
                Text = "✖",
                Location = new Point(360, 10),
                Width = 30,
                Height = 30,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);

            // ===== TABS =====
            pnlTabs = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(245, 246, 247),
                Padding = new Padding(10)
            };

            btnTabAll = new Button
            {
                Text = "Tất cả",
                Location = new Point(10, 10),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnTabAll.FlatAppearance.BorderSize = 0;
            btnTabAll.Click += BtnTabAll_Click;

            btnTabUnread = new Button
            {
                Text = "Chưa đọc",
                Location = new Point(120, 10),
                Width = 100,
                Height = 30,
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnTabUnread.FlatAppearance.BorderSize = 0;
            btnTabUnread.Click += BtnTabUnread_Click;

            btnMarkAllRead = new Button
            {
                Text = "✓ Đánh dấu đã đ���c",
                Location = new Point(240, 10),
                Width = 150,
                Height = 30,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnMarkAllRead.FlatAppearance.BorderColor = Color.LightGray;
            btnMarkAllRead.Click += BtnMarkAllRead_Click;

            pnlTabs.Controls.Add(btnTabAll);
            pnlTabs.Controls.Add(btnTabUnread);
            pnlTabs.Controls.Add(btnMarkAllRead);

            // ===== NOTIFICATIONS AREA =====
            flowNotifications = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(0)
            };

            // ===== ADD TO CONTROL =====
            this.Controls.Add(flowNotifications);
            this.Controls.Add(pnlTabs);
            this.Controls.Add(pnlHeader);

            this.Size = new Size(400, 500);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Load notifications
        /// </summary>
        public async Task LoadNotificationsAsync()
        {
            if (_notificationService == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            try
            {
                var request = new DanhSachThongBaoRequest
                {
                    MaNguoiNhan = UserSession.CurrentUser.MaNguoiDung,
                    ChiChuaDoc = _showUnreadOnly,
                    Trang = 1,
                    KichThuocTrang = 20
                };

                _notifications = await _notificationService.GetUserNotificationsAsync(request);

                RenderNotifications();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông báo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderNotifications()
        {
            flowNotifications?.Controls.Clear();

            if (_notifications == null || !_notifications.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "Không có thông báo nào.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 20, 10, 10)
                };
                flowNotifications?.Controls.Add(lblEmpty);
                return;
            }

            foreach (var notification in _notifications)
            {
                var notifCard = CreateNotificationCard(notification);
                flowNotifications?.Controls.Add(notifCard);
            }
        }

        private Panel CreateNotificationCard(ThongBaoResponse notification)
        {
            var pnlNotif = new Panel
            {
                Width = 380,
                Height = 80,
                BackColor = notification.DaDoc ? Color.White : Color.FromArgb(230, 240, 255),
                Cursor = Cursors.Hand,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 1)
            };
            pnlNotif.Click += async (s, e) => await MarkAsReadAsync(notification.MaThongBao);

            var lblIcon = new Label
            {
                Text = GetNotificationIcon(notification.LoaiThongBao),
                Location = new Point(10, 15),
                Width = 30,
                Height = 30,
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblContent = new Label
            {
                Text = notification.NoiDung ?? "Thông báo mới",
                Location = new Point(50, 10),
                Width = 300,
                Height = 40,
                Font = new Font("Segoe UI", 9F, notification.DaDoc ? FontStyle.Regular : FontStyle.Bold),
                AutoEllipsis = true
            };

            var lblTime = new Label
            {
                Text = GetRelativeTime(notification.ThoiGian),
                Location = new Point(50, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            pnlNotif.Controls.Add(lblIcon);
            pnlNotif.Controls.Add(lblContent);
            pnlNotif.Controls.Add(lblTime);

            return pnlNotif;
        }

        private string GetNotificationIcon(StudyApp.DTO.Enums.LoaiThongBaoEnum loaiThongBao)
        {
            return loaiThongBao switch
            {
                StudyApp.DTO.Enums.LoaiThongBaoEnum.ThichBaiDang => "👍",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.ThichBinhLuan => "❤️",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.BinhLuanBaiDang => "💬",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.ChiaSeBaiDang => "↗️",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.TheoDoi => "👥",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.MentionBaiDang => "📢",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.MentionBinhLuan => "💭",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.TraLoiBinhLuan => "💬",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.DenHanOnTap => "📚",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.HoanThanhBoDe => "✅",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.MoiThachDau => "⚔️",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.KetQuaThachDau => "🏆",
                StudyApp.DTO.Enums.LoaiThongBaoEnum.HeThong => "⚙️",
                _ => "🔔"
            };
        }

        private string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1) return "Vừa xong";
            if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} phút trước";
            if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} giờ trước";
            if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} ngày trước";

            return dateTime.ToString("dd/MM/yyyy");
        }

        // ===== SỰ KIỆN =====

        private async Task MarkAsReadAsync(int notificationId)
        {
            if (_notificationService == null) return;

            try
            {
                await _notificationService.MarkAsReadAsync(notificationId);
                await LoadNotificationsAsync();
                OnNotificationRead?.Invoke();
            }
            catch { }
        }

        private async void BtnMarkAllRead_Click(object? sender, EventArgs e)
        {
            if (_notificationService == null || UserSession.CurrentUser == null) return;

            try
            {
                await _notificationService.MarkAllAsReadAsync(UserSession.CurrentUser.MaNguoiDung);
                await LoadNotificationsAsync();
                OnNotificationRead?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnTabAll_Click(object? sender, EventArgs e)
        {
            _showUnreadOnly = false;
            UpdateTabStyles();
            await LoadNotificationsAsync();
        }

        private async void BtnTabUnread_Click(object? sender, EventArgs e)
        {
            _showUnreadOnly = true;
            UpdateTabStyles();
            await LoadNotificationsAsync();
        }

        private void UpdateTabStyles()
        {
            if (btnTabAll == null || btnTabUnread == null) return;

            if (_showUnreadOnly)
            {
                btnTabAll.BackColor = Color.White;
                btnTabAll.ForeColor = Color.Black;
                btnTabAll.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

                btnTabUnread.BackColor = Color.FromArgb(24, 119, 242);
                btnTabUnread.ForeColor = Color.White;
                btnTabUnread.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            else
            {
                btnTabAll.BackColor = Color.FromArgb(24, 119, 242);
                btnTabAll.ForeColor = Color.White;
                btnTabAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                btnTabUnread.BackColor = Color.White;
                btnTabUnread.ForeColor = Color.Black;
                btnTabUnread.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void NotificationPanel_Load(object sender, EventArgs e)
        {

        }
    }
}