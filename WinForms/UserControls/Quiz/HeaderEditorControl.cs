using StudyApp.DTO.Enums;
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
            SetupDifficultyComboBox();
            SetupImageEvents();
        }

        // 1. Cấu hình chọn ảnh
        private void SetupImageEvents()
        {
            btnThemAnh.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                        pictureBox1.Tag = ofd.FileName; // Lưu đường dẫn tạm
                    }
                }
            };
        }

        // 2. Nạp chế độ Riêng tư (LaCongKhai)
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

        // 3. Nạp Độ khó 1-5 (MucDoKho)
        private void SetupDifficultyComboBox()
        {
            // Nạp các số từ 1 đến 5. 
            // Lưu ý: Khi lưu sẽ ép kiểu sang MucDoKhoEnum
            cbbDoKho.DataSource = Enumerable.Range(1, 5).ToList();
            cbbDoKho.SelectedIndex = 1; // Mặc định mức 2 (Trung bình)
        }

        // 4. Nạp danh sách Chủ đề
        public void SetChuDeDataSource(IEnumerable<ChuDeResponse> dsChuDe)
        {
            cbbChuDe.DataSource = dsChuDe.ToList();
            cbbChuDe.DisplayMember = "TenChuDe";
            cbbChuDe.ValueMember = "MaChuDe";
        }

        // 5. Đóng gói dữ liệu khớp 100% với TaoBoDeHocRequest
        public TaoBoDeHocRequest GetHeaderData()
        {
            return new TaoBoDeHocRequest
            {
                TieuDe = txtTenBoDe.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                MaChuDe = (int?)cbbChuDe.SelectedValue,
                LaCongKhai = (bool)(cmbRiengTu.SelectedValue ?? true),
                // Ép kiểu từ int (1-5) sang Enum của bạn
                MucDoKho = (MucDoKhoEnum)(int)(cbbDoKho.SelectedItem ?? 2),
                AnhBia = pictureBox1.Tag?.ToString(),
                MaNguoiDung = Guid.Empty // Sẽ được gán tại TaoQuizPage từ UserSession
            };
        }

        // 6. Đổ dữ liệu ngược lại UI (khi chỉnh sửa)
        public void SetHeaderData(TaoBoDeHocRequest data)
        {
            if (data == null) return;
            txtTenBoDe.Text = data.TieuDe;
            txtMoTa.Text = data.MoTa;
            if (data.MaChuDe.HasValue)
            {
                cbbChuDe.SelectedValue = data.MaChuDe.Value;
            }
            cmbRiengTu.SelectedValue = data.LaCongKhai;
            cbbDoKho.SelectedItem = (int)data.MucDoKho;

            if (!string.IsNullOrEmpty(data.AnhBia) && File.Exists(data.AnhBia))
            {
                pictureBox1.Image = Image.FromFile(data.AnhBia);
                pictureBox1.Tag = data.AnhBia;
            }
        }
    }
}