using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using System.Text.RegularExpressions;

namespace WinForms.UserControls.Quiz
{
    public partial class DienKhuyetEditorControl : UserControl, IQuestionEditor
    {
        public DienKhuyetEditorControl()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // 1. Nút ẩn từ nhanh: Bọc đoạn đang chọn bằng dấu [...]
            btnHideWord.Click += (s, e) =>
            {
                string selected = txtSentence.SelectedText;
                if (!string.IsNullOrEmpty(selected))
                {
                    txtSentence.SelectedText = $"[{selected}]";
                    // Sau khi thay đổi, con trỏ sẽ nằm sau dấu ]
                }
                txtSentence.Focus();
            };

            // 2. Tự động cập nhật ô hiển thị đáp án khi nội dung câu hỏi thay đổi
            txtSentence.TextChanged += (s, e) =>
            {
                UpdateAnswersDisplay();
            };
        }

        private void UpdateAnswersDisplay()
        {
            // Trích xuất các nội dung trong ngoặc vuông để người dùng xem trước
            var matches = Regex.Matches(txtSentence.Text, @"\[(.*?)\]");
            var answers = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
            txtAnswers.Text = string.Join(" | ", answers);
        }

        /// <summary>
        /// THỰC THI INTERFACE: Thu thập dữ liệu từ UI gửi về trang cha
        /// </summary>
        public ChiTietTheRequest GetQuestionData()
        {
            string rawSentence = txtSentence.Text.Trim();
            var matches = Regex.Matches(rawSentence, @"\[(.*?)\]");

            var listBlanks = new List<TaoTuDienKhuyetRequest>();
            var answerStrings = new List<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                string val = matches[i].Groups[1].Value;
                answerStrings.Add(val);

                // Tạo object khớp với DTO TaoTuDienKhuyetRequest của bạn
                listBlanks.Add(new TaoTuDienKhuyetRequest
                {
                    TuCanDien = val,
                    ViTriTrongCau = i // Vị trí thứ 0, 1, 2...
                });
            }

            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.DienKhuyet,
                    MatTruoc = rawSentence,
                    MatSau = string.Join(", ", answerStrings), // Lưu chuỗi đáp án để học nhanh
                    GiaiThich = txtGiaiThich.Text.Trim(),
                    ThuTu = 0
                },
                // Danh sách chi tiết từng ô trống cho bảng phụ
                TuDienKhuyets = listBlanks
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Đổ dữ liệu từ trang cha lên UI để sửa
        /// </summary>
        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data == null || data.TheChinh == null) return;

            txtSentence.Text = data.TheChinh.MatTruoc;
            txtGiaiThich.Text = data.TheChinh.GiaiThich;

            // Tự động gọi hàm trích xuất để hiện thị lên txtAnswers
            UpdateAnswersDisplay();
        }
    }
}