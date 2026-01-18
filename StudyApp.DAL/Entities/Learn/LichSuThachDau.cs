using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Learn;

public partial class LichSuThachDau
{
    public int MaLichSu { get; set; }

    public Guid MaNguoiDung { get; set; }

    public int? MaBoDe { get; set; }

    public int MaThachDauGoc { get; set; }

    public int? Diem { get; set; }

    public int ThoiGianLamBaiGiay { get; set; }

    public int? SoTheDung { get; set; }

    public int? SoTheSai { get; set; }

    public bool? LaNguoiThang { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public virtual BoDeHoc? MaBoDeNavigation { get; set; }
}
