using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudyApp.DTO.Enums;

namespace StudyApp.DTO.Requests.Learn
{
    public class TaoTheFlashcardRequest
    {
        [Range(1, int.MaxValue)]
        public int MaBoDe { get; set; }

        public LoaiTheEnum LoaiThe { get; set; } = LoaiTheEnum.CoBan;

        [Required]
        public string MatTruoc { get; set; } = null!;

        [Required]
        public string MatSau { get; set; } = null!;

        public string? GiaiThich { get; set; }
        public string? HinhAnhTruoc { get; set; }
        public string? HinhAnhSau { get; set; }

        public int DoKho { get; set; } = 3;

        // Danh sách các thành phần con (Composition)
        public List<DapAnTracNghiemRequest> OptionsTracNghiem { get; set; } = [];
        public List<CapGhepRequest> ListCapGhep { get; set; } = [];
        public List<PhanTuSapXepRequest> ListSapXep { get; set; } = [];

        // Chỉ có khi LoaiThe = DienKhuyet
        public TuDienKhuyetRequest? DataDienKhuyet { get; set; }
    }
}
