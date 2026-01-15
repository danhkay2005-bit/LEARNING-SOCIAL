using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using System.Data;

namespace WinForms.UserControls.Quiz
{
    public partial class GhepCapEditorControl : UserControl, IQuestionEditor
    {
        public GhepCapEditorControl()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        /// <summary>
        /// Khởi tạo các cột cho bảng nhập liệu
        /// </summary>
        private void SetupDataGridView()
        {
            dgvPairs.Columns.Clear();

            // Thêm cột vế trái
            dgvPairs.Columns.Add("VeTrai", "Vế trái (Vế A)");

            // Thêm cột vế phải
            dgvPairs.Columns.Add("VePhai", "Vế phải (Vế B)");

            // Xử lý phím Delete để xóa hàng
            dgvPairs.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete && dgvPairs.CurrentRow != null && !dgvPairs.CurrentRow.IsNewRow)
                {
                    dgvPairs.Rows.Remove(dgvPairs.CurrentRow);
                }
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Thu thập dữ liệu từ bảng để lưu
        /// </summary>
        public ChiTietTheRequest GetQuestionData()
        {
            var listCapGhep = new List<CapGhepRequest>();
            int stt = 1;

            foreach (DataGridViewRow row in dgvPairs.Rows)
            {
                // Bỏ qua hàng trống cuối cùng của DataGridView
                if (row.IsNewRow) continue;

                string trai = row.Cells["VeTrai"].Value?.ToString()?.Trim() ?? "";
                string phai = row.Cells["VePhai"].Value?.ToString()?.Trim() ?? "";

                // Chỉ lấy những cặp có nhập đủ cả 2 vế
                if (!string.IsNullOrEmpty(trai) && !string.IsNullOrEmpty(phai))
                {
                    listCapGhep.Add(new CapGhepRequest
                    {
                        VeTrai = trai,
                        VePhai = phai,
                        ThuTu = stt++
                    });
                }
            }

            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.GhepCap,
                    MatTruoc = "Ghép cặp các nội dung tương ứng",
                    MatSau = $"{listCapGhep.Count} cặp", // Lưu thông tin số lượng để hiển thị nhanh
                    ThuTu = 0
                },
                // Gán danh sách cặp ghép vào đúng thuộc tính của ChiTietTheRequest
                CapGheps = listCapGhep
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Đổ dữ liệu từ Database lên bảng để sửa
        /// </summary>
        public void SetQuestionData(ChiTietTheRequest data)
        {
            dgvPairs.Rows.Clear();

            if (data?.CapGheps == null || data.CapGheps.Count == 0) return;

            // Sắp xếp theo ThuTu trước khi hiển thị lên bảng
            var sortedList = data.CapGheps.OrderBy(x => x.ThuTu).ToList();

            foreach (var item in sortedList)
            {
                dgvPairs.Rows.Add(item.VeTrai, item.VePhai);
            }
        }
    }
}