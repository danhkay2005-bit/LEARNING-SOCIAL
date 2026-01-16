using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            // Ghép các từ trong flpResult lại thành câu
            var userWords = flpResult.Controls.Cast<Button>().Select(b => b.Text);
            string userSentence = string.Join(" ", userWords);

            // So sánh (không phân biệt hoa thường và khoảng trắng thừa)
            IsCorrect = string.Equals(userSentence.Trim(), _correctSentence.Trim(), StringComparison.OrdinalIgnoreCase);

            // Hiển thị màu sắc feedback
            foreach (Button btn in flpResult.Controls)
            {
                btn.Enabled = false;
                btn.BackColor = IsCorrect ? Color.FromArgb(46, 125, 50) : Color.FromArgb(183, 28, 28);
            }
            flpBank.Enabled = false;
        }
    }
}
