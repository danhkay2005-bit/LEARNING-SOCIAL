using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class QuanLyNguoiDungAdminPage : UserControl
    {
        private readonly UserDbContext _context;

        // UI controls
        private Label? lblTitle;
        private TextBox? txtSearch;
        private Button? btnAdd, btnEdit, btnDelete, btnGift, btnRefresh;
        private DataGridView? dgvUsers;

        public QuanLyNguoiDungAdminPage(UserDbContext context)
        {
            _context = context;
            InitializeComponentManual();
            Load += QuanLyNguoiDungAdminPage_Load;
        }

        private void QuanLyNguoiDungAdminPage_Load(object? sender, EventArgs e)
        {
            LoadData();
        }

        private void InitializeComponentManual()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            lblTitle = new Label
            {
                Text = "QUẢN LÝ NGƯỜI DÙNG",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 122, 204),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(30, 0, 0, 0)
            };

            Panel pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(20, 10, 20, 10) };

            txtSearch = new TextBox { Width = 250, Font = new Font("Segoe UI", 11), Location = new Point(10, 15) };
            txtSearch.PlaceholderText = "Tìm kiếm tài khoản, họ tên...";
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text);

            btnAdd = CreateButton("➕ Thêm", Color.SeaGreen, 280);
            btnAdd.Click += btnAdd_Click;

            btnEdit = CreateButton("✏️ Sửa", Color.DodgerBlue, 390);
            btnEdit.Click += btnEdit_Click;

            btnDelete = CreateButton("🗑 Xóa", Color.IndianRed, 500);
            btnDelete.Click += btnDelete_Click;

            btnGift = CreateButton("🎁 Tặng/Trừ", Color.Orange, 610);
            btnGift.Click += btnGift_Click;

            btnRefresh = CreateButton("🔄 Tải lại", Color.Gray, 720);
            btnRefresh.Click += (s, e) => LoadData();

            pnlToolbar.Controls.AddRange(new Control[] { txtSearch, btnAdd, btnEdit, btnDelete, btnGift, btnRefresh });

            dgvUsers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 40 },
                Font = new Font("Segoe UI", 10)
            };
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsers.ColumnHeadersHeight = 40;

            Panel pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            pnlGrid.Controls.Add(dgvUsers);

            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(lblTitle);
        }

        private Button CreateButton(string text, Color color, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 12),
                Size = new Size(100, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void LoadData(string keyword = "")
        {
            var query = _context.NguoiDungs.Where(u => (u.DaXoa ?? false) == false);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u =>
                    (u.TenDangNhap != null && u.TenDangNhap.Contains(keyword)) ||
                    (u.HoVaTen != null && u.HoVaTen.Contains(keyword)));
            }

            var list = query.Select(u => new
            {
                u.MaNguoiDung,
                u.TenDangNhap,
                u.HoVaTen,
                u.Email,
                u.Vang,
                u.KimCuong,
                u.TongDiemXp,
                u.ChuoiNgayHocLienTiep,
                u.NgayHoatDongCuoi,
                VaiTro = u.MaVaiTro == 1 ? "Admin" : "Member"
            }).ToList();

            if (dgvUsers != null)
            {
                dgvUsers.DataSource = list;
                dgvUsers.AutoResizeColumns();

                var colMaNguoiDung = dgvUsers.Columns["MaNguoiDung"];
                if (colMaNguoiDung != null) colMaNguoiDung.Visible = false;

                var colTenDangNhap = dgvUsers.Columns["TenDangNhap"];
                if (colTenDangNhap != null) colTenDangNhap.HeaderText = "Tài Khoản";

                var colHoVaTen = dgvUsers.Columns["HoVaTen"];
                if (colHoVaTen != null) colHoVaTen.HeaderText = "Họ Tên";

                var colTongDiemXp = dgvUsers.Columns["TongDiemXp"];
                if (colTongDiemXp != null) colTongDiemXp.HeaderText = "XP";

                var colChuoiNgayHocLienTiep = dgvUsers.Columns["ChuoiNgayHocLienTiep"];
                if (colChuoiNgayHocLienTiep != null) colChuoiNgayHocLienTiep.HeaderText = "Streak";

                var colVaiTro = dgvUsers.Columns["VaiTro"];
                if (colVaiTro != null) colVaiTro.HeaderText = "Vai Trò";
            }
        }


        private void btnAdd_Click(object? sender, EventArgs e)
        {
            Form addForm = new Form { Text = "Thêm Người Dùng", Size = new Size(400, 400), StartPosition = FormStartPosition.CenterParent };

            var lblUser = new Label { Text = "Tên đăng nhập:", Left = 20, Top = 20, Width = 100 };
            var txtUser = new TextBox { Left = 130, Top = 20, Width = 200 };

            var lblPass = new Label { Text = "Mật khẩu:", Left = 20, Top = 60, Width = 100 };
            var txtPass = new TextBox { Left = 130, Top = 60, Width = 200, UseSystemPasswordChar = true };

            var lblName = new Label { Text = "Họ và tên:", Left = 20, Top = 100, Width = 100 };
            var txtName = new TextBox { Left = 130, Top = 100, Width = 200 };

            var lblEmail = new Label { Text = "Email:", Left = 20, Top = 140, Width = 100 };
            var txtEmail = new TextBox { Left = 130, Top = 140, Width = 200 };

            var lblRole = new Label { Text = "Vai trò:", Left = 20, Top = 180, Width = 100 };
            var cbRole = new ComboBox { Left = 130, Top = 180, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbRole.Items.AddRange(new object[] { "Admin", "Member" });
            cbRole.SelectedIndex = 1;

            var btnConfirm = new Button { Text = "TẠO TÀI KHOẢN", DialogResult = DialogResult.OK, Left = 130, Top = 240, Width = 200, Height = 40, BackColor = Color.SeaGreen, ForeColor = Color.White };

            addForm.Controls.AddRange(new Control[] { lblUser, txtUser, lblPass, txtPass, lblName, txtName, lblEmail, txtEmail, lblRole, cbRole, btnConfirm });

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                    return;
                }

                if (_context.NguoiDungs.Any(u => u.TenDangNhap == txtUser.Text))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!");
                    return;
                }

                var newUser = new NguoiDung
                {
                    TenDangNhap = txtUser.Text,
                    MatKhauMaHoa = HashPassword(txtPass.Text),
                    HoVaTen = txtName.Text,
                    Email = txtEmail.Text,
                    MaVaiTro = cbRole.SelectedIndex == 0 ? 1 : 2,
                    MaCapDo = 1,
                    Vang = 100,
                    KimCuong = 5,
                    TongDiemXp = 0,
                    DaXoa = false,
                    ThoiGianTao = DateTime.Now
                };

                _context.NguoiDungs.Add(newUser);
                _context.SaveChanges();
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            if (dgvUsers != null && dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                if (row != null && row.Cells["MaNguoiDung"] != null)
                {
                    var userIdObj = row.Cells["MaNguoiDung"].Value;
                    if (userIdObj is not Guid userId) return;

                    var user = _context.NguoiDungs.Find(userId);
                    if (user == null) return;

                    Form editForm = new Form { Text = "Sửa Người Dùng", Size = new Size(400, 400), StartPosition = FormStartPosition.CenterParent };

                    var lblUser = new Label { Text = "Tên đăng nhập:", Left = 20, Top = 20, Width = 100 };
                    var txtUser = new TextBox { Left = 130, Top = 20, Width = 200, Text = user.TenDangNhap, ReadOnly = true };

                    var lblPass = new Label { Text = "Mật khẩu mới:", Left = 20, Top = 60, Width = 100 };
                    var txtPass = new TextBox { Left = 130, Top = 60, Width = 200, UseSystemPasswordChar = true };

                    var lblName = new Label { Text = "Họ và tên:", Left = 20, Top = 100, Width = 100 };
                    var txtName = new TextBox { Left = 130, Top = 100, Width = 200, Text = user.HoVaTen };

                    var lblEmail = new Label { Text = "Email:", Left = 20, Top = 140, Width = 100 };
                    var txtEmail = new TextBox { Left = 130, Top = 140, Width = 200, Text = user.Email };

                    var lblRole = new Label { Text = "Vai trò:", Left = 20, Top = 180, Width = 100 };
                    var cbRole = new ComboBox { Left = 130, Top = 180, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                    cbRole.Items.AddRange(new object[] { "Admin", "Member" });
                    cbRole.SelectedIndex = user.MaVaiTro == 1 ? 0 : 1;

                    var btnConfirm = new Button { Text = "CẬP NHẬT", DialogResult = DialogResult.OK, Left = 130, Top = 240, Width = 200, Height = 40, BackColor = Color.Orange, ForeColor = Color.White };

                    editForm.Controls.AddRange(new Control[] { lblUser, txtUser, lblPass, txtPass, lblName, txtName, lblEmail, txtEmail, lblRole, cbRole, btnConfirm });

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        user.HoVaTen = txtName.Text;
                        user.Email = txtEmail.Text;
                        user.MaVaiTro = cbRole.SelectedIndex == 0 ? 1 : 2;
                        if (!string.IsNullOrWhiteSpace(txtPass.Text))
                        {
                            user.MatKhauMaHoa = HashPassword(txtPass.Text);
                        }
                        _context.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!");
                        LoadData();
                    }
                }
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvUsers != null && dgvUsers.SelectedRows.Count > 0)
            {
                var userIdObj = dgvUsers.SelectedRows[0].Cells["MaNguoiDung"].Value;
                if (userIdObj is not Guid userId) return;

                var user = _context.NguoiDungs.Find(userId);
                if (user == null) return;

                if (MessageBox.Show($"Bạn có chắc muốn xóa user '{user.TenDangNhap}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    user.DaXoa = true;
                    _context.SaveChanges();
                    MessageBox.Show("Đã xóa thành công!");
                    LoadData();
                }
            }
        }

        private void btnGift_Click(object? sender, EventArgs e)
        {
            if (dgvUsers != null && dgvUsers.SelectedRows.Count > 0)
            {
                var userIdObj = dgvUsers.SelectedRows[0].Cells["MaNguoiDung"].Value;
                if (userIdObj is not Guid userId) return;

                var user = _context.NguoiDungs.Find(userId);
                if (user == null) return;

                Form giftForm = new Form { Text = $"Tặng/Trừ tài nguyên cho {user.TenDangNhap}", Size = new Size(350, 300), StartPosition = FormStartPosition.CenterParent };

                var lblVang = new Label { Text = "Thêm/Trừ Vàng:", Left = 30, Top = 30 };
                var numVang = new NumericUpDown { Left = 150, Top = 30, Width = 120, Maximum = 999999, Minimum = -999999 };

                var lblKimCuong = new Label { Text = "Thêm/Trừ Kim Cương:", Left = 30, Top = 70 };
                var numKimCuong = new NumericUpDown { Left = 150, Top = 70, Width = 120, Maximum = 999999, Minimum = -999999 };

                var lblXP = new Label { Text = "Thêm/Trừ XP:", Left = 30, Top = 110 };
                var numXP = new NumericUpDown { Left = 150, Top = 110, Width = 120, Maximum = 999999, Minimum = -999999 };

                var lblNote = new Label { Text = "(Nhập số âm để trừ)", Left = 30, Top = 150, ForeColor = Color.Gray, AutoSize = true };

                var btnSave = new Button { Text = "CẬP NHẬT", DialogResult = DialogResult.OK, Left = 80, Top = 200, Width = 180, Height = 40, BackColor = Color.Orange, ForeColor = Color.White };

                giftForm.Controls.AddRange(new Control[] { lblVang, numVang, lblKimCuong, numKimCuong, lblXP, numXP, lblNote, btnSave });

                if (giftForm.ShowDialog() == DialogResult.OK)
                {
                    user.Vang += (int)numVang.Value;
                    user.KimCuong += (int)numKimCuong.Value;
                    user.TongDiemXp += (int)numXP.Value;

                    if (numVang.Value != 0) LogTransaction(userId, "Vang", (int)numVang.Value, user.Vang ?? 0);
                    if (numKimCuong.Value != 0) LogTransaction(userId, "KimCuong", (int)numKimCuong.Value, user.KimCuong ?? 0);
                    if (numXP.Value != 0) LogTransaction(userId, "XP", (int)numXP.Value, user.TongDiemXp ?? 0);

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật tài nguyên thành công!");
                    LoadData();
                }
            }
        }

        private void LogTransaction(Guid userId, string type, int amount, int balanceAfter)
        {
            var log = new LichSuGiaoDich
            {
                MaNguoiDung = userId,
                LoaiGiaoDich = "AdminGrant",
                LoaiTien = type,
                SoLuong = amount,
                SoDuTruoc = balanceAfter - amount,
                SoDuSau = balanceAfter,
                MoTa = "Admin điều chỉnh số dư",
                ThoiGian = DateTime.Now
            };
            _context.LichSuGiaoDiches.Add(log);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}