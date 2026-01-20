using WinForms.UserControls.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.Learn;
using StudyApp.DTO.Responses.Learn;
using System.Diagnostics;
using WinForms.Forms;
using WinForms.UserControls.Pages;
using WinForms.UserControls.Quiz;



namespace WinForms.UserControls
{
    public partial class HocBoDePage : UserControl
    {
        private readonly IBoDeHocService _boDeHocService;
        private readonly IThachDauService _thachDauService;
        private readonly HubConnection _hubConnection;

        private HocBoDeResponse? _data;
        private CheDoHocEnum _cheDo;
        private int? _maThachDau;

        private int _currentIndex = 0, _totalScore = 0, _correctCount = 0, _wrongCount = 0, _comboCount = 0, _currentTimeLeft = 100;
        private bool _iAnsweredCurrent = false, _opponentAnsweredCurrent = false;
        private bool _isFinishing = false; 
        private Stopwatch _totalTimer = new Stopwatch();
        private Stopwatch _questionStopwatch = new Stopwatch();
        private List<ChiTietTraLoiRequest> _danhSachTraLoiChiTiet = new List<ChiTietTraLoiRequest>();

        public HocBoDePage(IBoDeHocService boDeHocService, IThachDauService thachDauService, HubConnection hubConnection)
        {
            InitializeComponent();
            _boDeHocService = boDeHocService;
            _thachDauService = thachDauService;
            _hubConnection = hubConnection;
            timerTick.Tick += TimerTick_Tick;
            btnNext.Click += btnNext_Click;

            this.HandleDestroyed += HocBoDePage_HandleDestroyed;
        }



        public void Initialize(HocBoDeResponse data, CheDoHocEnum cheDo, int? maThachDau = null)
        {
            _cheDo = cheDo;
            _maThachDau = maThachDau;
            _data = data;

            // Lọc câu hỏi nếu học một mình
            if (_cheDo == CheDoHocEnum.HocMotMinh)
                _data.DanhSachCauHoi = _data.DanhSachCauHoi.Where(q => q.ThongTinThe.NgayOnTapTiepTheo == null || q.ThongTinThe.NgayOnTapTiepTheo <= DateTime.Now).ToList();

            if (_data.DanhSachCauHoi.Count == 0) { ShowFeed("Hôm nay đã hết bài tập!", Color.Lime); return; }

            prgStatus.Minimum = 0;
            prgStatus.Maximum = _data.DanhSachCauHoi.Count;
            // -----------------------------------------------------------

            _currentIndex = 0; _totalScore = 0; _correctCount = 0; _wrongCount = 0; _comboCount = 0;
            _isFinishing = false;
            _totalTimer.Restart();
            lblFeedContent.Text = ">> Bắt đầu trận đấu...";

            if (_cheDo == CheDoHocEnum.ThachDau) RegisterScoreEvents();
            else { lblOpponentScore.Visible = prgOpponent.Visible = false; }

            ShowQuestion(0);
        }

        private void RegisterScoreEvents()
        {
            _hubConnection.Remove("UpdateOpponentScore");
            _hubConnection.On<Guid, int>("UpdateOpponentScore", (userId, score) => {
                if (userId != UserSession.CurrentUser?.MaNguoiDung)
                {
                    this.Invoke(() => {
                        lblOpponentScore.Text = $"ĐỐI THỦ: {score} XP";
                        prgOpponent.Value = Math.Min(score, prgOpponent.Maximum);
                        ShowFeed($"Đối thủ vừa đạt {score} XP", Color.FromArgb(255, 128, 128), true);
                    });
                }
            });

            _hubConnection.Remove("OpponentReadyNext");
            _hubConnection.On<Guid, int>("OpponentReadyNext", (userId, qIndex) => {
                if (qIndex == _currentIndex && userId != UserSession.CurrentUser?.MaNguoiDung)
                {
                    _opponentAnsweredCurrent = true;
                    this.Invoke(async () => {
                        ShowFeed("Đối thủ đã xong. Đang đợi bạn...", Color.Cyan, true);
                        await CheckAndMoveToNext();
                    });
                }
            });

            _hubConnection.Remove("OpponentLeft");
            _hubConnection.On("OpponentLeft", () => {
                this.Invoke(() => {
                    timerTick.Stop();
                    MessageBox.Show("Đối thủ đã rời trận đấu. Bạn sẽ được đưa về trang học tập.", "Thông báo");

                    DisposeAndGoBack();
                });
            });
        }

