using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class QuanLyBoDeAdminPage : UserControl
    {
        private readonly IBoDeHocService _boDeService;
        private readonly IChuDeService _chuDeService;
        private readonly ITagService _tagService;


        private int _currentPageBoDe = 1;
        private readonly int _pageSize = 20;
        private int _totalPageBoDe = 1;

        public QuanLyBoDeAdminPage(
            IBoDeHocService boDeService,
            IChuDeService chuDeService,
            ITagService tagService)
        {
            InitializeComponent();

            _boDeService = boDeService;
            _chuDeService = chuDeService;
            _tagService = tagService;

            Load += async (_, __) => await LoadAllDataAsync();
        }

        private async Task LoadAllDataAsync()
        {
            try
            {
                await LoadBoDeData();
                await LoadChuDeData();
                await LoadTagData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= TAB 1: BỘ ĐỀ =================
        private async Task LoadBoDeData()
        {
            var result = await _boDeService.GetAllForAdminAsync(_currentPageBoDe, _pageSize, null, null);

            dgvBoDe.DataSource = result.Data.ToList();

            // Tính toán tổng số trang
            _totalPageBoDe = (int)Math.Ceiling((double)result.TotalCount / _pageSize);
            lblPageInfoBoDe.Text = $"Trang {_currentPageBoDe} / {_totalPageBoDe} (Tổng: {result.TotalCount})";

            // Cập nhật trạng thái nút
            btnPrevBoDe.Enabled = _currentPageBoDe > 1;
            btnNextBoDe.Enabled = _currentPageBoDe < _totalPageBoDe;

            FormatBoDeGrid();
        }

        private void FormatBoDeGrid()
        {
            foreach (DataGridViewRow row in dgvBoDe.Rows)
            {
                if (row.DataBoundItem is not BoDeHocResponse item) continue;

                if (item.DaXoa)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(60, 30, 30);

                if (!item.LaCongKhai)
                    row.DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            if (dgvBoDe.CurrentRow?.DataBoundItem is not BoDeHocResponse boDe)
                return;

            bool ok = await _boDeService.RestoreAsync(boDe.MaBoDe);
            if (ok) await LoadBoDeData();
        }

        // ================= TAB 2: CHỦ ĐỀ =================
        private async Task LoadChuDeData()
        {
            dgvChuDe.DataSource = (await _chuDeService.GetAllAsync()).ToList();
        }

        private async void btnAddChuDe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenChuDe.Text)) return;

            await _chuDeService.CreateAsync(new TaoChuDeRequest
            {
                TenChuDe = txtTenChuDe.Text.Trim()
            });

            txtTenChuDe.Clear();
            await LoadChuDeData();
        }

        // ================= TAB 3: HASHTAG =================
        private async Task LoadTagData()
        {
            dgvHashtags.DataSource = (await _tagService.GetAllAsync()).ToList();
        }

        private async void btnMergeTag_Click(object sender, EventArgs e)
        {
            if (dgvHashtags.CurrentRow?.DataBoundItem is not TagResponse tagSai)
                return;

            if (string.IsNullOrWhiteSpace(txtTagChuan.Text))
                return;

            var confirm = MessageBox.Show(
                $"Gộp #{tagSai.TenTag} vào #{txtTagChuan.Text.Trim()} ?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool ok = await _tagService.MergeTagsAsync(tagSai.MaTag, txtTagChuan.Text.Trim());
            if (ok)
            {
                txtTagChuan.Clear();
                await LoadTagData();
            }
        }

        private async void btnDeleteBoDe_Click(object sender, EventArgs e)
        {
            if (dgvBoDe.CurrentRow?.DataBoundItem is not BoDeHocResponse boDe) return;

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa bộ đề: {boDe.TieuDe}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                bool ok = await _boDeService.DeleteAsync(boDe.MaBoDe);
                if (ok) await LoadBoDeData();
            }
        }

        // Xóa chủ đề
        private async void btnDeleteChuDe_Click(object sender, EventArgs e)
        {
            if (dgvChuDe.CurrentRow?.DataBoundItem is not ChuDeResponse chuDe) return;

            var confirm = MessageBox.Show($"Xóa chủ đề '{chuDe.TenChuDe}'?\n(Các bộ đề thuộc chủ đề này sẽ trở thành 'Chưa phân loại')",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                bool ok = await _chuDeService.DeleteAsync(chuDe.MaChuDe);
                if (ok) await LoadChuDeData();
            }
        }

        // Xóa Hashtag rác
        private async void btnDeleteTag_Click(object sender, EventArgs e)
        {
            if (dgvHashtags.CurrentRow?.DataBoundItem is not TagResponse tag) return;

            var confirm = MessageBox.Show($"Xóa vĩnh viễn hashtag #{tag.TenTag}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                bool ok = await _tagService.DeleteTagAsync(tag.MaTag);
                if (ok) await LoadTagData();
            }
        }

        // ================= TAB 1: BỘ ĐỀ =================
        private async void btnTogglePublic_Click(object sender, EventArgs e)
        {
            if (dgvBoDe.CurrentRow?.DataBoundItem is not BoDeHocResponse boDe) return;

            // Đảo ngược trạng thái công khai hiện tại
            bool newStatus = !boDe.LaCongKhai;
            bool ok = await _boDeService.TogglePublicStatusAsync(boDe.MaBoDe, newStatus);

            if (ok) await LoadBoDeData();
        }

        // ================= TAB 2: CHỦ ĐỀ =================
        private async void btnEditChuDe_Click(object sender, EventArgs e)
        {
            if (dgvChuDe.CurrentRow?.DataBoundItem is not ChuDeResponse chuDe) return;
            if (string.IsNullOrWhiteSpace(txtTenChuDe.Text)) return;

            var request = new CapNhatChuDeRequest
            {
                MaChuDe = chuDe.MaChuDe,
                TenChuDe = txtTenChuDe.Text.Trim()
            };

            var result = await _chuDeService.UpdateAsync(request);
            if (result != null)
            {
                txtTenChuDe.Clear();
                await LoadChuDeData();
            }
        }

        // ================= TAB 3: HASHTAG =================
        private async void btnEditTag_Click(object sender, EventArgs e)
        {
            if (dgvHashtags.CurrentRow?.DataBoundItem is not TagResponse tag) return;
            if (string.IsNullOrWhiteSpace(txtTagChuan.Text)) return;

            string newName = txtTagChuan.Text.Trim();
            bool ok = await _tagService.UpdateTagNameAsync(tag.MaTag, newName);

            if (ok)
            {
                txtTagChuan.Clear();
                await LoadTagData();
            }
        }

        private async void btnNextBoDe_Click(object sender, EventArgs e)
        {
            if (_currentPageBoDe < _totalPageBoDe)
            {
                _currentPageBoDe++;
                await LoadBoDeData();
            }
        }

        // Sự kiện khi nhấn nút Prev
        private async void btnPrevBoDe_Click(object sender, EventArgs e)
        {
            if (_currentPageBoDe > 1)
            {
                _currentPageBoDe--;
                await LoadBoDeData();
            }
        }
    }
}
