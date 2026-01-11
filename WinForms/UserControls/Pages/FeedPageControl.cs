using StudyApp.BLL.Services.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using StudyApp.DTO.Responses.Social;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.UserControls.Post;

namespace WinForms.UserControls.Pages
{
    public partial class FeedPageControl : UserControl
    {
        private readonly IBaiDangService _baiDangService;
        private string? _selectedImagePath;
        private string? _selectedVideoPath;
        private int _currentPage = 1;
        private const int PAGE_SIZE = 20;
        private bool _isLoading = false;

        public FeedPageControl(IBaiDangService baiDangService)
        {
            InitializeComponent();
            _baiDangService = baiDangService;

            SetupUI();
            RegisterEvents();
            _ = LoadFeedAsync();
        }

        // ================= SETUP =================
        private void SetupUI()
        {
            // Setup Avatar tròn
            SetupRoundAvatar();

            // Setup ComboBox Privacy
            cboPrivacy.Items.Clear();
            cboPrivacy.Items.Add(new PrivacyItem("🌍 Công khai", QuyenRiengTuEnum.CongKhai));
            cboPrivacy.Items.Add(new PrivacyItem("👥 Bạn bè", QuyenRiengTuEnum.ChiFollower));
            cboPrivacy.Items.Add(new PrivacyItem("🔒 Chỉ mình tôi", QuyenRiengTuEnum.RiengTu));
            cboPrivacy.SelectedIndex = 0;

            // Hiển thị tên user
            if (UserSession.IsLoggedIn && UserSession.CurrentUser != null)
            {
                txtCreatePost.PlaceholderText = $"{UserSession.CurrentUser.HoVaTen} ơi, bạn đang nghĩ gì? ";
            }
        }

        private void SetupRoundAvatar()
        {
            // Tạo avatar tròn với chữ cái đầu
            var bmp = new Bitmap(45, 45);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Vẽ hình tròn
                using (var brush = new SolidBrush(Color.FromArgb(0, 132, 255)))
                {
                    g.FillEllipse(brush, 0, 0, 44, 44);
                }

                // Vẽ chữ cái đầu
                var initial = UserSession.CurrentUser?.TenDangNhap?.Substring(0, 1).ToUpper() ?? "U";
                using (var font = new Font("Segoe UI", 16, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var size = g.MeasureString(initial, font);
                    g.DrawString(initial, font, brush,
                        (45 - size.Width) / 2,
                        (45 - size.Height) / 2);
                }
            }

            picAvatar.Image = bmp;

            // Làm tròn PictureBox
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, picAvatar.Width, picAvatar.Height);
            picAvatar.Region = new Region(path);
        }

        private void RegisterEvents()
        {
            btnPost.Click += BtnPost_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnAddImage.Click += BtnAddImage_Click;
            btnAddVideo.Click += BtnAddVideo_Click;
            txtCreatePost.TextChanged += TxtCreatePost_TextChanged;
            flowFeed.Scroll += FlowFeed_Scroll;

            // Hover effects
            SetupButtonHover(btnPost, Color.FromArgb(0, 132, 255), Color.FromArgb(0, 110, 220));
            SetupButtonHover(btnRefresh, Color.FromArgb(240, 242, 245), Color.FromArgb(220, 222, 225));
            SetupButtonHover(btnAddImage, Color.FromArgb(240, 242, 245), Color.FromArgb(220, 242, 230));
            SetupButtonHover(btnAddVideo, Color.FromArgb(240, 242, 245), Color.FromArgb(250, 220, 235));
        }

