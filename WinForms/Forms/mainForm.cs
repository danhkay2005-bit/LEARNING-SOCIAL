using Microsoft.Extensions.DependencyInjection;
using StudyApp.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms.UserControls;
using WinForms.UserControls.Pages;

namespace WinForms.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // ================= LOAD =================
        private void MainForm_Load(object sender, EventArgs e)
        {
            RenderMenu();
            if(UserSession.IsLoggedIn)
                ShowSuggestedUsers();  
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
                AddMenuButton("🏠 Trang chủ", (s, e) => LoadPage(new TrangChuPage()));
                AddMenuButton("👤 Thông tin cá nhân", (s, e) => LoadPage(new ThongTinCaNhanPage()));
                AddMenuButton("📚 Học tập", (s, e) => LoadPage(new HocTapPage()));
                AddMenuButton("🛒 Cửa hàng", (s, e) => LoadPage(new CuaHangPage()));
                AddMenuButton("⚙️ Cài đặt", (s, e) => LoadPage(new CaiDatPage()));
                AddMenuButton("🏅 Thành Tựu", (s, e) => LoadPage(Program.ServiceProvider!.GetRequiredService <AchievementsPage>()));
                AddMenuButton("📋 Nhiệm Vụ", (s, e) => LoadPage(Program.ServiceProvider!.GetRequiredService<TaskPage>()));
                                                                        
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
            contentPanel.SuspendLayout(); // Tạm dừng vẽ để tránh giật hình
            contentPanel.Controls.Clear();

            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);

            contentPanel.ResumeLayout(true);
            page.PerformLayout(); // Ép trang con tính toán lại vị trí các nút
        }
        
        private void ShowSuggestedUsers()
        {
            splitContainer2.Panel2.Controls.Clear();

            var suggested = new SuggestedUsersControl();
            suggested.Dock = DockStyle.Fill;

            splitContainer2.Panel2.Controls.Add(suggested);
        }

        // ================= EVENTS =================
        private void BtnDangNhap_Click(object? sender, EventArgs e)
        {
            if (Program.ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not initialized.");
            var loginPage = Program.ServiceProvider.GetRequiredService<DangNhapControl>();

            // Xóa tham số 'user' ở đây vì Action không có tham số
            loginPage.DangNhapThanhCong += () =>
            {
                // Không cần dòng UserSession.Login(user) nữa vì Control đã làm rồi
                RenderMenu();
                ShowSuggestedUsers();

                LoadPage(Program.ServiceProvider.GetRequiredService<TrangChuPage>());
            };

            loginPage.YeuCauDangKy += () =>
            {
                var dangKyPage = Program.ServiceProvider.GetRequiredService<DangKyControl>();
                // Bạn có thể đăng ký sự kiện quay lại từ trang đăng ký tại đây nếu cần
                LoadPage(dangKyPage);
                dangKyPage.QuayVeDangNhap += () =>
                {
                    LoadPage(loginPage);
                };
            };

            // 3. Xử lý khi nhấn nút Quên mật khẩu
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
            RenderMenu();
        }
    }
}
