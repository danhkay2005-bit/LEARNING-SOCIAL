using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Quiz
{
    public partial class QuizReviewControl : UserControl
    {
        // Change the event declaration to nullable
        public event Action? OnBackClicked;

        public QuizReviewControl(List<ChiTietTraLoiRequest> details)
        {
            InitializeComponent();
            btnBack.Click += (s, e) => OnBackClicked?.Invoke();

            SetupColumns();
            BindData(details);
        }

        private void SetupColumns()
        {
            dgvReview.Columns.Clear();
            // Quan trọng: Phải gán Name để truy cập bằng Cells["Tên_Cột"]
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "STT", HeaderText = "STT", DataPropertyName = "STT", Width = 50 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", DataPropertyName = "Status", Width = 100 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserAnswer", HeaderText = "Bạn trả lời", DataPropertyName = "UserAnswer", Width = 200 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "CorrectAnswer", HeaderText = "Đáp án đúng", DataPropertyName = "CorrectAnswer", Width = 200 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Time", HeaderText = "Thời gian", DataPropertyName = "Time", Width = 80 });

            // Thêm cột ẩn IsCorrect để phục vụ việc tô màu
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsCorrect", DataPropertyName = "IsCorrect", Visible = false });
        }

        private void BindData(List<ChiTietTraLoiRequest> details)
        {
            var displayList = details.Select((x, index) => new {
                STT = index + 1,
                Status = x.TraLoiDung ? "✔️ Đúng" : "❌ Sai",
                UserAnswer = x.CauTraLoiUser,
                CorrectAnswer = x.DapAnDung,
                Time = x.ThoiGianTraLoiGiay + "s",
                IsCorrect = x.TraLoiDung // Dùng ẩn để tô màu
            }).ToList();

            dgvReview.DataSource = displayList;

            // Tô màu dòng dựa trên kết quả
            dgvReview.DataBindingComplete += (s, e) => {
                foreach (DataGridViewRow row in dgvReview.Rows)
                {
                    var isCorrectObj = dgvReview.Rows[row.Index].Cells["IsCorrect"].Value;
                    bool isCorrect = isCorrectObj is bool b && b;
                    if (!isCorrect)
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(255, 128, 128); // Đỏ nhạt
                    else
                        row.Cells["Status"].Style.ForeColor = Color.FromArgb(128, 255, 128); // Xanh nhạt
                }
            };

            if (dgvReview.Columns.Contains("IsCorrect") && dgvReview.Columns["IsCorrect"] != null)
                dgvReview.Columns["IsCorrect"]!.Visible = false;
        }
    }
}
