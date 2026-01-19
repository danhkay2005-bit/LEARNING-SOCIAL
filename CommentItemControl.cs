// File: WinForms\UserControls\Components\Social\CommentItemControl.cs

public partial class CommentItemControl : UserControl
{
    private readonly ICommentService _commentService;
    private BinhLuanBaiDangResponse? _comment;
    private int _indentLevel = 0; // Mức độ thụt lề (0 = top-level, 1+ = replies)

    // UI Controls
    private Panel? pnlContainer;
    private PictureBox? pbAvatar;
    private Label? lblAuthorName;
    private RichTextBox? rtbContent;
    private Label? lblTimestamp;
    private Button? btnReply;
    private Button? btnShowReplies;
    private Label? lblReplyCount;
    private FlowLayoutPanel? flpReplies; // Container chứa nested replies

    public CommentItemControl(ICommentService commentService)
    {
        _commentService = commentService;
        InitializeComponent();
        InitializeControls();
    }

    public void LoadComment(BinhLuanBaiDangResponse comment, int indentLevel = 0)
    {
        _comment = comment;
        _indentLevel = indentLevel;
        RenderComment();
        LoadRepliesCount();
    }

    private void InitializeControls()
    {
        // ===== CONTAINER với INDENT =====
        pnlContainer = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(15 + (_indentLevel * 30), 10, 15, 10), // ⭐ Indent
            AutoSize = true
        };

        // ===== AVATAR =====
        pbAvatar = new PictureBox
        {
            Width = 35,
            Height = 35,
            Location = new Point(10, 10),
            SizeMode = PictureBoxSizeMode.StretchImage,
            BorderStyle = BorderStyle.FixedSingle
        };

        // ===== AUTHOR NAME =====
        lblAuthorName = new Label
        {
            Location = new Point(55, 10),
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(24, 119, 242)
        };

        // ===== CONTENT (RichTextBox for @mention highlight) =====
        rtbContent = new RichTextBox
        {
            Location = new Point(55, 30),
            Width = 450,
            BorderStyle = BorderStyle.None,
            ReadOnly = true,
            Font = new Font("Segoe UI", 9F),
            BackColor = Color.White
        };

        // ===== TIMESTAMP =====
        lblTimestamp = new Label
        {
            Location = new Point(55, 80),
            AutoSize = true,
            Font = new Font("Segoe UI", 7.5F),
            ForeColor = Color.Gray
        };

