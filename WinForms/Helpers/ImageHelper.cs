using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WinForms.Helpers
{
    /// <summary>
    /// 🖼️ Helper xử lý ảnh
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// ✂️ Resize ảnh về kích thước chuẩn
        /// </summary>
        public static byte[] ResizeImage(string imagePath, int width, int height)
        {
            using (var image = Image.FromFile(imagePath))
            {
                var resized = new Bitmap(width, height);

                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(image, 0, 0, width, height);
                }

                using (var ms = new MemoryStream())
                {
                    resized.Save(ms, ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// ⭕ Tạo ảnh tròn
        /// </summary>
        public static Image CreateCircularImage(Image image, int size)
        {
            var output = new Bitmap(size, size);

            using (var graphics = Graphics.FromImage(output))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, size, size);
                    graphics.SetClip(path);

                    graphics.DrawImage(image, 0, 0, size, size);
                }
            }

            return output;
        }

        /// <summary>
        /// 🔤 Tạo ảnh avatar từ chữ cái đầu
        /// </summary>
        public static Image CreateInitialsAvatar(string initials, int size)
        {
            var bitmap = new Bitmap(size, size);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Background
                var random = new Random(initials.GetHashCode());
                var bgColor = Color.FromArgb(
                    random.Next(100, 200),
                    random.Next(100, 200),
                    random.Next(100, 200)
                );

                using (var brush = new SolidBrush(bgColor))
                {
                    graphics.FillEllipse(brush, 0, 0, size, size);
                }

                // Text
                var text = initials.Length > 2 ? initials.Substring(0, 2).ToUpper() : initials.ToUpper();
                var font = new Font("Segoe UI", size / 3, FontStyle.Bold);
                var textBrush = new SolidBrush(Color.White);

                var textSize = graphics.MeasureString(text, font);
                var x = (size - textSize.Width) / 2;
                var y = (size - textSize.Height) / 2;

                graphics.DrawString(text, font, textBrush, x, y);
            }

            return bitmap;
        }
    }
}