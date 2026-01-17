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

            RegisterSignalREvents();
        }

        public int MaBoDe { set => LoadDataById(value); }

        private void RegisterSignalREvents()
        {
            // 1. Khi đối thủ vào phòng (Dành cho Chủ phòng)
            _hubConnection.On("ReadyToStart", () =>
            {
                this.Invoke(() => {
                    if (_currentRole == LobbyRole.Host)
                    {
                        lblStatus.Text = "ĐỐI THỦ ĐÃ VÀO PHÒNG!";
                        lblStatus.ForeColor = Color.LimeGreen;
                        btnStartSolo.Enabled = true;
                        btnStartSolo.Text = "BẮT ĐẦU TRẬN ĐẤU";
                        btnStartSolo.BackColor = Color.LimeGreen;

                        btnStartSolo.Click -= btnStartSolo_Click;
                        btnStartSolo.Click += btnOwnerStartMatch_Click;
                    }
                });
            });

            // 2. Khi chủ phòng bấm Bắt đầu (Dành cho Khách)
            _hubConnection.On("StartMatchSignal", () =>
            {
                this.Invoke(async () => {
                    if (_currentRole == LobbyRole.Guest)
                    {
                        await ExecuteStart();
                    }
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
        }

        // --- DÀNH CHO KHÁCH (GUEST) GỌI TỪ TRANG CHỦ ---
        public async Task JoinAsGuest(int pin, int maBoDe)
        {
            _currentRole = LobbyRole.Guest;
            _currentPin = pin;
            LoadDataById(maBoDe);

            btnCreateChallenge.Visible = false;
            btnStartSolo.Enabled = false;
            btnStartSolo.Text = "ĐANG ĐỢI CHỦ PHÒNG...";
            lblChallengeCode.Text = pin.ToString("D6");
            lblStatus.Text = "Đã tham gia thách đấu";

            if (_hubConnection.State == HubConnectionState.Disconnected)
                await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync("JoinRoom", pin.ToString());
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
            // Gửi lệnh cho khách rồi tự chuyển trang
            await _hubConnection.InvokeAsync("SendStartSignal", _currentPin.ToString());
            await ExecuteStart();
        }

        private async void btnStartSolo_Click(object? sender, EventArgs e)
        {
            if (_currentBoDe == null) return;
            var data = await _boDeHocService.GetFullDataToLearnAsync(_currentBoDe.MaBoDe);
            NavigateToQuiz(data, CheDoHocEnum.HocMotMinh);
        }

        public async Task CleanupAsync()
        {
            await CleanupLobbyAsync(); // Hàm xóa phòng bạn đã viết
        }

        private async Task ExecuteStart()
        {
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
            if (_currentPin != 0)
            {
                int pinToCleanup = _currentPin;
                // Chạy một luồng tách biệt hoàn toàn với UI
                Task.Run(async () => {
                    try
                    {
                        // Tạo một bản sao Service mới từ ServiceProvider để tránh lỗi Dispose
                        if (Program.ServiceProvider != null)
                        {
                            using (var scope = Program.ServiceProvider.CreateScope())
                            {
                                var thachDauService = scope.ServiceProvider.GetRequiredService<IThachDauService>();
                                await thachDauService.HuyThachDauAsync(pinToCleanup);
                            }
                        }
                    }
                    catch { /* Tránh crash app khi tắt */ }
                });
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
            if (_currentPin == 0) return;

            // Lưu lại PIN vào biến tạm để tránh bị reset mất giá trị khi đang xử lý
            int pinToCancel = _currentPin;
            _currentPin = 0;

            try
            {
                // 1. Cố gắng thông báo qua SignalR (nhưng không để nó chặn việc xóa DB)
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    try
                    {
                        await _hubConnection.InvokeAsync("LeaveRoom", pinToCancel.ToString());
                        if (_currentRole == LobbyRole.Host)
                        {
                            await _hubConnection.InvokeAsync("NotifyOpponentLeft", pinToCancel.ToString());
                        }
                    }
                    catch { /* Bỏ qua lỗi SignalR để tiếp tục xóa DB */ }
                }

                // 2. QUAN TRỌNG: Gọi Service xóa phòng trong Database
                // Phải đảm bảo hàm này chạy bất kể SignalR có kết nối hay không
                await _thachDauService.HuyThachDauAsync(pinToCancel);

                System.Diagnostics.Debug.WriteLine($"[Success] Đã xóa phòng {pinToCancel} khỏi DB.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Error] Không thể xóa phòng: {ex.Message}");
            }
        }

    }
}