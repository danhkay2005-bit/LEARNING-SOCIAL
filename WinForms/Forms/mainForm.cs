using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms.UserControls;
using WinForms.UserControls.Admin;
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
        }

        // ================= MENU =================
        private void RenderMenu()
        {
            menuPanel.Controls.Clear();

            // Kiểm tra xem đã đăng nhập chưa và CurrentUser có khác null không
            if (!UserSession.IsLoggedIn || UserSession.CurrentUser == null)
            {
                AddMenuButton("🔑 Đăng nhập", BtnDangNhap_Click);
                return;
            }

            if (UserSession.CurrentUser?.MaVaiTro == (int)VaiTroEnum.Admin)
            {
                RenderAdminMenu();
            }
            else
            {

                RenderUserMenu();
            }

            // 2. Nút chung
            AddMenuButton("🚪 Đăng xuất", BtnDangXuat_Click);
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
        public async void LoadPage(UserControl page)
        {
            if (contentPanel.Controls.Count > 0)
            {
                var oldPage = contentPanel.Controls[0];
                if (oldPage is ICleanupControl cleanupPage)
                {
                    await cleanupPage.CleanupAsync();
                }
                oldPage.Dispose();
            }

            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();

            // THIẾT LẬP QUAN TRỌNG:
            page.Dock = DockStyle.Fill;   // Lấp đầy contentPanel
            page.AutoSize = false;        // Tắt tự động điều chỉnh kích thước theo nội dung

            contentPanel.Controls.Add(page);

            // Đảm bảo contentPanel cũng đang Fill trong cha của nó
            contentPanel.Dock = DockStyle.Fill;

            contentPanel.ResumeLayout(true);
            page.PerformLayout();
        }

        // ================= EVENTS =================
        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            if (Program.ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not initialized.");

            var loginPage = Program.ServiceProvider.GetRequiredService<DangNhapControl>();

            loginPage.DangNhapThanhCong += () =>
            {
                // 1. Cập nhật lại thanh Menu bên trái để khớp với quyền hạn
                RenderMenu();

                // 2. Kiểm tra vai trò để điều hướng trang đích mặc định
                // Sử dụng toán tử ?. để an toàn và ép kiểu Enum về int để so sánh
                if (UserSession.CurrentUser?.MaVaiTro == (int)VaiTroEnum.Admin)
                {
                    // Nếu là Admin -> Chuyển thẳng vào Dashboard quản trị
                    var adminDashboard = Program.ServiceProvider.GetRequiredService<AdminDashboardPage>();
                    LoadPage(adminDashboard);
                }
                else
                {
                    // Nếu là User/Sinh viên -> Chuyển vào Trang chủ ứng dụng
                    var homePage = Program.ServiceProvider.GetRequiredService<TrangChuPage>();
                    LoadPage(homePage);
                }
            };

            // --- Các sự kiện chuyển trang phụ giữ nguyên ---
            loginPage.YeuCauDangKy += () =>
            {
                var dangKyPage = Program.ServiceProvider.GetRequiredService<DangKyControl>();
                LoadPage(dangKyPage);
                dangKyPage.QuayVeDangNhap += () => LoadPage(loginPage);
            };

            loginPage.QuenMatKhau += () =>
            {
                var quenMKPage = Program.ServiceProvider.GetRequiredService<QuenMatKhauControl>();
                LoadPage(quenMKPage);
                quenMKPage.QuayVeDangNhap += () => LoadPage(loginPage);
            };

            LoadPage(loginPage);
        }

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
                    $"Không thể tải trang mạng xã hội.\n\nChi tiết: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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

        private void RenderUserMenu()
        {
            AddMenuButton("🏠 Trang chủ", (s, e) =>
            {
                if (Program.ServiceProvider == null) return;
                LoadPage(Program.ServiceProvider.GetRequiredService<TrangChuPage>());
            });

            AddMenuButton("👤 Thông tin cá nhân", (s, e) => LoadPage(new ThongTinCaNhanPage()));

            AddMenuButton("📚 Học tập", (s, e) =>
            {
                if (Program.ServiceProvider == null) return;
                LoadPage(Program.ServiceProvider.GetRequiredService<HocTapPage>());
            });

            AddMenuButton("🌐 Mạng xã hội", BtnMangXaHoi_Click);
            AddMenuButton("🗓 Điểm Danh", (s, e) => LoadPage(Program.ServiceProvider!.GetRequiredService<DiemDanhPage>()));

            AddMenuButton("🛒 Cửa hàng", (s, e) => LoadPage(Program.ServiceProvider!.GetRequiredService<CuaHangPage>()));
            AddMenuButton("📦 Kho vật phẩm", (s, e) => LoadPage(Program.ServiceProvider!.GetRequiredService<KhoVatPhamPage>()));
            AddMenuButton("🏅 Thành Tựu", (s, e) =>
            {
                if (Program.ServiceProvider == null) return;
                LoadPage(Program.ServiceProvider.GetRequiredService<AchievementsPage>());
            });
            AddMenuButton("📋 Nhiệm Vụ", (s, e) =>
            {
                if (Program.ServiceProvider == null) return;
                LoadPage(Program.ServiceProvider.GetRequiredService<TaskPage>());
            });
        }

        private void RenderAdminMenu()
        {
            if (Program.ServiceProvider == null) return;
            AddMenuButton("📊 Dashboard Thống kê", (s, e) => {
                LoadPage(Program.ServiceProvider.GetRequiredService<AdminDashboardPage>());
            });

            AddMenuButton("👥 Quản lý Người dùng", (s, e) => {
                 LoadPage(Program.ServiceProvider.GetRequiredService<QuanLyNguoiDungAdminPage>());
            });

            AddMenuButton("📚 Quản lý Bộ đề", (s, e) => {
                LoadPage(Program.ServiceProvider.GetRequiredService<QuanLyBoDeAdminPage>());
            });

            AddMenuButton("📝 Quản lý Bài đăng", (s, e) => {
                 LoadPage(Program.ServiceProvider.GetRequiredService<QuanLyBaiDangAdminPage>());
            });

            AddMenuButton("🏪 Quản lý Cửa hàng", (s, e) => {
                 LoadPage(Program.ServiceProvider.GetRequiredService<QuanLyCuaHangAdminPage>());
            });

        }

        // Add this method to your MainForm class

        private void RenderNotificationBadge()
        {
            // Xóa badge cũ nếu có
            if (_notificationBadge != null)
            {
                this.Controls.Remove(_notificationBadge);
                _notificationBadge.Dispose();
                _notificationBadge = null;
            }

            // ✅ SỬA: Lấy INotificationService từ ServiceProvider và truyền vào constructor đúng
            if (Program.ServiceProvider == null) return;
            
            var notificationService = Program.ServiceProvider.GetRequiredService<INotificationService>();
            
            // Tạo NotificationBadge với constructor đầy đủ tham số
            _notificationBadge = new NotificationBadge(notificationService, Program.ServiceProvider);
            
            // ✅ QUAN TRỌNG: Set kích thước nhỏ thay vì dùng mặc định 253x160
            _notificationBadge.Size = new Size(50, 50);
            _notificationBadge.Location = new Point(this.Width - 60, 10); // Góc trên bên phải
            _notificationBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            
            this.Controls.Add(_notificationBadge);
            _notificationBadge.BringToFront();
        }
    }
}