using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Constants
    {
        public static class Game
        {
            //XP NHẬN ĐƯỢC
            public const int XP_HOC_1_THE = 2;
            public const int XP_DUNG_1_CAU = 5;
            public const int XP_HOAN_THANH_1_BO_DE = 20;
            public const int XP_DIEM_DANH = 5;
            public const int XP_THANG_THACH_DAU = 50;
            public const int XP_THUA_THACH_DAU = 20;

            //VÀNG NHẬN ĐƯỢC
            public const int VANG_DIEM_DANH_NGAY_1 = 10;
            public const int VANG_DIEM_DANH_NGAY_7 = 100;
            public const int VANG_HOAN_THANH_NHIEM_VU = 20;
            public const int VANG_THANG_THACH_DAU = 30;

            //Streak
            public const int SO_FREEZE_MAC_DINH = 2;
            public const int SO_HOI_SINH_MAC_DINH = 1;
            public const int GIA_FREEZE = 200;
            public const int GIA_HOI_SINH = 500;

        }
        public static class HocTap {
            public const int SO_THE_MOI_MAC_DINH = 20;
            public const int SO_THE_ON_TAP_MAX = 100;
            public const int THOI_GIAN_HIEN_DAP_AN = 3;

            //SRS - Spaced Repetition System
            public const double HE_SO_THE_MAC_DINH = 2.5;
            public const double HE_SO_DE_MIN = 1.3;
            public const int KHOANG_CACH_NGAY_MIN = 1;
        }

        public static class MXH 
        {
            public const int STORY_HET_HAN_GIO = 24;
            public const int SO_THANH_VIEN_NHOM_MAX = 500;
            public const int THOI_GIAN_THACH_DAU_HET_HAN = 24;  // Giờ
        }

        public static class Validation
        {
            public const int USERNAME_MIN = 3;
            public const int USERNAME_MAX = 50;
            public const int PASSWORD_MIN = 6;
            public const int PASSWORD_MAX = 100;
            public const int HO_TEN_MAX = 100;
            public const int TIEU_DE_MAX = 200;
            public const int MO_TA_MAX = 1000;
        }


    }
}
