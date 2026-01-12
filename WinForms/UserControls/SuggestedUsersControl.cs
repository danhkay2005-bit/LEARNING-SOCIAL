using StudyApp.DTO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms.UserControls
{
    public partial class SuggestedUsersControl : UserControl
    {
        public SuggestedUsersControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            LoadSuggestedUsers();
        }

        private void LoadSuggestedUsers()
        {
            // DEMO 5 user bất kỳ
            var users = new List<NguoiDungDTO>
            {
                new() { TenDangNhap = "user1", HoVaTen = "Nguyễn Văn A" },
                new() { TenDangNhap = "user2", HoVaTen = "Trần Thị B" },
                new() { TenDangNhap = "user3", HoVaTen = "Lê Văn C" },
                new() { TenDangNhap = "user4", HoVaTen = "Phạm Thị D" },
                new() { TenDangNhap = "user5", HoVaTen = "Hoàng Văn E" }
            };

            flpUsers.Controls.Clear();

            foreach (var user in users)
            {
                var item = CreateUserItem(user);
                flpUsers.Controls.Add(item);
            }
        }

        private Control CreateUserItem(NguoiDungDTO user)
        {
            var panel = new Panel
            {
                Height = 45,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblName = new Label
            {
                Text = $"{user.HoVaTen} (@{user.TenDangNhap})",
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Padding = new Padding(8, 0, 0, 0),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            var btnFollow = new Button
            {
                Text = "Theo dõi",
                Width = 80,
                Dock = DockStyle.Right
            };

            bool isFollowing = false;
            btnFollow.Click += (s, e) =>
            {
                isFollowing = !isFollowing;
                btnFollow.Text = isFollowing ? "Bỏ theo dõi" : "Theo dõi";
            };

            panel.Controls.Add(btnFollow);
            panel.Controls.Add(lblName);

            return panel;
        }

    }
}
