using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Helpers; // ✅ THÊM để dùng AvatarHelper

namespace WinForms.UserControls.Social
{
    public partial class CommentSectionControl : UserControl
    {
        private readonly ICommentService? _commentService;
        private readonly IReactionService? _reactionService;
        private readonly IUserProfileService? _userProfileService; // ✅ THÊM

        private int _postId;
        private List<BinhLuanBaiDangResponse> _comments = new List<BinhLuanBaiDangResponse>();
        private Dictionary<int, List<BinhLuanBaiDangResponse>> _repliesCache = new(); // ✅ THÊM cache

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Button? btnClose;
        private FlowLayoutPanel? flowComments;
        private Panel? pnlInputArea;
        private PictureBox? pbUserAvatar;
        private TextBox? txtComment;
        private Button? btnSendComment;

        // Reply mode
        private int? _replyToCommentId = null;
        private Label? lblReplyMode;
        private Button? btnCancelReply;

        // ✅ THÊM:  Loading indicator
        private Label? lblLoading;

        // Constructor mặc định
        public CommentSectionControl()
        {
            InitializeComponent();
        }

        // ✅ SỬA: Constructor với DI
        public CommentSectionControl(
            ICommentService commentService,
            IReactionService reactionService,
            IUserProfileService userProfileService) : this()
        {
            _commentService = commentService;
            _reactionService = reactionService;
            _userProfileService = userProfileService;

            InitializeControls();
        }

        /// <summary>
        /// Load comments của bài đăng
        /// </summary>
        public async Task LoadCommentsAsync(int postId)
        {
            _postId = postId;
            await RefreshCommentsAsync();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };

