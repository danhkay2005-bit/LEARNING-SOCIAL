using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class SapXepHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;

        // ROOT FIX: Thuộc tính kiểm tra đã trả lời chưa
        // Trả về true nếu đã có ít nhất một từ được chuyển lên vùng kết quả (flpResult)
        public bool HasAnswered => flpResult.Controls.Count > 0;

        private List<PhanTuSapXepResponse> _originalElements;
        private string _correctSentence;

        public SapXepHocControl(TheFlashcardResponse info, List<PhanTuSapXepResponse> elements)
        {
            InitializeComponent();
            _originalElements = elements;
            _correctSentence = info.MatTruoc;
            lblHint.Text = $"Gợi ý: {info.MatSau}";

            InitializeBank();
        }

        private void InitializeBank()
        {
            flpBank.Controls.Clear();
            flpResult.Controls.Clear();

            // Xáo trộn ngẫu nhiên các từ trong ngân hàng từ
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
            if (sender is Button btn)
            {
                // Nếu đang ở kho từ -> Chuyển lên vùng kết quả
                if (btn.Parent == flpBank)
                {
                    flpBank.Controls.Remove(btn);
                    flpResult.Controls.Add(btn);
                }
                // Nếu đang ở vùng kết quả -> Trả về kho từ
                else
                {
                    flpResult.Controls.Remove(btn);
                    flpBank.Controls.Add(btn);
                }
            }
        }

        public void ShowResult()
        {
            // 1. Lấy danh sách từ người dùng đã xếp
            var userWords = flpResult.Controls.Cast<Button>()
                                     .Select(b => b.Text.Trim())
                                     .Where(t => !string.IsNullOrEmpty(t));

            string userSentence = string.Join(" ", userWords);

            // 2. Hàm chuẩn hóa chuỗi để so sánh (xóa khoảng trắng thừa)
            string Normalize(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return "";
                return Regex.Replace(input.Trim(), @"\s+", " ");
            }

            string finalUser = Normalize(userSentence);
            string finalCorrect = Normalize(_correctSentence);

            // 3. So sánh kết quả
            IsCorrect = string.Equals(finalUser, finalCorrect, StringComparison.OrdinalIgnoreCase);

            // 4. Hiển thị màu sắc phản hồi (Xanh: Đúng, Đỏ: Sai)
            foreach (Button btn in flpResult.Controls)
            {
                btn.Enabled = false;
                btn.BackColor = IsCorrect ? Color.FromArgb(46, 125, 50) : Color.FromArgb(183, 28, 28);
                btn.ForeColor = Color.White;
            }
            flpBank.Enabled = false;
        }

        private void ResetQuiz() => InitializeBank();
    }
}