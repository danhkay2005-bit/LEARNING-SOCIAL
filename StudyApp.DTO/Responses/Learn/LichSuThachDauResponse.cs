namespace StudyApp.DTO.Responses.Learn;

public class LichSuThachDauResponse
{
    public int MaLichSu { get; set; }
    public Guid MaNguoiDung { get; set; }

    // Thông tin bổ sung để hiển thị cho đẹp
    public int MaBoDe { get; set; }
    public string? TenBoDe { get; set; }
    public string? AnhBiaBoDe { get; set; }

    public int Diem { get; set; }
    public int SoTheDung { get; set; }
    public int SoTheSai { get; set; }
    public int ThoiGianLamBaiGiay { get; set; }
    public bool LaNguoiThang { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
}