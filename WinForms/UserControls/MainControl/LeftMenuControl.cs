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

        private Button? _activeButton;

        public LeftMenuControl()
        {
            InitializeComponent();
            BuildMenu();
        }

        public void RefreshMenu()
        {
            pnlMenu.Controls.Clear();
            BuildMenu();
        }

        private void BuildMenu()
        {
            if (!UserSession.IsLoggedIn)
            {
                pnlMenu.Controls.Add(CreateNavButton("🔑  Đăng nhập", "dangnhap"));
                return;
            }

            pnlMenu.Controls.Add(CreateNavButton("📰  Bảng tin", "feed"));
            pnlMenu.Controls.Add(CreateNavButton("📚  Học hôm nay", "study"));
            pnlMenu.Controls.Add(CreateNavButton("📂  Thư viện", "library"));
            pnlMenu.Controls.Add(CreateNavButton("👤  Trang cá nhân", "profile"));

            pnlMenu.Controls.Add(CreateSeparator());

            pnlMenu.Controls.Add(CreateNavButton("⚙️  Cài đặt", "settings"));
            pnlMenu.Controls.Add(CreateActionButton("🚪  Đăng xuất", () => OnLogoutClick?.Invoke()));
        }

        // ================= COMPONENTS =================

        private Button CreateNavButton(string text, string key)
        {
            var btn = CreateBaseButton(text);
            btn.Click += (s, e) =>
            {
                SetActive(btn);
                OnNavigate?.Invoke(key);
            };
            return btn;
        }

        private Button CreateActionButton(string text, Action action)
        {
            var btn = CreateBaseButton(text);
            btn.Click += (s, e) => action();
            return btn;
        }

        private Control CreateSeparator()
        {
            return new Panel
            {
                Height = 1,
                Width = pnlMenu.Width - 20,
                BackColor = Color.LightGray,
                Margin = new Padding(0, 10, 0, 10)
            };
        }

        private Button CreateBaseButton(string text)
        {
            var btn = new Button();
            InitButton(btn, text);
            return btn;
        }

        private void InitButton(Button btn, string text)
        {
            btn.Text = text;
            btn.Width = 200;
            btn.Height = 42;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Font = new Font("Segoe UI", 10);
            btn.BackColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(0, 4, 0, 4);

            btn.MouseEnter += (s, e) =>
            {
                if (btn != _activeButton)
                    btn.BackColor = Color.Gainsboro;
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != _activeButton)
                    btn.BackColor = Color.White;
            };
        }

        private void SetActive(Button btn)
        {
            if (_activeButton != null)
                _activeButton.BackColor = Color.White;

            _activeButton = btn;
            btn.BackColor = Color.LightBlue;
        }
    }
}