        private void ShowQuestion(int index)
        {
            if (_data == null || _data.DanhSachCauHoi == null || index >= _data.DanhSachCauHoi.Count) return;

            _currentIndex = index; _iAnsweredCurrent = false; _opponentAnsweredCurrent = false;
            lblStatusMessage.Visible = false; btnNext.Enabled = true; btnNext.Text = "XÁC NHẬN ➔";
            timerTick.Stop(); _currentTimeLeft = 100; prgTimeCountdown.Value = 100;

            lblProgress.Text = $"CÂU HỎI: {index + 1} / {_data.DanhSachCauHoi.Count}";
            prgStatus.Value = index + 1;
            // -----------------------------------------------------------

            pnlQuestionContent.Controls.Clear();
            var cauHoi = _data.DanhSachCauHoi[index];
            Control qc = CreateQuestionControl(cauHoi);
            qc.Dock = DockStyle.Fill;
            pnlQuestionContent.Controls.Add(qc);
            _questionStopwatch.Restart();
            timerTick.Start();
        }

        private async void btnNext_Click(object? sender, EventArgs? e)
        {
            if (_iAnsweredCurrent) return;
            if (pnlQuestionContent.Controls.Count > 0 && pnlQuestionContent.Controls[0] is IQuestionControl qc)
            {
                if (!qc.HasAnswered && sender != null)
                {
                    lblStatusMessage.Text = "⚠️ Vui lòng chọn hoặc nhập đáp án!";
                    lblStatusMessage.ForeColor = Color.OrangeRed;
                    lblStatusMessage.Visible = true;
                    return;
                }

                _questionStopwatch.Stop();

                timerTick.Stop();
                int thoiGianThucTe = (int)Math.Max(1, _questionStopwatch.Elapsed.TotalSeconds);
                bool isCorrect = false;
                if (sender != null)
                {
                    qc.ShowResult();
                    isCorrect = qc.IsCorrect;
                }
                var currentCauHoi = _data!.DanhSachCauHoi[_currentIndex];
                _danhSachTraLoiChiTiet.Add(new ChiTietTraLoiRequest
                {
                    MaThe = currentCauHoi.ThongTinThe.MaThe,
                    TraLoiDung = isCorrect,
                    CauTraLoiUser = qc.GetUserAnswer(), 
                    DapAnDung = currentCauHoi.ThongTinThe.MatSau,
                    ThoiGianTraLoiGiay = thoiGianThucTe
                });

                await Task.Delay(600);

                _iAnsweredCurrent = true;
                UpdateStats(isCorrect);
                if (UserSession.CurrentUser != null && _cheDo == CheDoHocEnum.ThachDau && _maThachDau.HasValue)
                {
                    await _hubConnection.InvokeAsync("SendScore", _maThachDau.Value.ToString(), UserSession.CurrentUser.MaNguoiDung, _totalScore);
                    await _hubConnection.InvokeAsync("SendReadyNext", _maThachDau.Value.ToString(), UserSession.CurrentUser.MaNguoiDung, _currentIndex);
                }

                await CheckAndMoveToNext();
            }
        }