        // ===== REPLY BUTTON =====
        btnReply = new Button
        {
            Text = "Trả lời",
            Location = new Point(150, 78),
            Width = 60,
            Height = 20,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 7.5F),
            Cursor = Cursors.Hand
        };
        btnReply.FlatAppearance.BorderSize = 0;
        btnReply.Click += BtnReply_Click;

        // ===== SHOW REPLIES BUTTON =====
        btnShowReplies = new Button
        {
            Text = "Xem phản hồi",
            Location = new Point(220, 78),
            Width = 80,
            Height = 20,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(24, 119, 242),
            Cursor = Cursors.Hand,
            Visible = false // Hiện khi có replies
        };
        btnShowReplies.FlatAppearance.BorderSize = 0;
        btnShowReplies.Click += BtnShowReplies_Click;

        // ===== REPLY COUNT LABEL =====
        lblReplyCount = new Label
        {
            Location = new Point(310, 80),
            AutoSize = true,
            Font = new Font("Segoe UI", 7.5F),
            ForeColor = Color.Gray,
            Visible = false
        };

        // ===== REPLIES CONTAINER =====
        flpReplies = new FlowLayoutPanel
        {
            Location = new Point(30, 110), // ⭐ Indent
            Width = 490,
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Visible = false
        };

        // Add controls to container
        pnlContainer.Controls.AddRange(new Control[] {
            pbAvatar, lblAuthorName, rtbContent, lblTimestamp,
            btnReply, btnShowReplies, lblReplyCount, flpReplies
        });

        this.Controls.Add(pnlContainer);
    }

    private void RenderComment()
    {
        if (_comment == null) return;

        // Avatar
        if (!string.IsNullOrEmpty(_comment.HinhDaiDien))
        {
            try
            {
                pbAvatar.Image = Image.FromFile(_comment.HinhDaiDien);
            }
            catch
            {
                pbAvatar.BackColor = Color.LightGray;
            }
        }

        // Author name
        lblAuthorName.Text = _comment.HoVaTen ?? _comment.TenDangNhap ?? "Unknown";

        // Content với highlight @mention
        rtbContent.Text = _comment.NoiDung;
        HighlightMentions();

        // Timestamp
        lblTimestamp.Text = FormatTimeAgo(_comment.ThoiGianTao);
    }

    private async void LoadRepliesCount()
    {
        if (_comment == null) return;

        try
        {
            var count = await _commentService.CountRepliesAsync(_comment.MaBinhLuan);

            if (count > 0)
            {
                lblReplyCount.Text = $"({count} phản hồi)";
                lblReplyCount.Visible = true;
                btnShowReplies.Visible = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi load replies: {ex.Message}");
        }
    }

    // ⭐⭐⭐ REPLY BUTTON CLICK ⭐⭐⭐
    private void BtnReply_Click(object sender, EventArgs e)
    {
        if (_comment == null || !UserSession.IsLoggedIn) return;

        // Show reply dialog
        var replyDialog = new ReplyCommentDialog(_comment);
        if (replyDialog.ShowDialog() == DialogResult.OK)
        {
            // Send reply
            CreateReplyAsync(replyDialog.ReplyContent);
        }
    }

    private async void CreateReplyAsync(string content)
    {
        if (_comment == null) return;

        try
        {
            var request = new TaoBinhLuanRequest
            {
                MaBaiDang = _comment.MaBaiDang,
                MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                NoiDung = content,
                MaBinhLuanCha = _comment.MaBinhLuan // ⭐⭐⭐ REPLY!
            };

            var result = await _commentService.CreateCommentAsync(request);

            MessageBox.Show("Đã trả lời bình luận!", "Thành công");

            // Refresh replies
            await LoadRepliesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi reply: {ex.Message}", "Lỗi");
        }
    }

    // ⭐⭐⭐ SHOW REPLIES BUTTON CLICK ⭐⭐⭐
    private async void BtnShowReplies_Click(object sender, EventArgs e)
    {
        if (flpReplies.Visible)
        {
            // Hide replies
            flpReplies.Visible = false;
            btnShowReplies.Text = "Xem phản hồi";
        }
        else
        {
            // Show replies
            await LoadRepliesAsync();
            flpReplies.Visible = true;
            btnShowReplies.Text = "Ẩn phản hồi";
        }
    }

    private async Task LoadRepliesAsync()
    {
        if (_comment == null) return;

        try
        {
            flpReplies.Controls.Clear();

            var repliesResult = await _commentService.GetRepliesAsync(
                _comment.MaBinhLuan,
                page: 1,
                pageSize: 10
            );

            foreach (var reply in repliesResult.Comments)
            {
                var replyControl = Program.ServiceProvider
                    .GetRequiredService<CommentItemControl>();

                replyControl.LoadComment(reply, _indentLevel + 1); // ⭐ Tăng indent
                replyControl.Width = flpReplies.Width - 10;

                flpReplies.Controls.Add(replyControl);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi load replies: {ex.Message}");
        }
    }

    private void HighlightMentions()
    {
        // Highlight @username with blue color
        var regex = new Regex(@"@\w+");
        var matches = regex.Matches(rtbContent.Text);

        foreach (Match match in matches)
        {
            rtbContent.Select(match.Index, match.Length);
            rtbContent.SelectionColor = Color.FromArgb(24, 119, 242);
            rtbContent.SelectionFont = new Font(rtbContent.Font, FontStyle.Bold);
        }

        rtbContent.Select(0, 0); // Deselect
    }

    private string FormatTimeAgo(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return "Unknown";

        var timeSpan = DateTime.Now - dateTime.Value;

        if (timeSpan.TotalMinutes < 1) return "vừa xong";
        if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} phút trước";
        if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} giờ trước";
        if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} ngày trước";

        return dateTime.Value.ToString("dd/MM/yyyy");
    }
}