using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.Social;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Components.Social;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls
{
    /// <summary>
    /// 👤 THÔNG TIN CÁ NHÂN - Trang profile của chính mình
    /// 
    /// CHỨC NĂNG:
    /// 1. Hiển thị thông tin cá nhân đầy đủ
    /// 2. Chỉnh sửa thông tin (Avatar, Bio, v.v.)
    /// 3. Thống kê:  Followers, Following, Số bài viết
    /// 4. Tab:  Bài viết / Followers / Following
    /// </summary>
    public partial class ThongTinCaNhanPage : UserControl
    {
        private readonly IUserProfileService? _userProfileService;
        private readonly ITheoDoiNguoiDungService? _followService;
        private readonly IPostService? _postService;
        private readonly IReactionService? _reactionService;
        private readonly ICommentService? _commentService;

        // Controls
        private Panel? pnlHeader;
        private PictureBox? pbAvatar;
        private Label? lblName;
        private Label? lblEmail;
        private Label? lblBio;
        private Button? btnEdit;
        private Panel? pnlStats;
        private Label? lblPostCount;
        private Label? lblFollowers;
        private Label? lblFollowing;

        // Tabs
        private TabControl? tabControl;
        private TabPage? tabPosts;
        private TabPage? tabFollowers;
        private TabPage? tabFollowing;
        private FlowLayoutPanel? flowPosts;
        private FlowLayoutPanel? flowFollowers;
        private FlowLayoutPanel? flowFollowing;

        private readonly IBoDeHocService? _boDeHocService;
        private readonly IThachDauService? _thachDauService;

        // Tab mới
        private TabPage? tabMyQuizzes;
        private TabPage? tabChallengeHistory;
        private FlowLayoutPanel? flowMyQuizzes;
        private FlowLayoutPanel? flowChallengeHistory;

        public ThongTinCaNhanPage()
        {
            InitializeComponent();

            if (Program.ServiceProvider != null)
            {
                _userProfileService = Program.ServiceProvider.GetService<IUserProfileService>();
                _followService = Program.ServiceProvider.GetService<ITheoDoiNguoiDungService>();
                _postService = Program.ServiceProvider.GetService<IPostService>();
                _reactionService = Program.ServiceProvider.GetService<IReactionService>();
                _commentService = Program.ServiceProvider.GetService<ICommentService>();

                // ✅ Service mới
                _boDeHocService = Program.ServiceProvider.GetService<IBoDeHocService>();
                _thachDauService = Program.ServiceProvider.GetService<IThachDauService>();
            }

            InitializeCustomControls();
            LoadProfileAsync();
        }

        private void InitializeCustomControls()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            #region HEADER
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 230,
                Padding = new Padding(40, 30, 40, 25),
                BackColor = Color.White
            };

            pnlHeader.Paint += (s, e) =>
            {
                using var brush = new LinearGradientBrush(
                    pnlHeader.ClientRectangle,
                    Color.FromArgb(245, 247, 250),
                    Color.White,
                    LinearGradientMode.Vertical);

                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            pbAvatar = new PictureBox
            {
                Size = new Size(140, 140),
                Location = new Point(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                BackColor = Color.LightGray
            };

            pbAvatar.Paint += (s, e) =>
            {
                using var path = new GraphicsPath();
                path.AddEllipse(0, 0, pbAvatar.Width - 1, pbAvatar.Height - 1);
                pbAvatar.Region = new Region(path);

                using var pen = new Pen(Color.FromArgb(24, 119, 242), 3);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(pen, 1, 1, pbAvatar.Width - 3, pbAvatar.Height - 3);
            };

            pbAvatar.Click += BtnChangeAvatar_Click;

            lblName = new Label
            {
                Location = new Point(210, 40),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = true
            };

            lblEmail = new Label
            {
                Location = new Point(210, 80),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                AutoSize = true
            };

            lblBio = new Label
            {
                Location = new Point(210, 110),
                MaximumSize = new Size(600, 0),
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true
            };

            btnEdit = CreateOutlineButton("✏️ Chỉnh sửa");
            btnEdit.Location = new Point(210, 155);
            btnEdit.Click += BtnEdit_Click;

            pnlStats = new Panel
            {
                Location = new Point(210, 195),
                Size = new Size(450, 50),
                BackColor = Color.Transparent
            };

            var statPosts = CreateStatCard("Bài viết", "0", 0);
            var statFollowers = CreateStatCard("Followers", "0", 150);
            var statFollowing = CreateStatCard("Following", "0", 300);

            lblPostCount = (Label)statPosts.Tag!;
            lblFollowers = (Label)statFollowers.Tag!;
            lblFollowing = (Label)statFollowing.Tag!;

            statFollowers.Click += (s, e) => {
                if (tabControl != null && tabFollowers != null)
                    tabControl.SelectTab(tabFollowers);
            };
            statFollowing.Click += (s, e) => {
                if (tabControl != null && tabFollowing != null)
                    tabControl.SelectTab(tabFollowing);
            };

            pnlStats.Controls.AddRange(new[] { statPosts, statFollowers, statFollowing });

            pnlHeader.Controls.AddRange(new Control[]
            {
        pbAvatar, lblName, lblEmail, lblBio, btnEdit, pnlStats
            });
            #endregion

            #region TABS
            tabControl = CreateModernTabControl();

            tabPosts = CreateTab("📝 Bài viết", out flowPosts);
            tabMyQuizzes = CreateTab("📚 Bộ đề", out flowMyQuizzes, true);
            tabChallengeHistory = CreateTab("⚔️ Thách đấu", out flowChallengeHistory);
            tabFollowers = CreateTab("👥 Followers", out flowFollowers);
            tabFollowing = CreateTab("➕ Following", out flowFollowing);

            tabControl.TabPages.AddRange(new[]
            {
        tabPosts, tabMyQuizzes, tabChallengeHistory, tabFollowers, tabFollowing
    });

            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            #endregion

            Controls.Add(tabControl);
            Controls.Add(pnlHeader);
        }



        private FlowLayoutPanel CreateTabFlowPanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };
        }
        /// <summary>
        /// 🏷️ Tạo label thống kê
        /// </summary>
        private Label CreateStatLabel(string text, int x)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242),
                Cursor = Cursors.Hand
            };
        }

        /// <summary>
        /// 📚 Tải danh sách bộ đề của tôi
        /// </summary>
        private async Task LoadMyQuizzesAsync()
        {
            if (_boDeHocService == null || flowMyQuizzes == null || UserSession.CurrentUser == null) return;
            flowMyQuizzes.Controls.Clear();

            try
            {
                var quizzes = await _boDeHocService.GetByUserAsync(UserSession.CurrentUser.MaNguoiDung);
                if (quizzes == null || !quizzes.Any())
                {
                    flowMyQuizzes.Controls.Add(CreateEmptyLabel("📚 Bạn chưa tạo bộ đề nào. Hãy bắt đầu ngay!"));
                    return;
                }

                foreach (var q in quizzes)
                {
                    var item = new BoDeItemControl();
                    item.SetData(q.MaBoDe, q.TieuDe, q.SoLuongThe, q.SoLuotHoc, q.AnhBia ?? "");
                    item.OnVaoThiClick += (s, ev) => {
                        if (this.FindForm() is Forms.MainForm mf && Program.ServiceProvider != null)
                        {
                            var detail = Program.ServiceProvider.GetRequiredService<ChiTietBoDeControl>();
                            detail.MaBoDe = q.MaBoDe;
                            mf.LoadPage(detail);
                        }
                    };
                    flowMyQuizzes.Controls.Add(item);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private Label CreateEmptyLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Margin = new Padding(20, 50, 20, 20), // Tạo khoảng cách phía trên để không bị dính vào header
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        /// <summary>
        /// ⚔️ Tải lịch sử thách đấu của tôi
        /// </summary>
        private async Task LoadChallengeHistoryAsync()
        {
            if (_thachDauService == null || flowChallengeHistory == null || UserSession.CurrentUser == null) return;
            flowChallengeHistory.Controls.Clear();

            try
            {
                var history = await _thachDauService.GetLichSuByUserAsync(UserSession.CurrentUser.MaNguoiDung);
                if (history == null || !history.Any())
                {
                    flowChallengeHistory.Controls.Add(CreateEmptyLabel("⚔️ Bạn chưa tham gia trận thách đấu nào."));
                    return;
                }

                foreach (var h in history)
                {
                    Panel pnlRow = new Panel { Width = 750, Height = 70, BackColor = Color.White, Margin = new Padding(0, 5, 0, 10), BorderStyle = BorderStyle.FixedSingle };

                    string statusText = h.LaNguoiThang ? "🏆 THẮNG" : "💀 THUA";
                    Color statusColor = h.LaNguoiThang ? Color.Goldenrod : Color.Gray;

                    Label lblResult = new Label
                    {
                        Text = $"[{statusText}] {h.TenBoDe} | {h.Diem} điểm | 🎯 {h.TyLeDung}%",
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        ForeColor = statusColor,
                        Location = new Point(15, 10),
                        AutoSize = true
                    };

                    Label lblTime = new Label
                    {
                        Text = $"Trận đấu: #{h.MaThachDauGoc} - Kết thúc: {h.ThoiGianKetThuc:dd/MM/yyyy HH:mm}",
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        Location = new Point(15, 38),
                        AutoSize = true
                    };

                    pnlRow.Controls.Add(lblResult);
                    pnlRow.Controls.Add(lblTime);
                    flowChallengeHistory.Controls.Add(pnlRow);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 🔄 Tải thông tin profile
        /// </summary>
        private async void LoadProfileAsync()
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Hiển thị thông tin cơ bản từ UserSession
                if (lblName != null) lblName.Text = UserSession.CurrentUser.HoVaTen ?? "Người dùng";
                if (lblEmail != null) lblEmail.Text = UserSession.CurrentUser.Email ?? "";

                // ✅ 2. Load avatar
                LoadAvatar(UserSession.CurrentUser.HinhDaiDien);

                // ✅ 3. Load tiểu sử từ API (không hard-code)
                if (_userProfileService != null && lblBio != null)
                {
                    try
                    {
                        var fullProfile = await _userProfileService.GetProfileAsync(UserSession.CurrentUser.MaNguoiDung);
                        if (fullProfile != null && !string.IsNullOrEmpty(fullProfile.TieuSu))
                        {
                            lblBio.Text = fullProfile.TieuSu;
                        }
                        else
                        {
                            lblBio.Text = "🎓 Đang học tập trên StudyApp";
                        }
                    }
                    catch
                    {
                        lblBio.Text = "🎓 Đang học tập trên StudyApp";
                    }
                }

                // 4. Lấy thống kê
                if (_followService != null)
                {
                    var stats = await _followService.LayThongKeTheoDoiAsync(UserSession.CurrentUser.MaNguoiDung);
                    if (lblFollowers != null) lblFollowers.Text = $"{stats.SoNguoiTheoDoi} Followers";
                    if (lblFollowing != null) lblFollowing.Text = $"{stats.SoDangTheoDoi} Following";
                }

                // 4. Tải bài viết
                await LoadMyPostsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ THÊM MỚI:  Load avatar với xử lý nâng cao
        /// </summary>
        private void LoadAvatar(string? avatarPath)
        {
            if (pbAvatar == null) return;

            try
            {
                // Nếu có đường dẫn avatar
                if (!string.IsNullOrEmpty(avatarPath) && System.IO.File.Exists(avatarPath))
                {
                    using (var fs = new System.IO.FileStream(avatarPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pbAvatar.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    // ✅ Tạo avatar placeholder với chữ cái đầu
                    pbAvatar.Image = CreatePlaceholderAvatar();
                }

                pbAvatar.Invalidate(); // ✅ Vẽ lại để hiển thị viền tròn
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadAvatar Error] {ex.Message}");
                pbAvatar.Image = CreatePlaceholderAvatar();
                pbAvatar.Invalidate();
            }
        }

        /// <summary>
        /// ✅ THÊM MỚI:  Tạo avatar placeholder với chữ cái đầu
        /// </summary>
        private Image CreatePlaceholderAvatar()
        {
            if (UserSession.CurrentUser == null) return CreateDefaultAvatar();

            var size = 150;
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // ✅ Vẽ background gradient
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(24, 119, 242),
                    Color.FromArgb(66, 153, 225),
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                // ✅ Lấy chữ cái đầu
                string initials = GetInitials(UserSession.CurrentUser.HoVaTen ?? UserSession.CurrentUser.TenDangNhap);

                // ✅ Vẽ chữ
                using (var font = new Font("Segoe UI", 50, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(initials, font);
                    var x = (size - textSize.Width) / 2;
                    var y = (size - textSize.Height) / 2;
                    g.DrawString(initials, font, textBrush, x, y);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// ✅ THÊM MỚI:  Tạo avatar mặc định (khi không có tên)
        /// </summary>
        private Image CreateDefaultAvatar()
        {
            var size = 150;
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(200, 200, 200)))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                // ✅ Vẽ icon user
                using (var font = new Font("Segoe UI", 60, FontStyle.Regular))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var text = "👤";
                    var textSize = g.MeasureString(text, font);
                    var x = (size - textSize.Width) / 2;
                    var y = (size - textSize.Height) / 2;
                    g.DrawString(text, font, textBrush, x, y);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// ✅ THÊM MỚI: Lấy chữ cái đầu từ tên
        /// </summary>
        private string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "? ";

            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();

            // Lấy chữ cái đầu của 2 từ đầu tiên
            return (parts[0][0].ToString() + parts[parts.Length - 1][0].ToString()).ToUpper();
        }

        /// <summary>
        /// ✅ THÊM MỚI: Xử lý thay đổi avatar
        /// </summary>
        private void BtnChangeAvatar_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    openFileDialog.Title = "Chọn ảnh đại diện";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // ✅ Load ảnh mới
                        LoadAvatar(openFileDialog.FileName);

                        // ✅ TODO: Gọi API để upload ảnh
                        // await _userProfileService.UpdateAvatarAsync(UserSession. CurrentUser.MaNguoiDung, openFileDialog.FileName);

                        MessageBox.Show("Đã cập nhật ảnh đại diện!\n(Chức năng lưu vào server đang phát triển)",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thay đổi avatar: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Load avatar cho user card (size nhỏ 60x60)
        /// </summary>
        private void LoadAvatarForCard(PictureBox pictureBox, string? avatarPath, string? displayName)
        {
            if (pictureBox == null) return;

            try
            {
                // Nếu có đường dẫn avatar local
                if (!string.IsNullOrEmpty(avatarPath) && System.IO.File.Exists(avatarPath))
                {
                    using (var fs = new System.IO.FileStream(avatarPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    // ✅ Tạo avatar placeholder nhỏ
                    pictureBox.Image = CreateSmallPlaceholderAvatar(displayName);
                }
            }
            catch
            {
                pictureBox.Image = CreateSmallPlaceholderAvatar(displayName);
            }
        }

        /// <summary>
        /// ✅ Tạo avatar placeholder nhỏ (60x60) cho user cards
        /// </summary>
        private Image CreateSmallPlaceholderAvatar(string? name)
        {
            var size = 60;
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Background gradient
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(24, 119, 242),
                    Color.FromArgb(66, 153, 225),
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                // Chữ cái đầu
                string initials = GetInitials(name);

                // Vẽ chữ (font nhỏ hơn)
                using (var font = new Font("Segoe UI", 20, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(initials, font);
                    var x = (size - textSize.Width) / 2;
                    var y = (size - textSize.Height) / 2;
                    g.DrawString(initials, font, textBrush, x, y);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// 📝 Tải bài viết của tôi
        /// </summary>
        private async Task LoadMyPostsAsync()
        {
            if (_postService == null || flowPosts == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowPosts.Controls.Clear();

                var posts = await _postService.GetUserPostsAsync(UserSession.CurrentUser.MaNguoiDung, 1);

                if (lblPostCount != null) lblPostCount.Text = $"{posts.Count} Bài viết";

                if (!posts.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "📭 Bạn chưa có bài viết nào.\nHãy tạo bài viết đầu tiên! ",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowPosts.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var post in posts)
                {
                    if (_reactionService != null && _commentService != null)
                    {
                        var postCard = new Components.Social.PostCardControl(
                            _postService,
                            _reactionService,
                            _commentService
                        )
                        {
                            Width = 800,
                            Margin = new Padding(0, 10, 0, 10)
                        };

                        postCard.LoadPost(post);
                        flowPosts.Controls.Add(postCard);

                        // ✅ FIX: Delay nhỏ để tránh DbContext conflict
                        await Task.Delay(50);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 👥 Tải danh sách Followers
        /// </summary>
        private async Task LoadFollowersAsync()
        {
            if (_followService == null || flowFollowers == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowFollowers.Controls.Clear();

                var followers = await _followService.LayDanhSachNguoiTheoDoiAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    UserSession.CurrentUser.MaNguoiDung
                );

                if (!followers.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "👤 Chưa có ai theo dõi bạn",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowFollowers.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var follower in followers)
                {
                    // ✅ FIX: Truyền thêm avatar
                    var card = CreateUserCard(
                        follower.MaNguoiDung, 
                        follower.HoVaTen, 
                        follower.TenDangNhap, 
                        follower.DangTheoDoiLai,
                        follower.HinhDaiDien  // ← Avatar path
                    );
                    flowFollowers.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải followers: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ➕ Tải danh sách Following
        /// </summary>
        private async Task LoadFollowingAsync()
        {
            if (_followService == null || flowFollowing == null || UserSession.CurrentUser == null)
                return;

            try
            {
                flowFollowing.Controls.Clear();

                var following = await _followService.LayDanhSachDangTheoDoiAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    UserSession.CurrentUser.MaNguoiDung
                );

                if (!following.Any())
                {
                    var lblEmpty = new Label
                    {
                        Text = "❌ Bạn chưa theo dõi ai",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(10, 50, 10, 10)
                    };
                    flowFollowing.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var user in following)
                {
                    // ✅ FIX: Truyền thêm avatar
                    var card = CreateUserCard(
                        user.MaNguoiDung, 
                        user.HoVaTen, 
                        user.TenDangNhap, 
                        true,
                        user.HinhDaiDien  // ← Avatar path
                    );
                    flowFollowing.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải following: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🃏 Tạo user card
        /// </summary>
        private Panel CreateUserCard(Guid userId, string? name, string? email, bool isFollowing, string? avatarPath = null)
        {
            var pnlUser = new Panel
            {
                Width = 800,
                Height = 80,
                BackColor = Color.White,
                Margin = new Padding(0, 5, 0, 5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var pbAvatar = new PictureBox
            {
                Width = 60,
                Height = 60,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // ✅ FIX: Load avatar
            LoadAvatarForCard(pbAvatar, avatarPath, name);

            var lblName = new Label
            {
                Text = name ?? "Người dùng",
                Location = new Point(80, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            var lblEmail = new Label
            {
                Text = email ?? "",
                Location = new Point(80, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            var btnView = new Button
            {
                Text = "👁️ Xem trang",
                Location = new Point(680, 25),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => OpenUserProfile(userId);

            pnlUser.Controls.Add(pbAvatar);
            pnlUser.Controls.Add(lblName);
            pnlUser.Controls.Add(lblEmail);
            pnlUser.Controls.Add(btnView);

            return pnlUser;
        }

        /// <summary>
        /// 📄 Mở trang profile người dùng
        /// </summary>
        private void OpenUserProfile(Guid userId)
        {
            if (Program.ServiceProvider == null) return;

            try
            {
                var profilePage = new Social.UserProfilePage(userId, Program.ServiceProvider);

                var mainForm = this.FindForm();
                if (mainForm is Forms.MainForm mf)
                {
                    mf.LoadPage(profilePage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở trang profile: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🔄 Sự kiện đổi tab
        /// </summary>
        private async void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl == null) return;

            if (tabControl.SelectedTab == tabPosts)
                await LoadMyPostsAsync();
            else if (tabControl.SelectedTab == tabMyQuizzes)
                await LoadMyQuizzesAsync();
            else if (tabControl.SelectedTab == tabChallengeHistory)
                await LoadChallengeHistoryAsync();
            else if (tabControl.SelectedTab == tabFollowers)
                await LoadFollowersAsync();
            else if (tabControl.SelectedTab == tabFollowing)
                await LoadFollowingAsync();
        }

        /// <summary>
        /// ✏️ Chỉnh sửa thông tin
        /// </summary>
        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_userProfileService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var editDialog = new Forms.Social.EditProfileDialog(_userProfileService);

                if (editDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadProfileAsync();
                }
            }
            catch (Exception ex)

            {
                MessageBox.Show($"Lỗi mở dialog:  {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TabControl CreateModernTabControl()
        {
            var tab = new TabControl
            {
                Dock = DockStyle.Fill,
                DrawMode = TabDrawMode.OwnerDrawFixed,
                ItemSize = new Size(160, 42),
                SizeMode = TabSizeMode.Fixed,
                Font = new Font("Segoe UI Semibold", 10F),
                BackColor = Color.White
            };

            tab.DrawItem += (s, e) =>
            {
                var page = tab.TabPages[e.Index];
                bool selected = e.Index == tab.SelectedIndex;

                using var textBrush = new SolidBrush(selected ? Color.Black : Color.Gray);
                var textSize = e.Graphics.MeasureString(page.Text, tab.Font);

                float x = e.Bounds.Left + (e.Bounds.Width - textSize.Width) / 2;
                float y = e.Bounds.Top + 12;

                e.Graphics.DrawString(page.Text, tab.Font, textBrush, x, y);

                // Gạch chân tab active
                if (selected)
                {
                    using var pen = new Pen(Color.FromArgb(24, 119, 242), 3);
                    e.Graphics.DrawLine(
                        pen,
                        e.Bounds.Left + 25,
                        e.Bounds.Bottom - 3,
                        e.Bounds.Right - 25,
                        e.Bounds.Bottom - 3
                    );
                }
            };

            return tab;
        }

        private TabPage CreateTab(
    string title,
    out FlowLayoutPanel flow,
    bool wrap = false)
        {
            flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = wrap ? FlowDirection.LeftToRight : FlowDirection.TopDown,
                WrapContents = wrap,
                Padding = new Padding(30),
                BackColor = Color.FromArgb(240, 242, 245)
            };

            var tab = new TabPage(title)
            {
                BackColor = flow.BackColor
            };

            tab.Controls.Add(flow);
            return tab;
        }
        private Button CreateOutlineButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(180, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(24, 119, 242),
                Font = new Font("Segoe UI Semibold", 10F),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(24, 119, 242);

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(24, 119, 242);
                btn.ForeColor = Color.White;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(24, 119, 242);
            };

            return btn;
        }
        private Panel CreateStatCard(string title, string value, int x)
        {
            var pnl = new Panel
            {
                Size = new Size(130, 45),
                Location = new Point(x, 0),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.Gainsboro);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRoundedRectangle(
                    pen,
                    new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1),
                    10
                );
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(12, 4),
                AutoSize = true
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                Location = new Point(12, 22),
                AutoSize = true
            };

            pnl.Controls.Add(lblValue);
            pnl.Controls.Add(lblTitle);

            // Gán label value vào Tag để update số liệu
            pnl.Tag = lblValue;

            return pnl;
        }

    }
}