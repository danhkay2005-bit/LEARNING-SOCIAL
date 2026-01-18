using WinForms.Forms.Social;
using WinForms.Forms; // ✅ THÊM
using StudyApp.BLL.Interfaces.User; // ✅ THÊM
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Responses.User; // ✅ THÊM
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Components.Social;

namespace WinForms.UserControls.Social
{
    public partial class NewsfeedControl : UserControl
    {
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;
        private readonly IServiceProvider? _serviceProvider;

        // ✅ SỬA: Dùng IUserProfileService thay vì INguoiDungService
        private readonly IUserProfileService? _userProfileService;

        private int _currentPage = 1;
        private const int PAGE_SIZE = 10;
        private bool _isLoading = false;

        // Controls
        private Panel? pnlToolbar;
        private Button? btnCreatePost;
        private Button? btnRefresh;
        private FlowLayoutPanel? flowPosts;
        private Button? btnLoadMore;
        private Label? lblLoading;

        // Controls cho search bar
        private Panel? pnlSearchBar;
        private TextBox? txtSearch;

        public NewsfeedControl()
        {
            InitializeComponent();
        }

        public NewsfeedControl(
            IPostService postService,
            IReactionService reactionService,
            ICommentService commentService,
            IServiceProvider serviceProvider) : this()
        {
            _postService = postService;
            _reactionService = reactionService;
            _commentService = commentService;
            _serviceProvider = serviceProvider;

            // ✅ SỬA:  Lấy IUserProfileService
            try
            {
                _userProfileService = _serviceProvider?.GetService(typeof(IUserProfileService)) as IUserProfileService;
                
                // ✅ DEBUG: Kiểm tra inject thành công
                if (_userProfileService != null)
                {
                    System.Diagnostics.Debug.WriteLine("✅ IUserProfileService đã được inject thành công!");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("⚠️ IUserProfileService = NULL! Không tìm thấy service.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi khi lấy IUserProfileService: {ex.Message}");
            }

            InitializeControls();
            LoadNewsfeedAsync();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            // SEARCH BAR
            InitializeSearchBar();

            // TOOLBAR (Giữ nguyên)
            pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(245, 246, 247),
                Padding = new Padding(10)
            };

            btnCreatePost = new Button
            {
                Text = "➕ Tạo bài viết",
                Location = new Point(10, 15),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCreatePost.FlatAppearance.BorderSize = 0;
            btnCreatePost.Click += BtnCreatePost_Click;

            btnRefresh = new Button
            {
                Text = "🔄",
                Location = new Point(170, 15),
                Width = 40,
                Height = 35,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderColor = Color.LightGray;
            btnRefresh.Click += BtnRefresh_Click;

            pnlToolbar.Controls.Add(btnCreatePost);
            pnlToolbar.Controls.Add(btnRefresh);

            // FLOW LAYOUT PANEL (Giữ nguyên)
            flowPosts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10)
            };

            lblLoading = new Label
            {
                Text = "⏳ Đang tải.. .",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(10, 20, 10, 10)
            };

            btnLoadMore = new Button
            {
                Text = "⬇️ Tải thêm bài viết",
                Width = 580,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Margin = new Padding(10, 10, 10, 20)
            };
            btnLoadMore.FlatAppearance.BorderColor = Color.LightGray;
            btnLoadMore.Click += BtnLoadMore_Click;

            flowPosts.Controls.Add(lblLoading);

            // ADD TO CONTROL
            this.Controls.Add(flowPosts);
            this.Controls.Add(pnlToolbar);

            if (pnlSearchBar != null)
                this.Controls.Add(pnlSearchBar);

            this.ResumeLayout(false);
        }

        private void InitializeSearchBar()
        {
            // ✅ SỬA:  Check _userProfileService thay vì _nguoiDungService
            if (_userProfileService == null) return;

            pnlSearchBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            txtSearch = new TextBox
            {
                Location = new Point(15, 15),
                Width = 520,
                Height = 35,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                PlaceholderText = "🔍 Tìm kiếm người dùng... (Nhấn Enter để tìm)",
                BorderStyle = BorderStyle.FixedSingle
            };

            // ✅ THAY ĐỔI: Nhấn Enter để tìm kiếm
            txtSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Ngăn tiếng "beep"
                    await PerformSearchAsync();
                }
            };

            // ✅ THÊM: Nút tìm kiếm
            var btnSearch = new Button
            {
                Text = "🔍 Tìm",
                Location = new Point(545, 15),
                Width = 80,
                Height = 35,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += async (s, e) => await PerformSearchAsync();

            pnlSearchBar.Controls.Add(txtSearch);
            pnlSearchBar.Controls.Add(btnSearch);
        }

