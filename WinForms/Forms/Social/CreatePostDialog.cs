using StudyApp.BLL.Interfaces.Social;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Social;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Forms.Social
{
    public partial class CreatePostDialog : Form
    {
        private readonly IPostService? _postService;
        private string? _selectedImagePath;

        // Controls
        private Panel? pnlHeader;
        private Label? lblTitle;
        private PictureBox? pbAvatar;
        private Label? lblUsername;
        private TextBox? txtContent;
        private PictureBox? pbPreview;
        private Button? btnRemoveImage;
        private Panel? pnlFooter;
        private Button? btnSelectImage;
        private ComboBox? cboPrivacy;
        private Button? btnCancel;
        private Button? btnPost;

        // ✅ Constructor mặc định (cho Designer)
        public CreatePostDialog()
        {
            InitializeComponent(); // ← Gọi method từ Designer. cs
            InitializeCustomControls(); // ← Tạo method mới
        }

        // ✅ Constructor với DI
        public CreatePostDialog(IPostService postService) : this()
        {
            _postService = postService;
        }

        // ✅ THAY ĐỔI:  Đổi tên method từ InitializeComponent → InitializeCustomControls
        private void InitializeCustomControls()
        {
            this.Text = "Tạo bài viết mới";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.SuspendLayout();

            // ===== HEADER =====
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            lblTitle = new Label
            {
                Text = "Tạo bài viết",
                Location = new Point(15, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };

            pbAvatar = new PictureBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(15, 40),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblUsername = new Label
            {
                Location = new Point(65, 48),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = UserSession.CurrentUser?.HoVaTen ?? "Người dùng"
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(pbAvatar);
            pnlHeader.Controls.Add(lblUsername);

            // ===== CONTENT =====
            txtContent = new TextBox
            {
                Location = new Point(15, 100),
                Width = 550,
                Height = 200,
                Multiline = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical,
                PlaceholderText = "Bạn đang nghĩ gì?"
            };
            txtContent.TextChanged += TxtContent_TextChanged;

            // ===== IMAGE PREVIEW =====
            pbPreview = new PictureBox
            {
                Location = new Point(15, 310),
                Width = 550,
                Height = 0,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            btnRemoveImage = new Button
            {
                Text = "✖",
                Location = new Point(540, 315),
                Width = 30,
                Height = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false
            };
            btnRemoveImage.FlatAppearance.BorderSize = 0;
            btnRemoveImage.Click += BtnRemoveImage_Click;

            // ===== FOOTER =====
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(245, 246, 247),
                Padding = new Padding(15)
            };

            btnSelectImage = new Button
            {
                Text = "📷 Thêm ảnh",
                Location = new Point(15, 15),
                Width = 120,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnSelectImage.FlatAppearance.BorderColor = Color.LightGray;
            btnSelectImage.Click += BtnSelectImage_Click;

            cboPrivacy = new ComboBox
            {
                Location = new Point(145, 15),
                Width = 150,
                Height = 40,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            cboPrivacy.Items.Add("🌐 Công khai");
            cboPrivacy.Items.Add("🔒 Riêng tư");
            cboPrivacy.Items.Add("👥 Chỉ Follower");
            cboPrivacy.SelectedIndex = 0;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(380, 15),
                Width = 80,
                Height = 40,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.Click += BtnCancel_Click;

            btnPost = new Button
            {
                Text = "Đăng bài",
                Location = new Point(470, 15),
                Width = 100,
                Height = 40,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnPost.FlatAppearance.BorderSize = 0;
            btnPost.Click += BtnPost_Click;

            pnlFooter.Controls.Add(btnSelectImage);
            pnlFooter.Controls.Add(cboPrivacy);
            pnlFooter.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(btnPost);

            // ===== ADD TO FORM =====
            this.Controls.Add(txtContent);
            this.Controls.Add(pbPreview);
            this.Controls.Add(btnRemoveImage);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlHeader);

            this.ResumeLayout(false);
        }

        // ===== SỰ KIỆN =====

        private void TxtContent_TextChanged(object? sender, EventArgs e)
        {
            if (btnPost != null && txtContent != null)
            {
                btnPost.Enabled = !string.IsNullOrWhiteSpace(txtContent.Text) || !string.IsNullOrEmpty(_selectedImagePath);
            }
        }

        private void BtnSelectImage_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn hình ảnh",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = openFileDialog.FileName;

                if (pbPreview != null && btnRemoveImage != null)
                {
                    pbPreview.Image = Image.FromFile(_selectedImagePath);
                    pbPreview.Height = 150;
                    pbPreview.Visible = true;
                    btnRemoveImage.Visible = true;
                    btnRemoveImage.BringToFront();
                    this.Height = 680;
                }

                if (btnPost != null)
                    btnPost.Enabled = true;
            }
        }

        private void BtnRemoveImage_Click(object? sender, EventArgs e)
        {
            _selectedImagePath = null;

            if (pbPreview != null && btnRemoveImage != null)
            {
                pbPreview.Image = null;
                pbPreview.Height = 0;
                pbPreview.Visible = false;
                btnRemoveImage.Visible = false;
                this.Height = 500;
            }

            TxtContent_TextChanged(null, EventArgs.Empty);
        }

        private async void BtnPost_Click(object? sender, EventArgs e)
        {
            if (_postService == null || !UserSession.IsLoggedIn || UserSession.CurrentUser == null)
                return;

            if (txtContent == null || cboPrivacy == null)
                return;

            try
            {
                if (btnPost != null)
                {
                    btnPost.Enabled = false;
                    btnPost.Text = "Đang đăng...";
                }

                var privacy = cboPrivacy.SelectedIndex switch
                {
                    0 => QuyenRiengTuEnum.CongKhai,
                    1 => QuyenRiengTuEnum.RiengTu,
                    2 => QuyenRiengTuEnum.ChiFollower,
                    _ => QuyenRiengTuEnum.CongKhai
                };

                var request = new TaoBaiDangRequest
                {
                    MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                    NoiDung = txtContent.Text.Trim(),
                    LoaiBaiDang = string.IsNullOrEmpty(_selectedImagePath) ? LoaiBaiDangEnum.VanBan : LoaiBaiDangEnum.HinhAnh,
                    HinhAnh = _selectedImagePath,
                    QuyenRiengTu = privacy
                };

                await _postService.CreatePostAsync(request);

                MessageBox.Show("✅ Đăng bài thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (btnPost != null)
                {
                    btnPost.Enabled = true;
                    btnPost.Text = "Đăng bài";
                }
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}