using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common
{
    public static class Helpers
    {
        // Static array for XP thresholds to avoid duplication in logic
        private static readonly int[] LevelThresholds = [100, 500, 1500, 4000, 10000, 25000, 50000];

        // ======== MÃ HÓA (ENCRYPTION & HASHING) ========

        /// <summary>
        /// Mã hóa MD5 - Thường dùng để check checksum hoặc tích hợp hệ thống cũ.
        /// </summary>
        public static string HashMD5(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = MD5.HashData(inputBytes);

            // Convert.ToHexString là cách nhanh nhất trong .NET hiện đại
            return Convert.ToHexString(hashBytes).ToLower();
        }

        /// <summary>
        /// Mã hóa SHA256 - Chuẩn bảo mật cho hash chuỗi/mật khẩu.
        /// </summary>
        public static string HashSHA256(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using var sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA256.HashData(inputBytes);

            return Convert.ToHexString(hash).ToLower();
        }

        // ======== THỜI GIAN (DATETIME) ========

        /// <summary>
        /// Tính thời gian tương đối (VD: 5 phút trước). 
        /// Khuyên dùng DateTime.UtcNow để tránh sai lệch múi giờ server.
        /// </summary>
        public static string TimeAgo(DateTime dateTime)
        {
            TimeSpan span = DateTime.Now - dateTime;

            if (span.TotalMinutes < 1) return "Vừa xong";
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} phút trước";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours} giờ trước";
            if (span.TotalDays < 7) return $"{(int)span.TotalDays} ngày trước";
            if (span.TotalDays < 30) return $"{(int)(span.TotalDays / 7)} tuần trước";
            if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)} tháng trước";

            return $"{(int)(span.TotalDays / 365)} năm trước";
        }

        public static int GetWeekOfYear(DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                date,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
        }

        // ======== CHUỖI (STRINGS) ========

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : string.Concat(value.AsSpan(0, maxLength), "...");
        }

        /// <summary>
        /// Tạo mã OTP bảo mật cao bằng RandomNumberGenerator (thay cho Random thường).
        /// </summary>
        public static string GenerateOTP(int length = 6)
        {
            const string chars = "0123456789";
            return string.Create(length, chars, (span, c) =>
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = c[RandomNumberGenerator.GetInt32(c.Length)];
                }
            });
        }

        // ======== SỐ (NUMBERS) ========

        /// <summary>
        /// Rút gọn số lớn (1000 -> 1K, 1000000 -> 1M).
        /// </summary>
        public static string FormatNumber(long number)
        {
            return number switch
            {
                >= 1000000 => (number / 1000000.0).ToString("0.#") + "M",
                >= 1000 => (number / 1000.0).ToString("0.#") + "K",
                _ => number.ToString()
            };
        }

        public static double GetPercentage(int value, int total)
        {
            if (total == 0) return 0;
            return Math.Round((double)value / total * 100, 1);
        }

        // ======== GAME LOGIC (XP & LEVEL) ========

        public static int GetLevelFromXP(int xp)
        {
            for (int i = 0; i < LevelThresholds.Length; i++)
            {
                if (xp < LevelThresholds[i]) return i + 1;
            }
            return LevelThresholds.Length + 1;
        }

        public static int GetXPForNextLevel(int currentXP)
        {
            foreach (int threshold in LevelThresholds)
            {
                if (currentXP < threshold) return threshold - currentXP;
            }
            return 0; // Max level reached
        }
    }
}