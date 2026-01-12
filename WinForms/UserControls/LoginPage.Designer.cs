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
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1077, 60);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "🔑 Đăng nhập";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(100, 100);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.Size = new Size(250, 23);
            txtUsername.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(100, 150);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(250, 23);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Đăng nhập";
            // 
            // LoginPage
            // 
            Controls.Add(btnLogin);
            Controls.Add(txtUsername);
            Controls.Add(lblTitle);
            Name = "LoginPage";
            Size = new Size(1077, 647);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
