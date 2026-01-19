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
        public partial class LatTheHocControl : UserControl, IQuestionControl
        {
            public bool IsCorrect { get; private set; } = false;
            private bool _isFlipped = false;
        private bool _hasEvaluated = false;
        private TheFlashcardResponse _info;

            public LatTheHocControl(TheFlashcardResponse info)
            {
                InitializeComponent();
                _info = info;
                UpdateUI();
            }

            private void btnLat_Click(object sender, EventArgs e)
            {
                _isFlipped = !_isFlipped;
                UpdateUI();

                // Hiện 2 nút đánh giá kết quả, ẩn nút Lật
                btnLat.Visible = false;
                btnDung.Visible = btnSai.Visible = true;
            }
        public bool HasAnswered => _hasEvaluated;

        private void UpdateUI()
            {
                if (!_isFlipped)
                {
                    lblSideIndicator.Text = "MẶT TRƯỚC";
                    lblContent.Text = _info.MatTruoc;
                    ShowImage(_info.HinhAnhTruoc);
                    pnlCard.BackColor = Color.FromArgb(35, 55, 60);
                }
                else
                {
                    lblSideIndicator.Text = "MẶT SAU";
                    lblContent.Text = _info.MatSau;
                    ShowImage(_info.HinhAnhSau);
                    // Đổi màu nhẹ để nhận biết đã lật
                    pnlCard.BackColor = Color.FromArgb(40, 70, 80);
                }
            }

            private void ShowImage(string? path)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    picImage.Image = Image.FromFile(path);
                    picImage.Visible = true;
                    lblContent.Dock = DockStyle.Fill; // Đẩy text xuống dưới ảnh
                }
                else
                {
                    picImage.Visible = false;
                    lblContent.Dock = DockStyle.Fill;
                }
            }

            private void btnDung_Click(object sender, EventArgs e)
            {
                IsCorrect = true;
                HighlightStatus(Color.Lime);
            }

            private void btnSai_Click(object sender, EventArgs e)
            {
                IsCorrect = false;
                HighlightStatus(Color.Red);
            }

            private void HighlightStatus(Color color)
            {
                pnlCard.BackColor = color;// Nếu bạn dùng Custom Panel hoặc vẽ viền
                btnDung.Enabled = btnSai.Enabled = false;
            }

            public void ShowResult()
            {
                // Hiển thị giải thích nếu có
                if (!string.IsNullOrEmpty(_info.GiaiThich))
                {
                    lblContent.Text += "\n\n" + _info.GiaiThich;
                    lblContent.Font = new Font("Segoe UI", 12);
                }
            }

        public string GetUserAnswer()
        {
            return IsCorrect ? "Đúng" : "Sai";
        }
        }
    }
