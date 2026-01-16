using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO;
using StudyApp.DTO.Requests.User;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Forms.Social
{
    /// <summary>
    /// ?? EDIT PROFILE DIALOG - Ch?nh s?a thông tin cá nhân
    /// 
    /// CH?C N?NG:
    /// 1. S?a H? và tên
    /// 2. S?a Email
    /// 3. S?a S? ?i?n tho?i
    /// 4. S?a Ti?u s?/Bio
    /// 5. Ch?n Gi?i tính
    /// 6. Ch?n Ngày sinh
    /// 7. ??i Avatar (path)
    /// </summary>
    public partial class EditProfileDialog : Form
    {
        private readonly IUserProfileService _userProfileService;

        // Controls
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
            this.Text = "Ch?nh s?a thông tin cá nhân";
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void InitializeControls()
        {
            int y = 20;
            int labelX = 30;
            int inputX = 180;
            int inputWidth = 370;

            // ========== TITLE ==========
            lblTitle = new Label
            {
                Text = "?? Ch?nh s?a thông tin cá nhân",
                Location = new Point(30, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 119, 242)
            };
            this.Controls.Add(lblTitle);
            y += 50;

            // ========== H? VÀ TÊN ==========
            lblName = new Label
            {
                Text = "H? và tên:",
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

            // ========== EMAIL ==========
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

            // ========== S? ?I?N THO?I ==========
            lblPhone = new Label
            {
                Text = "S? ?i?n tho?i:",
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

            // ========== GI?I TÍNH ==========
            lblGender = new Label
            {
                Text = "Gi?i tính:",
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
            cboGender.Items.AddRange(new object[] { "Không xác ??nh", "Nam", "N?" });
            cboGender.SelectedIndex = 0;
            this.Controls.Add(lblGender);
            this.Controls.Add(cboGender);
            y += 45;

            // ========== NGÀY SINH ==========
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

            // ========== AVATAR ==========
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
                Text = "??",
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

            // ========== TI?U S?/BIO ==========
            lblBio = new Label
            {
                Text = "Ti?u s?:",
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

            // ========== BUTTONS ==========
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
                Text = "?? L?u thay ??i",
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

        /// <summary>
        /// ?? Load thông tin hi?n t?i
        /// </summary>
        private async void LoadCurrentProfile()
        {
            if (UserSession.CurrentUser == null) return;

            // Load thông tin c? b?n t? UserSession
            if (txtName != null) txtName.Text = UserSession.CurrentUser.HoVaTen ?? "";
            if (txtEmail != null) txtEmail.Text = UserSession.CurrentUser.Email ?? "";
            if (txtPhone != null) txtPhone.Text = UserSession.CurrentUser.SoDienThoai ?? "";
            if (txtAvatar != null) txtAvatar.Text = UserSession.CurrentUser.HinhDaiDien ?? "";

            // ? FIX: Load thông tin ??y ?? t? API (bao g?m Gi?i tính, Ngày sinh, Ti?u s?)
            try
            {
                var fullProfile = await _userProfileService.GetProfileAsync(UserSession.CurrentUser.MaNguoiDung);
                if (fullProfile != null)
                {
                    // Load Gi?i tính
                    if (cboGender != null && fullProfile.GioiTinh.HasValue)
                    {
                        // GioiTinh t? API: 0 = Không xác ??nh, 1 = Nam, 2 = N?
                        cboGender.SelectedIndex = (int)fullProfile.GioiTinh.Value; // ? S?A: Cast sang int
                    }

                    // Load Ngày sinh
                    if (dtpBirthday != null && fullProfile.NgaySinh.HasValue)
                    {
                        dtpBirthday.Value = fullProfile.NgaySinh.Value.ToDateTime(TimeOnly.MinValue);
                    }

                    // Load Ti?u s?
                    if (txtBio != null && !string.IsNullOrEmpty(fullProfile.TieuSu))
                    {
                        txtBio.Text = fullProfile.TieuSu;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"?? L?i load profile ??y ??: {ex.Message}");
                // Không hi?n l?i cho user, vì thông tin c? b?n ?ã load
            }
        }

        /// <summary>
        /// ?? Ch?n file avatar
        /// </summary>
        private void BtnBrowseAvatar_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Ch?n ?nh ??i di?n",
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

        /// <summary>
        /// ?? L?u thay ??i
        /// </summary>
        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("B?n ch?a ??ng nh?p!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (btnSave != null)
            {
                btnSave.Enabled = false;
                btnSave.Text = "? ?ang l?u...";
            }

            try
            {
                // Validate
                if (!ValidateInput())
                {
                    if (btnSave != null)
                    {
                        btnSave.Enabled = true;
                        btnSave.Text = "?? L?u thay ??i";
                    }
                    return;
                }

                // T?o request
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

                // G?i API
                var success = await _userProfileService.UpdateProfileAsync(
                    UserSession.CurrentUser.MaNguoiDung,
                    request
                );

                if (success)
                {
                    // C?p nh?t UserSession (c?n reload t? DB ho?c update local)
                    if (request.HoVaTen != null) UserSession.CurrentUser.HoVaTen = request.HoVaTen;
                    if (request.Email != null) UserSession.CurrentUser.Email = request.Email;
                    if (request.SoDienThoai != null) UserSession.CurrentUser.SoDienThoai = request.SoDienThoai;
                    if (request.HinhDaiDien != null) UserSession.CurrentUser.HinhDaiDien = request.HinhDaiDien;

                    MessageBox.Show(
                        "? C?p nh?t thông tin thành công!",
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
                        "? C?p nh?t th?t b?i. Vui lòng th? l?i!",
                        "L?i",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"L?i: {ex.Message}",
                    "L?i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                if (btnSave != null)
                {
                    btnSave.Enabled = true;
                    btnSave.Text = "?? L?u thay ??i";
                }
            }
        }

        /// <summary>
        /// ?? Validate input
        /// </summary>
        private bool ValidateInput()
        {
            // Ki?m tra H? và tên
            if (txtName != null && string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nh?p h? và tên!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // Ki?m tra Email (n?u có)
            if (txtEmail != null && !string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email không h?p l?!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Ki?m tra Ti?u s? (max 500)
            if (txtBio != null && txtBio.Text.Length > 500)
            {
                MessageBox.Show("Ti?u s? t?i ?a 500 ký t?!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBio.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// ?? Ki?m tra email h?p l?
        /// </summary>
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
