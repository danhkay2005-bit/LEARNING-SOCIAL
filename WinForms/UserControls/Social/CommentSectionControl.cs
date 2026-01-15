using StudyApp.BLL.Interfaces.Social;
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

namespace WinForms.UserControls.Social
{
    public partial class CommentSectionControl : UserControl
    {
        private readonly ICommentService? _commentService;
        private readonly IReactionService? _reactionService;

        private int _postId;
        private List<BinhLuanBaiDangResponse> _comments = new List<BinhLuanBaiDangResponse>();

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Button? btnClose;

        private FlowLayoutPanel? flowComments;

        private Panel? pnlInputArea;
        private PictureBox? pbUserAvatar;
        private TextBox? txtComment;
        private Button? btnSendComment;

        // Constructor mặc định
        public CommentSectionControl()
        {
            InitializeComponent();
        }

        // Constructor với DI
        public CommentSectionControl(
            ICommentService commentService,
            IReactionService reactionService) : this()
        {
            _commentService = commentService;
            _reactionService = reactionService;

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

            // ===== INPUT AREA =====
            pnlInputArea = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            pbUserAvatar = new PictureBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(10, 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtComment = new TextBox
            {
                Location = new Point(60, 15),
                Width = this.Width - 140,
                Height = 40,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                PlaceholderText = "Viết bình luận..."
            };
            txtComment.KeyDown += TxtComment_KeyDown;

            btnSendComment = new Button
            {
                Text = "➤",
                Location = new Point(this.Width - 60, 15),
                Width = 40,
                Height = 40,
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

            pnlInputArea.Controls.Add(pbUserAvatar);
            pnlInputArea.Controls.Add(txtComment);
            pnlInputArea.Controls.Add(btnSendComment);

            // ===== ADD TO CONTROL =====
            this.Controls.Add(flowComments);
            this.Controls.Add(pnlInputArea);
            this.Controls.Add(pnlHeader);

            this.BackColor = Color.White;
            this.Size = new Size(600, 500);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Refresh danh sách comments
        /// </summary>
        private async Task RefreshCommentsAsync()
        {
            if (_commentService == null) return;

            try
            {
                _comments = await _commentService.GetCommentsByPostAsync(_postId);

                RenderComments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách comments
        /// </summary>
        private void RenderComments()
        {
            flowComments?.Controls.Clear();

            if (_comments == null || !_comments.Any())
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa có bình luận nào.  Hãy là người đầu tiên bình luận! ",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 20, 10, 10)
                };
                flowComments?.Controls.Add(lblEmpty);
                return;
            }

            foreach (var comment in _comments)
            {
                var commentCard = CreateCommentCard(comment);
                flowComments?.Controls.Add(commentCard);
            }

            // Update title
            if (lblTitle != null)
                lblTitle.Text = $"Bình luận ({_comments.Count})";
        }

        /// <summary>
        /// Tạo 1 comment card
        /// </summary>
        private Panel CreateCommentCard(BinhLuanBaiDangResponse comment)
        {
            var pnlComment = new Panel
            {
                Width = 560,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(5),
                BackColor = Color.White
            };

            var pbAvatar = new PictureBox
            {
                Width = 35,
                Height = 35,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblUsername = new Label
            {
                Text = "Người dùng", // TODO: Load from UserService
                Location = new Point(55, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

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

            var btnLike = new Label
            {
                Text = $"👍 {comment.SoLuotReactions}",
                Location = new Point(150, lblContent.Bottom + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            btnLike.Click += async (s, e) => await LikeCommentAsync(comment.MaBinhLuan);

            pnlComment.Controls.Add(pbAvatar);
            pnlComment.Controls.Add(lblUsername);
            pnlComment.Controls.Add(lblContent);
            pnlComment.Controls.Add(lblTime);
            pnlComment.Controls.Add(btnLike);

            pnlComment.Height = lblTime.Bottom + 15;

            return pnlComment;
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

        private async void TxtComment_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtComment?.Text))
            {
                e.SuppressKeyPress = true;
                await SendCommentAsync();
            }
        }

        private async Task SendCommentAsync()
        {
            if (_commentService == null || txtComment == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            if (string.IsNullOrWhiteSpace(txtComment.Text))
                return;

            try
            {
                var request = new TaoBinhLuanRequest
                {
                    MaBaiDang = _postId,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    NoiDung = txtComment.Text.Trim(),
                    MaBinhLuanCha = null
                };

                await _commentService.CreateCommentAsync(request);

                // Clear input
                txtComment.Clear();

                // Reload comments
                await RefreshCommentsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Reload comments
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