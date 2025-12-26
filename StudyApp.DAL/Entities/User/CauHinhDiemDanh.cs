using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.User;

public partial class CauHinhDiemDanh
{
    public int NgayThu { get; set; }

    public int? ThuongVang { get; set; }

    public int? ThuongXp { get; set; }

    public string? ThuongDacBiet { get; set; }
}
