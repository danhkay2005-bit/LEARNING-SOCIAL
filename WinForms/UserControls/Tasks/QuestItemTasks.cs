using StudyApp.DTO.Responses.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Tasks
{
    public partial class QuestItemTasks : UserControl
    {
        // Change the event declaration to nullable to resolve CS8618
        public event EventHandler<int>? OnClaimClicked;
        private int _maNhiemVu;
        public QuestItemTasks()
        {
            InitializeComponent();
        }

        public void SetData(TienDoNhiemVuResponse data)
        {
            _maNhiemVu = data.MaNhiemVu;
            lblIcon.Text = string.IsNullOrEmpty(data.BieuTuong) ? "📜" : data.BieuTuong;
            lblTen.Text = data.TenNhiemVu;
            

            string rewards = $"XP: + {data.ThuongXP}";
            if (data.ThuongVang > 0) rewards += $" | 🟡 +{data.ThuongVang}";
            if (data.ThuongKimCuong > 0) rewards += $" |💎 +{data.ThuongKimCuong}";

            lblMoTa.Text = $"{data.MoTa}\nQuà: {rewards}";

            // 2. TÍNH TOÁN TIẾN ĐỘ & PROGRESS BAR
            // Đảm bảo không chia cho 0
            int max = data.DieuKienDatDuoc > 0 ? data.DieuKienDatDuoc : 1;
            int current = Math.Min(data.TienDoHienTai, max); // Không vượt quá max

            pbTienDo.Maximum = 100;

            // Tính phần trăm
            int percent = (int)((double)current / max * 100);
            pbTienDo.Value = percent;

            // Hiển thị text tiến độ (VD: 1/3)
            if (data.DieuKienDatDuoc > 0)
            {
                lblTienDo.Text = $"{current}/{data.DieuKienDatDuoc}";
            }
            else
            {
                lblTienDo.Text = data.DaHoanThanh ? "Hoàn thành" : "Chưa xong";
            }

            // 3. XỬ LÝ TRẠNG THÁI NÚT BẤM (QUAN TRỌNG)
            if (data.DaHoanThanh)
            {
                if (data.DaNhanThuong)
                {
                    // Trạng thái: ĐÃ XONG & ĐÃ NHẬN -> Khóa nút
                    btnAction.Text = "Đã nhận";
                    btnAction.Enabled = false;
                    btnAction.BackColor = Color.LightGray;
                    btnAction.ForeColor = Color.DimGray;
                    btnAction.Cursor = Cursors.Default;
                }
                else
                {
                    // Trạng thái: ĐÃ XONG & CHƯA NHẬN -> Mở nút Nhận
                    btnAction.Text = "NHẬN THƯỞNG";
                    btnAction.Enabled = true;
                    btnAction.BackColor = Color.Gold; // Hoặc Color.Orange
                    btnAction.ForeColor = Color.Black; // Hoặc Color.White tùy thiết kế
                    btnAction.Cursor = Cursors.Hand;
                }
            }
            else
            {
                // Trạng thái: ĐANG LÀM -> Khóa nút (Hoặc để nút dẫn tới bài học)
                btnAction.Text = "Đang làm";
                btnAction.Enabled = false;
                btnAction.BackColor = Color.WhiteSmoke;
                btnAction.ForeColor = Color.Gray;
                btnAction.Cursor = Cursors.Default;
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            // Gửi sự kiện ra ngoài khi bấm nút
            OnClaimClicked?.Invoke(this, _maNhiemVu);
        }
    }
}
