using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WinForms.UserControls.Tasks
{
    public partial class FireworksForm : Form
    {

        // ==========================================
        // PHẦN 1: CẤU TRÚC DỮ LIỆU (ENUM & ENTITY)
        // ==========================================

        public enum ParticleColor
        {
            Red, Gold, Lime, Cyan, Magenta, Orange, White
        }

        // Entity đại diện cho 1 hạt pháo hoa
        private class FireworkParticle
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float VelX { get; set; } // Vận tốc ngang
            public float VelY { get; set; } // Vận tốc dọc
            public float Alpha { get; set; } = 255; // Độ trong suốt (255 -> 0)
            public float Size { get; set; }
            public Color Color { get; set; }

            // Constructor khởi tạo ngẫu nhiên
            public FireworkParticle(int centerX, int centerY, Random rnd)
            {
                X = centerX;
                Y = centerY;

                // Toán học: Bắn tỏa tròn 360 độ
                double angle = rnd.NextDouble() * Math.PI * 2;
                double speed = rnd.NextDouble() * 15 + 5; // Tốc độ nổ

                VelX = (float)(Math.Cos(angle) * speed);
                VelY = (float)(Math.Sin(angle) * speed);

                Size = rnd.Next(6, 14); // Kích thước hạt

                // Random màu từ Enum
                var randomColorEnum = (ParticleColor)rnd.Next(0, 7);
                Color = MapColor(randomColorEnum);
            }

            // Mapper: Chuyển Enum sang System.Drawing.Color
            private static Color MapColor(ParticleColor color)
            {
                switch (color)
                {
                    case ParticleColor.Red: return Color.OrangeRed;
                    case ParticleColor.Gold: return Color.Gold;
                    case ParticleColor.Lime: return Color.LimeGreen;
                    case ParticleColor.Cyan: return Color.Cyan;
                    case ParticleColor.Magenta: return Color.Magenta;
                    case ParticleColor.Orange: return Color.Orange;
                    default: return Color.White;
                }
            }

            // Logic vật lý: Di chuyển
            public void UpdatePosition()
            {
                X += VelX;
                Y += VelY;

                VelX *= 0.95f; // Ma sát không khí (chậm dần theo chiều ngang)
                VelY += 0.8f;  // Trọng lực (rơi xuống)

                Alpha -= 5;    // Mờ dần
                if (Alpha < 0) Alpha = 0;
            }
        }

        // ==========================================
        // PHẦN 2: LOGIC FORM
        // ==========================================

        private List<FireworkParticle> _particles = new List<FireworkParticle>();
        private Random _rnd = new Random();
        public FireworksForm()
        {
            InitializeComponent();
        }
        // Hàm kích hoạt nổ (Gọi từ bên ngoài)
        public void Explode()
        {
            int centerX = this.Width / 2;
            int centerY = this.Height / 2;

            // Tạo 150 hạt (Entities)
            for (int i = 0; i < 150; i++)
            {
                _particles.Add(new FireworkParticle(centerX, centerY, _rnd));
            }

            tmrRender.Start();
            this.Show();
        }

        // Timer cập nhật vị trí và vẽ lại
        private void tmrRender_Tick(object sender, EventArgs e)
        {
            if (_particles.Count == 0)
            {
                tmrRender.Stop();
                this.Close(); // Hết hạt thì tắt Form
                return;
            }

            // Cập nhật từng hạt
            foreach (var p in _particles)
            {
                p.UpdatePosition();
            }

            // Xóa hạt đã tắt ngóm
            _particles.RemoveAll(p => p.Alpha <= 0);

            // Vẽ lại màn hình
            this.Invalidate();
        }

        // Vẽ hình (GDI+)
        protected override void OnPaint(PaintEventArgs e)
        {
            // Bật khử răng cưa cho đẹp
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var p in _particles)
            {
                using (var brush = new SolidBrush(Color.FromArgb((int)p.Alpha, p.Color)))
                {
                    e.Graphics.FillEllipse(brush, p.X, p.Y, p.Size, p.Size);
                }
            }
        }
    }
}
