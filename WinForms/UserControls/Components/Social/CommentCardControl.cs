using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Helpers;

namespace WinForms.UserControls.Components.Social
{
    /// <summary>
    /// SIMPLIFIED - Card hi?n th? m?t bình lu?n v?i reaction
    /// </summary>
    public partial class CommentCardControl : UserControl
    {
        private readonly ICommentService? _commentService;
        private readonly IReactionBinhLuanService? _reactionBinhLuanService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;

        private BinhLuanBaiDangResponse? _comment;
        private LoaiReactionEnum? _currentReaction = null;

        // Controls
        private PictureBox? pbAvatar;
        private Label? lblAuthorName;
        private Label? lblTimestamp;
        private Button? btnMenu;
        private Label? lblContent;
        private Label? lblReactionCount;
        private Button? btnReact;
        private Button? btnReply;
        private Panel? pnlReactionPicker;

        // Events
        public event Action<int>? OnReplyClicked;
        public event Action<int>? OnEditClicked;
        public event Action<int>? OnDeleteClicked;

        public CommentCardControl()
        {
            InitializeComponent();
        }

        public CommentCardControl(ICommentService commentService, IReactionBinhLuanService reactionBinhLuanService) : this()
        {
            _commentService = commentService;
            _reactionBinhLuanService = reactionBinhLuanService;
            InitializeControls();
        }

        private void InitializeControls()
        {
            this.SuspendLayout();

            this.Size = new Size(560, 150);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Padding = new Padding(10);

            pbAvatar = new PictureBox { Location = new Point(10, 10), Size = new Size(40, 40), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.LightGray, BorderStyle = BorderStyle.FixedSingle };
            lblAuthorName = new Label { Location = new Point(60, 12), AutoSize = true, Text = "Author", Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 119, 242) };
            lblTimestamp = new Label { Location = new Point(60, 32), AutoSize = true, Text = "Just now", Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray };
            btnMenu = new Button { Location = new Point(510, 10), Size = new Size(30, 30), Text = "?", Font = new Font("Segoe UI", 14F, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, BackColor = Color.Transparent, Visible = false };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Click += BtnMenu_Click;

            lblContent = new Label { Location = new Point(60, 55), Size = new Size(480, 40), Text = "Comment content...", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, AutoEllipsis = true };
            lblReactionCount = new Label { Location = new Point(60, 100), AutoSize = true, Text = "", Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray, Cursor = Cursors.Hand, Visible = false };
            lblReactionCount.Click += LblReactionCount_Click;

            btnReact = new Button { Location = new Point(60, 120), Size = new Size(85, 24), Text = "?? Thích", Font = new Font("Segoe UI", 8F), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, BackColor = Color.White, ForeColor = Color.FromArgb(100, 100, 100), TabStop = false };
            btnReact.FlatAppearance.BorderSize = 0;
            btnReact.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 242, 245);
            btnReact.Click += BtnReact_Click;
            btnReact.MouseEnter += BtnReact_MouseEnter;
            btnReact.MouseLeave += BtnReact_MouseLeave;

            btnReply = new Button { Location = new Point(155, 120), Size = new Size(85, 24), Text = "?? Tr? l?i", Font = new Font("Segoe UI", 8F), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, BackColor = Color.White, ForeColor = Color.FromArgb(100, 100, 100), TabStop = false };
            btnReply.FlatAppearance.BorderSize = 0;
            btnReply.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 242, 245);
            btnReply.Click += BtnReply_Click;

            this.Controls.Add(pbAvatar);
            this.Controls.Add(lblAuthorName);
            this.Controls.Add(lblTimestamp);
            this.Controls.Add(btnMenu);
            this.Controls.Add(lblContent);
            this.Controls.Add(lblReactionCount);
            this.Controls.Add(btnReact);
            this.Controls.Add(btnReply);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void LoadComment(BinhLuanBaiDangResponse comment)
        {
            _comment = comment;
            RenderComment();
            LoadCurrentReactionAsync();
        }

