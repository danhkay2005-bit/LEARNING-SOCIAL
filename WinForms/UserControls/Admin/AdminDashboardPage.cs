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

        private List<dynamic> _allRecentActivities = new List<dynamic>();
        private int _currentLogPage = 0;
        private const int LOG_PAGE_SIZE = 10;

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
                // 1. LẤY DỮ LIỆU THỐNG KÊ (Dùng Task.WhenAll để nạp nhanh hơn)
                var usersTask = await _userService.GetTotalUsersCountAsync();
                var postsTask = await _postService.GetTotalPostsCountAsync();
                var setsTask = await _boDeHocService.GetPublicRandomAsync(100);
                var sessionsCountTask = await _boDeHocService.GetTotalPhienHocCountAsync();

                

                // 2. CẬP NHẬT CARD THỐNG KÊ
                pnlStatContainer.Controls.Clear();
                AddStatCard("NGƯỜI DÙNG", usersTask.ToString(), "👥", Color.FromArgb(0, 122, 204));
                AddStatCard("BÀI ĐĂNG MXH", postsTask.ToString(), "🌐", Color.FromArgb(204, 102, 0));
                AddStatCard("BỘ ĐỀ HỌC", setsTask.Count().ToString(), "📚", Color.FromArgb(46, 125, 50));
                AddStatCard("PHIÊN HỌC", sessionsCountTask.ToString(), "🔥", Color.FromArgb(183, 28, 28));

                // 3. NẠP NHẬT KÝ HỌC TẬP (KHÔNG TRỘN NỮA)
                // Lấy 50 phiên học gần nhất
                var recentSessions = await _boDeHocService.GetRecentSessionsAsync(50);

                // Đổ trực tiếp vào danh sách hoạt động với cấu trúc đồng nhất
                _allRecentActivities = recentSessions.Select(s => new {
                    ThoiGian = s.ThoiGian?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
                    HanhDong = "🔥 Học tập",
                    NoiDung = $"Học bộ đề: {s.TenBoDe}",
                    KetQua = $"{s.TyLeDung}% Đúng",
                    NguoiThucHien = s.TenNguoiDung
                }).Cast<dynamic>().ToList();

                // 4. HIỂN THỊ
                _currentLogPage = 0;
                DisplayCurrentLogPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp Dashboard: " + ex.Message);
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

        private void DisplayCurrentLogPage()
        {
            if (_allRecentActivities == null || !_allRecentActivities.Any())
            {
                dgvRecentLogs.DataSource = null;
                lblLogPageInfo.Text = "Không có hoạt động nào";
                return;
            }

            var pagedData = _allRecentActivities
                .Skip(_currentLogPage * LOG_PAGE_SIZE)
                .Take(LOG_PAGE_SIZE)
                .ToList();

            dgvRecentLogs.DataSource = pagedData;

            // Cấu hình tiêu đề cột cho chuyên nghiệp
            if (dgvRecentLogs.Columns != null && dgvRecentLogs.Columns.Count > 0)
            {
                var columns = dgvRecentLogs.Columns;
                if (columns["ThoiGian"] != null)
                    columns["ThoiGian"]!.HeaderText = "THỜI GIAN";
                if (columns["HanhDong"] != null)
                    columns["HanhDong"]!.HeaderText = "HÀNH ĐỘNG";
                if (columns["NoiDung"] != null)
                    columns["NoiDung"]!.HeaderText = "CHI TIẾT";
                if (columns["KetQua"] != null)
                    columns["KetQua"]!.HeaderText = "KẾT QUẢ";
                if (columns["NguoiThucHien"] != null)
                    columns["NguoiThucHien"]!.HeaderText = "NGƯỜI HỌC";
            }

            // Cập nhật trạng thái nút
            btnPrevLog.Enabled = _currentLogPage > 0;
            btnNextLog.Enabled = (_currentLogPage + 1) * LOG_PAGE_SIZE < _allRecentActivities.Count;

            lblLogPageInfo.Text = $"Trang {_currentLogPage + 1} / {Math.Max(1, (int)Math.Ceiling((double)_allRecentActivities.Count / LOG_PAGE_SIZE))}";
        }
        private void btnNextLog_Click(object sender, EventArgs e)
        {
            _currentLogPage++;
            DisplayCurrentLogPage();
        }

        private void btnPrevLog_Click(object sender, EventArgs e)
        {
            _currentLogPage--;
            DisplayCurrentLogPage();
        }
    }
}