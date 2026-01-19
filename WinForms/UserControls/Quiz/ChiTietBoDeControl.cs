using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.UserControls.Pages;

namespace WinForms.UserControls.Quiz
{
    public enum LobbyRole { None, Host, Guest }

    public partial class ChiTietBoDeControl : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IThachDauService _thachDauService;
        private readonly HubConnection _hubConnection;

        private bool _isStartingMatch = false;

        private BoDeHocResponse? _currentBoDe;
        private int _currentPin = 0;
        private LobbyRole _currentRole = LobbyRole.None;

        public ChiTietBoDeControl(IBoDeHocService boDeHocService, IThachDauService thachDauService, HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _hubConnection = hubConnection;

            // Đăng ký sự kiện mặc định
            btnStartSolo.Click += btnStartSolo_Click;
            btnCreateChallenge.Click += btnCreateChallenge_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;

            RegisterSignalREvents();    
        }

        public int MaBoDe { set => LoadDataById(value); }

        private void RegisterSignalREvents()
        {
            // Khi Khách gọi TriggerStartMatch, Chủ phòng sẽ nhận được cái này:
            _hubConnection.On("ReadyToStart", () =>
            {
                this.Invoke(() => {
                    if (_currentRole == LobbyRole.Host)
                    {
                        lblStatus.Text = "ĐỐI THỦ ĐÃ VÀO!";
                        btnStartSolo.Enabled = true;
                        btnStartSolo.Text = "BẮT ĐẦU TRẬN ĐẤU";
                        btnStartSolo.BackColor = Color.LimeGreen;

                        btnStartSolo.Click -= btnStartSolo_Click;
                        btnStartSolo.Click -= btnOwnerStartMatch_Click; // Xóa để tránh gán trùng
                        btnStartSolo.Click += btnOwnerStartMatch_Click;
                    }
                });
            });

            // Khi bất kỳ ai gọi SendStartSignal (thường là Chủ), CẢ HAI sẽ nhận được cái này:
            _hubConnection.On("StartMatchSignal", () =>
            {
                this.Invoke(async () => {
                    // Cả Host và Guest đều tự động nhảy vào đây để bắt đầu làm bài cùng lúc
                    await ExecuteStart();
                });
            });
        }

        private async void LoadDataById(int id)
        {
            try
            {
                lblStatus.Text = "Đang tải...";
                var data = await _boDeHocService.GetByIdAsync(id);
                if (data != null) SetData(data);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}"); }
        }

