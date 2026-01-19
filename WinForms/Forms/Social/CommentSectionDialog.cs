using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Components.Social;

namespace WinForms.Forms.Social
{
    /// <summary>
    /// Dialog hi?n th? danh sách bình lu?n v?i reactions
    /// </summary>
    public partial class CommentSectionDialog : Form
    {
        private readonly ICommentService _commentService;
        private readonly IReactionBinhLuanService _reactionBinhLuanService;
        private readonly int _postId;

        // Reply state
        private int? _replyingToCommentId = null;
        private string? _replyingToUserName = null;

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Button? btnClose;
        private Panel? pnlCommentInput;
        private Panel? pnlReplyIndicator;
        private Label? lblReplyingTo;
        private Button? btnCancelReply;
        private TextBox? txtComment;
        private Button? btnSend;
        private FlowLayoutPanel? flowComments;

        public CommentSectionDialog(
            int postId,
            ICommentService commentService,
            IReactionBinhLuanService reactionBinhLuanService)
        {
            InitializeComponent();

            _postId = postId;
            _commentService = commentService;
            _reactionBinhLuanService = reactionBinhLuanService;

            InitializeControls();
            _ = LoadCommentsAsync(); // ⭐ Fire-and-forget pattern (constructor không thể async)
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Bình luận";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(24, 119, 242)
            };

