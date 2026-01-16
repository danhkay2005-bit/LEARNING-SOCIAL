using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using System.IO;

namespace WinForms.UserControls.Quiz
{
    public partial class LatTheEditorControl : UserControl, IQuestionEditor
    {
        public LatTheEditorControl()
        {
            InitializeComponent();
            SetupImageEvents();
        }

        private void SetupImageEvents()
        {
            // Sự kiện chọn ảnh cho mặt trước
            btnPickFront.Click += (s, e) => SelectImageFor(picFront);
            // Sự kiện chọn ảnh cho mặt sau
            btnPickBack.Click += (s, e) => SelectImageFor(picBack);
        }

        private void SelectImageFor(PictureBox pb)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pb.Image = Image.FromFile(ofd.FileName);
                    pb.Tag = ofd.FileName; // Lưu đường dẫn vật lý vào Tag để lấy ra khi Save
                }
            }
        }

        /// <summary>
        /// THỰC THI INTERFACE: Lấy dữ liệu từ UI đóng gói vào Request
        /// </summary>
        public ChiTietTheRequest GetQuestionData()
        {
            return new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = LoaiTheEnum.CoBan, // Thẻ lật cơ bản
                    MatTruoc = txtMatTruoc.Text.Trim(),
                    MatSau = txtMatSau.Text.Trim(),
                    GiaiThich = txtGiaiThich.Text.Trim(),
                    HinhAnhTruoc = picFront.Tag?.ToString(),
                    HinhAnhSau = picBack.Tag?.ToString(),
                    ThuTu = 0 // Sẽ được TaoQuizPage đánh số lại sau
                }
                // Thẻ cơ bản không cần DapAnTracNghiem, DapAnDienKhuyet, v.v.
            };
        }

        /// <summary>
        /// THỰC THI INTERFACE: Đổ dữ liệu từ Request lên giao diện để chỉnh sửa
        /// </summary>
        public void SetQuestionData(ChiTietTheRequest data)
        {
            if (data == null || data.TheChinh == null) return;

            // 1. Đổ text
            txtMatTruoc.Text = data.TheChinh.MatTruoc;
            txtMatSau.Text = data.TheChinh.MatSau;
            txtGiaiThich.Text = data.TheChinh.GiaiThich;

            // 2. Load ảnh mặt trước (nếu có)
            if (!string.IsNullOrEmpty(data.TheChinh.HinhAnhTruoc) && File.Exists(data.TheChinh.HinhAnhTruoc))
            {
                picFront.Image = Image.FromFile(data.TheChinh.HinhAnhTruoc);
                picFront.Tag = data.TheChinh.HinhAnhTruoc;
            }

            // 3. Load ảnh mặt sau (nếu có)
            if (!string.IsNullOrEmpty(data.TheChinh.HinhAnhSau) && File.Exists(data.TheChinh.HinhAnhSau))
            {
                picBack.Image = Image.FromFile(data.TheChinh.HinhAnhSau);
                picBack.Tag = data.TheChinh.HinhAnhSau;
            }
        }
    }
}