        private void UpdateStats(bool isCorrect)
        {
            if (isCorrect) { _correctCount++; _comboCount++; } else { _wrongCount++; _comboCount = 0; }

            int quality = isCorrect ? (int)(_currentTimeLeft / 20) + 1 : 1;
            int gain = isCorrect ? (quality * 10) + (_comboCount * 5) : 0;
            _totalScore += gain;

            lblScore.Text = $"BẠN: {_totalScore} XP";
            if (isCorrect)
            {
                ShowFloatingXP(gain);
                ShowFeed($"Câu {_currentIndex + 1}: Đúng (+{gain} XP)", Color.Lime, true);
            }
            else
            {
                ShowFeed($"Câu {_currentIndex + 1}: Sai (0 XP)", Color.OrangeRed, true);
            }

            if (_comboCount > 1) { lblStatusMessage.Text = $"🔥 COMBO X{_comboCount}!"; lblStatusMessage.Visible = true; }
        }

        private async void ShowFloatingXP(int gain)
        {
            Label pop = new Label { Text = $"+{gain} XP", ForeColor = Color.Lime, Font = new Font("Segoe UI Black", 14F), AutoSize = true, BackColor = Color.Transparent, Location = new Point(lblScore.Left + 150, lblScore.Bottom) };
            this.Controls.Add(pop); pop.BringToFront();
            for (int i = 0; i < 15; i++) { pop.Top -= 4; await Task.Delay(20); }
            this.Controls.Remove(pop);
        }

        private async Task CheckAndMoveToNext()
        {
            if (_data?.DanhSachCauHoi == null)
                return;

            if (_cheDo == CheDoHocEnum.HocMotMinh || (_iAnsweredCurrent && _opponentAnsweredCurrent))
            {
                if (_currentIndex < _data.DanhSachCauHoi.Count - 1)
                    ShowQuestion(_currentIndex + 1);
                else
                    await FinishQuiz();
            }
            else if (_iAnsweredCurrent)
            {
                btnNext.Enabled = false; btnNext.Text = "ĐANG ĐỢI...";
                lblStatusMessage.Text = "Đang đợi đối thủ hoàn thành câu này...";
                lblStatusMessage.Visible = true;
            }
        }

