using Microsoft.Extensions.DependencyInjection;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;

namespace WinForms.UserControls.Quiz
{
    public partial class HeaderEditorControl : UserControl
    {
        public HeaderEditorControl()
        {
            if (Program.ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not initialized.");
            InitializeComponent();
            SetupPrivacyComboBox();
            SetupDifficultyComboBox();
            SetupImageEvents();
            SetupAiImageEvent();
        }

        private void SetupAiImageEvent()
        {
            btnTaoAnhAI.Click += async (s, e) =>
            {
                string title = txtTenBoDe.Text.Trim();
                if (string.IsNullOrEmpty(title)) return;

                try
                {
                    btnTaoAnhAI.Enabled = false;
                    btnTaoAnhAI.Text = "⌛ Đang tải...";

                    string prompt = Uri.EscapeDataString(title + " digital art, high quality");
                    string aiUrl = $"https://image.pollinations.ai/prompt/{prompt}?width=1024&height=768&nologo=true";

                    using (HttpClient client = new HttpClient())
                    {
                        // Tải dữ liệu ảnh về dưới dạng mảng byte
                        byte[] imageBytes = await client.GetByteArrayAsync(aiUrl);

                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            // Chuyển đổi mảng byte thành đối tượng Image
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.Tag = aiUrl; // Lưu URL để dùng khi lưu vào DB
                        }
                    }
                    MessageBox.Show("Đã tải ảnh thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiển thị ảnh: " + ex.Message, "Thất bại");
                }
                finally
                {
                    btnTaoAnhAI.Enabled = true;
                    btnTaoAnhAI.Text = "✨ Tạo ảnh AI";
                }
            };
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
                        pictureBox1.Tag = ofd.FileName;
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
                MucDoKho = (MucDoKhoEnum)(int)(cbbDoKho.SelectedItem ?? 2),
                AnhBia = pictureBox1.Tag?.ToString(), // Lưu URL hoặc đường dẫn file
                MaNguoiDung = Guid.Empty
            };
        }

        // 6. Đổ dữ liệu ngược lại UI (khi chỉnh sửa)
        public void SetHeaderData(TaoBoDeHocRequest data)
        {
            if (data == null) return;
            txtTenBoDe.Text = data.TieuDe;
            txtMoTa.Text = data.MoTa;
            if (data.MaChuDe.HasValue) cbbChuDe.SelectedValue = data.MaChuDe.Value;
            cmbRiengTu.SelectedValue = data.LaCongKhai;
            cbbDoKho.SelectedItem = (int)data.MucDoKho;

            if (!string.IsNullOrEmpty(data.AnhBia))
            {
                if (data.AnhBia.StartsWith("http")) // Nếu là link AI
                {
                    pictureBox1.Image = null;
                    pictureBox1.ImageLocation = data.AnhBia;
                }
                else if (File.Exists(data.AnhBia)) // Nếu là file cục bộ
                {
                    pictureBox1.Image = Image.FromFile(data.AnhBia);
                }
                pictureBox1.Tag = data.AnhBia;
            }
        }
    }
}