        private void RenderComment()
        {
            if (_comment == null) return;
            if (pbAvatar != null) AvatarHelper.SetAvatar(pbAvatar, null, "U");
            if (lblAuthorName != null) lblAuthorName.Text = "Ng??i dùng";
            if (lblTimestamp != null && _comment.ThoiGianTao.HasValue) lblTimestamp.Text = GetRelativeTime(_comment.ThoiGianTao.Value);
            if (lblContent != null) lblContent.Text = _comment.NoiDung ?? "";
            UpdateReactionDisplay();
            if (btnMenu != null && UserSession.CurrentUser != null) btnMenu.Visible = (_comment.MaNguoiDung == UserSession.CurrentUser.MaNguoiDung);
        }

        private async void LoadCurrentReactionAsync()
        {
            if (_comment == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null || _reactionBinhLuanService == null) return;
            try
            {
                var myReaction = await _reactionBinhLuanService.KiemTraReactionCuaNguoiDungAsync(_comment.MaBinhLuan, UserSession.CurrentUser.MaNguoiDung);
                if (myReaction != null && btnReact != null)
                {
                    _currentReaction = myReaction.LoaiReaction;
                    btnReact.Text = $"{GetEmojiFromReactionType(myReaction.LoaiReaction)} {GetReactionName(myReaction.LoaiReaction)}";
                    btnReact.BackColor = Color.FromArgb(230, 240, 255);
                    btnReact.ForeColor = Color.FromArgb(24, 119, 242);
                }
            }
            catch { }
        }

        private void UpdateReactionDisplay()
        {
            if (_comment == null || lblReactionCount == null) return;
            if (_comment.SoLuotReactions > 0) { lblReactionCount.Text = $"?? {_comment.SoLuotReactions} ng??i ?ã th? c?m xúc"; lblReactionCount.Visible = true; } else { lblReactionCount.Visible = false; }
        }

        private void BtnReact_MouseEnter(object? sender, EventArgs e) { if (pnlReactionPicker == null || !pnlReactionPicker.Visible) ShowReactionPicker(); }
        private void BtnReact_MouseLeave(object? sender, EventArgs e) { Task.Delay(200).ContinueWith(_ => { if (this.IsHandleCreated && pnlReactionPicker != null) { try { this.Invoke(() => { if (pnlReactionPicker != null && !pnlReactionPicker.RectangleToScreen(pnlReactionPicker.ClientRectangle).Contains(Cursor.Position)) HideReactionPicker(); }); } catch { } } }); }

        private void ShowReactionPicker()
        {
            if (btnReact == null) return;
            pnlReactionPicker = new Panel { Location = new Point(btnReact.Left, btnReact.Top - 60), Size = new Size(320, 55), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            var reactions = new[] { (LoaiReactionEnum.Thich, "??"), (LoaiReactionEnum.YeuThich, "??"), (LoaiReactionEnum.Haha, "??"), (LoaiReactionEnum.Wow, "??"), (LoaiReactionEnum.Buon, "??"), (LoaiReactionEnum.TucGian, "??") };
            int x = 5;
            foreach (var (type, emoji) in reactions)
            {
                var btn = new Button { Text = emoji, Size = new Size(48, 48), Location = new Point(x, 3), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 22F), Cursor = Cursors.Hand, Tag = type, BackColor = Color.White, TabStop = false };
                btn.FlatAppearance.BorderSize = 0;
                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(240, 242, 245);
                btn.MouseLeave += (s, e) => btn.BackColor = Color.White;
                btn.Click += (s, e) => HandleReactionClick(type);
                pnlReactionPicker.Controls.Add(btn);
                x += 52;
            }
            var hideTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            hideTimer.Tick += (s, e) => { hideTimer.Stop(); HideReactionPicker(); };
            pnlReactionPicker.MouseEnter += (s, e) => hideTimer.Stop();
            pnlReactionPicker.MouseLeave += (s, e) => hideTimer.Start();
            this.Controls.Add(pnlReactionPicker);
            pnlReactionPicker.BringToFront();
        }

