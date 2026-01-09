using System;
using System.Drawing;
using System.Windows.Forms;
using StudyApp.DTO;

namespace WinForms.UserControls.MainControl
{
    public partial class LeftMenuControl : UserControl
    {
        public event Action<string>? OnNavigate;
        public event Action? OnLogoutClick;

        public LeftMenuControl()
        {
            InitializeComponent();
            BuildMenu();
        }

        // ⭐ QUAN TRỌNG: cho phép rebuild menu
        public void RefreshMenu()
        {
            BuildMenu();
        }

        private void BuildMenu()
        {
            Controls.Clear();
            BackColor = Color.WhiteSmoke;
            Padding = new Padding(10);

            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            layout.Controls.Add(CreateTitle("LEARNING SOCIAL"));

            if (!UserSession.IsLoggedIn)
            {
                layout.Controls.Add(CreateNavButton("🔑  Đăng nhập", "dangnhap"));
                layout.Controls.Add(CreateNavButton("📝  Đăng ký", "dangky"));
            }
            else
            {
                layout.Controls.Add(CreateNavButton("📰  Bảng tin", "feed"));
                layout.Controls.Add(CreateNavButton("📚  Học hôm nay", "study"));
                layout.Controls.Add(CreateNavButton("📂  Thư viện", "library"));
                layout.Controls.Add(CreateNavButton("👤  Trang cá nhân", "profile"));

                layout.Controls.Add(new Label { Height = 20 });

                layout.Controls.Add(CreateNavButton("⚙️  Cài đặt", "settings"));
                layout.Controls.Add(CreateActionButton("🚪  Đăng xuất", () => OnLogoutClick?.Invoke()));
            }

            Controls.Add(layout);
        }

        private Label CreateTitle(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(5, 10, 5, 20)
            };
        }

        private Button CreateNavButton(string text, string key)
        {
            var btn = BaseButton(text);
            btn.Click += (s, e) => OnNavigate?.Invoke(key);
            return btn;
        }

        private Button CreateActionButton(string text, Action action)
        {
            var btn = BaseButton(text);
            btn.Click += (s, e) => action();
            return btn;
        }

        private Button BaseButton(string text)
        {
            return new Button
            {
                Text = text,
                Width = 210,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White,
                TabStop = false,
                FlatAppearance = { BorderSize = 0 }
            };
        }
    }
}
