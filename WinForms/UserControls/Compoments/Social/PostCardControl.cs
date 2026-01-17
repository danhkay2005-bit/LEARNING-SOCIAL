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
        private readonly IReactionBaiDangService? _reactionBaiDangService; // ✅ THÊM

        private BaiDangResponse? _post;
        private LoaiReactionEnum? _currentReaction = null; // ✅ THÊM: Lưu reaction hiện tại
        
        // ✅ DEBOUNCING: Thay vì throttling
        private System.Threading.CancellationTokenSource? _reactionCancellation;
        private LoaiReactionEnum? _pendingReaction = null;

        // ===== CONTROLS =====
        private Panel? pnlContainer;
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblAuthorName;
        private Label? lblTimestamp;
        private Button? btnMenu; // ✅ THÊM: Nút menu 3 chấm
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
        private Panel? pnlReactionPicker; // ✅ THÊM: Panel chọn reaction

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

            // ✅ THÊM: Lấy IReactionBaiDangService để check reaction hiện tại
            if (Program.ServiceProvider != null)
            {
                _reactionBaiDangService = Program.ServiceProvider.GetService(typeof(IReactionBaiDangService)) as IReactionBaiDangService;
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

            // ✅ THÊM: Nút menu 3 chấm
            btnMenu = new Button
            {
                Text = "⋮",
                Location = new Point(540, 10),
                Width = 35,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                ForeColor = Color.Gray
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Click += BtnMenu_Click;

            pnlHeader.Controls.Add(pbAvatar);
            pnlHeader.Controls.Add(lblAuthorName);
            pnlHeader.Controls.Add(lblTimestamp);
            pnlHeader.Controls.Add(btnMenu); // ✅ THÊM

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

            // ✅ THÊM: Long press hoặc hover để hiện reaction picker
            btnLike.MouseEnter += BtnLike_MouseEnter;
            btnLike.Click += BtnLike_Click;
            btnComment.Click += BtnComment_Click;
            btnShare.Click += BtnShare_Click;

            pnlActions.Controls.Add(btnLike);
            pnlActions.Controls.Add(btnComment);
            pnlActions.Controls.Add(btnShare);

            // ✅ THÊM: Reaction Picker (ẩn ban đầu)
            CreateReactionPicker();

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

        private void PostCardControl_Load(object sender, EventArgs e)
        {

        }
    }
}