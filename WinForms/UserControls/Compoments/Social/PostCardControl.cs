using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.Forms.Social;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls.Components.Social
{
    public partial class PostCardControl : UserControl
    {
        private static readonly SemaphoreSlim _dbSemaphore = new SemaphoreSlim(2, 2);

        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;
        private readonly IReactionBaiDangService? _reactionBaiDangService;
        private readonly IChiaSeBaiDangService? _chiaSeBaiDangService;
        private readonly IThachDauService? _thachDauService;

        private BaiDangResponse? _post;
        private LoaiReactionEnum? _currentReaction = null;
        private CancellationTokenSource? _reactionCancellation;
        private LoaiReactionEnum? _pendingReaction = null;

        #region UI Controls
        private Panel? pnlContainer;
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblAuthorName;
        private Label? lblTimestamp;
        private Button? btnMenu;
        private Panel? pnlContent;
        private RichTextBox? rtbContent;
        private PictureBox? pbImage;
        private Button? btnJoinChallenge; // Nút tham gia trận đấu từ Newsfeed
        private Panel? pnlStats;
        private Label? lblReactionCount;
        private Label? lblCommentCount;
        private Panel? pnlActions;
        private Button? btnLike;
        private Button? btnComment;
        private Button? btnShare;
        private Panel? pnlReactionPicker;

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

            // ✅ Lấy các service bổ sung từ ServiceProvider
            if (Program.ServiceProvider != null)
            {
                _reactionBaiDangService = Program.ServiceProvider.GetService(typeof(IReactionBaiDangService)) as IReactionBaiDangService;
                _chiaSeBaiDangService = Program.ServiceProvider.GetService(typeof(IChiaSeBaiDangService)) as IChiaSeBaiDangService;
                _thachDauService = Program.ServiceProvider.GetService(typeof(IThachDauService)) as IThachDauService;
            }

            InitializeControls();
        }

        public void LoadPost(BaiDangResponse post)
        {
            _post = post;
            RenderPost();
            LoadCurrentReactionAsync(); // ✅ THÊM: Load reaction hiện tại
        }

        /// <summary>
        /// ✅ THÊM: Load reaction hiện tại của user
        /// </summary>
        private async void LoadCurrentReactionAsync()
        {
            if (_post == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null || _reactionBaiDangService == null)
                return;

            // ✅ FIX: Chờ lượt để tránh DbContext conflict
            await _dbSemaphore.WaitAsync();

            try
            {
                var myReaction = await _reactionBaiDangService.KiemTraReactionCuaNguoiDungAsync(
                    _post.MaBaiDang,
                    UserSession.CurrentUser.MaNguoiDung
                );

                if (myReaction != null && btnLike != null)
                {
                    _currentReaction = myReaction.LoaiReaction;
                    var emoji = GetEmojiFromReactionType(myReaction.LoaiReaction);
                    btnLike.Text = $"{emoji} {GetReactionName(myReaction.LoaiReaction)}";
                    btnLike.BackColor = Color.FromArgb(230, 240, 255);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi load reaction: {ex.Message}");
            }
            finally
            {
                // ✅ QUAN TRỌNG: Luôn release semaphore
                _dbSemaphore.Release();
            }
        }

        /// <summary>
        /// ✅ THÊM: Convert LoaiReactionEnum sang emoji
        /// </summary>
        private string GetEmojiFromReactionType(LoaiReactionEnum reactionType)
        {
            return reactionType switch
            {
                LoaiReactionEnum.Thich => "👍",
                LoaiReactionEnum.YeuThich => "❤️",
                LoaiReactionEnum.Haha => "😂",
                LoaiReactionEnum.Wow => "😮",
                LoaiReactionEnum.Buon => "😢",
                LoaiReactionEnum.TucGian => "😡",
                _ => "👍"
            };
        }

        private void InitializeControls()
        {
            pnlContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15), AutoSize = true };

            // Header
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60 };
            pbAvatar = new PictureBox { Width = 45, Height = 45, Location = new Point(10, 7), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.LightGray, BorderStyle = BorderStyle.FixedSingle };
            lblAuthorName = new Label { Location = new Point(65, 10), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 119, 242) };
            lblTimestamp = new Label { Location = new Point(65, 32), AutoSize = true, Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray };
            btnMenu = new Button { Text = "...", Location = new Point(540, 10), Width = 35, Height = 35, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 16F), Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Click += BtnMenu_Click;

            pnlHeader.Controls.AddRange(new Control[] { pbAvatar, lblAuthorName, lblTimestamp, btnMenu });

            // Content
            pnlContent = new Panel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(10, 5, 10, 10) };
            rtbContent = new RichTextBox { Dock = DockStyle.Top, BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), ReadOnly = true, ScrollBars = RichTextBoxScrollBars.None, DetectUrls = false };
            rtbContent.ContentsResized += (s, e) => rtbContent.Height = e.NewRectangle.Height + 5;
            rtbContent.MouseClick += RtbContent_MouseClick;
            rtbContent.MouseMove += RtbContent_MouseMove;

            pbImage = new PictureBox { Dock = DockStyle.Top, SizeMode = PictureBoxSizeMode.Zoom, MaximumSize = new Size(560, 350), Visible = false };

            // Nút Tham Gia Thách Đấu
            btnJoinChallenge = new Button { Text = "🎮 THAM GIA THÁCH ĐẤU", Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, Visible = false, Margin = new Padding(0, 10, 0, 10) };
            btnJoinChallenge.FlatAppearance.BorderSize = 0;
            btnJoinChallenge.Click += HandleJoinChallengeAsync;

            pnlContent.Controls.AddRange(new Control[] { pbImage, btnJoinChallenge, rtbContent });

            // Stats & Actions
            pnlStats = new Panel { Dock = DockStyle.Top, Height = 35, Padding = new Padding(10, 5, 10, 5) };
            lblReactionCount = new Label { Location = new Point(10, 8), AutoSize = true, Text = "👍 0", ForeColor = Color.Gray };
            lblCommentCount = new Label { Location = new Point(100, 8), AutoSize = true, Text = "💬 0 bình luận", ForeColor = Color.Gray };
            pnlStats.Controls.AddRange(new Control[] { lblReactionCount, lblCommentCount });

            pnlActions = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.WhiteSmoke };
            btnLike = CreateActionButton("👍 Thích", 10);
            btnComment = CreateActionButton("💬 Bình luận", 200);
            btnShare = CreateActionButton("↗️ Chia sẻ", 390);

            btnLike.MouseEnter += BtnLike_MouseEnter;
            btnLike.Click += BtnLike_Click;
            btnComment.Click += BtnComment_Click;
            btnShare.Click += BtnShare_Click;
            pnlActions.Controls.AddRange(new Control[] { btnLike, btnComment, btnShare });

            pnlContainer.Controls.AddRange(new Control[] { pnlActions, pnlStats, pnlContent, pnlHeader });
            this.Controls.Add(pnlContainer);

            CreateReactionPicker();
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
            if (_post.LoaiBaiDang == LoaiBaiDangEnum.ChiaSeKhoaHoc) RenderSharedPost();
            else RenderNormalPost();
        }

        /// <summary>
        /// ✅ Render bài đăng bình thường (không phải share)
        /// </summary>
        private async void RenderNormalPost()
        {
            if (_post == null) return;
            LoadAvatarImage(pbAvatar!, _post.HinhDaiDien, _post.TenNguoiDung);
            lblAuthorName!.Text = _post.TenNguoiDung ?? "Người dùng";
            lblTimestamp!.Text = GetRelativeTime(_post.ThoiGianTao ?? DateTime.Now);
            RenderContentWithHashtags(rtbContent!, _post.NoiDung ?? "");

            // Xử lý nút Thách đấu
            if (_post.NoiDung != null && _post.NoiDung.Contains("[CHALLENGE_PIN:"))
            {
                int pin = ExtractIdFromTag(_post.NoiDung, "CHALLENGE_PIN");
                if (pin > 0 && btnJoinChallenge != null)
                {
                    btnJoinChallenge.Visible = true;
                    btnJoinChallenge.BringToFront();
                    await UpdateChallengeButtonStatus(pin);
                }
            }

            if (!string.IsNullOrEmpty(_post.HinhAnh)) { pbImage!.LoadAsync(_post.HinhAnh); pbImage.Visible = true; }
            lblReactionCount!.Text = $"👍 {_post.SoReaction}";
            lblCommentCount!.Text = $"💬 {_post.SoBinhLuan} bình luận";
        }

        /// <summary>
        /// ✅ MỚI: Render bài đăng SHARED (giống Facebook)
        /// </summary>
        private async void RenderSharedPost()
        {
            if (_post == null || _chiaSeBaiDangService == null) return;

            await _dbSemaphore.WaitAsync();

            try
            {
                // ✅ ADD: Check if control is disposed after await
                if (this.IsDisposed || this.Disposing) return;
                
                var shareInfo = await _chiaSeBaiDangService.LayChiTietChiaSeTheoBaiDangMoiAsync(_post.MaBaiDang);

                // ✅ ADD: Check again after second await
                if (this.IsDisposed || this.Disposing) return;

                if (shareInfo == null || shareInfo.BaiDangGoc == null)
                {
                    RenderNormalPost();
                    return;
                }

                // 2. Header: "X đã chia sẻ bài viết"
                if (pbAvatar != null)
                {
                    LoadAvatarImage(pbAvatar, _post.HinhDaiDien, _post.TenNguoiDung);
                }

                if (lblAuthorName != null)
                {
                    lblAuthorName.Text = $"{_post.TenNguoiDung ?? "Người dùng"}";
                }

                if (lblTimestamp != null)
                {
                    lblTimestamp.Text = $"đã chia sẻ • {GetRelativeTime(_post.ThoiGianTao ?? DateTime.Now)}";
                    lblTimestamp.ForeColor = Color.Gray;
                }

                // 3. Nội dung thêm của người share
                if (rtbContent != null)
                {
                    var noiDungShare = shareInfo.NoiDungThem;
                    if (!string.IsNullOrWhiteSpace(noiDungShare))
                    {
                        RenderContentWithHashtags(rtbContent, noiDungShare);
                    }
                    else
                    {
                        rtbContent.Visible = false;
                    }
                }

                // 4. Tạo panel bài gốc (nested)
                var nestedPanel = CreateNestedPostPanel(shareInfo.BaiDangGoc);
                if (pnlContent != null)
                {
                    pnlContent.Controls.Add(nestedPanel);
                    nestedPanel.BringToFront();
                }

                // 5. Stats của bài SHARE (không phải bài gốc)
                if (lblReactionCount != null)
                    lblReactionCount.Text = $"👍 {_post.SoReaction}";

                if (lblCommentCount != null)
                    lblCommentCount.Text = $"💬 {_post.SoBinhLuan} bình luận";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi render shared post: {ex.Message}");
                
                // ✅ ADD: Check before fallback
                if (!this.IsDisposed && !this.Disposing)
                {
                    RenderNormalPost();
                }
            }
            finally
            {
                _dbSemaphore.Release();
            }
        }

        /// <summary>
        /// ✅ MỚI: Tạo panel bài gốc (nested) - Style Facebook
        /// </summary>
        private Panel CreateNestedPostPanel(BaiDangResponse originalPost)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                BackColor = Color.FromArgb(245, 245, 245), // Màu xám nhạt
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12),
                Margin = new Padding(0, 10, 0, 0)
            };

            // ===== HEADER của bài gốc =====
            var pnlNestedHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent
            };

            var pbNestedAvatar = new PictureBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };
            LoadAvatarImage(pbNestedAvatar, originalPost.HinhDaiDien, originalPost.TenNguoiDung);

            var lblNestedAuthor = new Label
            {
                Text = originalPost.TenNguoiDung ?? "Người dùng",
                Location = new Point(52, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            var lblNestedTime = new Label
            {
                Text = GetRelativeTime(originalPost.ThoiGianTao ?? DateTime.Now),
                Location = new Point(52, 28),
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.Gray
            };

            pnlNestedHeader.Controls.Add(pbNestedAvatar);
            pnlNestedHeader.Controls.Add(lblNestedAuthor);
            pnlNestedHeader.Controls.Add(lblNestedTime);

            // ===== CONTENT của bài gốc =====
            var lblNestedContent = new Label
            {
                Text = originalPost.NoiDung ?? "(Không có nội dung)",
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(520, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(50, 50, 50),
                Padding = new Padding(0, 5, 0, 10)
            };

            // ===== HÌNH ẢNH của bài gốc (nếu có) =====
            PictureBox? pbNestedImage = null;
            if (!string.IsNullOrEmpty(originalPost.HinhAnh))
            {
                pbNestedImage = new PictureBox
                {
                    Dock = DockStyle.Top,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    MaximumSize = new Size(520, 250),
                    MinimumSize = new Size(520, 150)
                };

                try
                {
                    pbNestedImage.Load(originalPost.HinhAnh);
                }
                catch
                {
                    pbNestedImage.Visible = false;
                }
            }

            // ===== STATS của bài gốc (chỉ hiển thị số, không có button) =====
            var lblNestedStats = new Label
            {
                Text = $"👍 {originalPost.SoReaction}   💬 {originalPost.SoBinhLuan} bình luận",
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Padding = new Padding(0, 8, 0, 5),
                AutoSize = true
            };

            // ===== ADD CONTROLS =====
            panel.Controls.Add(lblNestedStats);
            if (pbNestedImage != null)
                panel.Controls.Add(pbNestedImage);
            panel.Controls.Add(lblNestedContent);
            panel.Controls.Add(pnlNestedHeader);

            // Click vào bài gốc → Mở detail (tùy chọn)
            panel.Cursor = Cursors.Hand;
            panel.Click += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Click vào bài gốc #{originalPost.MaBaiDang}");
                // TODO: Mở detail bài gốc nếu muốn
            };

            return panel;
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
            if (!UserSession.IsLoggedIn || _post == null || UserSession.CurrentUser == null || _reactionService == null)
                return;

            try
            {
                if (_currentReaction.HasValue)
                {
                    // Đã react → Bỏ reaction
                    await _reactionService.RemovePostReactionAsync(_post.MaBaiDang, UserSession.CurrentUser.MaNguoiDung);

                    // Reset UI
                    if (btnLike != null)
                    {
                        btnLike.Text = "👍 Thích";
                        btnLike.BackColor = Color.White;
                    }
                    _currentReaction = null;

                    // Cập nhật số lượng
                    var newCount = await _reactionService.GetPostReactionCountAsync(_post.MaBaiDang);
                    _post.SoReaction = newCount;
                    if (lblReactionCount != null)
                        lblReactionCount.Text = $"👍 {newCount}";
                }
                else
                {
                    // Chưa react → React "Thích"
                    var request = new TaoHoacCapNhatReactionBaiDangRequest
                    {
                        MaBaiDang = _post.MaBaiDang,
                        MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                        LoaiReaction = LoaiReactionEnum.Thich
                    };

                    await _reactionService.ReactToPostAsync(request);
                    _currentReaction = LoaiReactionEnum.Thich;

                    var newCount = await _reactionService.GetPostReactionCountAsync(_post.MaBaiDang);
                    _post.SoReaction = newCount;

                    if (lblReactionCount != null)
                        lblReactionCount.Text = $"👍 {newCount}";

                    if (btnLike != null)
                        btnLike.BackColor = Color.FromArgb(230, 240, 255);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnComment_Click(object? sender, EventArgs e)
        {
            if (_post == null || _commentService == null) return;

            // ✅ FIX: Dùng CommentSectionDialog mới với IReactionBinhLuanService
            try
            {
                // Lấy IReactionBinhLuanService từ Program.ServiceProvider
                var reactionBinhLuanService = Program.ServiceProvider?.GetService(typeof(IReactionBinhLuanService)) as IReactionBinhLuanService;
                
                if (reactionBinhLuanService == null)
                {
                    MessageBox.Show("Không thể tải dịch vụ bình luận", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var dialog = new CommentSectionDialog(
                    _post.MaBaiDang,
                    _commentService,
                    reactionBinhLuanService
                );

                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnShare_Click(object? sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn || _post == null || UserSession.CurrentUser == null || _chiaSeBaiDangService == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để chia sẻ bài viết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Hiển thị dialog xác nhận và nhập nội dung bổ sung
                using var shareDialog = new Form
                {
                    Text = "Chia sẻ bài viết",
                    Width = 450,
                    Height = 350,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                // Label hướng dẫn
                var lblTitle = new Label
                {
                    Text = "Bạn muốn chia sẻ bài viết này?",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(15, 15),
                    AutoSize = true
                };

                // TextBox nhập nội dung thêm
                var lblNoiDung = new Label
                {
                    Text = "Thêm suy nghĩ của bạn (tùy chọn):",
                    Font = new Font("Segoe UI", 9F),
                    Location = new Point(15, 50),
                    AutoSize = true
                };

                var txtNoiDung = new TextBox
                {
                    Location = new Point(15, 75),
                    Width = 400,
                    Height = 80,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    PlaceholderText = "Chia sẻ suy nghĩ của bạn về bài viết này..."
                };

                // Hiển thị preview bài gốc
                var pnlPreview = new Panel
                {
                    Location = new Point(15, 165),
                    Width = 400,
                    Height = 70,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(245, 245, 245)
                };

                var lblPreview = new Label
                {
                    Text = $"📄 Bài viết gốc của {_post.TenNguoiDung}",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(24, 119, 242)
                };

                var lblContent = new Label
                {
                    Text = _post.NoiDung?.Length > 80 
                        ? _post.NoiDung.Substring(0, 80) + "..." 
                        : _post.NoiDung ?? "(Không có nội dung)",
                    Font = new Font("Segoe UI", 8F),
                    Location = new Point(10, 30),
                    Width = 370,
                    Height = 30,
                    ForeColor = Color.Gray
                };

                pnlPreview.Controls.Add(lblPreview);
                pnlPreview.Controls.Add(lblContent);

                // Nút Chia sẻ
                var btnShare = new Button
                {
                    Text = "🔄 Chia sẻ",
                    Location = new Point(220, 250),
                    Width = 100,
                    Height = 35,
                    BackColor = Color.FromArgb(24, 119, 242),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    DialogResult = DialogResult.OK
                };
                btnShare.FlatAppearance.BorderSize = 0;

                // Nút Hủy
                var btnCancel = new Button
                {
                    Text = "Hủy",
                    Location = new Point(330, 250),
                    Width = 85,
                    Height = 35,
                    BackColor = Color.LightGray,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F),
                    Cursor = Cursors.Hand,
                    DialogResult = DialogResult.Cancel
                };
                btnCancel.FlatAppearance.BorderSize = 0;

                // Thêm controls vào dialog
                shareDialog.Controls.AddRange(new Control[] { 
                    lblTitle, lblNoiDung, txtNoiDung, pnlPreview, btnShare, btnCancel 
                });

                // Hiển thị dialog
                if (shareDialog.ShowDialog() == DialogResult.OK)
                {
                    // Tạo request chia sẻ
                    var request = new ChiaSeBaiDangRequest
                    {
                        MaBaiDangGoc = _post.MaBaiDang,
                        MaNguoiChiaSe = UserSession.CurrentUser.MaNguoiDung,
                        NoiDungThem = txtNoiDung.Text.Trim(),
                        QuyenRiengTu = QuyenRiengTuEnum.CongKhai
                    };

                    // Gọi API chia sẻ
                    var result = await _chiaSeBaiDangService.ChiaSeBaiDangAsync(request);

                    MessageBox.Show(
                        "✅ Đã chia sẻ bài viết lên trang cá nhân của bạn!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Cập nhật số lượng chia sẻ (nếu có)
                    System.Diagnostics.Debug.WriteLine($"✅ Đã chia sẻ bài đăng #{_post.MaBaiDang}");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                
                if (errorMessage.Contains("đã chia sẻ"))
                {
                    MessageBox.Show(
                        "❌ Bạn đã chia sẻ bài viết này rồi!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else if (errorMessage.Contains("không có quyền"))
                {
                    MessageBox.Show(
                        "❌ Bạn không có quyền chia sẻ bài viết này!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                else
                {
                    MessageBox.Show(
                        $"Lỗi khi chia sẻ bài viết:\n{errorMessage}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

                System.Diagnostics.Debug.WriteLine($"❌ Lỗi chia sẻ: {ex.Message}");
            }
        }

        // ===== ✅ THÊM: REACTION PICKER =====
        
        /// <summary>
        /// Tạo panel chọn reaction (6 cảm xúc)
        /// </summary>
        private void CreateReactionPicker()
        {
            // ✅ FIX: Tạo panel với vị trí DƯƠNG, add vào pnlContainer
            pnlReactionPicker = new Panel
            {
                Location = new Point(10, 10), // Vị trí tạm, sẽ tính lại khi hover
                Size = new Size(360, 50),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            // Danh sách reactions: Like, Love, Haha, Wow, Sad, Angry
            var reactions = new[]
            {
                new { Emoji = "👍", Type = LoaiReactionEnum.Thich, Name = "Thích" },
                new { Emoji = "❤️", Type = LoaiReactionEnum.YeuThich, Name = "Yêu thích" },
                new { Emoji = "😂", Type = LoaiReactionEnum.Haha, Name = "Haha" },
                new { Emoji = "😮", Type = LoaiReactionEnum.Wow, Name = "Wow" },
                new { Emoji = "😢", Type = LoaiReactionEnum.Buon, Name = "Buồn" },
                new { Emoji = "😡", Type = LoaiReactionEnum.TucGian, Name = "Phẫn nộ" }
            };

            int x = 5;
            foreach (var reaction in reactions)
            {
                var btnReaction = new Button
                {
                    Text = reaction.Emoji,
                    Location = new Point(x, 5),
                    Size = new Size(50, 40),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    Font = new Font("Segoe UI", 18F),
                    Cursor = Cursors.Hand,
                    Tag = reaction.Type,
                    TabStop = false // ✅ THÊM: Tránh focus issues
                };
                btnReaction.FlatAppearance.BorderSize = 0;

                // Tooltip
                var tooltip = new ToolTip();
                tooltip.SetToolTip(btnReaction, reaction.Name);

                // ✅ FIX: Đơn giản hóa event handlers
                var currentReaction = reaction; // Capture biến cho closure
                
                btnReaction.MouseEnter += (s, e) =>
                {
                    btnReaction.BackColor = Color.FromArgb(240, 242, 245);
                };
                btnReaction.MouseLeave += (s, e) =>
                {
                    btnReaction.BackColor = Color.White;
                };
                btnReaction.Click += (s, e) =>
                {
                    OnReactionSelected(currentReaction.Type, currentReaction.Emoji);
                };

                pnlReactionPicker.Controls.Add(btnReaction);
                x += 55;
            }

            // ✅ FIX: ADD VÀO pnlContainer (không phải pnlActions!)
            if (pnlContainer != null)
            {
                pnlContainer.Controls.Add(pnlReactionPicker);
                pnlReactionPicker.BringToFront();
            }
        }

        /// <summary>
        /// Hiển thị reaction picker khi hover nút Like
        /// </summary>
        private void BtnLike_MouseEnter(object? sender, EventArgs e)
        {
            if (pnlReactionPicker == null || btnLike == null || pnlActions == null || pnlContainer == null) 
                return;

            try
            {
                // ✅ FIX: Tính vị trí động dựa trên btnLike - SÁT NGAY PHÍA TRÊN NÚT LIKE
                int containerHeight = pnlHeader?.Height ?? 0;
                containerHeight += pnlContent?.Height ?? 0;
                containerHeight += pnlStats?.Height ?? 0;

                // ✅ SỬA: Đưa panel xuống gần nút Like hơn (chỉ cách 5px)
                int panelY = containerHeight - 5; // Từ -55 thành -5
                int panelX = 10;

                pnlReactionPicker.Location = new Point(panelX, panelY);
                pnlReactionPicker.Visible = true;
                pnlReactionPicker.BringToFront();

                System.Diagnostics.Debug.WriteLine($"✅ Reaction picker hiện tại: ({panelX}, {panelY})");

                // ✅ FIX MỚI: Thêm event MouseEnter cho panel để giữ nó hiển thị
                pnlReactionPicker.MouseEnter -= PnlReactionPicker_MouseEnter; // Tránh đăng ký nhiều lần
                pnlReactionPicker.MouseEnter += PnlReactionPicker_MouseEnter;
                
                pnlReactionPicker.MouseLeave -= PnlReactionPicker_MouseLeave;
                pnlReactionPicker.MouseLeave += PnlReactionPicker_MouseLeave;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi hiện reaction picker: {ex.Message}");
            }
        }

        /// <summary>
        /// ✅ MỚI: Giữ panel khi chuột vào panel
        /// </summary>
        private void PnlReactionPicker_MouseEnter(object? sender, EventArgs e)
        {
            // Khi chuột vào panel → Giữ panel hiển thị
            if (pnlReactionPicker != null)
            {
                pnlReactionPicker.Visible = true;
                System.Diagnostics.Debug.WriteLine("✅ Chuột vào reaction picker → Giữ hiển thị");
            }
        }

        /// <summary>
        /// ✅ MỚI: Ẩn panel khi chuột rời khỏi panel
        /// </summary>
        private void PnlReactionPicker_MouseLeave(object? sender, EventArgs e)
        {
            // Khi chuột rời khỏi panel → Ẩn panel
            if (pnlReactionPicker != null)
            {
                pnlReactionPicker.Visible = false;
                System.Diagnostics.Debug.WriteLine("✅ Chuột rời reaction picker → Ẩn");
            }
        }

        /// <summary>
        /// ✅ MỚI: Xử lý khi chọn reaction với DEBOUNCING + OPTIMISTIC UI
        /// </summary>
        private async void OnReactionSelected(LoaiReactionEnum reactionType, string emoji)
        {
            if (!UserSession.IsLoggedIn || _post == null || UserSession.CurrentUser == null || _reactionService == null)
                return;

            // ✅ Ẩn picker NGAY
            if (pnlReactionPicker != null)
                pnlReactionPicker.Visible = false;

            // ✅ OPTIMISTIC UI: Update UI NGAY LẬP TỨC (không đợi API)
            var oldReaction = _currentReaction;
            var oldEmoji = oldReaction.HasValue ? GetEmojiFromReactionType(oldReaction.Value) : "👍";
            
            _currentReaction = reactionType;
            if (btnLike != null)
            {
                btnLike.Text = $"{emoji} {GetReactionName(reactionType)}";
                btnLike.BackColor = Color.FromArgb(230, 240, 255);
            }

            // ✅ DEBOUNCING: Cancel request cũ nếu user click liên tục
            _reactionCancellation?.Cancel();
            _reactionCancellation = new System.Threading.CancellationTokenSource();
            _pendingReaction = reactionType;

            try
            {
                // ✅ Chờ 500ms - Nếu user click lại trong 500ms thì cancel và chờ tiếp
                await Task.Delay(500, _reactionCancellation.Token);

                // ✅ Sau 500ms không có click mới → GỬI REQUEST
                System.Diagnostics.Debug.WriteLine($"📤 Gửi request reaction: {reactionType}");

                // Xóa reaction cũ nếu có
                if (oldReaction.HasValue)
                {
                    await _reactionService.RemovePostReactionAsync(_post.MaBaiDang, UserSession.CurrentUser.MaNguoiDung);
                    await Task.Delay(50); // Delay nhỏ
                }

                // Thêm reaction mới
                var request = new TaoHoacCapNhatReactionBaiDangRequest
                {
                    MaBaiDang = _post.MaBaiDang,
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    LoaiReaction = reactionType
                };

                await _reactionService.ReactToPostAsync(request);

                // Cập nhật số lượng từ server
                var newCount = await _reactionService.GetPostReactionCountAsync(_post.MaBaiDang);
                _post.SoReaction = newCount;

                if (lblReactionCount != null)
                    lblReactionCount.Text = $"{emoji} {newCount}";

                System.Diagnostics.Debug.WriteLine($"✅ Reaction thành công: {reactionType}");
            }
            catch (TaskCanceledException)
            {
                // ✅ User click emoji khác trong 500ms → Request bị cancel (ĐÚNG)
                System.Diagnostics.Debug.WriteLine($"⏸️ Request bị cancel (user click tiếp)");
            }
            catch (Exception ex)
            {
                // ✅ Lỗi thật sự → Rollback UI
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"❌ Stack Trace: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"❌ Inner Exception: {ex.InnerException?.Message}");
                
                _currentReaction = oldReaction;
                if (btnLike != null)
                {
                    if (oldReaction.HasValue)
                    {
                        btnLike.Text = $"{oldEmoji} {GetReactionName(oldReaction.Value)}";
                        btnLike.BackColor = Color.FromArgb(230, 240, 255);
                    }
                    else
                    {
                        btnLike.Text = "👍 Thích";
                        btnLike.BackColor = Color.White;
                    }
                }

                MessageBox.Show(
                    "Không thể thay đổi cảm xúc. Vui lòng thử lại!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        /// <summary>
        /// Lấy tên reaction bằng tiếng Việt
        /// </summary>
        private string GetReactionName(LoaiReactionEnum reactionType)
        {
            return reactionType switch
            {
                LoaiReactionEnum.Thich => "Thích",
                LoaiReactionEnum.YeuThich => "Yêu thích",
                LoaiReactionEnum.Haha => "Haha",
                LoaiReactionEnum.Wow => "Wow",
                LoaiReactionEnum.Buon => "Buồn",
                LoaiReactionEnum.TucGian => "Phẫn nộ",
                _ => "Thích"
            };
        }

        // ===== ✅ THÊM: POST MENU (EDIT/DELETE) =====

        /// <summary>
        /// Hiển thị menu tuỳ chọn bài đăng
        /// </summary>
        private void BtnMenu_Click(object? sender, EventArgs e)
        {
            if (_post == null || UserSession.CurrentUser == null) return;

            // Kiểm tra quyền sở hữu bài đăng
            bool isOwner = _post.MaNguoiDung == UserSession.CurrentUser.MaNguoiDung;

            var contextMenu = new ContextMenuStrip();

            if (isOwner)
            {
                // Chủ bài đăng: Cho phép Edit/Delete
                var editItem = new ToolStripMenuItem("✏️ Chỉnh sửa bài viết");
                editItem.Click += (s, ev) => EditPost();
                contextMenu.Items.Add(editItem);

                var privacyItem = new ToolStripMenuItem("🔒 Thay đổi quyền xem");
                // ✅ SỬA: Dùng giá trị enum đúng (0 = RiengTu, 1 = CongKhai, 2 = ChiFollower)
                privacyItem.DropDownItems.Add(CreatePrivacyMenuItem("🌍 Công khai", 1));
                privacyItem.DropDownItems.Add(CreatePrivacyMenuItem("👥 Chỉ người theo dõi", 2));
                privacyItem.DropDownItems.Add(CreatePrivacyMenuItem("🔒 Chỉ mình tôi", 0)); // ✅ SỬA: 0 thay vì 3
                contextMenu.Items.Add(privacyItem);

                contextMenu.Items.Add(new ToolStripSeparator());

                var deleteItem = new ToolStripMenuItem("🗑️ Xóa bài viết");
                deleteItem.Click += (s, ev) => DeletePost();
                contextMenu.Items.Add(deleteItem);
            }
            else
            {
                // Người khác: Cho phép Report/Hide
                var hideItem = new ToolStripMenuItem("👁️‍🗨️ Ẩn bài viết");
                hideItem.Click += (s, ev) => MessageBox.Show("Đã ẩn bài viết này");
                contextMenu.Items.Add(hideItem);

                var reportItem = new ToolStripMenuItem("🚩 Báo cáo bài viết");
                reportItem.Click += (s, ev) => MessageBox.Show("Đã báo cáo bài viết");
                contextMenu.Items.Add(reportItem);
            }

            if (btnMenu != null)
            {
                contextMenu.Show(btnMenu, new Point(0, btnMenu.Height));
            }
        }

        /// <summary>
        /// Tạo menu item thay đổi quyền riêng tư
        /// </summary>
        private ToolStripMenuItem CreatePrivacyMenuItem(string text, byte privacyLevel)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += async (s, e) => await ChangePrivacy(privacyLevel);
            return item;
        }

        /// <summary>
        /// Thay đổi quyền riêng tư bài đăng
        /// </summary>
        private async Task ChangePrivacy(byte privacyLevel)
        {
            if (_post == null || _postService == null) return;

            try
            {
                var request = new CapNhatBaiDangRequest
                {
                    NoiDung = _post.NoiDung,
                    QuyenRiengTu = (QuyenRiengTuEnum)privacyLevel
                };

                await _postService.UpdatePostAsync(_post.MaBaiDang, request);

                MessageBox.Show("✅ Đã cập nhật quyền riêng tư!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chỉnh sửa bài viết
        /// </summary>
        private void EditPost()
        {
            if (_post == null) return;

            MessageBox.Show(
                $"Chỉnh sửa bài viết:\n\n{_post.NoiDung}\n\nChức năng sẽ được thêm trong phiên bản tiếp theo!",
                "Edit Post",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Xóa bài viết
        /// </summary>
        private async void DeletePost()
        {
            if (_post == null || _postService == null) return;

            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa bài viết này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _postService.DeletePostAsync(_post.MaBaiDang);

                    MessageBox.Show("✅ Đã xóa bài viết!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ẩn PostCard
                    this.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ✅ MỚI: Load avatar từ URL hoặc tạo placeholder
        /// </summary>
        private void LoadAvatarImage(PictureBox pictureBox, string? avatarUrl, string? displayName)
        {
            if (pictureBox == null) return;

            try
            {
                // Nếu có URL hợp lệ
                if (!string.IsNullOrWhiteSpace(avatarUrl))
                {
                    if (Uri.TryCreate(avatarUrl, UriKind.Absolute, out var uri) &&
                        (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                    {
                        pictureBox.LoadAsync(avatarUrl);
                        return;
                    }
                    else if (System.IO.File.Exists(avatarUrl))
                    {
                        pictureBox.Image = Image.FromFile(avatarUrl);
                        return;
                    }
                }

                // Fallback: Tạo avatar chữ cái đầu
                CreateInitialsAvatarForPost(pictureBox, displayName);
            }
            catch
            {
                CreateInitialsAvatarForPost(pictureBox, displayName);
            }
        }

        /// <summary>
        /// ✅ MỚI: Tạo avatar từ chữ cái đầu
        /// </summary>
        private void CreateInitialsAvatarForPost(PictureBox pictureBox, string? displayName)
        {
            if (pictureBox == null) return;

            try
            {
                var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    // Background màu xanh
                    g.Clear(Color.FromArgb(24, 119, 242));

                    // Lấy chữ cái đầu
                    string initials = "U";
                    if (!string.IsNullOrWhiteSpace(displayName))
                    {
                        var parts = displayName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2)
                        {
                            initials = $"{parts[0][0]}{parts[^1][0]}".ToUpper();
                        }
                        else if (parts.Length == 1)
                        {
                            initials = parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
                        }
                    }

                    // Vẽ chữ
                    using (var font = new Font("Segoe UI", pictureBox.Width / 3, FontStyle.Bold))
                    using (var brush = new SolidBrush(Color.White))
                    using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString(initials, font, brush, new RectangleF(0, 0, pictureBox.Width, pictureBox.Height), format);
                    }
                }
                pictureBox.Image = bitmap;
            }
            catch
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// ✅ HOÀN THIỆN: Render nội dung với hashtag được highlight trong RichTextBox
        /// 
        /// CÁCH HOẠT ĐỘNG:
        /// 1. Set text vào RichTextBox
        /// 2. Dùng Regex tìm vị trí các hashtag
        /// 3. Highlight hashtag bằng màu xanh (Color.Blue) và Bold
        /// 4. Click detection trong RtbContent_MouseClick
        /// </summary>
        private void RenderContentWithHashtags(RichTextBox rtbContent, string content)
        {
            if (rtbContent == null || string.IsNullOrEmpty(content))
                return;

            // 1. Set text thuần
            rtbContent.Text = content;

            // 2. Reset format
            rtbContent.SelectAll();
            rtbContent.SelectionColor = Color.Black;
            rtbContent.SelectionFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            rtbContent.DeselectAll();

            // 3. Tìm và highlight các hashtag
            var regex = new System.Text.RegularExpressions.Regex(@"#[\p{L}\p{N}_]+");
            var matches = regex.Matches(content);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                // Select hashtag
                rtbContent.Select(match.Index, match.Length);
                
                // ✅ Highlight: Màu xanh + Bold + Underline (giống link)
                rtbContent.SelectionColor = Color.FromArgb(24, 119, 242); // Facebook blue
                rtbContent.SelectionFont = new Font("Segoe UI", 9.5F, FontStyle.Bold | FontStyle.Underline);
            }

            // 4. Deselect để không hiện selection
            rtbContent.DeselectAll();

            // 5. Đặt cursor về đầu
            rtbContent.SelectionStart = 0;
            rtbContent.SelectionLength = 0;
        }

        /// <summary>
        /// ✅ MỚI: Xử lý click vào hashtag
        /// </summary>
        private void RtbContent_MouseClick(object? sender, MouseEventArgs e)
        {
            if (rtbContent == null) return;

            // Lấy vị trí click
            int clickIndex = rtbContent.GetCharIndexFromPosition(e.Location);

            // Lấy text
            string text = rtbContent.Text;
            if (clickIndex < 0 || clickIndex >= text.Length) return;

            // Tìm hashtag tại vị trí click
            var regex = new System.Text.RegularExpressions.Regex(@"#[\p{L}\p{N}_]+");
            var matches = regex.Matches(text);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                // Check xem click có nằm trong hashtag không
                if (clickIndex >= match.Index && clickIndex < match.Index + match.Length)
                {
                    // Lấy hashtag (bỏ dấu #)
                    string hashtag = match.Value.TrimStart('#');

                    // ✅ Mở trang tìm kiếm hashtag
                    OpenHashtagSearch(hashtag);
                    return;
                }
            }
        }

        /// <summary>
        /// ✅ MỚI: Detect hover vào hashtag và đổi cursor
        /// 
        /// CÁCH HOẠT ĐỘNG:
        /// 1. Lấy index của ký tự tại vị trí chuột
        /// 2. Kiểm tra xem index có nằm trong hashtag không
        /// 3. Nếu có → Cursor = Hand (👆)
        /// 4. Nếu không → Cursor = Arrow (mặc định)
        /// </summary>
        private void RtbContent_MouseMove(object? sender, MouseEventArgs e)
        {
            if (rtbContent == null) return;

            try
            {
                // Lấy index của ký tự tại vị trí chuột
                int hoverIndex = rtbContent.GetCharIndexFromPosition(e.Location);
                string text = rtbContent.Text;

                if (hoverIndex < 0 || hoverIndex >= text.Length)
                {
                    rtbContent.Cursor = Cursors.Arrow;
                    return;
                }

                // Tìm tất cả hashtag
                var regex = new System.Text.RegularExpressions.Regex(@"#[\p{L}\p{N}_]+");
                var matches = regex.Matches(text);

                // Kiểm tra xem đang hover hashtag không
                bool isOverHashtag = false;
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    if (hoverIndex >= match.Index && hoverIndex < match.Index + match.Length)
                    {
                        isOverHashtag = true;
                        break;
                    }
                }

                // Đổi cursor tương ứng
                rtbContent.Cursor = isOverHashtag ? Cursors.Hand : Cursors.Arrow;
            }
            catch
            {
                rtbContent.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// ✅ MỚI: Mở trang tìm kiếm hashtag
        /// </summary>
        private void OpenHashtagSearch(string hashtag)
        {
            try
            {
                // Tìm MainForm
                var mainForm = this.FindForm();
                if (mainForm is Forms.MainForm mf && Program.ServiceProvider != null)
                {
                    // ✅ Lấy HashtagService
                    var hashtagService = Program.ServiceProvider.GetService(typeof(IHashtagService)) as IHashtagService;
                    if (hashtagService != null)
                    {
                        // ✅ Tạo HashtagSearchPage và load
                        var searchPage = new WinForms.UserControls.Social.HashtagSearchPage(hashtagService, hashtag);
                        mf.LoadPage(searchPage);
                        
                        System.Diagnostics.Debug.WriteLine($"✅ Đã mở HashtagSearchPage cho #{hashtag}");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Không thể tải dịch vụ tìm kiếm hashtag",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi OpenHashtagSearch: {ex.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// ✅ MỚI: Tách hashtag từ nội dung
        /// </summary>
        private List<string> ExtractHashtags(string content)
        {
            if (string.IsNullOrEmpty(content))
                return new List<string>();

            var regex = new System.Text.RegularExpressions.Regex(@"#([\p{L}\p{N}_]+)");
            var matches = regex.Matches(content);

            return matches.Cast<System.Text.RegularExpressions.Match>()
                         .Select(m => m.Groups[1].Value.ToLower())
                         .Distinct()
                         .ToList();
        }

        private void PostCardControl_Load(object sender, EventArgs e)
        {

        }

        #region Logic Thách đấu (Join Game)
        private async Task UpdateChallengeButtonStatus(int pin)
        {
            if (btnJoinChallenge == null || _thachDauService == null) return;
            try
            {
                var room = await _thachDauService.GetByIdAsync(pin);
                if (room == null || room.TrangThai == TrangThaiThachDauEnum.Huy)
                {
                    btnJoinChallenge.Text = "⛔ PHÒNG ĐÃ ĐÓNG HOẶC HẾT HẠN";
                    btnJoinChallenge.Enabled = false;
                    btnJoinChallenge.BackColor = Color.Gray;
                }
                else
                {
                    btnJoinChallenge.Text = $"🎮 THAM GIA THÁCH ĐẤU (PIN: {pin:D6})";
                    btnJoinChallenge.Enabled = true;
                    btnJoinChallenge.BackColor = Color.FromArgb(46, 125, 50);
                }
            }
            catch { btnJoinChallenge.Visible = false; }
        }

        private async void HandleJoinChallengeAsync(object? sender, EventArgs e)
        {
            if (_post == null || string.IsNullOrEmpty(_post.NoiDung) || _thachDauService == null) return;
            if (UserSession.CurrentUser == null) { MessageBox.Show("Vui lòng đăng nhập để tham gia!"); return; }

            int pin = ExtractIdFromTag(_post.NoiDung, "CHALLENGE_PIN");
            int maBoDe = ExtractIdFromTag(_post.NoiDung, "BO_DE_ID");

            try
            {
                this.Cursor = Cursors.WaitCursor;
                var request = new LichSuThachDauRequest { MaThachDau = pin, MaNguoiDung = UserSession.CurrentUser.MaNguoiDung };
                bool success = await _thachDauService.ThamGiaThachDauAsync(request);

                if (success)
                {
                    var mainForm = this.FindForm() as MainForm;
                    if (mainForm != null && Program.ServiceProvider != null)
                    {
                        var chiTietPage = Program.ServiceProvider.GetRequiredService<ChiTietBoDeControl>();
                        mainForm.LoadPage(chiTietPage);
                        await chiTietPage.JoinAsGuest(pin, maBoDe);
                    }
                }
                else { MessageBox.Show("Không thể tham gia phòng này!"); await UpdateChallengeButtonStatus(pin); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        #endregion

        private int ExtractIdFromTag(string content, string tag)
        {
            var match = Regex.Match(content, $@"\[{tag}:(\d+)\]");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }
        #endregion
    }
}