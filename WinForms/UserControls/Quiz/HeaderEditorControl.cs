using StudyApp.DTO.Requests.Learn;

namespace WinForms.UserControls.Quiz
{
    public partial class HeaderEditorControl : UserControl
    {
        public HeaderEditorControl()
        {
            InitializeComponent();
        }

        // Đổ dữ liệu từ Object vào giao diện (Dùng khi quay lại từ trang câu hỏi)
        public void SetHeaderData(TaoBoDeHocRequest data)
        {
            if (data == null) return;
            txtTenBoDe.Text = data.TieuDe;
            txtMoTa.Text = data.MoTa; // Đây là nơi chứa các #hashtag
            // Nếu bạn có Image, hãy xử lý pictureBox1 ở đây
        }

        // Trích xuất dữ liệu từ giao diện để trả về cho trang cha
        public TaoBoDeHocRequest GetHeaderData()
        {
            return new TaoBoDeHocRequest
            {
                TieuDe = txtTenBoDe.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(), // Chuỗi này sẽ được Backend bóc tách hashtag
                // Giả sử MaNguoiDung lấy từ Session/Static User
                MaNguoiDung = Guid.Empty,
                LaCongKhai = true
            };
        }
    }
}