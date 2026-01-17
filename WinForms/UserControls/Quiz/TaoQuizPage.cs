using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using WinForms.Forms;
using WinForms.UserControls.Pages;
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

        private bool _isEditMode = false;
        private int _editingMaBoDe = 0;

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
            // 1. Lưu dữ liệu từ Editor hiện tại vào object request
            SaveCurrentData();

            // 2. Kiểm tra tính hợp lệ cơ bản
            if (!UserSession.IsLoggedIn || UserSession.CurrentUser == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để thực hiện!", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(_fullRequest.ThongTinChung.TieuDe))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề bộ đề!", "Thông báo");
                btnSetting.PerformClick();
                return;
            }

            // 3. Gán MaNguoiDung
            _fullRequest.ThongTinChung.MaNguoiDung = UserSession.CurrentUser.MaNguoiDung;

            try
            {
                if (_isEditMode)
                {
                    // --- CHẾ ĐỘ CẬP NHẬT ---
                    // Đảm bảo Service của bạn có hàm UpdateFullAsync(id, request)
                    var result = await _boDeHocService.UpdateFullAsync(_editingMaBoDe, _fullRequest);
                    if (result != null)
                    {
                        MessageBox.Show("Cập nhật bộ đề thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToPreviousPage();
                    }
                }
                else
                {
                    // --- CHẾ ĐỘ TẠO MỚI ---
                    var result = await _boDeHocService.CreateFullAsync(_fullRequest);
                    if (result != null)
                    {
                        MessageBox.Show("Tạo bộ đề thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToPreviousPage();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi hệ thống");
            }
        }

        #endregion
        private void ReturnToPreviousPage()
        {
            if (this.FindForm() is MainForm mainForm)
            {
                // Reset trạng thái trước khi thoát
                _isEditMode = false;
                _editingMaBoDe = 0;

                var hocTapPage = Program.ServiceProvider?.GetRequiredService<HocTapPage>();
                if (hocTapPage != null) mainForm.LoadPage(hocTapPage);
            }
        }
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
            // 1. Tạo Panel bao quanh (Container)
            Panel pnlThumb = new Panel
            {
                Size = new Size(110, 110),
                Margin = new Padding(5),
                Padding = new Padding(5)
            };

            // 2. Nút số chính (Dùng để chọn slide)
            Button btnSelect = new Button
            {
                Text = (index + 1).ToString(),
                Size = new Size(90, 90),
                Location = new Point(5, 15), // Chừa chỗ phía trên cho nút X
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Tag = index,
                Cursor = Cursors.Hand
            };

            btnSelect.Click += (s, e) => {
                SaveCurrentData();
                if (s is Button btnSender && btnSender.Tag is int idx)
                {
                    OpenEditorByIndex(idx);
                }
            };

            // 3. Nút X xóa (Nhỏ ở góc trên bên phải)
            Button btnDelete = new Button
            {
                Text = "✕",
                Size = new Size(25, 25),
                Location = new Point(80, 0), // Đặt ở góc
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = index
            };
            btnDelete.FlatAppearance.BorderSize = 0;

            btnDelete.Click += (s, e) => {
                if (s is Button b && b.Tag is int idx)
                {
                    HandleDeleteQuestion(idx);
                }
            };

            // Thêm các control vào panel
            pnlThumb.Controls.Add(btnDelete);
            pnlThumb.Controls.Add(btnSelect);
            btnDelete.BringToFront(); // Đảm bảo nút X nằm trên

            flpThumbnails.Controls.Add(pnlThumb);
        }

        private void OpenEditorByIndex(int index)
        {
            _currentIndex = index;
            var data = _fullRequest.DanhSachThe[index];
            IQuestionEditor? editor = null;

            // Điều hướng khởi tạo Editor dựa trên loại thẻ được chọn
            switch (data.TheChinh.LoaiThe)
            {
                case LoaiTheEnum.TracNghiem:
                    editor = new TracNghiemEditorControl();
                    break;

                case LoaiTheEnum.CoBan: // Lật thẻ
                    editor = new LatTheEditorControl();
                    break;

                case LoaiTheEnum.DienKhuyet:
                    editor = new DienKhuyetEditorControl();
                    break;

                case LoaiTheEnum.GhepCap:
                    editor = new GhepCapEditorControl();
                    break;

                case LoaiTheEnum.SapXep:
                    editor = new SapXepEditorControl();
                    break;

                default:
                    editor = new TracNghiemEditorControl();
                    break;
            }

            // Nạp dữ liệu vào Editor và hiển thị lên vùng pnlMid
            if (editor != null)
            {
                editor.SetQuestionData(data);
                if (editor is UserControl uc)
                {
                    SwitchMidContent(uc);
                }
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
            _currentIndex = -1; // Đánh dấu là đang ở trang Header, không phải câu hỏi nào

            var header = new HeaderEditorControl();

            // Nạp danh sách chủ đề đã cache từ trước (để người dùng chọn lại chủ đề)
            header.SetChuDeDataSource(_cachedChuDes);

            // QUAN TRỌNG: Nạp dữ liệu từ _fullRequest.ThongTinChung vào giao diện
            // (Nếu đang Edit, dữ liệu này chính là dữ liệu cũ đã load từ server)
            header.SetHeaderData(_fullRequest.ThongTinChung);

            // Hiển thị HeaderEditorControl vào vùng giữa màn hình
            SwitchMidContent(header);

            // Làm nổi bật nút Setting để người dùng biết mình đang ở đâu
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

        public void LoadDataForEdit(HocBoDeResponse data)
        {
            // 1. Đánh dấu trạng thái chỉnh sửa
            _isEditMode = true;
            _editingMaBoDe = data.ThongTinChung.MaBoDe;
            btnTaoQuiz.Text = "CẬP NHẬT BỘ ĐỀ";
            btnTaoQuiz.BackColor = Color.Orange; // Đổi màu để nhận diện

            // 2. Map thông tin chung (Header)
            _fullRequest.ThongTinChung = new TaoBoDeHocRequest
            {
                TieuDe = data.ThongTinChung.TieuDe,
                MoTa = data.ThongTinChung.MoTa,
                AnhBia = data.ThongTinChung.AnhBia,
                MucDoKho = data.ThongTinChung.MucDoKho,
                LaCongKhai = data.ThongTinChung.LaCongKhai,
                // Giả sử HocBoDeResponse có MaChuDe, nếu không hãy mặc định
                MaChuDe = 1
            };

            // 3. Map danh sách thẻ câu hỏi
            _fullRequest.DanhSachThe = new List<ChiTietTheRequest>();

            foreach (var item in data.DanhSachCauHoi)
            {
                // Tạo đối tượng CapNhat vì dữ liệu này lấy từ Database (đã có ID)
                var updateRequest = new CapNhatTheFlashcardRequest
                {
                    MaThe = item.ThongTinThe.MaThe, // Đã có MaThe vì dùng lớp CapNhat
                    LoaiThe = item.ThongTinThe.LoaiThe,
                    MatTruoc = item.ThongTinThe.MatTruoc,
                    MatSau = item.ThongTinThe.MatSau,
                    GiaiThich = item.ThongTinThe.GiaiThich,
                    HinhAnhTruoc = item.ThongTinThe.HinhAnhTruoc,
                    HinhAnhSau = item.ThongTinThe.HinhAnhSau,
                    DoKho = item.ThongTinThe.DoKho,
                    MaBoDe = _editingMaBoDe
                };

                var cardReq = new ChiTietTheRequest
                {
                    TheChinh = updateRequest 
                };

                // Tiếp tục mapping các phần đặc thù (Điền khuyết, Trắc nghiệm...)
                MapSpecializedData(item, cardReq);

                _fullRequest.DanhSachThe.Add(cardReq);
            }

            // 4. Làm mới giao diện Thumbnail (các nút số bên trái)
            RefreshThumbnails();

            // 5. Hiển thị trang Setting (Header) đầu tiên
            LoadDefaultHeader();
        }

        private void MapSpecializedData(ChiTietCauHoiHocResponse source, ChiTietTheRequest target)
        {
            switch (source.ThongTinThe.LoaiThe)
            {
                case LoaiTheEnum.TracNghiem:
                    target.DapAnTracNghiem = source.TracNghiem?.Select(x => new TaoDapAnTracNghiemRequest
                    {
                        NoiDung = x.NoiDung,
                        LaDapAnDung = x.LaDapAnDung
                    }).ToList();
                    break;

                case LoaiTheEnum.SapXep:
                    target.PhanTuSapXeps = source.SapXep?.Select(x => new TaoPhanTuSapXepRequest
                    {
                        NoiDung = x.NoiDung,
                        ThuTuDung = x.ThuTuDung
                    }).ToList();
                    break;

                case LoaiTheEnum.DienKhuyet:
                    target.TuDienKhuyets = source.DienKhuyet?.Select(x => new TaoTuDienKhuyetRequest
                    {
                        // Lưu ý: MaThe sẽ được gán khi Backend xử lý hoặc gán trực tiếp tại đây
                        MaThe = source.ThongTinThe.MaThe,
                        TuCanDien = x.TuCanDien, // Giả sử x.TuGoc từ Response là nội dung từ cần điền
                        ViTriTrongCau = x.ViTriTrongCau // Giả sử x.ViTri từ Response là index vị trí
                    }).ToList();
                    break;

                case LoaiTheEnum.GhepCap:
                    target.CapGheps = source.CapGhep?.Select(x => new CapGhepRequest
                    {
                        VeTrai = x.VeTrai,
                        VePhai = x.VePhai
                    }).ToList();
                    break;
            }
        }

        private void RefreshThumbnails()
        {
            flpThumbnails.Controls.Clear();
            // Thêm lại nút Setting đầu tiên nếu bạn muốn nó luôn nằm trên cùng
            flpThumbnails.Controls.Add(btnSetting); 

            for (int i = 0; i < _fullRequest.DanhSachThe.Count; i++)
            {
                AddThumbnailButton(i);
            }
        }

        private void HandleDeleteQuestion(int index)
        {
            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa câu hỏi số {index + 1}?",
                                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // 1. Lưu dữ liệu hiện tại trước khi thay đổi danh sách (tránh mất data các slide khác)
                SaveCurrentData();

                // 2. Xóa khỏi danh sách dữ liệu
                _fullRequest.DanhSachThe.RemoveAt(index);

                // 3. Xử lý logic điều hướng sau khi xóa
                if (_currentIndex == index)
                {
                    // Nếu xóa đúng slide đang mở, quay về Header
                    LoadDefaultHeader();
                }
                else if (_currentIndex > index)
                {
                    // Nếu xóa slide phía trước slide đang mở, giảm index hiện tại xuống 1
                    _currentIndex--;
                }

                // 4. Vẽ lại toàn bộ Thumbnail để cập nhật số thứ tự (1, 2, 3...)
                RefreshThumbnails();
            }
        }
    }
}