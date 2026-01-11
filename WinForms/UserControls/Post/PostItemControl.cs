using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinForms.UserControls.Post
{
    public partial class PostItemControl : UserControl
    {
        private readonly BaiDangResponse _post;
        private readonly IBaiDangService _baiDangService;
        private bool _isLiked;

        public event Action? OnDeleted;

        public PostItemControl(BaiDangResponse post, IBaiDangService baiDangService)
        {
            InitializeComponent();
            _post = post;
            _baiDangService = baiDangService;
            _isLiked = post.DaReaction;

            LoadPostData();
            RegisterEvents();
            SetupHoverEffects();
        }

        private void LoadPostData()
        {
            // User info
            lblUserName.Text = _post.NguoiDang?.TenHienThi ?? "Người dùng ẩn danh";
            lblPostTime.Text = FormatTimeAgo(_post.ThoiGianTao);
            lblPrivacy.Text = GetPrivacyIcon(_post.QuyenRiengTu);

            // Avatar
            CreateUserAvatar(_post.NguoiDang?.TenHienThi);

            // Content
            lblContent.Text = _post.NoiDung ?? "";
            lblContent.Visible = !string.IsNullOrEmpty(_post.NoiDung);

            // Image
            LoadImage();

            // Hashtags
            LoadHashtags();

            // Stats - Sửa lỗi ??  operator
            lblReactionCount.Text = $"❤️ {_post.SoReaction}";
            lblCommentCount.Text = $"💬 {_post.SoBinhLuan} bình luận";
            lblViewCount.Text = $"👁 {_post.SoLuotXem} lượt xem";

            // Like button state
            UpdateLikeButton();

            // Show/hide menu for own posts
            btnMenu.Visible = _post.MaNguoiDung == UserSession.CurrentUser?.MaNguoiDung;
        }

        private void LoadImage()
        {
            if (!string.IsNullOrEmpty(_post.HinhAnh) && File.Exists(_post.HinhAnh))
            {
                try
                {
                    picImage.Image = Image.FromFile(_post.HinhAnh);
                    picImage.Visible = true;
                }
                catch
                {
                    picImage.Visible = false;
                }
            }
            else
            {
                picImage.Visible = false;
            }
        }

        private void CreateUserAvatar(string? name)
        {
            var bmp = new Bitmap(45, 45);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Random color based on name
                var colors = new[]
                {
                    Color.FromArgb(0, 132, 255),
                    Color.FromArgb(66, 183, 42),
                    Color.FromArgb(246, 80, 88),
                    Color.FromArgb(255, 186, 0),
                    Color.FromArgb(160, 95, 210)
                };
                var colorIndex = Math.Abs((name ?? "").GetHashCode()) % colors.Length;

                using (var brush = new SolidBrush(colors[colorIndex]))
                {
                    g.FillEllipse(brush, 0, 0, 44, 44);
                }

                var initial = string.IsNullOrEmpty(name) ? "U" : name.Substring(0, 1).ToUpper();
                using (var font = new Font("Segoe UI", 14, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var size = g.MeasureString(initial, font);
                    g.DrawString(initial, font, brush,
                        (45 - size.Width) / 2,
                        (45 - size.Height) / 2);
                }
            }

            picUserAvatar.Image = bmp;

            // Làm tròn PictureBox
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, picUserAvatar.Width, picUserAvatar.Height);
            picUserAvatar.Region = new Region(path);
        }

        private void LoadHashtags()
        {
            pnlHashtags.Controls.Clear();

            if (_post.Hashtags == null || _post.Hashtags.Count == 0)
            {
                pnlHashtags.Visible = false;
                return;
            }

            pnlHashtags.Visible = true;
            var flowLayout = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(0)
            };

            foreach (var hashtag in _post.Hashtags.Take(5))
            {
                var lbl = new Label
                {
                    Text = $"#{hashtag.TenHashtag}",
                    AutoSize = true,
                    BackColor = Color.FromArgb(224, 242, 254),
                    ForeColor = Color.FromArgb(0, 132, 255),
                    Font = new Font("Segoe UI", 9F),
                    Padding = new Padding(8, 4, 8, 4),
                    Margin = new Padding(0, 0, 5, 5),
                    Cursor = Cursors.Hand
                };

                flowLayout.Controls.Add(lbl);
            }

            pnlHashtags.Controls.Add(flowLayout);
        }

        private void RegisterEvents()
        {
            btnLike.Click += BtnLike_Click;
            btnComment.Click += BtnComment_Click;
            btnShare.Click += BtnShare_Click;
            btnMenu.Click += BtnMenu_Click;
        }

        private void SetupHoverEffects()
        {
            SetupButtonHover(btnLike, Color.FromArgb(245, 247, 250), Color.FromArgb(232, 234, 237));
            SetupButtonHover(btnComment, Color.FromArgb(245, 247, 250), Color.FromArgb(232, 234, 237));
            SetupButtonHover(btnShare, Color.FromArgb(245, 247, 250), Color.FromArgb(232, 234, 237));
            SetupButtonHover(btnMenu, Color.White, Color.FromArgb(245, 247, 250));
        }

        private void SetupButtonHover(Button btn, Color normalColor, Color hoverColor)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = normalColor;
        }

        // ================= ACTIONS =================
        private void BtnLike_Click(object? sender, EventArgs e)
        {
            _isLiked = !_isLiked;
            UpdateLikeButton();

            // TODO: Call API to like/unlike
            // _baiDangService. ToggleReaction(new ReactionRequest { MaBaiDang = _post.MaBaiDang });
        }

        private void UpdateLikeButton()
        {
            if (_isLiked)
            {
                btnLike.Text = "❤️ Đã thích";
                btnLike.ForeColor = Color.FromArgb(237, 73, 86);
                btnLike.BackColor = Color.FromArgb(255, 240, 241);
            }
            else
            {
                btnLike.Text = "👍 Thích";
                btnLike.ForeColor = Color.FromArgb(101, 103, 107);
                btnLike.BackColor = Color.FromArgb(245, 247, 250);
            }
        }

        private void BtnComment_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Tính năng bình luận đang phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnShare_Click(object? sender, EventArgs e)
        {
            var content = $"Bài đăng của {_post.NguoiDang}:  {_post.NoiDung}";
            Clipboard.SetText(content);
            MessageBox.Show("Đã sao chép nội dung vào clipboard!", "Chia sẻ",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnMenu_Click(object? sender, EventArgs e)
        {
            var menu = new ContextMenuStrip();

            var isPinned = _post.GhimBaiDang;
            menu.Items.Add(isPinned ? "📌 Bỏ ghim" : "📌 Ghim bài đăng", null, (s, ev) => PinPost());
            menu.Items.Add("✏️ Chỉnh sửa", null, (s, ev) => EditPost());
            menu.Items.Add(new ToolStripSeparator());

            var deleteItem = menu.Items.Add("🗑️ Xóa bài đăng", null, (s, ev) => DeletePost());
            deleteItem.ForeColor = Color.Red;

            menu.Show(btnMenu, new Point(0, btnMenu.Height));
        }

        private void PinPost()
        {
            try
            {
                var newPinState = !_post.GhimBaiDang;

                _baiDangService.GhimBaiDang(new GhimBaiDangRequest
                {
                    MaBaiDang = _post.MaBaiDang,
                    GhimBaiDang = newPinState
                });

                MessageBox.Show(
                    newPinState ? "Đã ghim bài đăng!" : "Đã bỏ ghim bài đăng!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                OnDeleted?.Invoke(); // Reload feed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditPost()
        {
            MessageBox.Show("Tính năng chỉnh sửa đang phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeletePost()
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa bài đăng này?\nHành động này không thể hoàn tác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _baiDangService.XoaBaiDang(new XoaBaiDangRequest
                    {
                        MaBaiDang = _post.MaBaiDang
                    });

                    MessageBox.Show("Đã xóa bài đăng!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OnDeleted?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa bài: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= HELPERS =================
        private static string FormatTimeAgo(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return "Không rõ";

            var span = DateTime.Now - dateTime.Value;

            if (span.TotalSeconds < 60) return "Vừa xong";
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} phút trước";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours} giờ trước";
            if (span.TotalDays < 7) return $"{(int)span.TotalDays} ngày trước";
            if (span.TotalDays < 30) return $"{(int)(span.TotalDays / 7)} tuần trước";
            if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)} tháng trước";

            return dateTime.Value.ToString("dd/MM/yyyy");
        }

        private static string GetPrivacyIcon(QuyenRiengTuEnum? privacy)
        {
            return privacy switch
            {
                QuyenRiengTuEnum.CongKhai => "🌍",
                QuyenRiengTuEnum.ChiFollower => "👥",
                QuyenRiengTuEnum.RiengTu => "🔒",
                _ => "🌍"
            };
        }
    }
}