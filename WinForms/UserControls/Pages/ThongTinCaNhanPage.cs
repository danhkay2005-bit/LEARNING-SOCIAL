using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Components.Social;

namespace WinForms.UserControls
{
    /// <summary>
    /// 👤 THÔNG TIN CÁ NHÂN - Trang profile của chính mình
    /// 
    /// CHỨC NĂNG:
    /// 1. Hiển thị thông tin cá nhân đầy đủ
    /// 2. Chỉnh sửa thông tin (Avatar, Bio, v.v.)
    /// 3. Thống kê: Followers, Following, Số bài viết
    /// 4. Tab: Bài viết / Followers / Following
    /// </summary>
    public partial class ThongTinCaNhanPage : UserControl
    {
        private readonly IUserProfileService? _userProfileService;
        private readonly ITheoDoiNguoiDungService? _followService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;

        // Controls
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblName;
        private Label? lblEmail;
        private Label? lblBio;
        private Button? btnEdit;
        private Panel? pnlStats;
        private Label? lblPostCount;
        private Label? lblFollowers;
        private Label? lblFollowing;

        // Tabs
        private TabControl? tabControl;
        private TabPage? tabPosts;
        private TabPage? tabFollowers;
        private TabPage? tabFollowing;
        private FlowLayoutPanel? flowPosts;
        private FlowLayoutPanel? flowFollowers;
        private FlowLayoutPanel? flowFollowing;

        public ThongTinCaNhanPage()
        {
            InitializeComponent();

            // Lấy services từ Program.ServiceProvider
            if (Program.ServiceProvider != null)
            {
                _userProfileService = Program.ServiceProvider.GetService<IUserProfileService>();
                _followService = Program.ServiceProvider.GetService<ITheoDoiNguoiDungService>();
                _postService = Program.ServiceProvider.GetService<IPostService>();
                _reactionService = Program.ServiceProvider.GetService<IReactionService>();
                _commentService = Program.ServiceProvider.GetService<ICommentService>();
            }

            InitializeCustomControls();
            LoadProfileAsync();
        }

