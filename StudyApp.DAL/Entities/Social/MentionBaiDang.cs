    using System;
    using System.Collections.Generic;

    namespace StudyApp.DAL.Entities.Social;

    public partial class MentionBaiDang
    {
        public int MaBaiDang { get; set; }

        public Guid MaNguoiDuocMention { get; set; }

        public DateTime? ThoiGian { get; set; }

        public virtual BaiDang MaBaiDangNavigation { get; set; } = null!;
    }
