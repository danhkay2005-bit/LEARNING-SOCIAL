namespace StudyApp.BLL.Mappers;

/// <summary>
/// Helper methods cho AutoMapper
/// </summary>
public static class MappingHelpers
{
    #region Enum Helpers
    public static TEnum ParseEnum<TEnum>(string? value) where TEnum : struct, Enum
    {
        if (string.IsNullOrEmpty(value)) return default;
        return Enum.TryParse<TEnum>(value, true, out var result) ? result : default;
    }

    public static TEnum? ParseEnumNullable<TEnum>(string? value) where TEnum : struct, Enum
    {
        if (string.IsNullOrEmpty(value)) return null;
        return Enum.TryParse<TEnum>(value, true, out var result) ? result : null;
    }

    public static TEnum? ByteToEnum<TEnum>(byte? value) where TEnum : struct, Enum
    {
        if (!value.HasValue) return null;
        return (TEnum)Enum.ToObject(typeof(TEnum), value.Value);
    }
    #endregion

    #region String Helpers
    public static string? Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength) return value;
        return value.Substring(0, maxLength) + "...";
    }

    public static string? GetFirstImage(string? images)
    {
        if (string.IsNullOrEmpty(images)) return null;
        if (images.TrimStart().StartsWith("["))
        {
            try
            {
                var list = System.Text.Json.JsonSerializer.Deserialize<List<string>>(images);
                return list?.FirstOrDefault();
            }
            catch { return images; }
        }
        return images.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
    }
    #endregion

    #region Time Helpers
    public static string FormatDuration(int? seconds)
    {
        if (!seconds.HasValue || seconds.Value <= 0) return "0s";
        var time = TimeSpan.FromSeconds(seconds.Value);
        if (time.TotalHours >= 1) return $"{(int)time.TotalHours}h {time.Minutes}m {time.Seconds}s";
        if (time.TotalMinutes >= 1) return $"{time.Minutes}m {time.Seconds}s";
        return $"{time.Seconds}s";
    }

    public static string FormatRelativeTime(DateTime? dateTime)
    {
        if (!dateTime.HasValue) return "";
        var diff = DateTime.UtcNow - dateTime.Value;
        if (diff.TotalSeconds < 60) return "Vừa xong";
        if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} phút trước";
        if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} giờ trước";
        if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} ngày trước";
        if (diff.TotalDays < 30) return $"{(int)(diff.TotalDays / 7)} tuần trước";
        return $"{(int)(diff.TotalDays / 30)} tháng trước";
    }

    public static int? CalculateDaysRemaining(DateOnly? endDate)
    {
        if (!endDate.HasValue) return null;
        var days = endDate.Value.DayNumber - DateOnly.FromDateTime(DateTime.UtcNow).DayNumber;
        return days > 0 ? days : 0;
    }
    #endregion

    #region File Helpers
    public static string FormatFileSize(int? bytes)
    {
        if (!bytes.HasValue || bytes.Value <= 0) return "0 B";
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes.Value;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
        return $"{len:0.##} {sizes[order]}";
    }
    #endregion

    #region Math Helpers
    public static double CalculateProgress(int? current, int target)
    {
        if (target <= 0) return 0;
        return Math.Min(100, Math.Round((double)(current ?? 0) / target * 100, 2));
    }

    public static double CalculatePercentage(int? numerator, int? denominator)
    {
        if ((denominator ?? 0) <= 0) return 0;
        return Math.Round((double)(numerator ?? 0) / denominator!.Value * 100, 2);
    }
    #endregion
}