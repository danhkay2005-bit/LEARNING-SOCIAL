using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DTO;
using System;
using System.Windows.Forms;
using WinForms.UserControls.Author;
using WinForms.UserControls.MainControl;
using WinForms.UserControls.Pages;

namespace WinForms.Forms
{
    public partial class mainForm : Form
    {
        private readonly INguoiDungService _nguoiDungService;
        private LeftMenuControl? _leftMenu;

        public mainForm(INguoiDungService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;

            InitLeftMenu();

            // ⭐ QUAN TRỌNG: vào đúng màn hình ban đầu
            if (!UserSession.IsLoggedIn)
                HandleNavigate("dangnhap");
            else
                HandleNavigate("feed");
        }

        // ================= LEFT MENU =================
        private void InitLeftMenu()
        {
            pnlLeft.Controls.Clear();

            _leftMenu = new LeftMenuControl
            {
                Dock = DockStyle.Fill
            };

            _leftMenu.OnNavigate += HandleNavigate;
            _leftMenu.OnLogoutClick += Logout;

            pnlLeft.Controls.Add(_leftMenu);
        }

        // ================= NAVIGATION =================
        private void HandleNavigate(string key)
        {
            midControl.Controls.Clear();

            UserControl page = key switch
            {
                "feed" => new FeedPageControl(),
                "study" => new StudyPageControl(),
                "library" => new LibraryPageControl(),
                "profile" => new ProfilePageControl(),
                "settings" => new SettingPageControl(),

                "dangnhap" => CreateDangNhapControl(),
                "dangky" => CreateDangKyControl(),

                _ => new FeedPageControl()
            };

            page.Dock = DockStyle.Fill;
            midControl.Controls.Add(page);
        }

        // ================= LOGIN =================
        private DangNhapControl CreateDangNhapControl()
        {
            var login = new DangNhapControl(_nguoiDungService);

            login.LoginSuccess += () =>
            {
                _leftMenu?.RefreshMenu();   // ⭐ refresh menu
                HandleNavigate("feed");
            };

            login.RequestRegister += () =>
            {
                HandleNavigate("dangky");
            };

            return login;
        }

        // ================= REGISTER =================
        private DangKyControl CreateDangKyControl()
        {
            var register = new DangKyControl(_nguoiDungService);

            register.RequestBackToLogin += () =>
            {
                HandleNavigate("dangnhap");
            };

            return register;
        }

        // ================= LOGOUT =================
        private void Logout()
        {
            UserSession.Logout();
            _leftMenu?.RefreshMenu();       // ⭐ refresh menu
            HandleNavigate("dangnhap");
        }
    }
}
