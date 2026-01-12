using System.Windows.Forms;

namespace WinForms.UserControls.Pages
{
    partial class LoginPage
    {
        private TextBox txtUsername;
        private Button btnLogin;
        private Label lblTitle;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtUsername = new TextBox();
            btnLogin = new Button();

            lblTitle.Text = "🔑 Đăng nhập";
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.Width = 250;
            txtUsername.Top = 100;
            txtUsername.Left = 100;

            btnLogin.Text = "Đăng nhập";
            btnLogin.Width = 250;
            btnLogin.Top = 150;
            btnLogin.Left = 100;

            Controls.Add(btnLogin);
            Controls.Add(txtUsername);
            Controls.Add(lblTitle);
        }
    }
}
