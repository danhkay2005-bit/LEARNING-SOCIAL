using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Linq;
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

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private Button? btnClose;
        private Panel? pnlCommentInput;
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
            LoadCommentsAsync();
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
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            txtComment = new TextBox
            {
                Location = new Point(10, 15),
                Width = this.Width - 110,
                Height = 30,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Viết bình luận..."
            };
            txtComment.KeyDown += TxtComment_KeyDown;

            btnSend = new Button
            {
                Text = "??",
                Width = 80,
                Height = 30,
                Location = new Point(this.Width - 95, 15),
                Anchor = AnchorStyles.Right,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += BtnSend_Click;

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

        private async void LoadCommentsAsync()
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

                foreach (var comment in comments)
                {
                    var commentCard = new CommentCardControl(_commentService, _reactionBinhLuanService)
                    {
                        Width = flowComments.Width - 30,
                        Margin = new Padding(5)
                    };

                    commentCard.LoadComment(comment);

                    // Event handlers
                    commentCard.OnReplyClicked += (commentId) =>
                    {
                        MessageBox.Show($"Trả lời bình luận #{commentId}", "Reply", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // TODO: Implement reply functionality
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

                                LoadCommentsAsync();
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
                            "bạn có chắc muốn xoá bình luận này",
                            "Xác nhận",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                await _commentService.DeleteCommentAsync(commentId);
                                LoadCommentsAsync();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    };

                    flowComments.Controls.Add(commentCard);

                    // ✅ FIX: Delay để tránh DbContext conflict
                    await System.Threading.Tasks.Task.Delay(50);
                }
            }
            catch (Exception ex)
            {
                flowComments.Controls.Clear();
                MessageBox.Show($"Lỗi khi tải bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    NoiDung = content
                });

                txtComment.Clear();
                LoadCommentsAsync();
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
