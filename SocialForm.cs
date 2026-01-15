// Inject service vào Form/UserControl
public class SocialForm : Form
{
    private readonly IReactionBaiDangService _reactionBaiDangService;
    private readonly IReactionBinhLuanService _reactionBinhLuanService;

    public SocialForm(
        IReactionBaiDangService reactionBaiDangService,
        IReactionBinhLuanService reactionBinhLuanService)
    {
        _reactionBaiDangService = reactionBaiDangService;
        _reactionBinhLuanService = reactionBinhLuanService;
        InitializeComponent();
    }

    // Ví dụ: Thêm reaction Like cho bài đăng
    private async void btnLike_Click(object sender, EventArgs e)
    {
        var request = new TaoHoacCapNhatReactionBaiDangRequest
        {
            MaBaiDang = 123,
            MaNguoiDung = Guid.Parse("user-guid"),
            LoaiReaction = LoaiReactionEnum.Thich
        };

        var result = await _reactionBaiDangService.TaoHoacCapNhatReactionAsync(request);
        MessageBox.Show($"Đã thả reaction: {result.LoaiReaction}");
    }

    // Lấy thống kê reaction
    private async void LoadThongKeReaction(int maBaiDang)
    {
        var thongKe = await _reactionBaiDangService.LayThongKeReactionTheoMaBaiDangAsync(maBaiDang);
        foreach (var item in thongKe)
        {
            Console.WriteLine($"{item.LoaiReaction}: {item.SoLuong}");
        }
    }
}