using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms.UserControls;
using WinForms.UserControls.Components.Social;
using WinForms.UserControls.Pages;
using WinForms.UserControls.Social;

namespace WinForms.Forms
{
    public partial class MainForm : Form
    {
        // ✅ THÊM:  Biến lưu NotificationBadge
        private NotificationBadge? _notificationBadge;

        public MainForm()
        {
            InitializeComponent();
        }

        // ================= LOAD =================
        private void MainForm_Load(object sender, EventArgs e)
        {
            RenderMenu();
            if (UserSession.IsLoggedIn)
            {
                ShowSuggestedUsers();
                InitializeNotificationBadge(); // ✅ THÊM: Khởi tạo chuông thông báo
            }
        }

        // ================= MENU =================
        private void RenderMenu()
        {
            menuPanel.Controls.Clear();

            if (!UserSession.IsLoggedIn)
            {
                AddMenuButton("🔑 Đăng nhập", BtnDangNhap_Click);
            }
            else
            {
                AddMenuButton("🏠 Trang chủ", (s, e) => 
                {
                    if (Program.ServiceProvider == null) return;
                    LoadPage(Program.ServiceProvider.GetRequiredService<TrangChuPage>());
                });
                AddMenuButton("👤 Thông tin cá nhân", (s, e) => LoadPage(new ThongTinCaNhanPage()));
                AddMenuButton("📚 Học tập", (s, e) => LoadPage(new HocTapPage()));

                // ✅ THÊM: Nút Mạng xã hội
                AddMenuButton("🌐 Mạng xã hội", BtnMangXaHoi_Click);

                AddMenuButton("🛒 Cửa hàng", (s, e) => LoadPage(new CuaHangPage()));
                AddMenuButton("⚙️ Cài đặt", (s, e) => LoadPage(new CaiDatPage()));
                AddMenuButton("🚪 Đăng xuất", BtnDangXuat_Click);
            }
        }

        // ================= MENU BUTTON =================
        private void AddMenuButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Width = splitContainer1.Panel1.Width - 10,
                Height = 42,
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 48),
                TextAlign = ContentAlignment.MiddleLeft
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.Click += onClick;

            menuPanel.Controls.Add(btn);
        }

        // ================= PAGE LOAD =================
        public void LoadPage(UserControl page)
        {
            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();

            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);

            contentPanel.ResumeLayout(true);
            page.PerformLayout();
        }

        private void ShowSuggestedUsers()
        {
            splitContainer2.Panel2.Controls.Clear();

            var suggested = new SuggestedUsersControl();
            suggested.Dock = DockStyle.Fill;

            splitContainer2.Panel2.Controls.Add(suggested);
        }

        // ✅ THÊM:  Khởi tạo NotificationBadge (Icon chuông)
        private void InitializeNotificationBadge()
        {
            if (Program.ServiceProvider == null) return;

            try
            {
                var notificationService = Program.ServiceProvider.GetRequiredService<INotificationService>();

                _notificationBadge = new NotificationBadge(
                    notificationService,
                    Program.ServiceProvider
                )
                {
                    Location = new Point(this.Width - 80, 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                this.Controls.Add(_notificationBadge);
                _notificationBadge.BringToFront();
            }
            catch
            {
                // Nếu chưa đăng ký service thì bỏ qua
            }
        }

        // ✅ THÊM:  Sự kiện click nút Mạng xã hội
        private void BtnMangXaHoi_Click(object? sender, EventArgs e)
        {
            if (Program.ServiceProvider == null) return;

            try
            {
                var postService = Program.ServiceProvider.GetRequiredService<IPostService>();
                var reactionService = Program.ServiceProvider.GetRequiredService<IReactionService>();
                var commentService = Program.ServiceProvider.GetRequiredService<ICommentService>();

                var newsfeedControl = new NewsfeedControl(
                    postService,
                    reactionService,
                    commentService,
                    Program.ServiceProvider
                );

                LoadPage(newsfeedControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể tải trang mạng xã hội.\n\nChi tiết:  {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= EVENTS =================
        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            if (Program.ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not initialized.");
            var loginPage = Program.ServiceProvider.GetRequiredService<DangNhapControl>();

            loginPage.DangNhapThanhCong += () =>
            {
                RenderMenu();
                ShowSuggestedUsers();
                InitializeNotificationBadge(); // ✅ THÊM: Hiển thị chuông sau khi đăng nhập

                LoadPage(Program.ServiceProvider.GetRequiredService<TrangChuPage>());
            };

            loginPage.YeuCauDangKy += () =>
            {
                var dangKyPage = Program.ServiceProvider.GetRequiredService<DangKyControl>();
                LoadPage(dangKyPage);
                dangKyPage.QuayVeDangNhap += () =>
                {
                    LoadPage(loginPage);
                };
            };

            loginPage.QuenMatKhau += () =>
            {
                var quenMKPage = Program.ServiceProvider.GetRequiredService<QuenMatKhauControl>();
                LoadPage(quenMKPage);

                quenMKPage.QuayVeDangNhap += () =>
                {
                    LoadPage(loginPage);
                };
            };

            LoadPage(loginPage);
        }

        private void BtnDangXuat_Click(object? sender, EventArgs e)
        {
            UserSession.Logout();
            contentPanel.Controls.Clear();

            // ✅ THÊM: Ẩn chuông thông báo khi đăng xuất
            if (_notificationBadge != null)
            {
                this.Controls.Remove(_notificationBadge);
                _notificationBadge.Dispose();
                _notificationBadge = null;
            }

            RenderMenu();
        }
    }
}