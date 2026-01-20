using StudyApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using StudyApp.DAL.Entities.Social;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Admin
{
    public partial class QuanLyBaiDangAdminPage : UserControl
    {
        private readonly SocialDbContext _context;

        public class UserDbContext : DbContext
        {
            public DbSet<BaiDang> BaiDang { get; set; }                    
        }

        public QuanLyBaiDangAdminPage(SocialDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void QuanLyBaiDangAdminPage_Load(object sender, EventArgs e)
        {

            LoadData();
            ResetForm(); // Đặt trạng thái ban đầu
        }


        private void LoadData(string keyword = "")
        {
            try
            {
                var query = _context.BaiDangs.AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string k = keyword.ToLower().Trim();
                    Guid guidSearch;
                    bool isGuid = Guid.TryParse(k, out guidSearch);

                    query = query.Where(bv =>
                        (bv.NoiDung != null && bv.NoiDung.ToLower().Contains(k)) ||
                        (isGuid && bv.MaNguoiDung == guidSearch) ||
                        bv.MaNguoiDung.ToString().ToLower().Contains(k)
                    );
                }

                var list = query
                    .Where(bv => bv.DaXoa == false)
                    .OrderByDescending(bv => bv.ThoiGianTao)
                    .Select(bv => new
                    {
                        bv.MaBaiDang,
                        bv.NoiDung,
                        bv.ThoiGianTao,
                        TenNguoiDung = bv.MaNguoiDung.ToString(),
                        TaiKhoan = bv.MaNguoiDung.ToString(),
                        bv.MaNguoiDung
                    })
                    .ToList();

                dgvBaiViet.DataSource = list;

                if(dgvBaiViet.DataSource == null)
                {
                    MessageBox.Show("Không có bài viết nào để hiển thị.");
                    return;
                }
                // Cấu hình hiển thị cột
                if (dgvBaiViet.Columns["MaBaiDang"] != null) dgvBaiViet?.Columns["MaBaiDang"]?.Visible = false;
                if (dgvBaiViet?.Columns["MaNguoiDung"] != null) dgvBaiViet?.Columns["MaNguoiDung"]?.Visible = false;

                // if (dgvBaiViet.Columns["TieuDe"] != null) dgvBaiViet.Columns["TieuDe"].HeaderText = "Tiêu Đề";
                if (dgvBaiViet?.Columns["NoiDung"] != null) dgvBaiViet?.Columns["NoiDung"]?.HeaderText = "Nội Dung";
                if (dgvBaiViet?.Columns["NoiDung"] != null) dgvBaiViet?.Columns["NoiDung"]?.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (dgvBaiViet?.Columns["TenNguoiDung"] != null) dgvBaiViet?.Columns["TenNguoiDung"]?.HeaderText = "Người Đăng";
                if (dgvBaiViet?.Columns["TaiKhoan"] != null) dgvBaiViet?.Columns["TaiKhoan"]?.HeaderText = "Tài Khoản";
                if (dgvBaiViet?.Columns["NgayTao"] != null) dgvBaiViet?.Columns["NgayTao"]?.HeaderText = "Ngày Đăng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }




        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBaiViet.Text)) return;
            if (!Guid.TryParse(txtMaBaiViet.Text, out Guid id))
            {
                MessageBox.Show("Mã bài viết không hợp lệ.");
                return;
            }
            try
            {
                var bv = _context.BaiDangs.Find(id);

                if (bv == null)
                {
                    MessageBox.Show("Không tìm thấy bài viết trong cơ sở dữ liệu!");
                    return;
                }

                bv.NoiDung = txtNoiDung.Text;
                _context.SaveChanges();

                MessageBox.Show("Cập nhật nội dung bài viết thành công!");
                ResetForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBaiViet.Text)) return;
            if (!Guid.TryParse(txtMaBaiViet.Text, out Guid id))
            {
                MessageBox.Show("Mã bài viết không hợp lệ.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn ẩn bài viết này khỏi hệ thống?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    var bv = _context.BaiDangs.Find(id);

                    if (bv == null)
                    {
                        MessageBox.Show("Không tìm thấy bài viết trong cơ sở dữ liệu!");
                        return;
                    }

                    bv.DaXoa = true;
                    _context.SaveChanges();

                    MessageBox.Show("Đã xóa bài viết!");
                    ResetForm();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
        }

        private void ResetForm()
        {
            txtMaBaiViet.Text = "";
            txtNguoiDang.Text = "";
            txtNoiDung.Text = "";
            if (txtTieuDe != null) txtTieuDe.Text = ""; // Kiểm tra null phòng hờ chưa kéo textbox này
            txtTimKiem.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Bỏ chọn trên grid
            dgvBaiViet.ClearSelection();
        }



        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvBaiViet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBaiViet.Rows[e.RowIndex];

            // Lấy đúng giá trị Guid của MaBaiDang (cột ẩn)
            object maBaiDangValue = row.Cells["MaBaiDang"].Value ?? "";
            txtMaBaiViet.Text = (maBaiDangValue != null && Guid.TryParse(maBaiDangValue.ToString(), out var guid))
      ? guid.ToString()
      : "";

            txtNoiDung.Text = row.Cells["NoiDung"].Value?.ToString() ?? "";
            txtNguoiDang.Text = row.Cells["TenNguoiDung"].Value?.ToString() ?? "";

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
    }
}
