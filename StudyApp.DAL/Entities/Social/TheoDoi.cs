using System;
using System.Collections.Generic;

namespace StudyApp.DAL.Entities.Social;

public partial class TheoDoi
{
    public Guid MaNguoiTheoDoi { get; set; }

    public Guid MaNguoiDuocTheoDoi { get; set; }

    public DateTime? ThoiGian { get; set; }

    public bool? ThongBaoBaiMoi { get; set; }
}
