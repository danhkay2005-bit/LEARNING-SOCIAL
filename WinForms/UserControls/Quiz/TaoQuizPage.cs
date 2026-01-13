using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using WinForms.UserControls.Quiz;

namespace WinForms.UserControls
{
    public partial class TaoQuizPage : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IChuDeService _chuDeService; // Inject thêm service chủ đề

        private LuuToanBoBoDeRequest _fullRequest = new LuuToanBoBoDeRequest();
        private List<ChuDeResponse> _cachedChuDes = new List<ChuDeResponse>();
        private int _currentIndex = -1;
        private UserControl? _currentEditor = null;

        public TaoQuizPage(IBoDeHocService boDeHocService, IChuDeService chuDeService)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _chuDeService = chuDeService;

            // Đăng ký sự kiện thủ công để đảm bảo không bị lỗi Designer
            this.btnSetting.Click += btnSetting_Click;
            this.btnThemCauHoi.Click += btnThemCauHoi_Click;
            this.btnTaoQuiz.Click += btnTaoQuiz_Click;

            btnThemCauHoi.BringToFront();

            // Tải dữ liệu ban đầu (Chủ đề) trước khi hiện Header
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            try
            {
                var response = await _chuDeService.GetAllAsync();
                _cachedChuDes = response.ToList();
                LoadDefaultHeader();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách chủ đề: {ex.Message}");
            }
        }

        #region Event Handlers

        private void btnSetting_Click(object? sender, EventArgs e)
        {
            SaveCurrentData();
            LoadDefaultHeader();
        }

        private void btnThemCauHoi_Click(object? sender, EventArgs e)
        {
            SaveCurrentData();

            // Hiển thị bảng chọn loại thẻ
            var selector = LoaiTheSelectorControl.Create((selectedType) =>
            {
                HandleCreateNewQuestion(selectedType);
            });

            SwitchMidContent(selector);
        }

        private async void btnTaoQuiz_Click(object? sender, EventArgs e)
        {
            SaveCurrentData();

            if (string.IsNullOrWhiteSpace(_fullRequest.ThongTinChung.TieuDe))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề bộ đề tại phần Setting!", "Thông báo");
                btnSetting.PerformClick(); // Tự động quay về trang Setting
                return;
            }

            try
            {
                var result = await _boDeHocService.CreateFullAsync(_fullRequest);
                if (result != null)
                {
                    MessageBox.Show("Lưu bộ đề thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi hệ thống");
            }
        }

        #endregion

        #region Helper Methods

        private void HandleCreateNewQuestion(LoaiTheEnum loai)
        {
            var newCard = new ChiTietTheRequest
            {
                TheChinh = new TaoTheFlashcardRequest
                {
                    LoaiThe = loai,
                    MatTruoc = "",
                    MatSau = ""
                }
            };
            _fullRequest.DanhSachThe.Add(newCard);
            _currentIndex = _fullRequest.DanhSachThe.Count - 1;

            AddThumbnailButton(_currentIndex);
            OpenEditorByIndex(_currentIndex);
        }

        private void AddThumbnailButton(int index)
        {
            Button btn = new Button
            {
                Text = (index + 1).ToString(),
                Size = new Size(100, 100),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Tag = index
            };

            btn.Click += (s, e) => {
                SaveCurrentData();
                if (s is Button btnSender && btnSender.Tag is int idx)
                {
                    OpenEditorByIndex(idx);
                }
            };

            flpThumbnails.Controls.Add(btn);
        }

        private void OpenEditorByIndex(int index)
        {
            _currentIndex = index;
            var data = _fullRequest.DanhSachThe[index];
            IQuestionEditor? editor = null;

            // Factory: Trả về editor dựa trên loại thẻ
            switch (data.TheChinh.LoaiThe)
            {
                case LoaiTheEnum.TracNghiem:
                    editor = new TracNghiemEditorControl();
                    break;
                // Thêm các loại khác ở đây: case LoaiTheEnum.GhepCap: ...
                default:
                    editor = new TracNghiemEditorControl();
                    break;
            }

            editor.SetQuestionData(data);
            if (editor is UserControl userControl)
            {
                SwitchMidContent(userControl);
            }
            HighlightSelectedThumbnail(index);
        }

        private void SwitchMidContent(UserControl uc)
        {
            // Xóa các control cũ nhưng giữ lại nút (+)
            var controlsToRemove = pnlMid.Controls.Cast<Control>()
                                     .Where(c => c != btnThemCauHoi)
                                     .ToList();
            foreach (var c in controlsToRemove) pnlMid.Controls.Remove(c);

            uc.Dock = DockStyle.Fill;
            pnlMid.Controls.Add(uc);
            uc.SendToBack(); // Đẩy editor xuống dưới nút (+)
            _currentEditor = uc;
        }

        private void SaveCurrentData()
        {
            if (_currentEditor is HeaderEditorControl header)
                _fullRequest.ThongTinChung = header.GetHeaderData();
            else if (_currentEditor is IQuestionEditor qEditor && _currentIndex >= 0)
                _fullRequest.DanhSachThe[_currentIndex] = qEditor.GetQuestionData();
        }

        private void LoadDefaultHeader()
        {
            _currentIndex = -1;
            var header = new HeaderEditorControl();

            // Nạp danh sách chủ đề đã cache vào ComboBox của Header
            header.SetChuDeDataSource(_cachedChuDes);

            header.SetHeaderData(_fullRequest.ThongTinChung);
            SwitchMidContent(header);
            HighlightSelectedThumbnail(-1);
        }

        private void HighlightSelectedThumbnail(int index)
        {
            // Highlight nút Setting (màu vàng) hoặc màu xanh nhạt
            btnSetting.BackColor = (index == -1) ? Color.Gold : Color.FromArgb(192, 255, 255);

            foreach (Control c in flpThumbnails.Controls)
            {
                if (c is Button b && b.Tag != null && b != btnSetting)
                {
                    b.BackColor = (int)b.Tag == index ? Color.Gold : Color.White;
                }
            }
        }

        #endregion
    }
}