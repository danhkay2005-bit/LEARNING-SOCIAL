using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class AdminDashboardPage : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IPostService _postService;
        private readonly IUserProfileService _userService;

        public AdminDashboardPage(IBoDeHocService boDeHocService, IPostService postService, IUserProfileService userService)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _postService = postService;
            _userService = userService;

            this.Load += AdminDashboardPage_Load;
        }

        private async void AdminDashboardPage_Load(object? sender, EventArgs e)
        {
            await LoadDashboardData();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                // 1. Lấy dữ liệu thống kê (Giả lập hoặc từ Service nếu đã có hàm Count)
                // Lưu ý: Nếu Service chưa có hàm Count, bạn có thể lấy List rồi .Count() 
                var users = await _userService.TimKiemNguoiDungAsync(""); // Lấy tất cả
                var posts = await _postService.GetNewsfeedAsync(Guid.Empty, 1, 100);
                var sets = await _boDeHocService.GetPublicRandomAsync(100);

                // 2. Xóa các thẻ cũ nếu có và thêm thẻ mới
                pnlStatContainer.Controls.Clear();
                AddStatCard("NGƯỜI DÙNG", users.Count.ToString(), "👥", Color.FromArgb(0, 122, 204));
                AddStatCard("BỘ ĐỀ HỌC", sets.Count().ToString(), "📚", Color.FromArgb(46, 125, 50));
                AddStatCard("BÀI ĐĂNG", posts.Count.ToString(), "🌐", Color.FromArgb(204, 102, 0));
                AddStatCard("PHIÊN HỌC", "Chua them", "🔥", Color.FromArgb(183, 28, 28));

                // 3. Nạp dữ liệu vào Grid (Hoạt động gần đây)
                var logs = posts.Select(p => new {
                    ThoiGian = p.ThoiGianTao.HasValue ? p.ThoiGianTao.Value.ToString("dd/MM/yyyy HH:mm") : "",
                    HanhDong = "Bài đăng mới",
                    NoiDung = !string.IsNullOrEmpty(p.NoiDung)
        ? (p.NoiDung.Length > 50 ? p.NoiDung.Substring(0, 50) + "..." : p.NoiDung)
        : "",
                    NguoiThucHien = p.TenNguoiDung
                }).ToList();

                dgvRecentLogs.DataSource = logs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu Dashboard: " + ex.Message);
            }
        }

        private void AddStatCard(string title, string value, string icon, Color accentColor)
        {
            Panel card = new Panel
            {
                Size = new Size(220, 120),
                BackColor = Color.FromArgb(35, 55, 60),
                Margin = new Padding(10)
            };

            Label lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 25),
                ForeColor = accentColor,
                Location = new Point(10, 15),
                AutoSize = true
            };

            Label lblVal = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 55),
                AutoSize = true
            };

            Label lblTit = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(15, 95),
                AutoSize = true
            };

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblTit);
            pnlStatContainer.Controls.Add(card);
        }
    }
}