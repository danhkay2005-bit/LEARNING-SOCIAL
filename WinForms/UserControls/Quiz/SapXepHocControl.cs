using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class SapXepHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;
        private List<PhanTuSapXepResponse> _originalElements;
        private string _correctSentence;

        public SapXepHocControl(TheFlashcardResponse info, List<PhanTuSapXepResponse> elements)
        {
            InitializeComponent();
            _originalElements = elements;
            _correctSentence = info.MatTruoc; // Câu gốc
            lblHint.Text = $"Gợi ý: {info.MatSau}";

            InitializeBank();
        }

        private void InitializeBank()
        {
            flpBank.Controls.Clear();
            flpResult.Controls.Clear();

            // Xáo trộn các từ
            var shuffled = _originalElements.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var item in shuffled)
            {
                Button btn = CreateWordButton(item.NoiDung);
                btn.Click += WordButton_Click;
                flpBank.Controls.Add(btn);
            }
        }

        private Button CreateWordButton(string text)
        {
            return new Button
            {
                Text = text,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 70, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 11),
                Cursor = Cursors.Hand
            };
        }

        private void WordButton_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!;

            // Nếu đang ở Bank -> Chuyển lên Result
            if (btn.Parent == flpBank)
            {
                flpBank.Controls.Remove(btn);
                flpResult.Controls.Add(btn);
            }
            // Nếu đang ở Result -> Chuyển về Bank
            else
            {
                flpResult.Controls.Remove(btn);
                flpBank.Controls.Add(btn);
            }
        }

        private void ResetQuiz() => InitializeBank();

        public void ShowResult()
        {
            // 1. Lấy danh sách từ, Trim từng từ để loại bỏ khoảng trắng thừa trong nút
            var userWords = flpResult.Controls.Cast<Button>()
                                     .Select(b => b.Text.Trim())
                                     .Where(t => !string.IsNullOrEmpty(t));

            // 2. Ghép lại thành câu
            string userSentence = string.Join(" ", userWords);

            // 3. Hàm chuẩn hóa chuỗi: 
            // - Xóa khoảng trắng đầu/cuối.
            // - Thay thế các cụm khoảng trắng ở giữa (2-3 dấu cách, tab...) bằng 1 dấu cách duy nhất.
            string Normalize(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return "";
                string trimmed = input.Trim();
                return Regex.Replace(trimmed, @"\s+", " ");
            }

            string finalUser = Normalize(userSentence);
            string finalCorrect = Normalize(_correctSentence);

            // 4. So sánh
            IsCorrect = string.Equals(finalUser, finalCorrect, StringComparison.OrdinalIgnoreCase);

            // Hiển thị màu sắc feedback
            foreach (Button btn in flpResult.Controls)
            {
                btn.Enabled = false;
                btn.BackColor = IsCorrect ? Color.FromArgb(46, 125, 50) : Color.FromArgb(183, 28, 28);
            }
            flpBank.Enabled = false;

            // Debug (Tùy chọn): In ra để kiểm tra nếu vẫn sai
            // Console.WriteLine($"User: '{finalUser}'");
            // Console.WriteLine($"Correct: '{finalCorrect}'");
        }
    }
}
