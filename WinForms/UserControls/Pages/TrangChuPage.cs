using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using WinForms.UserControls.Tasks;
using StudyApp.DTO;
using StudyApp.DTO.Responses.User;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms;

namespace WinForms.UserControls.Pages
{
    public partial class TrangChuPage : UserControl
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ISocialService _socialService;
       
        private NguoiDungGamificationResponse? _userStats;

        private Panel? _rootPanel;

        public TrangChuPage(IUserProfileService userProfileService, ISocialService socialService)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            _userProfileService = userProfileService;
            _socialService = socialService;
           
        }

        private async void TrangChuPage_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
                return;
           await LoadUserStats();
            RenderDashboard();

            this.VisibleChanged +=async (s, ev) =>
            {
                if (this.Visible)
                {
                   await LoadUserStats();
                    RenderDashboard();
                }
            };
        }

        // ================= LOAD USER STATS =================
        private async Task LoadUserStats()
        {
            try
            {
                var currentUserId = UserSession.CurrentUser;
                // Tạo response từ CurrentUser (đã có đầy đủ properties sau khi cập nhật)
                _userStats = new NguoiDungGamificationResponse
                {
                    TongDiemXp = UserSession.CurrentUser?.TongDiemXp ?? 0,
                    Vang = UserSession.CurrentUser?.Vang ?? 0,
                    KimCuong = UserSession.CurrentUser?.KimCuong ?? 0,
                    ChuoiNgayHocLienTiep = UserSession.CurrentUser?.ChuoiNgayHocLienTiep ?? 0,
                    TongSoTheHoc = UserSession.CurrentUser?.TongSoTheHoc ?? 0,
                    TongSoTheDung = UserSession.CurrentUser?.TongSoTheDung ?? 0
                };

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        // ================= RENDER =================
        private void RenderDashboard()
        {
            if (_rootPanel != null)
                Controls.Remove(_rootPanel);

            _rootPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 240, 242)
            };

            var header = CreateHeaderSection();
            var stats = CreateStatsSection();
            var actions = CreateQuickActionsSection();
            var activity = CreateRecentActivitySection();

            header.Dock = DockStyle.Top;
            stats.Dock = DockStyle.Top;
            actions.Dock = DockStyle.Top;
            activity.Dock = DockStyle.Top;

            activity.Height = 260;
            actions.Height = 120;
            stats.Height = 150;
            header.Height = 120;

            _rootPanel.Controls.Add(activity);
            _rootPanel.Controls.Add(actions);
            _rootPanel.Controls.Add(stats);
            _rootPanel.Controls.Add(header);

            Controls.Add(_rootPanel);
            _rootPanel.BringToFront();
        }

        // ================= HEADER =================
        private Panel CreateHeaderSection()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20) };

            var lblWelcome = new Label
            {
                Text = $"👋 Chào mừng, {UserSession.CurrentUser?.HoVaTen ?? "Bạn"}!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Top = 10
            };

            var lblDate = new Label
            {
                Text = $"Hôm nay: {DateTime.Now:dddd, dd/MM/yyyy}",
                Top = 50,
                ForeColor = Color.Gray,
                AutoSize = true
            };

            var lblQuote = new Label
            {
                Text = "🎯 Hãy tiếp tục học hỏi mỗi ngày!",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 180, 255),
                Top = 75,
                AutoSize = true
            };

            panel.Controls.Add(lblWelcome);
            panel.Controls.Add(lblDate);
            panel.Controls.Add(lblQuote);
            return panel;
        }

        // ================= STATS =================
        private Panel CreateStatsSection()
        {
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 5, 15, 5) };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            if (_userStats != null)
            {
                flow.Controls.Add(CreateStatCard("⭐ XP", _userStats.TongDiemXp.ToString(), Color.FromArgb(255, 193, 7)));
                flow.Controls.Add(CreateStatCard("🪙 Vàng", _userStats.Vang.ToString(), Color.FromArgb(255, 152, 0)));
                flow.Controls.Add(CreateStatCard("💎 Kim Cương", _userStats.KimCuong.ToString(), Color.FromArgb(3, 169, 244)));
                flow.Controls.Add(CreateStatCard("🔥 Chuỗi Học", $"{_userStats.ChuoiNgayHocLienTiep} ngày", Color.FromArgb(244, 67, 54)));
            }

            container.Controls.Add(flow);
            return container;
        }

        private Panel CreateStatCard(string title, string value, Color accentColor)
        {
            var panel = new Panel
            {
                Width = 130,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            panel.Controls.Add(new Label
            {
                Text = title,
                Left = 10,
                Top = 10,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true
            });

            panel.Controls.Add(new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = accentColor,
                Left = 10,
                Top = 40,
                AutoSize = true
            });

            var progress = new ProgressBar
            {
                Width = 110,
                Height = 5,
                Left = 10,
                Top = 90,
                Value = 65,
                Style = ProgressBarStyle.Continuous
            };
            panel.Controls.Add(progress);

            return panel;
        }

        // ================= QUICK ACTIONS =================
        private Panel CreateQuickActionsSection()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20) };

            var lblTitle = new Label
            {
                Text = "⚡ Hành Động Nhanh",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Top = 5,
                AutoSize = true
            };

            var btnHoc = CreateActionButton("📚 Học Ngay", 10, 40);
            var btnShop = CreateActionButton("🛒 Cửa Hàng", 140, 40);
            var btnCompete = CreateActionButton("🎮 Thách Đấu", 270, 40);

            btnHoc.Click += (s, e) => Navigate<HocTapPage>();
            btnShop.Click += (s, e) => Navigate<CuaHangPage>();
            btnCompete.Click += (s, e) => MessageBox.Show("🎮 Chức năng sắp ra mắt!");

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(btnHoc);
            panel.Controls.Add(btnShop);
            panel.Controls.Add(btnCompete);

            return panel;
        }

        private Button CreateActionButton(string text, int left, int top)
        {
            var btn = new Button
            {
                Text = text,
                Width = 120,
                Height = 45,
                Left = left,
                Top = top,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ================= RECENT =================
        private Panel CreateRecentActivitySection()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20) };

            var lblTitle = new Label
            {
                Text = "📊 Hoạt Động Gần Đây",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Top = 5,
                AutoSize = true
            };

            var list = new ListBox
            {
                Left = 0,
                Top = 40,
                Width = panel.Width - 40,
                Height = 180,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 248, 248)
            };

            list.Items.AddRange(new object[]
            {
                "✅ Hoàn thành bộ đề 'Tiếng Anh Cơ Bản' - 95% chính xác",
                "🏆 Nhận thành tựu 'Học viên Siêng Năng'",
                "⭐ Tăng 150 XP từ học tập hôm nay",
                "🎁 Nhận 50 vàng từ điểm danh",
                "👥 Bạn bè mới theo dõi bạn"
            });

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(list);
            return panel;
        }

        // ================= NAVIGATION =================
        private void Navigate<T>() where T : UserControl
        {
            var main = FindForm() as WinForms.Forms.MainForm;
            if (main == null) return;

            var page = Program.ServiceProvider?.GetRequiredService<T>();
            if (page != null)
                main.LoadPage(page);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            AppEvents.UserStatsChanged -= OnUserStatsChanged;
            AppEvents.UserStatsChanged += OnUserStatsChanged;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            AppEvents.UserStatsChanged -= OnUserStatsChanged;
            base.OnHandleDestroyed(e);
        }

        private void OnUserStatsChanged()
        {
            if (!IsHandleCreated) return;

            BeginInvoke(async () =>
            {
                if (!Visible) return; // chỉ refresh khi TrangChuPage đang mở
                await LoadUserStats();

                RenderDashboard();
            });
        }
    }
}
