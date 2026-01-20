using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.User;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Components.Social;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls.Social
{
    public partial class UserProfilePage : UserControl
    {
        private readonly Guid _userId;
        private readonly IUserProfileService? _userProfileService;
        private readonly ITheoDoiNguoiDungService? _followService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;
        private readonly IBoDeHocService? _boDeHocService;
        private readonly IThachDauService? _thachDauService;

        private NguoiDungResponse? _userInfo;
        private bool _isFollowing = false;

        public UserProfilePage(Guid userId, IServiceProvider serviceProvider)
        {
            _userId = userId;

            // Resolve Services an toàn
            _userProfileService = serviceProvider.GetService<IUserProfileService>();
            _followService = serviceProvider.GetService<ITheoDoiNguoiDungService>();
            _postService = serviceProvider.GetService<IPostService>();
            _reactionService = serviceProvider.GetService<IReactionService>();
            _commentService = serviceProvider.GetService<ICommentService>();
            _boDeHocService = serviceProvider.GetService<IBoDeHocService>();
            _thachDauService = serviceProvider.GetService<IThachDauService>();

            InitializeComponent();
            SetupEventHandlers();
            LoadUserProfileAsync();
        }

        private void SetupEventHandlers()
        {
            btnBack.Click += BtnBack_Click;
            btnFollow.Click += BtnFollow_Click;
            lblFollowers.Click += (s, e) => MessageBox.Show("Tính năng xem danh sách followers đang phát triển.", "Thông báo");
            lblFollowing.Click += (s, e) => MessageBox.Show("Tính năng xem danh sách following đang phát triển.", "Thông báo");
        }

        private async void LoadUserProfileAsync()
        {
            if (_userProfileService == null || _followService == null) return;

            try
            {
                _userInfo = await _userProfileService.GetProfileAsync(_userId);
                if (_userInfo == null) return;

                // Bind UI cơ bản
                lblName.Text = _userInfo.HoVaTen ?? "Người dùng";
                lblEmail.Text = _userInfo.Email ?? "";
                lblBio.Text = _userInfo.TieuSu ?? "Chưa có tiểu sử";
                LoadAvatar(_userInfo.HinhDaiDien, _userInfo.HoVaTen ?? _userInfo.Email);

                // Kiểm tra follow
                if (UserSession.CurrentUser != null)
                {
                    if (_userId == UserSession.CurrentUser.MaNguoiDung)
                        btnFollow.Visible = false;
                    else
                    {
                        btnFollow.Visible = true;
                        var check = await _followService.KiemTraTheoDoiAsync(new KiemTraTheoDoiRequest
                        {
                            MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung,
                            MaNguoiDuocTheoDoi = _userId
                        });
                        _isFollowing = check.DangTheoDoi;
                        UpdateFollowButtonUI();
                    }
                }

                var stats = await _followService.LayThongKeTheoDoiAsync(_userId);
                lblFollowers.Text = $"{stats.SoNguoiTheoDoi} Followers";
                lblFollowing.Text = $"{stats.SoDangTheoDoi} Following";

                // Tải các tab song song
                await Task.WhenAll(
                    LoadUserPostsAsync(),
                    LoadUserQuizzesAsync(),
                    LoadChallengeHistoryAsync()
                );
            }
            catch (Exception ex) { Console.WriteLine($"[LoadProfile Error] {ex.Message}"); }
        }

        private async Task LoadUserPostsAsync()
        {
            if (_postService == null || flowPosts == null) return;
            flowPosts.Controls.Clear();
            var posts = await _postService.GetUserPostsAsync(_userId, 1);
            if (!posts.Any()) { flowPosts.Controls.Add(CreateEmptyLabel("📭 Chưa có bài viết nào")); return; }

            foreach (var post in posts)
            {
                if (_reactionService != null && _commentService != null)
                {
                    var pc = new PostCardControl(_postService, _reactionService, _commentService) { Width = 740 };
                    pc.LoadPost(post);
                    flowPosts.Controls.Add(pc);
                }
            }
        }

        private async Task LoadUserQuizzesAsync()
        {
            if (_boDeHocService == null || flowUserQuizzes == null) return;
            flowUserQuizzes.Controls.Clear();
            var quizzes = await _boDeHocService.GetByUserAsync(_userId);
            if (quizzes == null || !quizzes.Any()) { flowUserQuizzes.Controls.Add(CreateEmptyLabel("📚 Chưa tạo bộ đề nào")); return; }

            foreach (var q in quizzes)
            {
                var item = new BoDeItemControl();
                item.SetData(q.MaBoDe, q.TieuDe, q.SoLuongThe, q.SoLuotHoc, q.AnhBia ?? "");
                item.OnVaoThiClick += (s, ev) => NavigateToQuizDetail(q.MaBoDe);
                flowUserQuizzes.Controls.Add(item);
            }
        }

        private async Task LoadChallengeHistoryAsync()
        {
            if (_thachDauService == null || flowChallengeHistory == null) return;
            flowChallengeHistory.Controls.Clear();
            try
            {
                var history = await _thachDauService.GetLichSuByUserAsync(_userId);
                if (history == null || !history.Any()) { flowChallengeHistory.Controls.Add(CreateEmptyLabel("⚔️ Chưa có lịch sử thách đấu")); return; }

                foreach (var h in history)
                {
                    Panel p = new Panel { Width = 740, Height = 65, BackColor = Color.White, Margin = new Padding(0, 5, 0, 10), BorderStyle = BorderStyle.FixedSingle };
                    string statusText = h.LaNguoiThang ? "🏆 THẮNG" : "💀 THUA";
                    Label lbl = new Label { Text = $"[{statusText}] {h.TenBoDe} | ⭐ {h.Diem}đ | 🎯 {h.TyLeDung}%", AutoSize = true, Location = new Point(15, 10), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = h.LaNguoiThang ? Color.Goldenrod : Color.Gray };
                    Label lblT = new Label { Text = h.ThoiGianKetThuc.ToString("dd/MM/yyyy HH:mm"), AutoSize = true, Location = new Point(15, 35), ForeColor = Color.Gray };
                    p.Controls.AddRange(new Control[] { lbl, lblT });
                    flowChallengeHistory.Controls.Add(p);
                }
            }
            catch { }
        }

        private Label CreateEmptyLabel(string text) => new Label { Text = text, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.Gray, Margin = new Padding(20) };

        private void NavigateToQuizDetail(int maBoDe)
        {
            if (this.FindForm() is MainForm mf && Program.ServiceProvider != null)
            {
                var detail = Program.ServiceProvider.GetRequiredService<ChiTietBoDeControl>();
                detail.MaBoDe = maBoDe;
                mf.LoadPage(detail);
            }
        }

        private async void BtnFollow_Click(object? sender, EventArgs e)
        {
            if (_followService == null || UserSession.CurrentUser == null) return;
            btnFollow.Enabled = false;
            try
            {
                if (_isFollowing) await _followService.BoTheoDoiAsync(new BoTheoDoiNguoiDungRequest { MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung, MaNguoiDuocTheoDoi = _userId });
                else await _followService.TheoDoiAsync(new TheoDoiNguoiDungRequest { MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung, MaNguoiDuocTheoDoi = _userId });
                _isFollowing = !_isFollowing;
                UpdateFollowButtonUI();
                var s = await _followService.LayThongKeTheoDoiAsync(_userId);
                lblFollowers.Text = $"{s.SoNguoiTheoDoi} Followers";
            }
            finally { btnFollow.Enabled = true; }
        }

        private void UpdateFollowButtonUI()
        {
            btnFollow.Text = _isFollowing ? "✓ Đang theo dõi" : "➕ Theo dõi";
            btnFollow.BackColor = _isFollowing ? Color.Gray : Color.FromArgb(24, 119, 242);
        }

        private void LoadAvatar(string? path, string? name)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    using var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    pbAvatar.Image = Image.FromStream(fs);
                }
                else pbAvatar.Image = CreatePlaceholderAvatar(name);
            }
            catch { pbAvatar.Image = CreatePlaceholderAvatar(name); }
        }

        private Image CreatePlaceholderAvatar(string? name)
        {
            var b = new Bitmap(120, 120);
            using var g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var br = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, 120, 120), Color.FromArgb(24, 119, 242), Color.FromArgb(66, 153, 225), 45f);
            g.FillEllipse(br, 0, 0, 120, 120);
            string i = GetInitials(name);
            using var f = new Font("Segoe UI", 40, FontStyle.Bold);
            var sz = g.MeasureString(i, f);
            g.DrawString(i, f, Brushes.White, (120 - sz.Width) / 2, (120 - sz.Height) / 2);
            return b;
        }

        private string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var p = name.Trim().Split(' ');
            return p.Length == 1 ? p[0].Substring(0, Math.Min(2, p[0].Length)).ToUpper() : (p[0][0].ToString() + p[^1][0]).ToUpper();
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            if (this.FindForm() is MainForm mf)
            {
                var provider = Program.ServiceProvider;
                if (provider != null) mf.LoadPage(provider.GetRequiredService<NewsfeedControl>());
                else MessageBox.Show("Dịch vụ hệ thống chưa sẵn sàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}