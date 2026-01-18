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
    public partial class DienKhuyetHocControl : UserControl, IQuestionControl
    {
        public bool IsCorrect { get; private set; } = false;
        private List<TextBox> _listInputs = new List<TextBox>();
        private List<string> _correctAnswers = new List<string>();

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

            // Tách chuỗi: "Hanoi is the [capital] of [Vietnam]" 
            // -> parts: ["Hanoi is the ", "capital", " of ", "Vietnam"]
            string[] parts = Regex.Split(rawText, @"\[(.*?)\]");
            var matches = Regex.Matches(rawText, @"\[(.*?)\]");

            int matchIndex = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 0) // Đây là phần văn bản thường
                {
                    if (!string.IsNullOrEmpty(parts[i]))
                    {
                        flpContent.Controls.Add(new Label
                        {
                            Text = parts[i],
                            AutoSize = true,
                            Font = new Font("Segoe UI", 13),
                            Margin = new Padding(0, 5, 0, 0)
                        });
                    }
                }
                else // Đây là phần nằm trong ngoặc vuông [...]
                {
                    var txt = new TextBox
                    {
                        Width = 120,
                        Font = new Font("Segoe UI", 12),
                        TextAlign = HorizontalAlignment.Center
                    };
                    _listInputs.Add(txt);
                    _correctAnswers.Add(parts[i]);
                    flpContent.Controls.Add(txt);
                    matchIndex++;
                }
            }
        }

        public bool HasAnswered => _listInputs.Any(txt => !string.IsNullOrWhiteSpace(txt.Text));

        public void ShowResult()
        {
            bool allCorrect = true;
            for (int i = 0; i < _listInputs.Count; i++)
            {
                bool isRight = _listInputs[i].Text.Trim().Equals(_correctAnswers[i], StringComparison.OrdinalIgnoreCase);
                _listInputs[i].BackColor = isRight ? Color.LightGreen : Color.LightPink;
                _listInputs[i].ReadOnly = true;
                if (!isRight) allCorrect = false;
            }
            IsCorrect = allCorrect;
        }

        private TextBox CreateStyledTextBox()
        {
            return new TextBox
            {
                Width = 140,
                BackColor = Color.FromArgb(40, 70, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle, // Hoặc None nếu bạn dùng Panel bọc ngoài
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                Margin = new Padding(5, 0, 5, 0)
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
                Margin = new Padding(0, 5, 0, 0)
            };
        }
    }
}