        private async Task FinishQuiz()
        {
            if (_isFinishing) return;
            timerTick.Stop();
            _totalTimer.Stop();
            _questionStopwatch.Stop(); // Đảm bảo stopwatch câu cuối cùng cũng dừng

            pnlQuestionContent.Controls.Clear();

            // 1. TÍNH TỔNG THỜI GIAN THỰC TẾ LÀM BÀI (Cộng dồn từng câu)
            int? tongThoiGianThucGiay = _danhSachTraLoiChiTiet.Sum(x => x.ThoiGianTraLoiGiay);
            TimeSpan thoiGianThucSpan = TimeSpan.FromSeconds((double)(tongThoiGianThucGiay ?? 0));

            var resultUI = new QuizResultControl();
            resultUI.Dock = DockStyle.Fill;
            resultUI.OnFinishClicked += () => { _isFinishing = true; DisposeAndGoBack(); };

            if (UserSession.CurrentUser != null)
            {
                if (_cheDo == CheDoHocEnum.HocMotMinh)
                {
                    var phienHoc = new PhienHoc
                    {
                        MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                        MaBoDe = _data?.ThongTinChung?.MaBoDe,
                        LoaiPhien = "KiemTra",
                        // Thời gian bắt đầu có thể giữ nguyên để biết thời điểm bắt đầu học
                        ThoiGianBatDau = DateTime.Now.Subtract(_totalTimer.Elapsed),
                        ThoiGianKetThuc = DateTime.Now,
                        // SỬ DỤNG TỔNG THỜI GIAN THỰC TẾ
                        ThoiGianHocGiay = tongThoiGianThucGiay,
                        TongSoThe = _data?.DanhSachCauHoi?.Count ?? 0,
                        SoTheDung = _correctCount,
                        SoTheSai = _wrongCount,
                        TyLeDung = (_data?.DanhSachCauHoi?.Count ?? 0) > 0 ? (double)_correctCount / (_data?.DanhSachCauHoi?.Count ?? 1) * 100 : 0
                    };

                    await _boDeHocService.LuuKetQuaPhienHocAsync(phienHoc, _danhSachTraLoiChiTiet);
                    await RefreshUserStats();

                    // Hiển thị thời gian thực lên UI kết quả
                    resultUI.DisplaySoloResult(_correctCount, _wrongCount, _data?.DanhSachCauHoi?.Count ?? 0, thoiGianThucSpan);
                }
                else if (_maThachDau.HasValue)
                {
                    var updateReq = new CapNhatKetQuaThachDauRequest
                    {
                        MaThachDau = _maThachDau.Value,
                        MaNguoiDung = UserSession.CurrentUser.MaNguoiDung,
                        MaBoDe = _data?.ThongTinChung?.MaBoDe ?? 0,
                        Diem = _totalScore,
                        SoTheDung = _correctCount,
                        SoTheSai = _wrongCount,
                      
                        ThoiGianLamBaiGiay = tongThoiGianThucGiay
                    };

                    bool isSaved = await _thachDauService.CapNhatKetQuaNguoiChoiAsync(updateReq, _danhSachTraLoiChiTiet);

                    if (isSaved)
                    {
                        await _thachDauService.HoanThanhVaCleanupAsync(_maThachDau.Value);
                        var bxh = (await _thachDauService.GetBangXepHangAsync(_maThachDau.Value)).ToList();

                        var me = bxh.FirstOrDefault(x => x.MaNguoiDung == UserSession.CurrentUser.MaNguoiDung);
                        var opp = bxh.FirstOrDefault(x => x.MaNguoiDung != UserSession.CurrentUser.MaNguoiDung);

                        // So sánh thắng thua dựa trên Diem, sau đó là ThoiGianLamBaiGiay thực tế
                        bool isWin = (opp == null) || (me != null && opp != null &&
                                     (me.Diem > opp.Diem || (me.Diem == opp.Diem && me.ThoiGianLamBaiGiay < opp.ThoiGianLamBaiGiay)));

                        resultUI.DisplayChallengeResult(_totalScore, _correctCount, _wrongCount, isWin, _maThachDau.Value);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi lưu kết quả trận đấu!");
                    }

                    await RefreshUserStats();
                }

                // Logic xem lại chi tiết câu hỏi (giữ nguyên)
                resultUI.OnShowDetailsClicked += () => {
                    var reviewUI = new QuizReviewControl(_danhSachTraLoiChiTiet);
                    reviewUI.Dock = DockStyle.Fill;
                    reviewUI.OnBackClicked += () => {
                        pnlQuestionContent.Controls.Clear();
                        pnlQuestionContent.Controls.Add(resultUI);
                    };
                    pnlQuestionContent.Controls.Clear();
                    pnlQuestionContent.Controls.Add(reviewUI);
                };
            }

            pnlQuestionContent.Controls.Add(resultUI);
            pnlBottom.Visible = false;
        }

        private void ShowFeed(string msg, Color color, bool append = false)
        {
            lblFeedContent.ForeColor = color;
            if (append)
            {
                var lines = lblFeedContent.Text.Split('\n').ToList();
                if (lines.Count > 12) lines.RemoveAt(0);
                lines.Add(">> " + msg);
                lblFeedContent.Text = string.Join("\n", lines);
            }
            else lblFeedContent.Text = ">> " + msg;
        }

        private void TimerTick_Tick(object? sender, EventArgs e)
        {
            if (_currentTimeLeft > 0) { _currentTimeLeft--; prgTimeCountdown.Value = _currentTimeLeft; }
            else { timerTick.Stop(); btnNext_Click(null, null); }
        }

