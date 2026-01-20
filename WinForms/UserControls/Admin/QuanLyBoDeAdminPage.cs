using StudyApp.BLL.Interfaces.Learn;
using StudyApp.BLL.Interfaces.User;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class QuanLyBoDeAdminPage : UserControl
    {
        private readonly IBoDeHocService _boDeService;
        private readonly IChuDeService _chuDeService;
        private readonly ITagService _tagService;
        private readonly IUserProfileService _userService;
        private readonly IThachDauService _thachDauService;

        private int _currentPageBoDe = 1;
        private readonly int _pageSize = 20;
        private int _totalPageBoDe = 1;

        public QuanLyBoDeAdminPage(
            IBoDeHocService boDeService,
            IChuDeService chuDeService,
            ITagService tagService,
            IUserProfileService userService,
            IThachDauService thachDauService)
        {
            InitializeComponent();
            _boDeService = boDeService;
            _chuDeService = chuDeService;
            _tagService = tagService;
            _thachDauService = thachDauService;
            _userService = userService;

            // --- CẤU HÌNH GIAO DIỆN ---
            var allGrids = new[] { dgvBoDe, dgvChuDe, dgvHashtags, dgvLichSuHoc, dgvLichSuThachDau };
            foreach (var grid in allGrids) ApplyModernWhiteStyle(grid);

            this.Load += async (_, __) => await LoadAllDataAsync();

            // Đổ dữ liệu vào TextBox khi chọn dòng (Tiện cho việc Sửa)
            dgvChuDe.SelectionChanged += (s, e) => {
                if (dgvChuDe.CurrentRow?.DataBoundItem is ChuDeResponse chuDe) txtTenChuDe.Text = chuDe.TenChuDe;
            };
            dgvHashtags.SelectionChanged += (s, e) => {
                if (dgvHashtags.CurrentRow?.DataBoundItem is TagResponse tag) txtTagChuan.Text = tag.TenTag;
            };
        }

        private void ApplyModernWhiteStyle(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true; // ✅ Bật tự động tạo cột
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(241, 242, 246);
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F);
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.Height = 50;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(dgv, true, null);
        }

        private async Task LoadAllDataAsync()
        {
            try
            {
                // Nạp tuần tự từng bảng để tránh lỗi Database Context
                await LoadBoDeData();
                await LoadChuDeData();
                await LoadTagData();
                await LoadStudyHistoryData();
                await LoadChallengeHistoryData();
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi nạp dữ liệu: {ex.Message}"); }
        }

        private async Task LoadBoDeData()
        {
            var result = await _boDeService.GetAllForAdminAsync(_currentPageBoDe, _pageSize, null, null);
            dgvBoDe.DataSource = result.Data.ToList();
            _totalPageBoDe = (int)Math.Ceiling((double)result.TotalCount / _pageSize);
            lblPageInfoBoDe.Text = $"Trang {_currentPageBoDe} / {_totalPageBoDe}";
            btnPrevBoDe.Enabled = _currentPageBoDe > 1;
            btnNextBoDe.Enabled = _currentPageBoDe < _totalPageBoDe;
            FormatBoDeGrid();
        }

        private async Task LoadStudyHistoryData()
        {
            var sessions = await _boDeService.GetRecentSessionsAsync(50);
            dgvLichSuHoc.DataSource = sessions.Select(s => new {
                Time = s.ThoiGian?.ToString("dd/MM HH:mm"),
                User = s.TenNguoiDung,
                Deck = s.TenBoDe,
                Result = $"{s.TyLeDung}% Đúng"
            }).ToList();
        }

        private async Task LoadChallengeHistoryData()
        {
            var challenges = await _thachDauService.GetRecentChallengesAsync(50);
            dgvLichSuThachDau.DataSource = challenges.Select(c => new {
                Time = c.ThoiGianKetThuc.ToString("dd/MM HH:mm"),
                User = c.TenNguoiDung,
                Deck = c.TenBoDe,
                Status = c.LaNguoiThang ? "🏆 THẮNG" : "🏳️ THUA",
                Score = c.Diem
            }).ToList();
        }

        private void FormatBoDeGrid()
        {
            foreach (DataGridViewRow row in dgvBoDe.Rows)
            {
                if (row.DataBoundItem is not BoDeHocResponse item) continue;
                if (item.DaXoa) { row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235); row.DefaultCellStyle.ForeColor = Color.Red; }
                else if (!item.LaCongKhai) row.DefaultCellStyle.ForeColor = Color.Silver;
            }
        }

        // --- CÁC SỰ KIỆN CLICK (Gắn với Designer qua +=) ---
        private async void btnRestore_Click(object sender, EventArgs e) { if (dgvBoDe.CurrentRow?.DataBoundItem is BoDeHocResponse b && await _boDeService.RestoreAsync(b.MaBoDe)) await LoadBoDeData(); }
        private async void btnDeleteBoDe_Click(object sender, EventArgs e) { if (dgvBoDe.CurrentRow?.DataBoundItem is BoDeHocResponse b && MessageBox.Show("Xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) if (await _boDeService.DeleteAsync(b.MaBoDe)) await LoadBoDeData(); }
        private async void btnTogglePublic_Click(object sender, EventArgs e) { if (dgvBoDe.CurrentRow?.DataBoundItem is BoDeHocResponse b) if (await _boDeService.TogglePublicStatusAsync(b.MaBoDe, !b.LaCongKhai)) await LoadBoDeData(); }
        private async void btnAddChuDe_Click(object sender, EventArgs e) { if (!string.IsNullOrWhiteSpace(txtTenChuDe.Text)) { await _chuDeService.CreateAsync(new TaoChuDeRequest { TenChuDe = txtTenChuDe.Text.Trim() }); txtTenChuDe.Clear(); await LoadChuDeData(); } }
        private async void btnEditChuDe_Click(object sender, EventArgs e) { if (dgvChuDe.CurrentRow?.DataBoundItem is ChuDeResponse c && !string.IsNullOrWhiteSpace(txtTenChuDe.Text)) { await _chuDeService.UpdateAsync(new CapNhatChuDeRequest { MaChuDe = c.MaChuDe, TenChuDe = txtTenChuDe.Text.Trim() }); txtTenChuDe.Clear(); await LoadChuDeData(); } }
        private async void btnDeleteChuDe_Click(object sender, EventArgs e) { if (dgvChuDe.CurrentRow?.DataBoundItem is ChuDeResponse c && MessageBox.Show("Xóa chủ đề?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { await _chuDeService.DeleteAsync(c.MaChuDe); await LoadChuDeData(); } }
        private async void btnMergeTag_Click(object sender, EventArgs e) { if (dgvHashtags.CurrentRow?.DataBoundItem is TagResponse t && !string.IsNullOrWhiteSpace(txtTagChuan.Text)) if (await _tagService.MergeTagsAsync(t.MaTag, txtTagChuan.Text.Trim())) { txtTagChuan.Clear(); await LoadTagData(); } }
        private async void btnEditTag_Click(object sender, EventArgs e) { if (dgvHashtags.CurrentRow?.DataBoundItem is TagResponse t && !string.IsNullOrWhiteSpace(txtTagChuan.Text)) { await _tagService.UpdateTagNameAsync(t.MaTag, txtTagChuan.Text.Trim()); txtTagChuan.Clear(); await LoadTagData(); } }
        private async void btnDeleteTag_Click(object sender, EventArgs e) { if (dgvHashtags.CurrentRow?.Tag is TagResponse t && MessageBox.Show("Xóa tag?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { await _tagService.DeleteTagAsync(t.MaTag); await LoadTagData(); } }
        private async void btnNextBoDe_Click(object sender, EventArgs e) { if (_currentPageBoDe < _totalPageBoDe) { _currentPageBoDe++; await LoadBoDeData(); } }
        private async void btnPrevBoDe_Click(object sender, EventArgs e) { if (_currentPageBoDe > 1) { _currentPageBoDe--; await LoadBoDeData(); } }

        private async Task LoadChuDeData() => dgvChuDe.DataSource = (await _chuDeService.GetAllAsync()).ToList();
        private async Task LoadTagData() => dgvHashtags.DataSource = (await _tagService.GetAllAsync()).ToList();
    }
}