        private async Task PerformSearchAsync()
        {
            if (flowPosts == null || txtSearch == null)
                return;

            var keyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // ✅ PHÁT HIỆN LOẠI TÌM KIẾM
                if (keyword.StartsWith("#"))
                {
                    // Tìm kiếm HASHTAG
                    await SearchHashtagAsync(keyword);
                }
                else
                {
                    // Tìm kiếm NGƯỜI DÙNG (mặc định)
                    await SearchUserAsync(keyword);
                }
            }
            catch (Exception ex)
            {
                flowPosts?.Controls.Clear();
                var lblError = new Label
                {
                    Text = $"❌ Lỗi tìm kiếm:\n{ex.Message}",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.Red,
                    Margin = new Padding(10, 50, 10, 10)
                };
                flowPosts?.Controls.Add(lblError);
            }
        }

        /// <summary>
        /// 🔍 Tìm kiếm HASHTAG
        /// </summary>
        private async Task SearchHashtagAsync(string keyword)
        {
            if (flowPosts == null) return;

            // Lấy HashtagService
            var hashtagService = _serviceProvider?.GetService(typeof(IHashtagService)) as IHashtagService;
            if (hashtagService == null)
            {
                MessageBox.Show("Không thể tải dịch vụ tìm kiếm hashtag", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bỏ dấu # để tìm kiếm
            var hashtag = keyword.TrimStart('#');

            // Chuyển đến HashtagSearchPage
            var mainForm = this.FindForm();
            if (mainForm is Forms.MainForm mf)
            {
                var searchPage = new HashtagSearchPage(hashtagService, hashtag);
                mf.LoadPage(searchPage);
            }
        }

        /// <summary>
        /// 👤 Tìm kiếm NGƯỜI DÙNG
        /// </summary>
        private async Task SearchUserAsync(string keyword)
        {
            if (_userProfileService == null || flowPosts == null)
                return;

            // ✅ Xóa nội dung cũ
            flowPosts.Controls.Clear();

            // ✅ Hiển thị loading
            var lblLoading = new Label
            {
                Text = "🔍 Đang tìm kiếm người dùng...",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Margin = new Padding(10, 20, 10, 10)
            };
            flowPosts.Controls.Add(lblLoading);

            // ✅ Gọi API tìm kiếm
            var results = await _userProfileService.TimKiemNguoiDungAsync(keyword);

            // ✅ Xóa loading
            flowPosts.Controls.Clear();

            // ✅ Hiển thị kết quả
            if (results == null || !results.Any())
            {
                var lblEmpty = new Label
                {
                    Text = $"❌ Không tìm thấy người dùng với từ khóa: \"{keyword}\"",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 50, 10, 10)
                };
                flowPosts.Controls.Add(lblEmpty);
            }
            else
            {
                // ✅ Header
                var lblHeader = new Label
                {
                    Text = $"🔍 Kết quả tìm kiếm: \"{keyword}\" ({results.Count} người dùng)",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 119, 242),
                    Margin = new Padding(10, 10, 10, 20)
                };
                flowPosts.Controls.Add(lblHeader);

                // ✅ Danh sách user
                foreach (var user in results)
                {
                    var userCard = CreateUserCard(user);
                    flowPosts.Controls.Add(userCard);
                }
            }
        }

        private Panel CreateUserCard(NguoiDungResponse user)
        {
            var pnlUser = new Panel
            {
                Width = 580,
                Height = 80,
                BackColor = Color.White,
                Margin = new Padding(10, 5, 10, 5),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            pnlUser.MouseEnter += (s, e) => pnlUser.BackColor = Color.FromArgb(240, 242, 245);
            pnlUser.MouseLeave += (s, e) => pnlUser.BackColor = Color.White;

            var pbAvatar = new PictureBox
            {
                Width = 60,
                Height = 60,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ✅ FIX: Load avatar
            LoadAvatarForSearchResult(pbAvatar, user.HinhDaiDien, user.HoVaTen ?? user.Email);

            var lblName = new Label
            {
                Text = user.HoVaTen ?? "Người dùng",
                Location = new Point(80, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            var lblEmail = new Label
            {
                Text = user.Email ?? "",
                Location = new Point(80, 40),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            var btnViewProfile = new Button
            {
                Text = "👁️ Xem trang cá nhân",
                Location = new Point(450, 25),
                Width = 120,
                Height = 30,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnViewProfile.FlatAppearance.BorderSize = 0;
            btnViewProfile.Click += (s, e) =>
            {
                // ✅ MỞ TRANG PROFILE
                OpenUserProfile(user.MaNguoiDung);
            };

            pnlUser.Controls.Add(pbAvatar);
            pnlUser.Controls.Add(lblName);
            pnlUser.Controls.Add(lblEmail);
            pnlUser.Controls.Add(btnViewProfile);

            return pnlUser;
        }

        /// <summary>
        /// 📄 Mở trang profile người dùng
        /// </summary>
        private void OpenUserProfile(Guid userId)
        {
            if (_serviceProvider == null) return;

            try
            {
                var profilePage = new UserProfilePage(userId, _serviceProvider);

                // Tìm MainForm để load page
                var mainForm = this.FindForm();
                if (mainForm is MainForm mf)
                {
                    mf.LoadPage(profilePage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở trang profile: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CÁC METHOD CŨ (GIỮ NGUYÊN)
        private async void LoadNewsfeedAsync()
        {
            await LoadPostsAsync(isRefresh: true);
        }

        private async Task LoadPostsAsync(bool isRefresh = false)
        {
            if (_isLoading || !UserSession.IsLoggedIn || UserSession.CurrentUser == null || _postService == null)
                return;

            _isLoading = true;

            try
            {
                if (lblLoading != null)
                    lblLoading.Visible = true;

                if (isRefresh)
                {
                    _currentPage = 1;
                    flowPosts?.Controls.Clear();
                    if (lblLoading != null)
                        flowPosts?.Controls.Add(lblLoading);
                }

                var posts = await _postService.GetNewsfeedAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    _currentPage,
                    PAGE_SIZE
                );

                if (posts != null && posts.Any())
                {
                    if (_reactionService != null && _commentService != null)
                    {
                        foreach (var post in posts)
                        {
                            var postCard = new PostCardControl(
                                _postService,
                                _reactionService,
                                _commentService
                            )
                            {
                                Width = 600,
                                Margin = new Padding(10, 5, 10, 5)
                            };

                            postCard.LoadPost(post);
                            flowPosts?.Controls.Add(postCard);
                        }

                        if (posts.Count == PAGE_SIZE && btnLoadMore != null)
                        {
                            flowPosts?.Controls.Remove(btnLoadMore);
                            flowPosts?.Controls.Add(btnLoadMore);
                        }
                    }
                }
                else if (_currentPage == 1)
                {
                    var lblEmpty = new Label
                    {
                        Text = "📭 Chưa có bài đăng nào.\nHãy theo dõi bạn bè hoặc tạo bài viết đầu tiên!  ",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowPosts?.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải newsfeed: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                _isLoading = false;
                if (lblLoading != null)
                    lblLoading.Visible = false;
            }
        }

        private void BtnCreatePost_Click(object? sender, EventArgs e)
        {
            if (_postService == null)
                return;

            var dialog = new CreatePostDialog(_postService);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadNewsfeedAsync();
            }
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadPostsAsync(isRefresh: true);
        }

        private async void BtnLoadMore_Click(object? sender, EventArgs e)
        {
            _currentPage++;
            await LoadPostsAsync(isRefresh: false);
        }

        /// <summary>
        /// ✅ Load avatar cho search result (60x60)
        /// </summary>
        private void LoadAvatarForSearchResult(PictureBox pictureBox, string? avatarPath, string? displayName)
        {
            if (pictureBox == null) return;

            try
            {
                if (!string.IsNullOrEmpty(avatarPath) && System.IO.File.Exists(avatarPath))
                {
                    using (var fs = new System.IO.FileStream(avatarPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pictureBox.Image = CreateSmallPlaceholderAvatar(displayName);
                }
            }
            catch
            {
                pictureBox.Image = CreateSmallPlaceholderAvatar(displayName);
            }
        }

        /// <summary>
        /// ✅ Tạo avatar placeholder 60x60
        /// </summary>
        private Image CreateSmallPlaceholderAvatar(string? name)
        {
            var size = 60;
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(24, 119, 242),
                    Color.FromArgb(66, 153, 225),
                    System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                string initials = GetInitials(name);

                using (var font = new Font("Segoe UI", 20, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(initials, font);
                    var x = (size - textSize.Width) / 2;
                    var y = (size - textSize.Height) / 2;
                    g.DrawString(initials, font, textBrush, x, y);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// ✅ Lấy chữ cái đầu
        /// </summary>
        private string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "?";

            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();

            return (parts[0][0].ToString() + parts[parts.Length - 1][0].ToString()).ToUpper();
        }
    }
}