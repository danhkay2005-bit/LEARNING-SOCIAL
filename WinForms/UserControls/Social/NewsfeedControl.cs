using WinForms.Forms.Social;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
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
        // ✅ SỬA:  Thêm nullable cho các service
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;
        private readonly IServiceProvider? _serviceProvider;

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

        // ✅ Constructor mặc định (cho Designer)
        public NewsfeedControl()
        {
            InitializeComponent();
        }

        // ✅ Constructor với DI
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

            InitializeControls();
            LoadNewsfeedAsync();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            // ===== TOOLBAR =====
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

            // ===== FLOW LAYOUT PANEL =====
            flowPosts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10)
            };

            // ===== LOADING LABEL =====
            lblLoading = new Label
            {
                Text = "⏳ Đang tải...",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(10, 20, 10, 10)
            };

            // ===== LOAD MORE BUTTON =====
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

            // ===== ADD TO CONTROL =====
            this.Controls.Add(flowPosts);
            this.Controls.Add(pnlToolbar);

            this.ResumeLayout(false);
        }

        private async void LoadNewsfeedAsync()
        {
            await LoadPostsAsync(isRefresh: true);
        }

        private async Task LoadPostsAsync(bool isRefresh = false)
        {
            // ✅ SỬA: Kiểm tra null cho services
            if (_isLoading || !UserSession.IsLoggedIn || UserSession.CurrentUser == null || _postService == null)
                return;

            _isLoading = true;

            try
            {
                // Hiển thị loading
                if (lblLoading != null)
                    lblLoading.Visible = true;

                if (isRefresh)
                {
                    _currentPage = 1;
                    flowPosts?.Controls.Clear();
                    if (lblLoading != null)
                        flowPosts?.Controls.Add(lblLoading);
                }

                // Gọi API
                var posts = await _postService.GetNewsfeedAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    _currentPage,
                    PAGE_SIZE
                );

                // Render từng bài đăng
                if (posts != null && posts.Any())
                {
                    // ✅ SỬA: Kiểm tra null trước khi tạo PostCard
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

                        // Thêm nút Load More
                        if (posts.Count == PAGE_SIZE && btnLoadMore != null)
                        {
                            flowPosts?.Controls.Remove(btnLoadMore);
                            flowPosts?.Controls.Add(btnLoadMore);
                        }
                    }
                }
                else if (_currentPage == 1)
                {
                    // Không có bài đăng
                    var lblEmpty = new Label
                    {
                        Text = "📭 Chưa có bài đăng nào.\nHãy theo dõi bạn bè hoặc tạo bài viết đầu tiên! ",
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

        // ===== SỰ KIỆN =====

        private void BtnCreatePost_Click(object? sender, EventArgs e)
        {
            if (_postService == null)
                return;

            // Mở CreatePostDialog
            var dialog = new CreatePostDialog(_postService);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Reload newsfeed sau khi đăng bài thành công
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
    }
}