        private void InitializeCustomControls()
        {
            // ========== HEADER PANEL ==========
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 250,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            // Avatar
            pbAvatar = new PictureBox
            {
                Width = 150,
                Height = 150,
                Location = new Point(30, 30),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Tên
            lblName = new Label
            {
                Location = new Point(210, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(29, 29, 29)
            };

            // Email
            lblEmail = new Label
            {
                Location = new Point(210, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            // Bio
            lblBio = new Label
            {
                Location = new Point(210, 100),
                MaximumSize = new Size(600, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.FromArgb(65, 65, 65)
            };

            // Nút Chỉnh sửa
            btnEdit = new Button
            {
                Text = "✏️ Chỉnh sửa thông tin",
                Location = new Point(210, 150),
                Width = 180,
                Height = 40,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;

            // Stats Panel
            pnlStats = new Panel
            {
                Location = new Point(210, 200),
                Size = new Size(600, 40),
                BackColor = Color.Transparent
            };

            lblPostCount = CreateStatLabel("0 Bài viết", 0);
            lblFollowers = CreateStatLabel("0 Followers", 150);
            lblFollowing = CreateStatLabel("0 Following", 300);

            // ✅ SỬA: Kiểm tra null đầy đủ trước khi SelectTab
            lblFollowers.Click += (s, e) =>
            {
                if (tabControl != null && tabFollowers != null)
                    tabControl.SelectTab(tabFollowers);
            };
            lblFollowing.Click += (s, e) =>
            {
                if (tabControl != null && tabFollowing != null)
                    tabControl.SelectTab(tabFollowing);
            };

            pnlStats.Controls.Add(lblPostCount);
            pnlStats.Controls.Add(lblFollowers);
            pnlStats.Controls.Add(lblFollowing);

            pnlHeader.Controls.Add(pbAvatar);
            pnlHeader.Controls.Add(lblName);
            pnlHeader.Controls.Add(lblEmail);
            pnlHeader.Controls.Add(lblBio);
            pnlHeader.Controls.Add(btnEdit);
            pnlHeader.Controls.Add(pnlStats);

            // ========== TAB CONTROL ==========
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };

            // Tab Bài viết
            tabPosts = new TabPage("📝 Bài viết của tôi");
            flowPosts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };
            tabPosts.Controls.Add(flowPosts);

            // Tab Followers
            tabFollowers = new TabPage("👥 Người theo dõi");
            flowFollowers = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };
            tabFollowers.Controls.Add(flowFollowers);

            // Tab Following
            tabFollowing = new TabPage("➕ Đang theo dõi");
            flowFollowing = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };
            tabFollowing.Controls.Add(flowFollowing);

            tabControl.TabPages.Add(tabPosts);
            tabControl.TabPages.Add(tabFollowers);
            tabControl.TabPages.Add(tabFollowing);

            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlHeader);
        }

        /// <summary>
        /// 🏷️ Tạo label thống kê
        /// </summary>
        private Label CreateStatLabel(string text, int x)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242),
                Cursor = Cursors.Hand
            };
        }

        /// <summary>
        /// 🔄 Tải thông tin profile
        /// </summary>
        private async void LoadProfileAsync()
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Hiển thị thông tin cơ bản
                if (lblName != null) lblName.Text = UserSession.CurrentUser.HoVaTen ?? "Người dùng";
                if (lblEmail != null) lblEmail.Text = UserSession.CurrentUser.Email ?? "";
                if (lblBio != null) lblBio.Text = "🎓 Đang học tập trên StudyApp"; // TieuSu không có trong DTO

                // 2. Lấy thống kê
                if (_followService != null)
                {
                    var stats = await _followService.LayThongKeTheoDoiAsync(UserSession.CurrentUser.MaNguoiDung);
                    if (lblFollowers != null) lblFollowers.Text = $"{stats.SoNguoiTheoDoi} Followers";
                    if (lblFollowing != null) lblFollowing.Text = $"{stats.SoDangTheoDoi} Following";
                }

                // 3. Tải bài viết (tab đầu tiên)
                await LoadMyPostsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 📝 Tải bài viết của tôi
        /// </summary>
        private async Task LoadMyPostsAsync()
        {
            if (_postService == null || flowPosts == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowPosts.Controls.Clear();

                var posts = await _postService.GetUserPostsAsync(UserSession.CurrentUser.MaNguoiDung, 1);

                if (lblPostCount != null) lblPostCount.Text = $"{posts.Count} Bài viết";

                if (!posts.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "📭 Bạn chưa có bài viết nào.\nHãy tạo bài viết đầu tiên!",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10),
                        TextAlign = ContentAlignment.MiddleCenter
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
                            Width = 800,
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
        /// 👥 Tải danh sách Followers
        /// </summary>
        private async Task LoadFollowersAsync()
        {
            if (_followService == null || flowFollowers == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowFollowers.Controls.Clear();

                var followers = await _followService.LayDanhSachNguoiTheoDoiAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    UserSession.CurrentUser.MaNguoiDung
                );

                if (!followers.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "👤 Chưa có ai theo dõi bạn",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowFollowers.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var follower in followers)
                {
                    var card = CreateUserCard(follower.MaNguoiDung, follower.HoVaTen, follower.TenDangNhap, follower.DangTheoDoiLai);
                    flowFollowers.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải followers: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ➕ Tải danh sách Following
        /// </summary>
        private async Task LoadFollowingAsync()
        {
            if (_followService == null || flowFollowing == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowFollowing.Controls.Clear();

                var following = await _followService.LayDanhSachDangTheoDoiAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    UserSession.CurrentUser.MaNguoiDung
                );

                if (!following.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "❌ Bạn chưa theo dõi ai",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowFollowing.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var user in following)
                {
                    var card = CreateUserCard(user.MaNguoiDung, user.HoVaTen, user.TenDangNhap, true);
                    flowFollowing.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải following: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🃏 Tạo user card
        /// </summary>
        private Panel CreateUserCard(Guid userId, string? name, string? email, bool isFollowing)
        {
            var pnlUser = new Panel
            {
                Width = 800,
                Height = 80,
                BackColor = Color.White,
                Margin = new Padding(0, 5, 0, 5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pbAvatar = new PictureBox
            {
                Width = 60,
                Height = 60,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblName = new Label
            {
                Text = name ?? "Người dùng",
                Location = new Point(80, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            var lblEmail = new Label
            {
                Text = email ?? "",
                Location = new Point(80, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            var btnView = new Button
            {
                Text = "👁️ Xem trang",
                Location = new Point(680, 25),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => OpenUserProfile(userId);

            pnlUser.Controls.Add(pbAvatar);
            pnlUser.Controls.Add(lblName);
            pnlUser.Controls.Add(lblEmail);
            pnlUser.Controls.Add(btnView);

            return pnlUser;
        }

        /// <summary>
        /// 📄 Mở trang profile người dùng
        /// </summary>
        private void OpenUserProfile(Guid userId)
        {
            if (Program.ServiceProvider == null) return;

            try
            {
                var profilePage = new Social.UserProfilePage(userId, Program.ServiceProvider);

                var mainForm = this.FindForm();
                if (mainForm is Forms.MainForm mf)
                {
                    mf.LoadPage(profilePage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở trang profile: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🔄 Sự kiện đổi tab
        /// </summary>
        private async void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl == null) return;

            if (tabControl.SelectedTab == tabPosts)
            {
                await LoadMyPostsAsync();
            }
            else if (tabControl.SelectedTab == tabFollowers)
            {
                await LoadFollowersAsync();
            }
            else if (tabControl.SelectedTab == tabFollowing)
            {
                await LoadFollowingAsync();
            }
        }

        /// <summary>
        /// ✏️ Chỉnh sửa thông tin
        /// </summary>
        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_userProfileService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Mở dialog chỉnh sửa
                var editDialog = new Forms.Social.EditProfileDialog(_userProfileService);

                if (editDialog.ShowDialog() == DialogResult.OK)
                {
                    // Reload profile sau khi cập nhật
                    LoadProfileAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở dialog: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