        private void SetupButtonHover(Button btn, Color normalColor, Color hoverColor)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = normalColor;
        }

        // ================= LOAD FEED =================
        private async Task LoadFeedAsync(bool append = false)
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                ShowLoading(true);

                if (!append)
                {
                    _currentPage = 1;
                    flowFeed.Controls.Clear();
                }

                var data = await Task.Run(() => _baiDangService.LayDanhSachBaiDang(new LayBaiDangRequest
                {
                    PageNumber = _currentPage,
                    PageSize = PAGE_SIZE
                }));

                if (data?.BaiDangs == null || data.BaiDangs.Count == 0)
                {
                    if (!append)
                    {
                        ShowEmptyState();
                    }
                    return;
                }

                flowFeed.SuspendLayout();

                foreach (var post in data.BaiDangs)
                {
                    var postControl = new PostItemControl(post, _baiDangService);
                    postControl.OnDeleted += () => _ = LoadFeedAsync();
                    postControl.Width = flowFeed.ClientSize.Width - 40;
                    flowFeed.Controls.Add(postControl);
                }

                flowFeed.ResumeLayout();
                _currentPage++;
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi tải bài đăng: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                ShowLoading(false);
            }
        }

        private void ShowLoading(bool show)
        {
            lblLoading.Visible = show;
            flowFeed.Visible = !show || flowFeed.Controls.Count > 0;
        }

        private void ShowEmptyState()
        {
            flowFeed.Controls.Add(new Label
            {
                Text = "🎉 Chưa có bài đăng nào!\n\nHãy là người đầu tiên chia sẻ với mọi người.",
                AutoSize = false,
                Size = new Size(flowFeed.ClientSize.Width - 40, 150),
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12F),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(20),
                BackColor = Color.White,
                Margin = new Padding(0, 10, 0, 10)
            });
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ================= CREATE POST =================
        private async void BtnPost_Click(object? sender, EventArgs e)
        {
            var content = txtCreatePost.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(content) &&
                string.IsNullOrEmpty(_selectedImagePath) &&
                string.IsNullOrEmpty(_selectedVideoPath))
            {
                MessageBox.Show("Vui lòng nhập nội dung hoặc thêm ảnh/video!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCreatePost.Focus();
                return;
            }

            if (content.Length > 5000)
            {
                MessageBox.Show("Nội dung không được vượt quá 5000 ký tự!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Disable button để tránh spam
            btnPost.Enabled = false;
            btnPost.Text = "⏳... ";

            try
            {
                var privacy = (cboPrivacy.SelectedItem as PrivacyItem)?.Value ?? QuyenRiengTuEnum.CongKhai;
                var loaiBaiDang = DeterminePostType();

                await Task.Run(() => _baiDangService.TaoBaiDang(new TaoBaiDangRequest
                {
                    NoiDung = content,
                    LoaiBaiDang = loaiBaiDang,
                    QuyenRiengTu = privacy,
                    HinhAnh = _selectedImagePath,
                    Video = _selectedVideoPath
                }));

                // Reset form
                txtCreatePost.Clear();
                _selectedImagePath = null;
                _selectedVideoPath = null;
                ResetMediaButtons();

                // Reload feed
                await LoadFeedAsync();

                MessageBox.Show("Đăng bài thành công!  🎉", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi đăng bài:  {ex.Message}");
            }
            finally
            {
                btnPost.Enabled = true;
                btnPost.Text = "Đăng bài";
            }
        }

        private LoaiBaiDangEnum DeterminePostType()
        {
            if (!string.IsNullOrEmpty(_selectedVideoPath))
                return LoaiBaiDangEnum.Video;
            if (!string.IsNullOrEmpty(_selectedImagePath))
                return LoaiBaiDangEnum.HinhAnh;
            return LoaiBaiDangEnum.VanBan;
        }

        // ================= MEDIA SELECTION =================
        private void BtnAddImage_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*. gif;*.bmp",
                Title = "Chọn ảnh"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = dialog.FileName;
                _selectedVideoPath = null;
                btnAddImage.Text = "✅ Đã chọn ảnh";
                btnAddImage.ForeColor = Color.Green;
                btnAddVideo.Text = "🎬 Video";
                btnAddVideo.ForeColor = Color.FromArgb(214, 41, 118);
            }
        }

        private void BtnAddVideo_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*. mov;*.wmv",
                Title = "Chọn video"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _selectedVideoPath = dialog.FileName;
                _selectedImagePath = null;
                btnAddVideo.Text = "✅ Đã chọn video";
                btnAddVideo.ForeColor = Color.Green;
                btnAddImage.Text = "🖼️ Ảnh";
                btnAddImage.ForeColor = Color.FromArgb(45, 134, 89);
            }
        }

        private void ResetMediaButtons()
        {
            btnAddImage.Text = "🖼️ Ảnh";
            btnAddImage.ForeColor = Color.FromArgb(45, 134, 89);
            btnAddVideo.Text = "🎬 Video";
            btnAddVideo.ForeColor = Color.FromArgb(214, 41, 118);
        }

        // ================= OTHER EVENTS =================
        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            btnRefresh.Text = "⏳... ";
            btnRefresh.Enabled = false;

            await LoadFeedAsync();

            btnRefresh.Text = "🔄 Tải lại";
            btnRefresh.Enabled = true;
        }

        private void TxtCreatePost_TextChanged(object? sender, EventArgs e)
        {
            var length = txtCreatePost.Text.Length;
            if (length > 4500)
            {
                txtCreatePost.ForeColor = length > 5000 ? Color.Red : Color.Orange;
            }
            else
            {
                txtCreatePost.ForeColor = Color.FromArgb(51, 51, 51);
            }
        }

        private void FlowFeed_Scroll(object? sender, ScrollEventArgs e)
        {
            // Infinite scroll - load thêm khi cuộn gần cuối
            if (e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement)
            {
                var scrollPos = flowFeed.VerticalScroll.Value;
                var scrollMax = flowFeed.VerticalScroll.Maximum - flowFeed.ClientSize.Height;

                if (scrollPos >= scrollMax - 100)
                {
                    _ = LoadFeedAsync(append: true);
                }
            }
        }
    }

    // ================= HELPER CLASS =================
    public class PrivacyItem
    {
        public string Display { get; }
        public QuyenRiengTuEnum Value { get; }

        public PrivacyItem(string display, QuyenRiengTuEnum value)
        {
            Display = display;
            Value = value;
        }

        public override string ToString() => Display;
    }
}