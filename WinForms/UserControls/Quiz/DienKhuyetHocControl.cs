using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class DienKhuyetHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;
        private List<TextBox> _listInputs = new List<TextBox>();
        private List<string> _correctAnswers = new List<string>();

        // Màu sắc đồng bộ với hệ thống
        private readonly Color CorrectColor = Color.FromArgb(46, 125, 50); // Green
        private readonly Color WrongColor = Color.FromArgb(183, 28, 28);    // Red

        public DienKhuyetHocControl(TheFlashcardResponse info)
        {
            InitializeComponent();
            RenderUI(info.MatTruoc);
        }

        private void RenderUI(string rawText)
        {
            flpContent.Controls.Clear();
            _listInputs.Clear();
            _correctAnswers.Clear();

            // Tách chuỗi dựa trên ngoặc vuông [...]
            string[] parts = Regex.Split(rawText, @"\[(.*?)\]");

            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 0) // Phần văn bản thường
                {
                    if (!string.IsNullOrEmpty(parts[i]))
                    {
                        flpContent.Controls.Add(CreateStyledLabel(parts[i]));
                    }
                }
                else // Phần ô trống cần điền
                {
                    var txt = CreateStyledTextBox();
                    _listInputs.Add(txt);
                    _correctAnswers.Add(parts[i]);
                    flpContent.Controls.Add(txt);
                }
            }
        }

        // HasAnswered: Trả về true nếu TẤT CẢ các ô trống đã được nhập dữ liệu
        public bool HasAnswered => _listInputs.Count > 0 && _listInputs.All(txt => !string.IsNullOrWhiteSpace(txt.Text));

        // HÀM MỚI: Trả về toàn bộ nội dung người dùng đã nhập, phân cách bằng dấu gạch đứng
        public string GetUserAnswer()
        {
            if (_listInputs.Count == 0) return "Không có dữ liệu nhập";
            return string.Join(" | ", _listInputs.Select(txt => txt.Text.Trim()));
        }

        public void ShowResult()
        {
            bool allCorrect = true;
            for (int i = 0; i < _listInputs.Count; i++)
            {
                string userAns = _listInputs[i].Text.Trim();
                string correctAns = _correctAnswers[i].Trim();

                bool isRight = userAns.Equals(correctAns, StringComparison.OrdinalIgnoreCase);

                // Hiển thị phản hồi trực quan bằng màu sắc
                _listInputs[i].BackColor = isRight ? CorrectColor : WrongColor;
                _listInputs[i].ForeColor = Color.White;
                _listInputs[i].ReadOnly = true;

                if (!isRight) allCorrect = false;
            }
            IsCorrect = allCorrect;
        }

        private TextBox CreateStyledTextBox()
        {
            return new TextBox
            {
                Width = 130,
                BackColor = Color.FromArgb(25, 45, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                Margin = new Padding(5, 5, 5, 5)
            };
        }

        private Label CreateStyledLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13),
                Margin = new Padding(0, 8, 0, 0)
            };
        }
    }
}