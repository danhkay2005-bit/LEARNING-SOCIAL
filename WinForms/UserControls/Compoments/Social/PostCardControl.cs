using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms.UserControls.Social;

namespace WinForms.UserControls.Components.Social
{
    public partial class PostCardControl : UserControl
    {
        // ✅ SỬA:  Thêm nullable cho các service (vì constructor mặc định không inject)
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;

        private BaiDangResponse? _post;

        // ===== CONTROLS =====
        private Panel? pnlContainer;
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblAuthorName;
        private Label? lblTimestamp;
        private Panel? pnlContent;
        private Label? lblContent;
        private PictureBox? pbImage;
        private Panel? pnlStats;
        private Label? lblReactionCount;
        private Label? lblCommentCount;
        private Panel? pnlActions;
        private Button? btnLike;
        private Button? btnComment;
        private Button? btnShare;

        // ✅ Constructor mặc định (cho Designer)
        public PostCardControl()
        {
            InitializeComponent();
        }

        // ✅ Constructor với Dependency Injection (cho runtime)
        public PostCardControl(
            IPostService postService,
            IReactionService reactionService,
            ICommentService commentService) : this()
        {
            _postService = postService;
            _reactionService = reactionService;
            _commentService = commentService;

            InitializeControls();
        }

        public void LoadPost(BaiDangResponse post)
        {
            _post = post;
            RenderPost();
        }

        private void InitializeControls()
        {
            // ===== CONTAINER CHÍNH =====
            pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15),
                AutoSize = true
            };

            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White
            };

            pbAvatar = new PictureBox
            {
                Width = 45,
                Height = 45,
                Location = new Point(10, 7),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblAuthorName = new Label
            {
                Location = new Point(65, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold), // ✅ SỬA: Đúng cú pháp
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            lblTimestamp = new Label
            {
                Location = new Point(65, 32),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular), // ✅ SỬA
                ForeColor = Color.Gray
            };

            pnlHeader.Controls.Add(pbAvatar);
            pnlHeader.Controls.Add(lblAuthorName);
            pnlHeader.Controls.Add(lblTimestamp);

            // ===== CONTENT =====
            pnlContent = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 10),
                BackColor = Color.White
            };

            lblContent = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(560, 0),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular), // ✅ SỬA
                ForeColor = Color.Black,
                Padding = new Padding(0, 0, 0, 10)
            };

            pbImage = new PictureBox
            {
                Dock = DockStyle.Top,
                SizeMode = PictureBoxSizeMode.Zoom,
                MaximumSize = new Size(560, 350),
                Visible = false
            };

            pnlContent.Controls.Add(pbImage);
            pnlContent.Controls.Add(lblContent);

            // ===== STATS =====
            pnlStats = new Panel
            {
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            lblReactionCount = new Label
            {
                Location = new Point(10, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular), // ✅ SỬA
                ForeColor = Color.Gray,
                Text = "👍 0"
            };

            lblCommentCount = new Label
            {
                Location = new Point(100, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular), // ✅ SỬA
                ForeColor = Color.Gray,
                Text = "💬 0 bình luận"
            };

            pnlStats.Controls.Add(lblReactionCount);
            pnlStats.Controls.Add(lblCommentCount);

            // ===== ACTIONS =====
            pnlActions = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.WhiteSmoke
            };

            btnLike = CreateActionButton("👍 Thích", 10);
            btnComment = CreateActionButton("💬 Bình luận", 200);
            btnShare = CreateActionButton("↗️ Chia sẻ", 390);

            btnLike.Click += BtnLike_Click;
            btnComment.Click += BtnComment_Click;
            btnShare.Click += BtnShare_Click;

            pnlActions.Controls.Add(btnLike);
            pnlActions.Controls.Add(btnComment);
            pnlActions.Controls.Add(btnShare);

            // ===== ADD TO CONTAINER =====
            pnlContainer.Controls.Add(pnlActions);
            pnlContainer.Controls.Add(pnlStats);
            pnlContainer.Controls.Add(pnlContent);
            pnlContainer.Controls.Add(pnlHeader);

            this.Controls.Add(pnlContainer);
        }

        private Button CreateActionButton(string text, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 7),
                Width = 180,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular), // ✅ SỬA
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private void RenderPost()
        {
            if (_post == null) return;

            if (lblAuthorName != null)
                lblAuthorName.Text = "Người dùng";

            if (lblTimestamp != null)
                lblTimestamp.Text = GetRelativeTime(_post.ThoiGianTao ?? DateTime.Now);

            if (lblContent != null)
                lblContent.Text = _post.NoiDung ?? "(Không có nội dung)";

            if (!string.IsNullOrEmpty(_post.HinhAnh) && pbImage != null)
            {
                try
                {
                    pbImage.Load(_post.HinhAnh);
                    pbImage.Visible = true;
                }
                catch
                {
                    pbImage.Visible = false;
                }
            }

            if (lblReactionCount != null)
                lblReactionCount.Text = $"👍 {_post.SoReaction}";

            if (lblCommentCount != null)
                lblCommentCount.Text = $"💬 {_post.SoBinhLuan} bình luận";
        }

        private string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1) return "Vừa xong";
            if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} phút trước";
            if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} giờ trước";
            if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} ngày trước";

            return dateTime.ToString("dd/MM/yyyy");
        }

        private async void BtnLike_Click(object? sender, EventArgs e)
        {
            // ✅ SỬA: Kiểm tra service != null
            if (!UserSession.IsLoggedIn || _post == null || UserSession.CurrentUser == null || _reactionService == null)
                return;

            try
            {
                var request = new TaoHoacCapNhatReactionBaiDangRequest
                {
                    MaBaiDang = _post.MaBaiDang,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    LoaiReaction = LoaiReactionEnum.Thich
                };

                await _reactionService.ReactToPostAsync(request);

                var newCount = await _reactionService.GetPostReactionCountAsync(_post.MaBaiDang);
                _post.SoReaction = newCount;

                if (lblReactionCount != null)
                    lblReactionCount.Text = $"👍 {newCount}";

                if (btnLike != null)
                    btnLike.BackColor = Color.FromArgb(230, 240, 255);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnComment_Click(object? sender, EventArgs e)
        {
            if (_post == null || _commentService == null || _reactionService == null) return;

            // Tạo CommentSection
            var commentSection = new CommentSectionControl(_commentService, _reactionService)
            {
                Dock = DockStyle.Fill
            };

            // Tạo Form popup
            var form = new Form
            {
                Text = "Bình luận",
                Size = new Size(650, 600),
                StartPosition = FormStartPosition.CenterParent
            };
            form.Controls.Add(commentSection);

            // Load comments
            _ = commentSection.LoadCommentsAsync(_post.MaBaiDang);

            form.ShowDialog();
        }

        private void BtnShare_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng chia sẻ đang phát triển!");
        }

        private void PostCardControl_Load(object sender, EventArgs e)
        {

        }
    }
}