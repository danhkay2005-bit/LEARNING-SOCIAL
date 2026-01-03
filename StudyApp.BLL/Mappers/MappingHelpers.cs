using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace StudyApp.BLL.Mappers
{
    /// <summary>
    /// Các helper methods dùng chung cho AutoMapper
    /// </summary>
    public static class MappingHelpers
    {
        #region Enum Helpers
        /// <summary>
        /// Parse string sang Enum, trả về default nếu không parse được
        /// </summary>
        public static TEnum ParseEnum<TEnum>(string? value) where TEnum : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                return default;

            return Enum.TryParse<TEnum>(value, true, out var result) ? result : default;
        }

        /// <summary>
        /// Parse string sang nullable Enum
        /// </summary>
        public static TEnum? ParseEnumNullable<TEnum>(string? value) where TEnum : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
        }
        #endregion

        #region String Helpers
        /// <summary>
        /// Rút gọn chuỗi với độ dài tối đa
        /// </summary>
        public static string? Truncate(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength) + "...";
        }

        /// <summary>
        /// Lấy hình ảnh đầu tiên từ chuỗi (JSON array hoặc phân tách bằng dấu phẩy)
        /// </summary>
        public static string? GetFirstImage(string? images)
        {
            if (string.IsNullOrEmpty(images))
                return null;

            // Nếu là JSON array
            if (images.TrimStart().StartsWith("["))
            {
                try
                {
                    var list = JsonSerializer.Deserialize<List<string>>(images);
                    return list?.FirstOrDefault();
                }
                catch
                {
                    return images;
                }
            }

            // Nếu phân tách bằng dấu phẩy
            var parts = images.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return parts.FirstOrDefault()?.Trim();
        }

        /// <summary>
        /// Đếm số hình ảnh
        /// </summary>
        public static int CountImages(string? images)
        {
            if (string.IsNullOrEmpty(images))
                return 0;

            if (images.TrimStart().StartsWith("["))
            {
                try
                {
                    var list = JsonSerializer.Deserialize<List<string>>(images);
                    return list?.Count ?? 0;
                }
                catch
                {
                    return 1;
                }
            }

            return images.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
        }
        #endregion

        #region Time Helpers
        /// <summary>
        /// Format thời gian từ giây sang chuỗi hiển thị
        /// </summary>
        public static string FormatDuration(int? seconds)
        {
            if (!seconds.HasValue || seconds.Value <= 0)
                return "0s";

            var time = TimeSpan.FromSeconds(seconds.Value);

            if (time.TotalHours >= 1)
                return $"{(int)time.TotalHours}h {time.Minutes}m {time.Seconds}s";

            if (time.TotalMinutes >= 1)
                return $"{time.Minutes}m {time.Seconds}s";

            return $"{time.Seconds}s";
        }

        /// <summary>
        /// Format thời gian ngắn gọn
        /// </summary>
        public static string FormatDurationShort(int? seconds)
        {
            if (!seconds.HasValue || seconds.Value <= 0)
                return "0s";

            var time = TimeSpan.FromSeconds(seconds.Value);

            if (time.TotalHours >= 1)
                return $"{(int)time.TotalHours}h {time.Minutes}m";

            if (time.TotalMinutes >= 1)
                return $"{time.Minutes}m";

            return $"{time.Seconds}s";
        }

        /// <summary>
        /// Format thời gian tương đối (vd: "5 phút trước")
        /// </summary>
        public static string FormatRelativeTime(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "";

            var now = DateTime.UtcNow;
            var diff = now - dateTime.Value;

            if (diff.TotalSeconds < 60)
                return "Vừa xong";

            if (diff.TotalMinutes < 60)
                return $"{(int)diff.TotalMinutes} phút trước";

            if (diff.TotalHours < 24)
                return $"{(int)diff.TotalHours} giờ trước";

            if (diff.TotalDays < 7)
                return $"{(int)diff.TotalDays} ngày trước";

            if (diff.TotalDays < 30)
                return $"{(int)(diff.TotalDays / 7)} tuần trước";

            if (diff.TotalDays < 365)
                return $"{(int)(diff.TotalDays / 30)} tháng trước";

            return $"{(int)(diff.TotalDays / 365)} năm trước";
        }

        /// <summary>
        /// Tính số ngày còn lại
        /// </summary>
        public static int? CalculateDaysRemaining(DateOnly? endDate)
        {
            if (!endDate.HasValue)
                return null;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var days = endDate.Value.DayNumber - today.DayNumber;
            return days > 0 ? days : 0;
        }
        #endregion

        #region File Helpers
        /// <summary>
        /// Format kích thước file
        /// </summary>
        public static string FormatFileSize(int? bytes)
        {
            if (!bytes.HasValue || bytes.Value <= 0)
                return "0 B";

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes.Value;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return $"{len: 0.##} {sizes[order]}";
        }

        /// <summary>
        /// Lấy extension của file từ đường dẫn
        /// </summary>
        public static string? GetFileExtension(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            var lastDot = filePath.LastIndexOf('.');
            return lastDot >= 0 ? filePath.Substring(lastDot + 1).ToLower() : null;
        }

        /// <summary>
        /// Kiểm tra file có phải là hình ảnh không
        /// </summary>
        public static bool IsImageFile(string? filePath)
        {
            var ext = GetFileExtension(filePath);
            if (string.IsNullOrEmpty(ext))
                return false;

            string[] imageExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "webp", "svg" };
            return imageExtensions.Contains(ext);
        }

        /// <summary>
        /// Kiểm tra file có phải là video không
        /// </summary>
        public static bool IsVideoFile(string? filePath)
        {
            var ext = GetFileExtension(filePath);
            if (string.IsNullOrEmpty(ext))
                return false;

            string[] videoExtensions = { "mp4", "avi", "mov", "wmv", "flv", "mkv", "webm" };
            return videoExtensions.Contains(ext);
        }
        #endregion

        #region Math Helpers
        /// <summary>
        /// Tính phần trăm tiến độ
        /// </summary>
        public static double CalculateProgress(int? current, int target)
        {
            if (target <= 0)
                return 0;

            var currentValue = current ?? 0;
            return Math.Min(100, Math.Round((double)currentValue / target * 100, 2));
        }

        /// <summary>
        /// Tính tỷ lệ phần trăm
        /// </summary>
        public static double CalculatePercentage(int? numerator, int? denominator)
        {
            var num = numerator ?? 0;
            var den = denominator ?? 0;

            if (den <= 0)
                return 0;

            return Math.Round((double)num / den * 100, 2);
        }

        /// <summary>
        /// Làm tròn số thập phân
        /// </summary>
        public static double RoundToDecimal(double? value, int decimals = 2)
        {
            return Math.Round(value ?? 0, decimals);
        }
        #endregion

        #region Null Safe Helpers
        /// <summary>
        /// Lấy giá trị hoặc default
        /// </summary>
        public static T GetValueOrDefault<T>(T? value, T defaultValue) where T : struct
        {
            return value ?? defaultValue;
        }

        /// <summary>
        /// Chuyển nullable byte sang enum
        /// </summary>
        public static TEnum? ByteToEnum<TEnum>(byte? value) where TEnum : struct, Enum
        {
            if (!value.HasValue)
                return null;

            return (TEnum)Enum.ToObject(typeof(TEnum), value.Value);
        }

        /// <summary>
        /// Chuyển enum sang byte
        /// </summary>
        public static byte EnumToByte<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Convert.ToByte(value);
        }
        #endregion
    }
}