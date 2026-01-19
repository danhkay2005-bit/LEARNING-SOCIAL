using System;
using System.Drawing;
using System.Windows.Forms;
using StudyApp.DTO.Enums; // Đảm bảo đã using namespace chứa QuyenRiengTuEnum

namespace WinForms.UserControls.Quiz
{
    public partial class ShareChallengeDialog : Form
    {
        public string UserMessage { get; private set; } = string.Empty;

        // Thuộc tính mới để lấy quyền riêng tư sau khi đóng Dialog
        public QuyenRiengTuEnum SelectedPrivacy { get; private set; }

        public ShareChallengeDialog(string deckName, int pin)
        {
            InitializeComponent();

            lblDeckName.Text = deckName;
            lblPinCode.Text = pin.ToString("D6");

            // Thiết lập giá trị mặc định cho ComboBox
            cbbPrivacy.SelectedIndex = 0; // Mặc định là Công khai
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UserMessage = txtMessage.Text.Trim();

            // Ánh xạ lựa chọn từ ComboBox sang Enum
            // Giả sử: Index 0 = Công khai, Index 1 = Chỉ người theo dõi (Bạn bè)
            SelectedPrivacy = (cbbPrivacy.SelectedIndex == 0)
                ? QuyenRiengTuEnum.CongKhai
                : QuyenRiengTuEnum.ChiFollower;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
    }
}