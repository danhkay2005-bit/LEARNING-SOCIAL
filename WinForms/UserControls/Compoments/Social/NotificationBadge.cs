using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Social;

namespace WinForms.UserControls.Components.Social
{
    public partial class NotificationBadge : UserControl
    {
        private readonly INotificationService? _notificationService;
        private readonly IServiceProvider? _serviceProvider;

        private int _unreadCount = 0;
        private System.Windows.Forms.Timer? _refreshTimer;

        // Controls
        private Button? btnBell;
        private Label? lblBadge;
        private NotificationPanel? _notificationPanel;

        public NotificationBadge()
        {
            InitializeComponent();
        }

        public NotificationBadge(
            INotificationService notificationService,
            IServiceProvider serviceProvider) : this()
        {
            _notificationService = notificationService;
            _serviceProvider = serviceProvider;

            InitializeControls();
            StartAutoRefresh();
            _ = RefreshUnreadCountAsync();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            btnBell = new Button
            {
                Text = "🔔",
                Location = new Point(0, 0),
                Width = 50,
                Height = 50,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 18F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnBell.FlatAppearance.BorderSize = 0;
            btnBell.Click += BtnBell_Click;

            lblBadge = new Label
            {
                Text = "0",
                Location = new Point(30, 5),
                Width = 20,
                Height = 20,
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            lblBadge.Paint += (s, e) =>
            {
                // ✅ THÊM:  Null check
                if (lblBadge == null) return;

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.Red))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, lblBadge.Width - 1, lblBadge.Height - 1);
                }
                TextRenderer.DrawText(e.Graphics, lblBadge.Text, lblBadge.Font, lblBadge.ClientRectangle, lblBadge.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            this.Controls.Add(lblBadge);
            this.Controls.Add(btnBell);
            lblBadge.BringToFront();

            this.Size = new Size(50, 50);
            this.BackColor = Color.Transparent;

            this.ResumeLayout(false);
        }

        private void StartAutoRefresh()
        {
            _refreshTimer = new System.Windows.Forms.Timer
            {
                Interval = 30000
            };
            _refreshTimer.Tick += async (s, e) => await RefreshUnreadCountAsync();
            _refreshTimer.Start();
        }

        public async Task RefreshUnreadCountAsync()
        {
            if (_notificationService == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            try
            {
                _unreadCount = await _notificationService.GetUnreadCountAsync(UserSession.CurrentUser.MaNguoiDung);
                UpdateBadge();
            }
            catch
            {
                // Ignore errors
            }
        }

        private void UpdateBadge()
        {
            if (lblBadge == null) return;

            if (_unreadCount > 0)
            {
                lblBadge.Text = _unreadCount > 99 ? "99+" : _unreadCount.ToString();
                lblBadge.Visible = true;
            }
            else
            {
                lblBadge.Visible = false;
            }
        }

        private void BtnBell_Click(object? sender, EventArgs e)
        {
            if (_notificationService == null || _serviceProvider == null) return;

            if (_notificationPanel != null && _notificationPanel.Visible)
            {
                _notificationPanel.Visible = false;
                return;
            }

            // ✅ SỬA: Kiểm tra Parent != null TRƯỚC KHI truy cập . Width
            if (this.Parent == null) return;

            _notificationPanel = new NotificationPanel(_notificationService)
            {
                // ✅ SỬA: Bây giờ an toàn vì đã check null ở trên
                Location = new Point(this.Parent.Width - 420, this.Bottom + 10),
                Visible = true
            };
            _notificationPanel.OnNotificationRead += async () => await RefreshUnreadCountAsync();

            // ✅ SỬA: Bỏ ? .  vì đã check != null rồi
            this.Parent.Controls.Add(_notificationPanel);
            _notificationPanel.BringToFront();

            _ = _notificationPanel.LoadNotificationsAsync();
        }
    }
}