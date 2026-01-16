public class ChiaSeBaiDangRequest
{
    public int MaBaiDangGoc { get; set; }
    public Guid MaNguoiChiaSe { get; set; }
    public string? NoiDungThem { get; set; }
    public QuyenRiengTuEnum QuyenRiengTu { get; set; }
    public ChiaSeBaiDangRequest()
    {
        QuyenRiengTu = QuyenRiengTuEnum.CongKhai;
    }
    public ChiaSeBaiDangRequest(int maBaiDangGoc, Guid maNguoiChiaSe, string? noiDungThem, QuyenRiengTuEnum quyenRiengTu)
    {
        MaBaiDangGoc = maBaiDangGoc;
        MaNguoiChiaSe = maNguoiChiaSe;
        NoiDungThem = noiDungThem;
        QuyenRiengTu = quyenRiengTu;
    }
    
}
