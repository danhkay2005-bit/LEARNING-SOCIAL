using StudyApp.BLL.Services.Interfaces.User;
using StudyApp.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms.UserControls.MainControl;
using WinForms.UserControls.Pages;

namespace WinForms.Forms
{
    public partial class mainForm : Form
    {
        private readonly INguoiDungService _nguoiDungService;
        public mainForm(INguoiDungService nguoiDungService)
        {
            InitializeComponent();
            _nguoiDungService = nguoiDungService;
            InitLeftMenu();
        }

        private void InitLeftMenu()
        {
            pnlLeft.Controls.Clear();

            var leftMenu = new LeftMenuControl
            {
                Dock = DockStyle.Fill
            };

            leftMenu.OnNavigate += HandleNavigate;
            leftMenu.OnLoginClick += OpenLoginForm;
            leftMenu.OnRegisterClick += OpenRegisterForm;
            leftMenu.OnLogoutClick += Logout;

            pnlLeft.Controls.Add(leftMenu);
        }

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
                _ => new FeedPageControl()
            };

            midControl.Controls.Add(page);
        }

        private void OpenLoginForm()
        {
            using var frm = new frmDangNhap(_nguoiDungService);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Replace frm.LoggedInUser with UserSession.CurrentUser
                UserSession.Login(UserSession.CurrentUser!);
                InitLeftMenu();
                HandleNavigate("feed");
            }
        }

        private void OpenRegisterForm()
        {
            MessageBox.Show("Form đăng ký (chưa làm)");
        }

        private void Logout()
        {
            UserSession.Logout();
            InitLeftMenu();
            midControl.Controls.Clear();
        }
    }
}
