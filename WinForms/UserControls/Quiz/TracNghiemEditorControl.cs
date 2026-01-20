using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http; // Thêm thư viện này
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class TracNghiemEditorControl : UserControl, IQuestionEditor
    {
        private static readonly Random _random = new Random();
        private static readonly HttpClient _httpClient = new HttpClient(); // Dùng chung HttpClient để tối ưu

        public TracNghiemEditorControl()
        {
            InitializeComponent();
            SetupEvents();
            SetupAiImageEvent(); // Đăng ký sự kiện AI
        }

        private void SetupEvents()
        {
            // Xử lý chọn ảnh thủ công từ máy tính
            btnThemAnh.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        picCauHoi.Image = Image.FromFile(ofd.FileName);
                        picCauHoi.Tag = ofd.FileName;
                    }
                }
            };
        }

        /// <summary>
        /// Logic tạo ảnh bằng AI dựa trên nội dung câu hỏi
        /// </summary>
        private void SetupAiImageEvent()
        {
            btnTaoAnhAI.Click += async (s, e) =>
            {
                string promptText = txtCauHoi.Text.Trim();
                if (string.IsNullOrEmpty(promptText))
                {
                    MessageBox.Show("Vui lòng nhập nội dung câu hỏi để làm dữ liệu tạo ảnh!", "Thông báo");
                    return;
                }

                try
                {
                    // 1. Cập nhật UI trạng thái chờ
                    btnTaoAnhAI.Enabled = false;
                    btnTaoAnhAI.Text = "⌛...";

                    // 2. Chuẩn bị URL (Sử dụng Pollinations.ai tương tự HeaderEditor)
                    // Thêm các keyword để ảnh trông chuyên nghiệp hơn cho giáo dục
                    string prompt = Uri.EscapeDataString(promptText + ", educational illustration, clean background, 3d render style");
                    string aiUrl = $"https://image.pollinations.ai/prompt/{prompt}?width=1024&height=768&nologo=true";

                    // 3. Tải dữ liệu ảnh
                    byte[] imageBytes = await _httpClient.GetByteArrayAsync(aiUrl);

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // 4. Hiển thị lên PictureBox
                        picCauHoi.Image = Image.FromStream(ms);
                        picCauHoi.Tag = aiUrl; // Lưu URL để GetQuestionData lấy và lưu vào database
                    }


                    MessageBox.Show("Đã tạo ảnh minh họa AI thành công!", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối AI: " + ex.Message, "Thất bại");
                }
                finally
                {
                    btnTaoAnhAI.Enabled = true;
                    btnTaoAnhAI.Text = "✨ Tạo AI";
                }
            };
        }

        public ChiTietTheRequest GetQuestionData()
        {
            string dapAnDung = "";
            if (rbA.Checked) dapAnDung = txtDapAnA.Text.Trim();
            else if (rbB.Checked) dapAnDung = txtDapAnB.Text.Trim();
            else if (rbC.Checked) dapAnDung = txtDapAnC.Text.Trim();
            else if (rbD.Checked) dapAnDung = txtDapAnD.Text.Trim();

            var danhSachTam = new List<TaoDapAnTracNghiemRequest>
            {
                new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnA.Text.Trim(), LaDapAnDung = rbA.Checked },
                new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnB.Text.Trim(), LaDapAnDung = rbB.Checked },
                new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnC.Text.Trim(), LaDapAnDung = rbC.Checked },
                new TaoDapAnTracNghiemRequest { NoiDung = txtDapAnD.Text.Trim(), LaDapAnDung = rbD.Checked }
            };

            var danhSachDaTron = danhSachTam.OrderBy(x => _random.Next()).ToList();

            for (int i = 0; i < danhSachDaTron.Count; i++)
            {
                danhSachDaTron[i].ThuTu = i + 1;
            }

            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.TracNghiem,
                    MatTruoc = txtCauHoi.Text.Trim(),
                    MatSau = dapAnDung,
                    HinhAnhTruoc = picCauHoi.Tag?.ToString(), // Có thể là link HTTP hoặc đường dẫn file
                    ThuTu = 0
                },
                DapAnTracNghiem = danhSachDaTron
            };
        }

        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data == null || data.TheChinh == null) return;

            txtCauHoi.Text = data.TheChinh.MatTruoc;

            // Xử lý hiển thị ảnh: Link AI hoặc File cục bộ
            if (!string.IsNullOrEmpty(data.TheChinh.HinhAnhTruoc))
            {
                string path = data.TheChinh.HinhAnhTruoc;
                picCauHoi.Tag = path;

                if (path.StartsWith("http")) // Nếu là link AI
                {
                    picCauHoi.ImageLocation = path; // Tự động tải từ URL
                }
                else if (File.Exists(path)) // Nếu là file máy tính
                {
                    picCauHoi.Image = Image.FromFile(path);
                }
            }

            if (data.DapAnTracNghiem != null && data.DapAnTracNghiem.Count >= 4)
            {
                var sortedList = data.DapAnTracNghiem.OrderBy(x => x.ThuTu).ToList();
                txtDapAnA.Text = sortedList[0].NoiDung;
                rbA.Checked = sortedList[0].LaDapAnDung;
                txtDapAnB.Text = sortedList[1].NoiDung;
                rbB.Checked = sortedList[1].LaDapAnDung;
                txtDapAnC.Text = sortedList[2].NoiDung;
                rbC.Checked = sortedList[2].LaDapAnDung;
                txtDapAnD.Text = sortedList[3].NoiDung;
                rbD.Checked = sortedList[3].LaDapAnDung;
            }
        }
    }
}