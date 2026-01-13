using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace WinForms.UserControls.Quiz
{
    public partial class HeaderEditorControl : UserControl
    {
        public HeaderEditorControl()
        {
            InitializeComponent();
            SetupPrivacyComboBox();
        }

        // 1. Nạp chế độ Riêng tư (Static)
        private void SetupPrivacyComboBox()
        {
            var options = new[]
            {
                new { Name = "Công khai", Value = true },
                new { Name = "Riêng tư", Value = false }
            };
            cmbRiengTu.DataSource = options;
            cmbRiengTu.DisplayMember = "Name";
            cmbRiengTu.ValueMember = "Value";
        }

        // 2. Nạp danh sách Chủ đề từ Database (Dynamic)
        public void SetChuDeDataSource(IEnumerable<ChuDeResponse> dsChuDe)
        {
            cbbChuDe.DataSource = dsChuDe.ToList();
            cbbChuDe.DisplayMember = "TenChuDe"; // Tên thuộc tính trong ChuDeResponse
            cbbChuDe.ValueMember = "MaChuDe";      // ID thuộc tính trong ChuDeResponse
        }

        // 3. Lấy dữ liệu từ UI đóng gói vào Request
        public TaoBoDeHocRequest GetHeaderData()
        {
            return new TaoBoDeHocRequest
            {
                TieuDe = txtTenBoDe.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(), // Hashtag nằm ở đây
                MaChuDe = (int)(cbbChuDe.SelectedValue ?? 0),
                LaCongKhai = (bool)(cmbRiengTu.SelectedValue ?? true),
                // pictureBox1.Tag chứa đường dẫn ảnh bia nếu người dùng đã chọn
                AnhBia = pictureBox1.Tag?.ToString()
            };
        }

        // 4. Đổ dữ liệu cũ vào UI (Dùng khi quay lại từ slide câu hỏi)
        public void SetHeaderData(TaoBoDeHocRequest data)
        {
            if (data == null) return;
            txtTenBoDe.Text = data.TieuDe;
            txtMoTa.Text = data.MoTa;
            if (data.MaChuDe > 0) cbbChuDe.SelectedValue = data.MaChuDe;
            cmbRiengTu.SelectedValue = data.LaCongKhai;
        }
    }
}