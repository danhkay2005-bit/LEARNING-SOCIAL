using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;

namespace WinForms.UserControls.Quiz
{
    public partial class SapXepEditorControl : UserControl, IQuestionEditor
    {
        public SapXepEditorControl()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // Tự động phân tích khi nhấn nút hoặc khi rời khỏi ô nhập liệu
            btnPhanTich.Click += (s, e) => RefreshPreview();
            txtCauGoc.Leave += (s, e) => RefreshPreview();
        }

        private void RefreshPreview()
        {
            flpPreview.Controls.Clear();
            string content = txtCauGoc.Text.Trim();
            if (string.IsNullOrEmpty(content)) return;

            // Tách các từ dựa trên khoảng trắng
            string[] parts = content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                Label lblPart = new Label
                {
                    Text = part,
                    AutoSize = true,
                    BackColor = Color.FromArgb(13, 56, 56),
                    ForeColor = Color.White,
                    Padding = new Padding(10, 5, 10, 5),
                    Margin = new Padding(5),
                    Font = new Font("Segoe UI Semibold", 10),
                    BorderStyle = BorderStyle.FixedSingle
                };
                flpPreview.Controls.Add(lblPart);
            }
        }

        /// <summary>
        /// THỰC THI INTERFACE: Thu thập các phần tử đã xé lẻ để lưu
        /// </summary>
        public ChiTietTheRequest GetQuestionData()
        {
            string content = txtCauGoc.Text.Trim();
            string[] parts = content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var listPhanTu = new List<TaoPhanTuSapXepRequest>();
            for (int i = 0; i < parts.Length; i++)
            {
                listPhanTu.Add(new TaoPhanTuSapXepRequest
                {
                    NoiDung = parts[i],
                    ThuTuDung = i + 1 // Lưu thứ tự đúng: 1, 2, 3...
                });
            }

            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.SapXep,
                    MatTruoc = content, // Lưu câu hoàn chỉnh làm đề bài
                    MatSau = txtGiaiThich.Text.Trim(), // Lưu nghĩa/giải thích làm kết quả
                    GiaiThich = txtGiaiThich.Text.Trim(),
                    ThuTu = 0
                },
                // Gửi danh sách đã tách xuống Service để đồng bộ (SyncElementsAsync)
                PhanTuSapXeps = listPhanTu
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Đổ dữ liệu từ Database lên giao diện
        /// </summary>
        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data?.TheChinh == null) return;

            txtCauGoc.Text = data.TheChinh.MatTruoc;
            txtGiaiThich.Text = data.TheChinh.GiaiThich;

            RefreshPreview();
        }
    }
}