            lblTitle = new Label
            {
                Text = "Bình luận",
                Location = new Point(15, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            btnClose = new Button
            {
                Text = "✖",
                Location = new Point(this.Width - 50, 10),
                Width = 30,
                Height = 30,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);

            // ===== COMMENTS AREA =====
            flowComments = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(245, 246, 247),
                Padding = new Padding(10)
            };

            // ✅ THÊM: Loading label
            lblLoading = new Label
            {
                Text = "⏳ Đang tải bình luận...",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Visible = false
            };

            // ===== INPUT AREA =====
            pnlInputArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100, // ✅ Tăng height
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Reply mode indicator
            lblReplyMode = new Label
            {
                Text = "Đang trả lời.. .",
                Location = new Point(60, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(24, 119, 242),
                Visible = false
            };

            btnCancelReply = new Button
            {
                Text = "✖",
                Location = new Point(200, 5),
                Width = 20,
                Height = 20,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 50, 50),
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnCancelReply.FlatAppearance.BorderSize = 0;
            btnCancelReply.Click += (s, e) => CancelReplyMode();

            pbUserAvatar = new PictureBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(10, 35),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ✅ SỬA: Multiline TextBox
            txtComment = new TextBox
            {
                Location = new Point(60, 35),
                Width = this.Width - 140,
                Height = 50,
                Multiline = true, // ✅ THÊM
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                PlaceholderText = "Viết bình luận...  (Enter để gửi, Shift+Enter để xuống dòng)",
                ScrollBars = ScrollBars.Vertical // ✅ THÊM
            };
            txtComment.KeyDown += TxtComment_KeyDown;

            btnSendComment = new Button
            {
                Text = "➤",
                Location = new Point(this.Width - 60, 35),
                Width = 40,
                Height = 50,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnSendComment.FlatAppearance.BorderSize = 0;
            btnSendComment.Click += BtnSendComment_Click;

            txtComment.TextChanged += (s, e) =>
            {
                if (btnSendComment != null && txtComment != null)
                    btnSendComment.Enabled = !string.IsNullOrWhiteSpace(txtComment.Text);
            };

            // ✅ THÊM: Load avatar của user hiện tại
            if (UserSession.IsLoggedIn && UserSession.CurrentUser != null && pbUserAvatar != null)
            {
                var initials = UserSession.CurrentUser.HoVaTen?.Substring(0, 1) ?? "U";
                AvatarHelper.SetAvatar(pbUserAvatar, UserSession.CurrentUser.HinhDaiDien, initials);
            }

            pnlInputArea.Controls.Add(lblReplyMode);
            pnlInputArea.Controls.Add(btnCancelReply);
            pnlInputArea.Controls.Add(pbUserAvatar);
            pnlInputArea.Controls.Add(txtComment);
            pnlInputArea.Controls.Add(btnSendComment);

            // ===== ADD TO CONTROL =====
            this.Controls.Add(flowComments);
            this.Controls.Add(pnlInputArea);
            this.Controls.Add(pnlHeader);

            this.BackColor = Color.White;
            this.Size = new Size(600, 600); // ✅ Tăng height

            this.ResumeLayout(false);
        }

        /// <summary>
        /// ✅ SỬA: Refresh danh sách comments với loading state
        /// </summary>
        private async Task RefreshCommentsAsync()
        {
            if (_commentService == null || flowComments == null) return;

            try
            {
                // Show loading
                flowComments.Controls.Clear();
                if (lblLoading != null)
                {
                    lblLoading.Visible = true;
                    flowComments.Controls.Add(lblLoading);
                }

                // Load comments
                _comments = await _commentService.GetCommentsByPostAsync(_postId);

                // ✅ THÊM: Load tất cả replies cùng lúc (better performance)
                _repliesCache.Clear();
                var commentIds = _comments.Select(c => c.MaBinhLuan).ToList();

                foreach (var commentId in commentIds)
                {
                    var replies = await _commentService.GetRepliesAsync(commentId);
                    if (replies.Any())
                    {
                        _repliesCache[commentId] = replies.ToList();
                    }
                }

                // Hide loading
                if (lblLoading != null)
                {
                    lblLoading.Visible = false;
                }

                RenderComments();
            }
            catch (Exception ex)
            {
                if (lblLoading != null)
                {
                    lblLoading.Visible = false;
                }
                MessageBox.Show($"Lỗi khi tải bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ SỬA: Render comments với replies
        /// </summary>
        private void RenderComments()
        {
            flowComments?.Controls.Clear();

            if (_comments == null || !_comments.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "💬 Chưa có bình luận nào.\nHãy là người đầu tiên bình luận!",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 20, 10, 10),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowComments?.Controls.Add(lblEmpty);
                return;
            }

            foreach (var comment in _comments)
            {
                // Add comment chính
                var commentCard = CreateCommentCard(comment, isReply: false);
                flowComments?.Controls.Add(commentCard);

                // ✅ THÊM:  Add replies từ cache
                if (_repliesCache.TryGetValue(comment.MaBinhLuan, out var replies))
                {
                    foreach (var reply in replies)
                    {
                        var replyCard = CreateCommentCard(reply, isReply: true);
                        flowComments?.Controls.Add(replyCard);
                    }
                }
            }

            // Update title
            if (lblTitle != null)
            {
                var totalCount = _comments.Count + _repliesCache.Values.Sum(r => r.Count);
                lblTitle.Text = $"Bình luận ({totalCount})";
            }
        }

        /// <summary>
        /// ✅ SỬA: Tạo comment card với đầy đủ thông tin user
        /// </summary>
        private Panel CreateCommentCard(BinhLuanBaiDangResponse comment, bool isReply)
        {
            var pnlComment = new Panel
            {
                Width = 560,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(isReply ? 50 : 5, 5, 5, 5),
                BackColor = isReply ? Color.FromArgb(250, 250, 250) : Color.White,
                Tag = comment.MaBinhLuan.ToString()
            };

            // ✅ THÊM: Border cho reply
            if (isReply)
            {
                pnlComment.Paint += (s, e) =>
                {
                    e.Graphics.DrawLine(
                        new Pen(Color.FromArgb(200, 200, 200), 2),
                        0, 0, 0, pnlComment.Height
                    );
                };
            }

            var pbAvatar = new PictureBox
            {
                Width = 35,
                Height = 35,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ✅ THÊM: Load avatar thật
            _ = LoadUserAvatarAsync(comment.MaNguoiDung, pbAvatar);

            var lblUsername = new Label
            {
                Text = "Đang tải...",
                Location = new Point(55, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            // ✅ THÊM: Load username thật
            _ = LoadUsernameAsync(comment.MaNguoiDung, lblUsername);

            var lblContent = new Label
            {
                Text = comment.NoiDung ?? "",
                Location = new Point(55, 30),
                MaximumSize = new Size(480, 0),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };

            var lblTime = new Label
            {
                Text = GetRelativeTime(comment.ThoiGianTao ?? DateTime.Now),
                Location = new Point(55, lblContent.Bottom + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            // ✅ SỬA: Reaction button với số lượng chính xác
            var btnLike = new Label
            {
                Text = $"👍 {comment.SoLuotReactions }",
                Location = new Point(150, lblContent.Bottom + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Cursor = Cursors.Hand
            };
            btnLike.MouseEnter += (s, e) => btnLike.ForeColor = Color.FromArgb(24, 119, 242);
            btnLike.MouseLeave += (s, e) => btnLike.ForeColor = Color.FromArgb(100, 100, 100);
            btnLike.Click += async (s, e) => await LikeCommentAsync(comment.MaBinhLuan);

            // ✅ SỬA: Reply button
            var btnReply = new Label
            {
                Text = "↩️ Trả lời",
                Location = new Point(250, lblContent.Bottom + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100),
                Cursor = Cursors.Hand,
                Visible = !isReply
            };
            btnReply.MouseEnter += (s, e) => btnReply.ForeColor = Color.FromArgb(24, 119, 242);
            btnReply.MouseLeave += (s, e) => btnReply.ForeColor = Color.FromArgb(100, 100, 100);
            btnReply.Click += (s, e) => EnterReplyMode(comment.MaBinhLuan, lblUsername.Text);

            pnlComment.Controls.Add(pbAvatar);
            pnlComment.Controls.Add(lblUsername);
            pnlComment.Controls.Add(lblContent);
            pnlComment.Controls.Add(lblTime);
            pnlComment.Controls.Add(btnLike);
            pnlComment.Controls.Add(btnReply);

            pnlComment.Height = lblTime.Bottom + 15;

            return pnlComment;
        }

        /// <summary>
        /// ✅ THÊM: Load avatar của user
        /// </summary>
        private async Task LoadUserAvatarAsync(Guid userId, PictureBox pbAvatar)
        {
            if (_userProfileService == null) return;

            try
            {
                var user = await _userProfileService.GetProfileAsync(userId);
                if (user != null && pbAvatar != null)
                {
                    var initials = user.HoVaTen?.Substring(0, 1) ?? user.Email?.Substring(0, 1) ?? "U";
                    AvatarHelper.SetAvatar(pbAvatar, user.HinhDaiDien, initials);
                }
            }
            catch { /* Bỏ qua lỗi load avatar */ }
        }

        /// <summary>
        /// ✅ THÊM: Load username của user
        /// </summary>
        private async Task LoadUsernameAsync(Guid userId, Label lblUsername)
        {
            if (_userProfileService == null) return;

            try
            {
                var user = await _userProfileService.GetProfileAsync(userId);
                if (user != null && lblUsername != null)
                {
                    lblUsername.Text = user.HoVaTen ?? user.TenDangNhap ?? "Người dùng";
                }
            }
            catch
            {
                if (lblUsername != null)
                {
                    lblUsername.Text = "Người dùng";
                }
            }
        }

        /// <summary>
        /// ✅ Bật chế độ reply
        /// </summary>
        private void EnterReplyMode(int commentId, string username)
        {
            _replyToCommentId = commentId;

            if (lblReplyMode != null && btnCancelReply != null)
            {
                lblReplyMode.Text = $"↩️ Đang trả lời @{username}";
                lblReplyMode.Visible = true;
                btnCancelReply.Visible = true;
            }

            if (txtComment != null)
            {
                txtComment.PlaceholderText = $"Trả lời @{username}... ";
                txtComment.Focus();
            }
        }

        /// <summary>
        /// ✅ Hủy chế độ reply
        /// </summary>
        private void CancelReplyMode()
        {
            _replyToCommentId = null;

            if (lblReplyMode != null && btnCancelReply != null)
            {
                lblReplyMode.Visible = false;
                btnCancelReply.Visible = false;
            }

            if (txtComment != null)
            {
                txtComment.PlaceholderText = "Viết bình luận...  (Enter để gửi, Shift+Enter để xuống dòng)";
            }
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

        // ===== SỰ KIỆN =====

        private async void BtnSendComment_Click(object? sender, EventArgs e)
        {
            await SendCommentAsync();
        }

        /// <summary>
        /// ✅ SỬA: Xử lý Enter và Shift+Enter
        /// </summary>
        private async void TxtComment_KeyDown(object? sender, KeyEventArgs e)
        {
            if (txtComment == null) return;

            // Shift+Enter: Xuống dòng (mặc định)
            if (e.KeyCode == Keys.Enter && e.Shift)
            {
                return; // Để TextBox xử lý
            }

            // Enter:  Gửi comment
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtComment.Text))
            {
                e.SuppressKeyPress = true;
                await SendCommentAsync();
            }
        }

        /// <summary>
        /// ✅ SỬA: Gửi comment hoặc reply
        /// </summary>
        private async Task SendCommentAsync()
        {
            if (_commentService == null || txtComment == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            if (string.IsNullOrWhiteSpace(txtComment.Text))
                return;

            try
            {
                // Disable button để tránh spam
                if (btnSendComment != null)
                {
                    btnSendComment.Enabled = false;
                    btnSendComment.Text = "⏳";
                }

                var request = new TaoBinhLuanRequest
                {
                    MaBaiDang = _postId,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    NoiDung = txtComment.Text.Trim(),
                    MaBinhLuanCha = _replyToCommentId
                };

                await _commentService.CreateCommentAsync(request);

                // Clear input
                txtComment.Clear();
                CancelReplyMode();

                // Reload comments
                await RefreshCommentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable button
                if (btnSendComment != null)
                {
                    btnSendComment.Enabled = true;
                    btnSendComment.Text = "➤";
                }
            }
        }

        private async Task LikeCommentAsync(int commentId)
        {
            if (_reactionService == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            try
            {
                var request = new TaoHoacCapNhatReactionBinhLuanRequest
                {
                    MaBinhLuan = commentId,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    LoaiReaction = LoaiReactionEnum.Thich
                };

                await _reactionService.ReactToCommentAsync(request);

                // ✅ SỬA: Chỉ reload 1 lần, không spam
                await RefreshCommentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}