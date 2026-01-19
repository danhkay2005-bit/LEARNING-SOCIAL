using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.User; // Namespace chứa VatPham (Tuỳ project của bạn)
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class QuanLyCuaHangAdminPage : UserControl
    {
        private readonly UserDbContext _context;
        public QuanLyCuaHangAdminPage( UserDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void QuanLyCuaHangAdminPage_Load(object sender, EventArgs e)
        {
            LoadComboBoxLoaiTien();
        }

        private void LoadComboBoxLoaiTien()
        {
            // 1: Vàng, 2: Kim Cương (Theo SQL của bạn)
            cboLoaiTien.Items.Clear();
            cboLoaiTien.Items.Add(new { Value = 1, Text = "Vàng 🪙" });
            cboLoaiTien.Items.Add(new { Value = 2, Text = "Kim Cương 💎" });

            cboLoaiTien.DisplayMember = "Text";
            cboLoaiTien.ValueMember = "Value";
            cboLoaiTien.SelectedIndex = 0; // Mặc định chọn Vàng
        }

        private void LoadData()
        {
            try
            {
                // Lấy danh sách vật phẩm từ DB
                var list = _context.VatPhams
                    .Select(v => new
                    {
                        v.MaVatPham,
                        v.TenVatPham,
                        v.Gia,
                        LoaiTienHienThi = v.LoaiTienTe == 1 ? "Vàng" : "Kim Cương",
                        v.LoaiTienTe, // Cột ẩn để xử lý logic
                        v.MoTa,
                        TrangThai = v.ConHang == true ? "Đang bán" : "Ngừng bán",
                        v.ConHang // Cột ẩn
                    })
                    .ToList();

                dgvVatPham.DataSource = list;

                // Format Grid
                var colMaVatPham = dgvVatPham.Columns["MaVatPham"];
                if (colMaVatPham != null) colMaVatPham.Width = 50;

                var colLoaiTienTe = dgvVatPham.Columns["LoaiTienTe"];
                if (colLoaiTienTe != null) colLoaiTienTe.Visible = false;

                var colConHang = dgvVatPham.Columns["ConHang"];
                if (colConHang != null) colConHang.Visible = false;

                var colTenVatPham = dgvVatPham.Columns["TenVatPham"];
                if (colTenVatPham != null) colTenVatPham.HeaderText = "Tên Vật Phẩm";

                var colGia = dgvVatPham.Columns["Gia"];
                if (colGia != null) colGia.HeaderText = "Giá";

                var colLoaiTienHienThi = dgvVatPham.Columns["LoaiTienHienThi"];
                if (colLoaiTienHienThi != null) colLoaiTienHienThi.HeaderText = "Loại Tiền";

                var colMoTa = dgvVatPham.Columns["MoTa"];
                if (colMoTa != null) colMoTa.HeaderText = "Mô Tả";

                var colTrangThai = dgvVatPham.Columns["TrangThai"];
                if (colTrangThai != null) colTrangThai.HeaderText = "Trạng Thái";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvVatPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvVatPham.Rows[e.RowIndex];

            txtMaVatPham.Text = row.Cells["MaVatPham"]?.Value?.ToString() ?? "";
            txtTenVatPham.Text = row.Cells["TenVatPham"]?.Value?.ToString() ?? "";
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";
            
            var giaObj = row.Cells["Gia"]?.Value;
            numGia.Value = giaObj != null ? Convert.ToDecimal(giaObj) : 0;

            // Xử lý Checkbox
            var conHangObj = row.Cells["ConHang"]?.Value;
            bool conHang = conHangObj != null && Convert.ToBoolean(conHangObj);
            chkConHang.Checked = conHang;

            // Xử lý ComboBox Loại tiền
            int loaiTien = Convert.ToInt32(row.Cells["LoaiTienTe"].Value);
            if (loaiTien == 1) cboLoaiTien.SelectedIndex = 0; // Vàng
            else cboLoaiTien.SelectedIndex = 1; // Kim Cương

            // Bật tắt nút
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenVatPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vật phẩm!");
                return;
            }

            try
            {
                // Lấy giá trị từ ComboBox
                if (cboLoaiTien.SelectedItem is not null)
                {
                    dynamic selectedItem = cboLoaiTien.SelectedItem;
                    int maLoaiTien = selectedItem.Value;

                    var vp = new VatPham
                    {
                        TenVatPham = txtTenVatPham.Text,
                        Gia = (int)numGia.Value,
                        LoaiTienTe = maLoaiTien,
                        MoTa = txtMoTa.Text,
                        ConHang = chkConHang.Checked,
                        MaDanhMuc = 1, // Mặc định danh mục 1 (Bảo vệ streak) hoặc bạn tạo thêm Combobox DanhMuc
                        DoHiem = 1,
                        ThoiGianTao = DateTime.Now
                    };

                    _context.VatPhams.Add(vp);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm vật phẩm thành công!");
                    ResetForm();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại tiền!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVatPham.Text)) return;

            try
            {
                int id = int.Parse(txtMaVatPham.Text);
                var vp = _context.VatPhams.Find(id);

                if (vp != null)
                {
                    dynamic selectedItem = cboLoaiTien.SelectedItem ?? "";

                    vp.TenVatPham = txtTenVatPham.Text;
                    vp.Gia = (int)numGia.Value;
                    vp.LoaiTienTe = selectedItem?.Value;
                    vp.MoTa = txtMoTa.Text;
                    vp.ConHang = chkConHang.Checked;

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!");
                    ResetForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVatPham.Text)) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa vật phẩm này? Hành động này có thể ảnh hưởng đến lịch sử mua hàng.", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaVatPham.Text);
                    var vp = _context.VatPhams.Find(id);
                    if (vp != null)
                    {
                        // Cách 1: Xóa cứng (Nếu chưa có ai mua)
                        _context.VatPhams.Remove(vp);

                        // Cách 2: Nếu sợ lỗi khoá ngoại thì chỉ set ConHang = false (Ẩn đi)
                        // vp.ConHang = false; 

                        _context.SaveChanges();
                        MessageBox.Show("Đã xóa vật phẩm!");
                        ResetForm();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa vật phẩm này vì đã có người mua. Hãy thử tắt 'Đang bán' thay vì xóa.\n\nChi tiết: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaVatPham.Text = "";
            txtTenVatPham.Text = "";
            txtMoTa.Text = "";
            numGia.Value = 0;
            chkConHang.Checked = true;
            cboLoaiTien.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
    }
}
