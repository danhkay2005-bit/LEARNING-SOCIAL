using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class LichSuThachDau
{
    // Khóa chính tự tăng
    public int MaLichSu { get; set; }

    // ID người dùng (Không có navigation vì nằm khác DB)
    public Guid MaNguoiDung { get; set; }

    // ID bộ đề học
    public int MaBoDe { get; set; }

    // Điểm số đạt được trong trận đấu
    public int? Diem { get; set; }

    // Số lượng thẻ Flashcard trả lời đúng
    public int? SoTheDung { get; set; }

    // Số lượng thẻ Flashcard trả lời sai
    public int? SoTheSai { get; set; }

    // Tổng thời gian làm bài tính bằng giây
    public int? ThoiGianLamBaiGiay { get; set; }

    // Xác định người này có thắng trận đấu hay không
    public bool? LaNguoiThang { get; set; }

    // Lưu lại mã PIN 6 số của phòng đấu để đối chiếu khi cần
    public int? MaThachDauGoc { get; set; }

    // Thời điểm kết thúc trận đấu và lưu lịch sử
    public DateTime? ThoiGianKetThuc { get; set; }

    // Navigation property liên kết với bảng Bộ đề trong cùng DB
    public virtual BoDeHoc MaBoDeNavigation { get; set; } = null!;
}