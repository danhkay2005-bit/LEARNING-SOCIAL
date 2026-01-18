using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Components.Social;

namespace WinForms.UserControls.Social
{
    /// <summary>
    /// ?? HASHTAG SEARCH PAGE - Tìm ki?m bài vi?t theo hashtag
    /// </summary>
    public class HashtagSearchPage : UserControl
    {
        private readonly IHashtagService _hashtagService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;
        private readonly string _hashtag;

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Label? lblStats;
        private Button? btnBack;
        private FlowLayoutPanel? flowPosts;
        private Label? lblLoading;

        public HashtagSearchPage(IHashtagService hashtagService, string hashtag)
        {
            _hashtagService = hashtagService;
            _hashtag = hashtag;

            // L?y services t? ServiceProvider
            if (Program.ServiceProvider != null)
            {
                _postService = Program.ServiceProvider.GetService<IPostService>();
                _reactionService = Program.ServiceProvider.GetService<IReactionService>();
                _commentService = Program.ServiceProvider.GetService<ICommentService>();
            }

            InitializeComponent();
            InitializeControls();
            LoadPostsAsync();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "HashtagSearchPage";
            this.Size = new Size(900, 700);
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.ResumeLayout(false);
        }

        private void InitializeControls()
        {
            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Nút Back
            btnBack = new Button
            {
                Text = "< Quay lai", // ? S?A: Dùng < thay vì ?, b? d?u
                Location = new Point(20, 20),
                Width = 110, // ? T?ng t? 100 ?? fit text
                Height = 35,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            // Title - ? S?A: T?ng size, ??i style
            lblTitle = new Label
            {
                Text = $"#{_hashtag}",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold), // ? T?ng t? 18F ? 20F
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            // Stats - ? S?A: T?ng size, d? ??c h?n
            lblStats = new Label
            {
                Text = "?ang t?i...",
                Location = new Point(20, 95),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular), // ? Gi? nguyên size
                ForeColor = Color.FromArgb(128, 128, 128) // ? Màu xám ??m h?n
            };

            pnlHeader.Controls.Add(btnBack);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblStats);

            // ===== POSTS FLOW =====
            flowPosts = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };

            // Loading label - ? S?A: Font ??ng nh?t
            lblLoading = new Label
            {
                Text = "Dang tai bai viet...", // ? B? d?u
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Margin = new Padding(10, 50, 10, 10)
            };
            flowPosts.Controls.Add(lblLoading);

            // Add to page
            this.Controls.Add(flowPosts);
            this.Controls.Add(pnlHeader);
        }

        /// <summary>
        /// ?? T?i bài vi?t theo hashtag
        /// </summary>
        private async void LoadPostsAsync()
        {
            if (flowPosts == null || _postService == null) return;

            try
            {
                // 1. L?y thông tin hashtag
                var hashtagInfo = await _hashtagService.LayThongTinHashtagAsync(_hashtag);

                // 2. Tìm ki?m bài vi?t
                var posts = await _hashtagService.TimKiemBaiDangTheoHashtagAsync(_hashtag, page: 1, pageSize: 20);

                // 3. Update stats - ? S?A: ??n gi?n hóa text
                if (lblStats != null && hashtagInfo != null)
                {
                    lblStats.Text = $"{posts.Count} bai viet | {hashtagInfo.SoLuotDung} luot su dung"; // ? B? d?u
                }
                else if (lblStats != null)
                {
                    lblStats.Text = $"{posts.Count} bai viet";
                }

                // 4. Clear loading
                flowPosts.Controls.Clear();

                // 5. Hi?n th? k?t qu? - ? S?A: Font rõ ràng h?n
                if (!posts.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = $"Khong tim thay bai viet cong khai voi hashtag #{_hashtag}", // ? B? d?u
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                        ForeColor = Color.FromArgb(128, 128, 128),
                        Margin = new Padding(10, 50, 10, 10),
                        MaximumSize = new Size(800, 0)
                    };
                    flowPosts.Controls.Add(lblEmpty);
                    return;
                }

                // 6. Render posts
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

                        // ? Delay nh? ?? tránh DbContext conflict
                        await Task.Delay(50);
                    }
                }
            }
            catch (Exception ex)
            {
                flowPosts?.Controls.Clear();
                var lblError = new Label
                {
                    Text = $"Loi tai bai viet: {ex.Message}", // ? B? d?u
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(220, 53, 69),
                    Margin = new Padding(10, 50, 10, 10),
                    MaximumSize = new Size(800, 0)
                };
                flowPosts?.Controls.Add(lblError);

                System.Diagnostics.Debug.WriteLine($"Loi HashtagSearchPage: {ex.Message}");
            }
        }

        /// <summary>
        /// ? Quay l?i
        /// </summary>
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            var mainForm = this.FindForm();
            if (mainForm is Forms.MainForm mf && Program.ServiceProvider != null)
            {
                try
                {
                    // Quay v? Newsfeed
                    var newsfeedControl = Program.ServiceProvider.GetRequiredService<NewsfeedControl>();
                    mf.LoadPage(newsfeedControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Khong the quay lai: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
