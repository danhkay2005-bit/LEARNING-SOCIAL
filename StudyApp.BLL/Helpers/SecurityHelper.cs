using System;
using System.Security.Cryptography;
using System.Text;

namespace StudyApp.BLL.Helpers
{
    internal class SecurityHelper
    {
        // Mã hóa mật khẩu (MD5)
        public static string? HashPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return null;

            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
