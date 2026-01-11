using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.BLL.Services.Social;
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
        private readonly IBaiDangService _baiDangService;

        public mainForm(INguoiDungService nguoiDungService, IBaiDangService baiDangService)
        {
            InitializeComponent();
            _baiDangService = baiDangService;
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
                "dangnhap" => CreateDangNhapControl(),
                "dangky" => CreateDangKyControl(),
                "quenmatkhau" => CreateQuenMatKhauControl(),

                "feed" => new FeedPageControl(_baiDangService),
                "study" => new StudyPageControl(),
                "library" => new LibraryPageControl(),
                "profile" => new ProfilePageControl(),
                "settings" => new SettingPageControl(),

                _ => new FeedPageControl(_baiDangService)
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
                _leftMenu?.RefreshMenu();
                HandleNavigate("feed");
            };

            login.RequestRegister += () =>
            {
                HandleNavigate("dangky");
            };

            // ⭐ QUÊN MẬT KHẨU
            login.RequestForgotPassword += () =>
            {
                HandleNavigate("quenmatkhau");
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

        private QuenMatKhauControl CreateQuenMatKhauControl()
        {
            var forgot = new QuenMatKhauControl(_nguoiDungService);

            forgot.RequestBackToLogin += () =>
            {
                HandleNavigate("dangnhap");
            };

            return forgot;
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
