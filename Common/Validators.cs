using System;
using System.Text.RegularExpressions;

namespace Common
{
    // LƯU Ý QUAN TRỌNG: Class bắt buộc phải là 'partial'
    public static partial class Validators
    {
        // --- CÁC PHƯƠNG THỨC SINH REGEX TỰ ĐỘNG (SOURCE GENERATORS) ---

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();

        [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
        private static partial Regex UsernameRegex();

        [GeneratedRegex(@"^(0[3|5|7|8|9])+([0-9]{8})$")]
        private static partial Regex PhoneVNRegex();

        // Các regex kiểm tra mật khẩu
        [GeneratedRegex(@"[A-Z]")]
        private static partial Regex UpperCaseRegex();

        [GeneratedRegex(@"[a-z]")]
        private static partial Regex LowerCaseRegex();

        [GeneratedRegex(@"[0-9]")]
        private static partial Regex DigitRegex();


        // --- CÁC HÀM VALIDATION PUBLIC (LOGIC CHÍNH) ---

        // Kiểm tra Email
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Gọi phương thức đã được sinh code
            return EmailRegex().IsMatch(email);
        }

        // Kiểm tra Username
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // Giả định class Constants vẫn tồn tại như code cũ của bạn
            if (username.Length < Constants.Validation.USERNAME_MIN ||
                username.Length > Constants.Validation.USERNAME_MAX)
                return false;

            return UsernameRegex().IsMatch(username);
        }

        // Kiểm tra Password (Độ dài)
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < Constants.Validation.PASSWORD_MIN ||
                password.Length > Constants.Validation.PASSWORD_MAX)
                return false;

            return true;
        }

        // Kiểm tra Password mạnh (có chữ hoa, thường, số)
        public static bool IsStrongPassword(string password)
        {
            if (!IsValidPassword(password))
                return false;

            // Sử dụng các regex đã sinh sẵn -> Tốc độ nhanh hơn nhiều so với tạo mới mỗi lần
            bool hasUpper = UpperCaseRegex().IsMatch(password);
            bool hasLower = LowerCaseRegex().IsMatch(password);
            bool hasDigit = DigitRegex().IsMatch(password);

            return hasUpper && hasLower && hasDigit;
        }

        // Kiểm tra Số điện thoại VN
        public static bool IsValidPhoneVN(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return PhoneVNRegex().IsMatch(phone);
        }

        // --- CÁC HÀM HỖ TRỢ KHÁC (GIỮ NGUYÊN) ---

        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidLength(string value, int min, int max)
        {
            if (string.IsNullOrEmpty(value))
                return min == 0;

            return value.Length >= min && value.Length <= max;
        }

        public static bool IsPositive(int value)
        {
            return value > 0;
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}