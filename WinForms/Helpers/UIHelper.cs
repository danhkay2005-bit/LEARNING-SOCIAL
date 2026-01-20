using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace WinForms.Helpers
{
     class UIHelper
    {

        public static void DrawRoundedPanel(Graphics g, Rectangle rect, int radius, Color borderColor, Color backColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            using var brush = new SolidBrush(backColor);
            g.FillPath(brush, path);

            using var pen = new Pen(borderColor, 1);
            g.DrawPath(pen, path);
        }


    }
}
