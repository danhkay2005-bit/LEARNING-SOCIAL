using Microsoft.Extensions.DependencyInjection;
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
using WinForms.UserControls.Components.Social;
using WinForms.Forms;
using WinForms.Helpers;  // ✅ THÊM để dùng AvatarHelper

namespace WinForms.UserControls.Social
{
    /// <summary>
    /// 📄 USERPROFILEPAGE - Trang thông tin người dùng
    /// 
    /// CHỨC NĂNG:
    /// 1. Hiển thị thông tin cá nhân (Avatar, Tên, Email, Bio)
    /// 2. Nút Follow/Unfollow
    /// 3. Thống kê: Số followers, following
    /// 4. Danh sách bài viết của người dùng
    /// </summary>
    public partial class UserProfilePage : UserControl
    {
        private readonly Guid _userId; // ID người dùng đang xem
        private readonly IUserProfileService? _userProfileService;
        private readonly ITheoDoiNguoiDungService? _followService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;

        private NguoiDungResponse? _userInfo;
        private bool _isFollowing = false;

        // Controls
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblName;
        private Label? lblEmail;
        private Label? lblBio;
        private Button? btnFollow;
        private Button? btnBack; // ✅ THÊM
        private Label? lblFollowers;
        private Label? lblFollowing;
        private FlowLayoutPanel? flowPosts;

        public UserProfilePage(
            Guid userId,
            IServiceProvider serviceProvider)
        {
            _userId = userId;

            // Lấy services
            _userProfileService = serviceProvider?.GetService(typeof(IUserProfileService)) as IUserProfileService;
            _followService = serviceProvider?.GetService(typeof(ITheoDoiNguoiDungService)) as ITheoDoiNguoiDungService;
            _postService = serviceProvider?.GetService(typeof(IPostService)) as IPostService;
            _reactionService = serviceProvider?.GetService(typeof(IReactionService)) as IReactionService;
            _commentService = serviceProvider?.GetService(typeof(ICommentService)) as ICommentService;

            InitializeComponent();
            InitializeControls();
            LoadUserProfileAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "UserProfilePage";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.ResumeLayout(false);
        }