        private Control CreateQuestionControl(ChiTietCauHoiHocResponse cauHoi)
        {
            var loai = cauHoi.ThongTinThe.LoaiThe;
            if (_cheDo == CheDoHocEnum.ThachDau && loai == LoaiTheEnum.CoBan) return TransformToQuizControl(cauHoi);
            return loai switch
            {
                LoaiTheEnum.TracNghiem => new TracNghiemHocControl(cauHoi.ThongTinThe, cauHoi.TracNghiem ?? new()),
                LoaiTheEnum.DienKhuyet => new DienKhuyetHocControl(cauHoi.ThongTinThe),
                LoaiTheEnum.GhepCap => new GhepCapHocControl(cauHoi.ThongTinThe, cauHoi.CapGhep ?? new()),
                LoaiTheEnum.SapXep => new SapXepHocControl(cauHoi.ThongTinThe, cauHoi.SapXep ?? new()),
                _ => new LatTheHocControl(cauHoi.ThongTinThe)
            };
        }

        private Control TransformToQuizControl(ChiTietCauHoiHocResponse cauHoi)
        {
            string correctAns = cauHoi.ThongTinThe.MatSau;
            var distractors = _data?.DanhSachCauHoi?
                .Where(q => q.ThongTinThe.MaThe != cauHoi.ThongTinThe.MaThe)
                .Select(q => q.ThongTinThe.MatSau).Distinct().OrderBy(x => Guid.NewGuid()).Take(3).ToList() ?? new List<string>();
            var listDapAn = distractors.Select(d => new DapAnTracNghiemResponse { NoiDung = d, LaDapAnDung = false }).ToList();
            listDapAn.Add(new DapAnTracNghiemResponse { NoiDung = correctAns, LaDapAnDung = true });
            return new TracNghiemHocControl(cauHoi.ThongTinThe, listDapAn.OrderBy(x => Guid.NewGuid()).ToList());
        }

        private async void HocBoDePage_HandleDestroyed(object? sender, EventArgs e)
        {
            // Nếu thoát ngang khi đang đấu hoặc chưa nhấn Hoàn Thành
            if (_cheDo == CheDoHocEnum.ThachDau && _maThachDau.HasValue && !_isFinishing)
            {
                _isFinishing = true;
                try
                {
                    if (_hubConnection.State == HubConnectionState.Connected)
                        await _hubConnection.InvokeAsync("NotifyOpponentLeft", _maThachDau.Value.ToString());

                    await _thachDauService.HuyThachDauAsync(_maThachDau.Value);
                }
                catch (Exception ex) { Debug.WriteLine("Cleanup Error: " + ex.Message); }
            }
        }

        private void DisposeAndGoBack()
        {
            // 1. Khóa cờ hiệu để sự kiện HandleDestroyed biết là chúng ta đang chủ động thoát
            _isFinishing = true;

            // 2. Dừng các bộ đếm thời gian đang chạy
            timerTick.Stop();
            _totalTimer.Stop();

            // 3. Sử dụng Singleton của MainForm để nạp trang mới
            // Giả sử bạn muốn quay về Trang Chủ
            if (this.FindForm() is MainForm mainForm)
            {
                mainForm.LoadPage((Program.ServiceProvider!.GetRequiredService<HocTapPage>()));
            }
        }

        private async Task RefreshUserStats()
        {
            if (Program.ServiceProvider != null && UserSession.CurrentUser != null)
            {
                using (var scope = Program.ServiceProvider.CreateScope())
                {
                    var userDb = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                    var user = await userDb.NguoiDungs.FindAsync(UserSession.CurrentUser.MaNguoiDung);

                    if (user != null)
                    {
                        // Cập nhật các thông số mới nhất từ DB vào Session
                        UserSession.CurrentUser.TongDiemXp = user.TongDiemXp ?? 0;
                        UserSession.CurrentUser.Vang = user.Vang ?? 0;
                        UserSession.CurrentUser.KimCuong = user.KimCuong ?? 0;

                        // Kích hoạt sự kiện để các trang khác (như Dashboard) cập nhật theo
                        AppEvents.OnUserStatsChanged();
                    }
                }
            }
        }
    }
}