        public void SetData(BoDeHocResponse data)
        {
            _currentBoDe = data;
            lblMainTitle.Text = data.TieuDe.ToUpper();
            lblSideTitle.Text = data.TieuDe;
            lblSideInfo.Text = $"{data.SoLuongThe} thẻ • Độ khó: {data.MucDoKho}";
            if (!string.IsNullOrEmpty(data.AnhBia)) picThumb.ImageLocation = data.AnhBia;

            // 2. Kiểm tra quyền sở hữu để hiện nút Sửa và Xóa
            bool isOwner = UserSession.CurrentUser != null && data.MaNguoiDung == UserSession.CurrentUser.MaNguoiDung;
            btnEdit.Visible = isOwner;
            btnDelete.Visible = isOwner; // Hiện nút xóa nếu là chủ
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_currentBoDe == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa bộ đề '{_currentBoDe.TieuDe}' không?\nDữ liệu đã xóa không thể khôi phục.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Gọi service để xóa (Giả sử bạn đã có hàm DeleteAsync trong IBoDeHocService)
                    // Nếu backend dùng xóa mềm (soft delete), trạng thái sẽ chuyển thành DaXoa = true
                    await _boDeHocService.DeleteAsync(_currentBoDe.MaBoDe);

                    MessageBox.Show("Đã xóa bộ đề thành công.", "Thông báo");

                    // Quay lại trang danh sách sau khi xóa
                    btnBack.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa bộ đề: {ex.Message}", "Lỗi");
                }
            }
        }

        private async void btnEdit_Click(object? sender, EventArgs e)
        {
            if (_currentBoDe == null) return;

            try
            {
                // 1. Lấy toàn bộ dữ liệu (bao gồm các câu hỏi)
                var fullData = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe.MaBoDe);

                // 2. Chuyển hướng sang TaoQuizPage
                var mainForm = this.FindForm() as MainForm;
                if (Program.ServiceProvider != null && mainForm != null)
                {
                    var taoQuizPage = Program.ServiceProvider.GetRequiredService<TaoQuizPage>();

                    // 3. Gọi hàm load dữ liệu để chỉnh sửa (chúng ta sẽ viết hàm này ở bước sau)
                    taoQuizPage.LoadDataForEdit(fullData);

                    mainForm.LoadPage(taoQuizPage);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tải dữ liệu chỉnh sửa: {ex.Message}"); }
        }

        // --- DÀNH CHO KHÁCH (GUEST) GỌI TỪ TRANG CHỦ ---
        public async Task JoinAsGuest(int pin, int maBoDe)
        {
            _currentRole = LobbyRole.Guest;
            _currentPin = pin;
            LoadDataById(maBoDe);

            // Cập nhật UI chờ
            btnCreateChallenge.Visible = false;
            btnStartSolo.Enabled = false;
            btnStartSolo.Text = "ĐANG ĐỢI CHỦ PHÒNG...";
            lblChallengeCode.Text = pin.ToString("D6");

            try
            {
                if (_hubConnection.State == HubConnectionState.Disconnected)
                    await _hubConnection.StartAsync();

                // 1. Khách vào phòng
                await _hubConnection.InvokeAsync("JoinRoom", pin.ToString());

                // 2. QUAN TRỌNG: Gọi hàm này để báo cho máy Chủ phòng hiện nút "BẮT ĐẦU"
                // Hub sẽ gửi lệnh "ReadyToStart" về cho máy Chủ
                await _hubConnection.InvokeAsync("TriggerStartMatch", pin.ToString());

                lblStatus.Text = "Đã vào phòng, đợi chủ phòng bắt đầu!";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }


        // --- DÀNH CHO CHỦ PHÒNG (HOST) ---
        private async void btnCreateChallenge_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentBoDe == null) return;
                if (UserSession.CurrentUser == null)
                {
                    MessageBox.Show("Bạn cần đăng nhập để tạo thách đấu.");
                    return;
                }
                _currentRole = LobbyRole.Host;

                var req = new TaoThachDauRequest { MaBoDe = _currentBoDe.MaBoDe, NguoiTao = UserSession.CurrentUser.MaNguoiDung };
                var challenge = await _thachDauService.TaoThachDauAsync(req);
                _currentPin = challenge.MaThachDau;

                lblChallengeCode.Text = _currentPin.ToString("D6");
                btnCreateChallenge.Enabled = false;
                btnStartSolo.Enabled = false;
                btnStartSolo.Text = "ĐỢI ĐỐI THỦ...";

                if (_hubConnection.State == HubConnectionState.Disconnected)
                    await _hubConnection.StartAsync();

                await _hubConnection.InvokeAsync("JoinRoom", _currentPin.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void btnOwnerStartMatch_Click(object? sender, EventArgs e)
        {
            try
            {
                btnStartSolo.Enabled = false;

                // 1. Gửi lệnh cho đối thủ (Guest) vào trận
                await _hubConnection.InvokeAsync("SendStartSignal", _currentPin.ToString());
                if (_currentRole == LobbyRole.Host)
                {
                    await ExecuteStart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message);
                btnStartSolo.Enabled = true;
            }
        }

        private async void btnStartSolo_Click(object? sender, EventArgs e)
        {
            if (_currentBoDe == null) return;

            try
            {
                var data = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe.MaBoDe);
                var filteredCards = data.DanhSachCauHoi
                    .Where(q => q.ThongTinThe.NgayOnTapTiepTheo == null || q.ThongTinThe.NgayOnTapTiepTheo <= DateTime.Now)
                    .ToList();

                if (filteredCards.Count > 0)
                {
                    data.DanhSachCauHoi = filteredCards;
                    NavigateToQuiz(data, CheDoHocEnum.HocMotMinh);
                }
                else
                {
                    var nextReviewDate = data.DanhSachCauHoi
                        .Where(q => q.ThongTinThe.NgayOnTapTiepTheo.HasValue)
                        .Select(q => q.ThongTinThe.NgayOnTapTiepTheo!.Value)
                        .OrderBy(d => d) 
                        .FirstOrDefault();

                    if (nextReviewDate != default)
                    {
                        TimeSpan diff = nextReviewDate - DateTime.Now;
                        string countdownText = diff.TotalDays >= 1
                            ? $"khoảng {Math.Ceiling(diff.TotalDays)} ngày nữa"
                            : $"khoảng {Math.Ceiling(diff.TotalHours)} giờ nữa";

                        MessageBox.Show(
                            $"🎉 Tuyệt vời! Bạn đã ôn tập hoàn tất bộ đề này.\n\n" +
                            $"📅 Ngày quay lại dự kiến: {nextReviewDate:dd/MM/yyyy HH:mm}\n" +
                            $"(Tức là {countdownText})",
                            "Thông báo ôn tập",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bộ đề này hiện không có thẻ nào để học!", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu học: {ex.Message}");
            }
        }

        public async Task CleanupAsync()
        {
            await CleanupLobbyAsync(); // Hàm xóa phòng bạn đã viết
        }

        private async Task ExecuteStart()
        {
            _isStartingMatch = true;

            var data = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe!.MaBoDe);

            NavigateToQuiz(data, CheDoHocEnum.ThachDau, _currentPin);
        }

        private void NavigateToQuiz(HocBoDeResponse data, CheDoHocEnum mode, int? pin = null)
        {
            var mainForm = this.ParentForm as MainForm;
            if (Program.ServiceProvider == null)
            {
                MessageBox.Show("ServiceProvider is not initialized.");
                return;
            }
            var hocPage = (HocBoDePage)Program.ServiceProvider.GetRequiredService(typeof(HocBoDePage));
            hocPage.Initialize(data, mode, pin);
            mainForm?.LoadPage(hocPage);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Chỉ xóa phòng nếu KHÔNG PHẢI đang vào trận và pin vẫn còn
            if (!_isStartingMatch && _currentPin != 0)
            {
                int pinToCleanup = _currentPin;
                if (Program.ServiceProvider != null)
                {
                    Task.Run(async () => {
                        using (var scope = Program.ServiceProvider.CreateScope())
                        {
                            var service = scope.ServiceProvider.GetRequiredService<IThachDauService>();
                            await service.HuyThachDauAsync(pinToCleanup);
                        }
                    });
                }
            }
            base.OnHandleDestroyed(e);
        }

        // 1. Sự kiện Click nút Back
        private async void btnBack_Click(object? sender, EventArgs e)
        {
            btnBack.Enabled = false; // Chống bấm nhiều lần

            if (this.FindForm() is MainForm mForm && Program.ServiceProvider != null)
            {
                // Lấy trang cần quay về
                var hocTapPage = Program.ServiceProvider.GetRequiredService<HocTapPage>();
                mForm.LoadPage(hocTapPage);
            }
        }

        // 2. Hàm xử lý logic chính
        private async Task HandleLeavePage()
        {
            try
            {
                // Thực hiện hủy phòng nếu đang trong lobby
                await CleanupLobbyAsync();

                // Điều hướng lùi về trang trước (HocTapPage)
                if (this.ParentForm is MainForm mainForm)
                {
                    var hocTapPage = Program.ServiceProvider?.GetRequiredService<HocTapPage>();
                    if (hocTapPage != null)
                    {
                        mainForm.LoadPage(hocTapPage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi rời trang: {ex.Message}");
            }
        }

        // 3. Hàm dọn dẹp (Dùng chung cho cả Back và Destroyed)
        private async Task CleanupLobbyAsync()
        {
            // Nếu đang chuyển sang màn hình thi đấu, tuyệt đối không được xóa phòng!
            if (_isStartingMatch || _currentPin == 0) return;

            int pinToCancel = _currentPin;
            _currentPin = 0; // Reset ngay để tránh các luồng khác gọi trùng

            try
            {
                // SignalR: Thông báo rời phòng
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("LeaveRoom", pinToCancel.ToString());
                    if (_currentRole == LobbyRole.Host)
                    {
                        await _hubConnection.InvokeAsync("NotifyOpponentLeft", pinToCancel.ToString());
                    }
                }

                // Database: Xóa phòng tạm
                await _thachDauService.HuyThachDauAsync(pinToCancel);
                System.Diagnostics.Debug.WriteLine($"[Cleanup] Đã hủy phòng chờ {pinToCancel}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Error] CleanupLobby: {ex.Message}");
            }
        }

    }
}