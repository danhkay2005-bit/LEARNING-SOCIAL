using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.User;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Forms.Social
{
    public partial class EditProfileDialog : Form
    {
        private readonly IUserProfileService _userProfileService;
        private Label? lblTitle;
        private Label? lblName;
        private TextBox? txtName;
        private Label? lblEmail;
        private TextBox? txtEmail;
        private Label? lblPhone;
        private TextBox? txtPhone;
        private Label? lblBio;
        private TextBox? txtBio;
        private Label? lblGender;
        private ComboBox? cboGender;
        private Label? lblBirthday;
        private DateTimePicker? dtpBirthday;
        private Label? lblAvatar;
        private TextBox? txtAvatar;
        private Button? btnBrowseAvatar;
        private Button? btnSave;
        private Button? btnCancel;

        public EditProfileDialog(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
            InitializeComponent();
            InitializeControls();
            LoadCurrentProfile();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // EditProfileDialog
            // 
            ClientSize = new Size(582, 603);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditProfileDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chỉnh Sửa Thông Tin Cá Nhân";
            ResumeLayout(false);
        }

        private void InitializeControls()
        {
            int y = 20;
            int labelX = 30;
            int inputX = 180;
            int inputWidth = 370;

            lblTitle = new Label
            {
                Text = "Chỉnh Sửa thông tin cá nhân",
                Location = new Point(30, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };
            this.Controls.Add(lblTitle);
            y += 50;

            lblName = new Label
            {
                Text = "Họ và tên:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            txtName = new TextBox
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            y += 45;

            lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            txtEmail = new TextBox
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            y += 45;

            lblPhone = new Label
            {
                Text = "Số Điện Thoại:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            txtPhone = new TextBox
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            y += 45;

            lblGender = new Label
            {
                Text = "Giới tính:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            cboGender = new ComboBox
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboGender.Items.AddRange(new object[] { "Không xác Định", "Nam", "Nữ" });
            cboGender.SelectedIndex = 0;
            this.Controls.Add(lblGender);
            this.Controls.Add(cboGender);
            y += 45;

            lblBirthday = new Label
            {
                Text = "Ngày sinh:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dtpBirthday = new DateTimePicker
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F),
                Format = DateTimePickerFormat.Short
            };
            this.Controls.Add(lblBirthday);
            this.Controls.Add(dtpBirthday);
            y += 45;

            lblAvatar = new Label
            {
                Text = "Avatar URL:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            txtAvatar = new TextBox
            {
                Location = new Point(inputX, y),
                Width = 280,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "https://..."
            };
            btnBrowseAvatar = new Button
            {
                Text = "...",
                Location = new Point(inputX + 290, y),
                Width = 80,
                Height = 25,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnBrowseAvatar.Click += BtnBrowseAvatar_Click;
            this.Controls.Add(lblAvatar);
            this.Controls.Add(txtAvatar);
            this.Controls.Add(btnBrowseAvatar);
            y += 45;

            lblBio = new Label
            {
                Text = "Tiểu Sử:",
                Location = new Point(labelX, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            txtBio = new TextBox
            {
                Location = new Point(inputX, y),
                Width = inputWidth,
                Height = 80,
                Font = new Font("Segoe UI", 10F),
                Multiline = true,
                MaxLength = 500,
                PlaceholderText = "Vi?t vài dòng v? b?n..."
            };
            this.Controls.Add(lblBio);
            this.Controls.Add(txtBio);
            y += 100;

            btnCancel = new Button
            {
                Text = "H?y",
                Location = new Point(340, y),
                Width = 100,
                Height = 40,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSave = new Button
            {
                Text = "Lưu Thay Đổi",
                Location = new Point(450, y),
                Width = 130,
                Height = 40,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);
            this.CancelButton = btnCancel;
        }

        private async void LoadCurrentProfile()
        {
            if (UserSession.CurrentUser == null) return;

            if (txtName != null) txtName.Text = UserSession.CurrentUser.HoVaTen ?? "";
            if (txtEmail != null) txtEmail.Text = UserSession.CurrentUser.Email ?? "";
            if (txtPhone != null) txtPhone.Text = UserSession.CurrentUser.SoDienThoai ?? "";
            if (txtAvatar != null) txtAvatar.Text = UserSession.CurrentUser.HinhDaiDien ?? "";

            try
            {
                var fullProfile = await _userProfileService.GetProfileAsync(UserSession.CurrentUser.MaNguoiDung);
                if (fullProfile != null)
                {
                    if (cboGender != null && fullProfile.GioiTinh.HasValue)
                    {
                        cboGender.SelectedIndex = (int)fullProfile.GioiTinh.Value;
                    }

                    if (dtpBirthday != null && fullProfile.NgaySinh.HasValue)
                    {
                        dtpBirthday.Value = fullProfile.NgaySinh.Value.ToDateTime(TimeOnly.MinValue);
                    }

                    if (txtBio != null && !string.IsNullOrEmpty(fullProfile.TieuSu))
                    {
                        txtBio.Text = fullProfile.TieuSu;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Loại load profile: {ex.Message}");
            }
        }

        private void BtnBrowseAvatar_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn Ảnh Đại Diện",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                FilterIndex = 1
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (txtAvatar != null)
                {
                    txtAvatar.Text = openFileDialog.FileName;
                }
            }
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Bạn Chưa Đăng Nhập!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (btnSave != null)
            {
                btnSave.Enabled = false;
                btnSave.Text = "Đang Lưu...";
            }

            try
            {
                if (!ValidateInput())
                {
                    if (btnSave != null)
                    {
                        btnSave.Enabled = true;
                        btnSave.Text = "Lưu Thay Đổi";
                    }
                    return;
                }

                var request = new CapNhatHoSoRequest
                {
                    HoVaTen = txtName?.Text?.Trim(),
                    Email = txtEmail?.Text?.Trim(),
                    SoDienThoai = txtPhone?.Text?.Trim(),
                    TieuSu = txtBio?.Text?.Trim(),
                    HinhDaiDien = txtAvatar?.Text?.Trim(),
                    GioiTinh = cboGender?.SelectedIndex > 0 ? (byte?)cboGender.SelectedIndex : null,
                    NgaySinh = dtpBirthday != null ? DateOnly.FromDateTime(dtpBirthday.Value) : null
                };

                var success = await _userProfileService.UpdateProfileAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    request
                );

                if (success)
                {
                    if (request.HoVaTen != null) UserSession.CurrentUser.HoVaTen = request.HoVaTen;
                    if (request.Email != null) UserSession.CurrentUser.Email = request.Email;
                    if (request.SoDienThoai != null) UserSession.CurrentUser.SoDienThoai = request.SoDienThoai;
                    if (request.HinhDaiDien != null) UserSession.CurrentUser.HinhDaiDien = request.HinhDaiDien;

                    MessageBox.Show(
                        "Cập nhật thông tin thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật thất bại. vui lòng thử lại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                if (btnSave != null)
                {
                    btnSave.Enabled = true;
                    btnSave.Text = "Lưu Thay Đổi";
                }
            }
        }

        private bool ValidateInput()
        {
            if (txtName != null && string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (txtEmail != null && !string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email không h?p l?!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            if (txtBio != null && txtBio.Text.Length > 500)
            {
                MessageBox.Show("Tiểu sử tối đa 500 kí tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBio.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
