using System.Collections.Generic;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Responses.Learn
{
    public class FlashcardResponse
    {
        public int MaThe { get; set; }
        public int MaBoDe { get; set; }
        public LoaiTheEnum LoaiThe { get; set; }
        public int DoKho { get; set; }

        public string MatTruoc { get; set; } = null!;
        public string MatSau { get; set; } = null!;
        public string? GiaiThich { get; set; }
        public string? HinhAnhTruoc { get; set; }
        public string? HinhAnhSau { get; set; }

        public List<DapAnTracNghiemResponse> OptionsTracNghiem { get; set; } = [];
        public List<CapGhepResponse> ListCapGhep { get; set; } = [];
        public List<PhanTuSapXepResponse> ListSapXep { get; set; } = [];

        public TuDienKhuyetResponse? DataDienKhuyet { get; set; }
    }
}