        private void InitializeControls()
        {
            // ========== HEADER PANEL ==========
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Avatar
            pbAvatar = new PictureBox
            {
                Width = 120,
                Height = 120,
                Location = new Point(20, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Tên
            lblName = new Label
            {
                Location = new Point(160, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(29, 29, 29)
            };

            // Email
            lblEmail = new Label
            {
                Location = new Point(160, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            // Bio
            lblBio = new Label
            {
                Location = new Point(160, 85),
                MaximumSize = new Size(500, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(65, 65, 65)
            };

            // ✅ Nút Back (Quay lại)
            btnBack = new Button
            {
                Text = "← Quay lại",
                Location = new Point(20, 160),
                Width = 100,
                Height = 30,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            // Nút Follow/Unfollow
            btnFollow = new Button
            {
                Text = "➕ Theo dõi",
                Location = new Point(160, 120),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnFollow.FlatAppearance.BorderSize = 0;
            btnFollow.Click += BtnFollow_Click;

            // Thống kê Followers
            lblFollowers = new Label
            {
                Text = "0 Followers",
                Location = new Point(300, 125),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242),
                Cursor = Cursors.Hand
            };
            lblFollowers.Click += (s, e) => MessageBox.Show("Chức năng xem danh sách followers", "Thông báo");

            // Thống kê Following
            lblFollowing = new Label
            {
                Text = "0 Following",
                Location = new Point(420, 125),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242),
                Cursor = Cursors.Hand
            };
            lblFollowing.Click += (s, e) => MessageBox.Show("Chức năng xem danh sách following", "Thông báo");

            pnlHeader.Controls.Add(pbAvatar);
            pnlHeader.Controls.Add(lblName);
            pnlHeader.Controls.Add(lblEmail);
            pnlHeader.Controls.Add(lblBio);
            pnlHeader.Controls.Add(btnBack);    // ✅ Nút quay lại
            pnlHeader.Controls.Add(btnFollow);  // ✅ Nút theo dõi
            pnlHeader.Controls.Add(lblFollowers);
            pnlHeader.Controls.Add(lblFollowing);

            // ========== FLOW POSTS ==========
            flowPosts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };

            this.Controls.Add(flowPosts);
            this.Controls.Add(pnlHeader);
        }

        /// <summary>
        /// 🔄 Tải thông tin người dùng
        /// </summary>
        private async void LoadUserProfileAsync()
        {
            if (_userProfileService == null || _followService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Lấy thông tin người dùng
                _userInfo = await _userProfileService.GetProfileAsync(_userId);
                if (_userInfo == null)
                {
                    MessageBox.Show("Không tìm thấy người dùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Hiển thị thông tin
                if (lblName != null) lblName.Text = _userInfo.HoVaTen ?? "Người dùng";
                if (lblEmail != null) lblEmail.Text = _userInfo.Email ?? "";
                if (lblBio != null) lblBio.Text = _userInfo.TieuSu ?? "Chưa có tiểu sử";

                // ✅ FIX: Hiển thị avatar với AvatarHelper
                if (pbAvatar != null)
                {
                    var initials = !string.IsNullOrEmpty(_userInfo.HoVaTen) 
                        ? _userInfo.HoVaTen 
                        : _userInfo.Email?.Substring(0, 1).ToUpper() ?? "?";
                    AvatarHelper.SetAvatar(pbAvatar, _userInfo.HinhDaiDien, initials);
                }

                // ✅ FIX: Kiểm tra nếu đang xem profile chính mình
                if (UserSession.CurrentUser != null && btnFollow != null)
                {
                    if (_userId == UserSession.CurrentUser.MaNguoiDung)
                    {
                        // Đang xem profile chính mình → Ẩn nút follow
                        btnFollow.Visible = false;
                    }
                    else
                    {
                        // Đang xem profile người khác → Hiện nút follow
                        btnFollow.Visible = true;

                        // 3. Kiểm tra trạng thái follow
                        var checkRequest = new KiemTraTheoDoiRequest
                        {
                            MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung,
                            MaNguoiDuocTheoDoi = _userId
                        };
                        var checkResult = await _followService.KiemTraTheoDoiAsync(checkRequest);
                        _isFollowing = checkResult.DangTheoDoi;

                        UpdateFollowButtonUI();
                    }
                }

                // 4. Lấy thống kê
                var stats = await _followService.LayThongKeTheoDoiAsync(_userId);
                if (lblFollowers != null) lblFollowers.Text = $"{stats.SoNguoiTheoDoi} Followers";
                if (lblFollowing != null) lblFollowing.Text = $"{stats.SoDangTheoDoi} Following";

                // 5. Tải bài viết của người dùng
                await LoadUserPostsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🔄 Tải bài viết của người dùng
        /// </summary>
        private async Task LoadUserPostsAsync()
        {
            if (_postService == null || flowPosts == null) return;

            try
            {
                flowPosts.Controls.Clear();

                var posts = await _postService.GetUserPostsAsync(_userId, 1);

                if (!posts.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "📭 Chưa có bài viết nào",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowPosts.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var post in posts)
                {
                    if (_reactionService != null && _commentService != null)
                    {
                        var postCard = new PostCardControl(
                            _postService,
                            _reactionService,
                            _commentService
                        )
                        {
                            Width = 700,
                            Margin = new Padding(0, 10, 0, 10)
                        };

                        postCard.LoadPost(post);
                        flowPosts.Controls.Add(postCard);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🔘 Xử lý Follow/Unfollow
        /// </summary>
        private async void BtnFollow_Click(object? sender, EventArgs e)
        {
            if (_followService == null || UserSession.CurrentUser == null || btnFollow == null)
                return;

            try
            {
                btnFollow.Enabled = false;
                btnFollow.Text = "⏳ Đang xử lý...";

                if (_isFollowing)
                {
                    // Unfollow
                    var request = new BoTheoDoiNguoiDungRequest
                    {
                        MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung,
                        MaNguoiDuocTheoDoi = _userId
                    };
                    await _followService.BoTheoDoiAsync(request);
                    _isFollowing = false;
                }
                else
                {
                    // Follow
                    var request = new TheoDoiNguoiDungRequest
                    {
                        MaNguoiTheoDoi = UserSession.CurrentUser.MaNguoiDung,
                        MaNguoiDuocTheoDoi = _userId
                    };
                    await _followService.TheoDoiAsync(request);
                    _isFollowing = true;
                }

                UpdateFollowButtonUI();

                // Reload stats
                var stats = await _followService.LayThongKeTheoDoiAsync(_userId);
                if (lblFollowers != null) lblFollowers.Text = $"{stats.SoNguoiTheoDoi} Followers";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btnFollow != null)
                {
                    btnFollow.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 🎨 Cập nhật UI nút Follow
        /// </summary>
        private void UpdateFollowButtonUI()
        {
            if (btnFollow == null) return;

            if (_isFollowing)
            {
                btnFollow.Text = "✓ Đang theo dõi";
                btnFollow.BackColor = Color.FromArgb(108, 117, 125);
            }
            else
            {
                btnFollow.Text = "➕ Theo dõi";
                btnFollow.BackColor = Color.FromArgb(24, 119, 242);
            }
        }

        /// <summary>
        /// ← Quay lại trang trước
        /// </summary>
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            // Tìm MainForm parent
            var mainForm = this.FindForm();
            if (mainForm is MainForm mf)
            {
                // Load lại Newsfeed
                try
                {
                    // ✅ FIX: Check null trước khi gọi GetRequiredService
                    if (Program.ServiceProvider == null)
                    {
                        MessageBox.Show("Service chưa được khởi tạo", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newsfeedControl = Program.ServiceProvider.GetRequiredService<NewsfeedControl>();
                    mf.LoadPage(newsfeedControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể quay lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