            lblTitle = new Label
            {
                Text = "bình luận",
                Location = new Point(15, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White
            };

            btnClose = new Button
            {
                Text = ". . .",
                Width = 35,
                Height = 35,
                Location = new Point(this.Width - 50, 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);

            // ===== COMMENT INPUT =====
            pnlCommentInput = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Reply indicator panel
            pnlReplyIndicator = new Panel
            {
                Location = new Point(10, 5),
                Width = this.Width - 30,
                Height = 25,
                BackColor = Color.FromArgb(240, 242, 245),
                Visible = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            lblReplyingTo = new Label
            {
                Location = new Point(5, 4),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 100, 100),
                Text = "Đang trả lời..."
            };

            btnCancelReply = new Button
            {
                Text = "✕",
                Size = new Size(20, 20),
                Location = new Point(pnlReplyIndicator.Width - 25, 2),
                Anchor = AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                ForeColor = Color.Gray
            };
            btnCancelReply.FlatAppearance.BorderSize = 0;
            btnCancelReply.Click += BtnCancelReply_Click;

            pnlReplyIndicator.Controls.Add(lblReplyingTo);
            pnlReplyIndicator.Controls.Add(btnCancelReply);

            txtComment = new TextBox
            {
                Location = new Point(10, 35),
                Width = this.Width - 110,
                Height = 30,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Viết bình luận..."
            };
            txtComment.KeyDown += TxtComment_KeyDown;

            btnSend = new Button
            {
                Text = "➤",
                Width = 80,
                Height = 30,
                Location = new Point(this.Width - 95, 35),
                Anchor = AnchorStyles.Right,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += BtnSend_Click;

            pnlCommentInput.Controls.Add(pnlReplyIndicator);
            pnlCommentInput.Controls.Add(txtComment);
            pnlCommentInput.Controls.Add(btnSend);

            // ===== COMMENTS AREA =====
            flowComments = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10)
            };

            // ===== ADD TO FORM =====
            this.Controls.Add(flowComments);
            this.Controls.Add(pnlCommentInput);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        private async Task LoadCommentsAsync()
        {
            if (flowComments == null) return;

            try
            {
                flowComments.Controls.Clear();

                var lblLoading = new Label
                {
                    Text = "đang tải bình luận...",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(10, 20, 10, 10)
                };
                flowComments.Controls.Add(lblLoading);

                var comments = await _commentService.GetCommentsByPostAsync(_postId);

                flowComments.Controls.Clear();

                if (comments == null || !comments.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "chưa có bình luận nào.\nHãy là người đầu tiên bình luận!",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowComments.Controls.Add(lblEmpty);
                    return;
                }

                // ⭐ Tách comments thành parent và replies
                var parentComments = comments
                    .Where(c => c.MaBinhLuanCha == null)
                    .OrderByDescending(c => c.ThoiGianTao)  // Parent mới nhất lên trên
                    .ToList();
                    
                var replyComments = comments
                    .Where(c => c.MaBinhLuanCha != null)
                    .OrderBy(c => c.ThoiGianTao)  // Reply cũ nhất lên trước
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"=== LoadComments Debug ===");
                System.Diagnostics.Debug.WriteLine($"Total comments: {comments.Count}");
                System.Diagnostics.Debug.WriteLine($"Parent comments: {parentComments.Count}");
                System.Diagnostics.Debug.WriteLine($"Reply comments: {replyComments.Count}");

                foreach (var comment in parentComments)
                {
                    // Thêm comment cha
                    await AddCommentCardAsync(comment, isReply: false);

                    // ⭐ Thêm TẤT CẢ các replies của comment này
                    var replies = replyComments
                        .Where(r => r.MaBinhLuanCha == comment.MaBinhLuan)
                        .ToList();
                        
                    System.Diagnostics.Debug.WriteLine($"Comment {comment.MaBinhLuan} has {replies.Count} replies");
                    
                    foreach (var reply in replies)
                    {
                        await AddCommentCardAsync(reply, isReply: true);
                    }
                }
            }
            catch (Exception ex)
            {
                flowComments.Controls.Clear();
                System.Diagnostics.Debug.WriteLine($"Error loading comments: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AddCommentCardAsync(BinhLuanBaiDangResponse comment, bool isReply)
        {
            if (flowComments == null) return;

            var commentCard = new CommentCardControl(_commentService, _reactionBinhLuanService)
            {
                Width = isReply ? flowComments.Width - 70 : flowComments.Width - 30,
                Margin = isReply ? new Padding(40, 5, 5, 5) : new Padding(5),
                BackColor = isReply ? Color.FromArgb(248, 249, 250) : Color.White  // ⭐ Reply có màu khác
            };

            commentCard.LoadComment(comment);

            // Event handlers
            commentCard.OnReplyClicked += (commentId) =>
            {
                // ⭐ Nếu là reply thì reply về comment cha gốc
                var replyToId = comment.MaBinhLuanCha ?? commentId;
                var replyToName = comment.HoVaTen ?? comment.TenDangNhap ?? "Người dùng";
                
                System.Diagnostics.Debug.WriteLine($"Reply clicked: CommentId={commentId}, ParentId={replyToId}, Name={replyToName}");
                
                SetReplyMode(replyToId, replyToName);
            };

            commentCard.OnEditClicked += async (commentId) =>
            {
                var editDialog = new InputDialog("Chỉnh sửa bình luận", comment.NoiDung ?? "");
                if (editDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(editDialog.InputText))
                {
                    try
                    {
                        await _commentService.UpdateCommentAsync(commentId, new CapNhatBinhLuanRequest
                        {
                            NoiDung = editDialog.InputText
                        });

                        await LoadCommentsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            commentCard.OnDeleteClicked += async (commentId) =>
            {
                var result = MessageBox.Show(
                    "Bạn có chắc muốn xoá bình luận này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _commentService.DeleteCommentAsync(commentId);
                        await LoadCommentsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            flowComments.Controls.Add(commentCard);

            // Delay để tránh DbContext conflict
            await System.Threading.Tasks.Task.Delay(50);
        }

        private async void BtnSend_Click(object? sender, EventArgs e)
        {
            await SendCommentAsync();
        }

        private async void TxtComment_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await SendCommentAsync();
            }
        }

        private async Task SendCommentAsync()
        {
            if (txtComment == null || string.IsNullOrWhiteSpace(txtComment.Text))
                return;

            if (!UserSession.IsLoggedIn || UserSession.CurrentUser == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để bình luận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var content = txtComment.Text.Trim();
                txtComment.Enabled = false;
                btnSend!.Enabled = false;

                await _commentService.CreateCommentAsync(new TaoBinhLuanRequest
                {
                    MaBaiDang = _postId,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    NoiDung = content,
                    MaBinhLuanCha = _replyingToCommentId
                });

                txtComment.Clear();
                CancelReplyMode();
                await LoadCommentsAsync(); // ⭐ THÊM AWAIT để đợi reload comments
            }
            catch (Exception ex)
            {
                MessageBox.Show($"lỗi khi gửi bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtComment.Enabled = true;
                btnSend!.Enabled = true;
                txtComment.Focus();
            }
        }

        private void SetReplyMode(int commentId, string userName)
        {
            _replyingToCommentId = commentId;
            _replyingToUserName = userName;

            if (lblReplyingTo != null)
                lblReplyingTo.Text = $"↩ Đang trả lời {userName}";

            if (pnlReplyIndicator != null)
                pnlReplyIndicator.Visible = true;

            if (txtComment != null)
            {
                txtComment.PlaceholderText = $"Trả lời {userName}...";
                txtComment.Focus();
            }
        }

        private void CancelReplyMode()
        {
            _replyingToCommentId = null;
            _replyingToUserName = null;

            if (pnlReplyIndicator != null)
                pnlReplyIndicator.Visible = false;

            if (txtComment != null)
                txtComment.PlaceholderText = "Viết bình luận...";
        }

        private void BtnCancelReply_Click(object? sender, EventArgs e)
        {
            CancelReplyMode();
        }
    }

    /// <summary>
    /// Simple input dialog for editing comments
    /// </summary>
    public class InputDialog : Form
    {
        public string InputText { get; private set; } = string.Empty;

        private TextBox? txtInput;
        private Button? btnOK;
        private Button? btnCancel;

        public InputDialog(string title, string defaultText)
        {
            this.Text = title;
            this.Size = new Size(400, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            txtInput = new TextBox
            {
                Text = defaultText,
                Location = new Point(10, 10),
                Width = 360,
                Height = 60,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            btnOK = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(200, 80),
                Width = 80
            };
            btnOK.Click += (s, e) => InputText = txtInput.Text;

            btnCancel = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                Location = new Point(290, 80),
                Width = 80
            };

            this.Controls.Add(txtInput);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}