        private void HideReactionPicker() { if (pnlReactionPicker != null) { this.Controls.Remove(pnlReactionPicker); pnlReactionPicker.Dispose(); pnlReactionPicker = null; } }
        private void BtnReact_Click(object? sender, EventArgs e) => HandleReactionClick(_currentReaction ?? LoaiReactionEnum.Thich);

        private async void HandleReactionClick(LoaiReactionEnum reactionType)
        {
            if (_comment == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null || _reactionBinhLuanService == null) return;
            HideReactionPicker();
            try
            {
                if (_currentReaction == reactionType)
                {
                    await _reactionBinhLuanService.XoaReactionAsync(new XoaReactionBinhLuanRequest { MaBinhLuan = _comment.MaBinhLuan, MaNguoiDung = UserSession.CurrentUser.MaNguoiDung });
                    _currentReaction = null;
                    if (btnReact != null) { btnReact.Text = "?? Thích"; btnReact.BackColor = Color.White; btnReact.ForeColor = Color.FromArgb(100, 100, 100); }
                    if (_comment.SoLuotReactions > 0) _comment.SoLuotReactions--;
                }
                else
                {
                    await _reactionBinhLuanService.TaoHoacCapNhatReactionAsync(new TaoHoacCapNhatReactionBinhLuanRequest { MaBinhLuan = _comment.MaBinhLuan, MaNguoiDung = UserSession.CurrentUser.MaNguoiDung, LoaiReaction = reactionType });
                    var oldReaction = _currentReaction;
                    _currentReaction = reactionType;
                    if (btnReact != null) { btnReact.Text = $"{GetEmojiFromReactionType(reactionType)} {GetReactionName(reactionType)}"; btnReact.BackColor = Color.FromArgb(230, 240, 255); btnReact.ForeColor = Color.FromArgb(24, 119, 242); }
                    if (oldReaction == null) _comment.SoLuotReactions++;
                }
                UpdateReactionDisplay();
            }
            catch (Exception ex) { MessageBox.Show($"L?i: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnMenu_Click(object? sender, EventArgs e) { if (_comment == null || btnMenu == null) return; var menu = new ContextMenuStrip(); menu.Items.Add("?? Ch?nh s?a", null, (s, ev) => OnEditClicked?.Invoke(_comment.MaBinhLuan)); menu.Items.Add("??? Xóa", null, (s, ev) => OnDeleteClicked?.Invoke(_comment.MaBinhLuan)); menu.Show(btnMenu, new Point(0, btnMenu.Height)); }
        private void BtnReply_Click(object? sender, EventArgs e) { if (_comment != null) OnReplyClicked?.Invoke(_comment.MaBinhLuan); }
        private void LblReactionCount_Click(object? sender, EventArgs e) { if (_comment == null) return; MessageBox.Show($"{_comment.SoLuotReactions} ng??i ?ã th? c?m xúc", "Danh sách", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        private string GetEmojiFromReactionType(LoaiReactionEnum type) => type switch { LoaiReactionEnum.Thich => "??", LoaiReactionEnum.YeuThich => "??", LoaiReactionEnum.Haha => "??", LoaiReactionEnum.Wow => "??", LoaiReactionEnum.Buon => "??", LoaiReactionEnum.TucGian => "??", _ => "??" };
        private string GetReactionName(LoaiReactionEnum type) => type switch { LoaiReactionEnum.Thich => "Thích", LoaiReactionEnum.YeuThich => "Yêu thích", LoaiReactionEnum.Haha => "Haha", LoaiReactionEnum.Wow => "Wow", LoaiReactionEnum.Buon => "Bu?n", LoaiReactionEnum.TucGian => "T?c gi?n", _ => "Thích" };
        private string GetRelativeTime(DateTime dateTime) { var timeSpan = DateTime.Now - dateTime; if (timeSpan.TotalMinutes < 1) return "V?a xong"; if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} phút tr??c"; if (timeSpan.TotalHours < 24) return $"{(int)timeSpan.TotalHours} gi? tr??c"; if (timeSpan.TotalDays < 7) return $"{(int)timeSpan.TotalDays} ngày tr??c"; return dateTime.ToString("dd/MM/yyyy"); }
    }
}
