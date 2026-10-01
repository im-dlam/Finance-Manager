using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 30;

        // Thêm thuộc tính Đổi màu viền (Mặc định là trong suốt)
        private Color _borderColor = Color.Transparent;
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate(); // Lệnh này giúp Form lập tức vẽ lại màu mới khi bạn chọn ở Properties
            }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Hình cắt viền (To bằng 100% kích thước)
            RectangleF rectRegion = new RectangleF(0, 0, this.Width, this.Height);

            // Hình để vẽ viền (Thụt vào 1 pixel ở cạnh phải và cạnh dưới để không bị lẹm)
            RectangleF rectBorder = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            if (BorderRadius > 0)
            {
                // 1. Gọt các góc
                using (GraphicsPath pathRegion = GetRoundedPath(rectRegion, BorderRadius))
                {
                    this.Region = new Region(pathRegion);
                }
                // 2. Vẽ cái nét viền màu thụt vào 1 tí
                using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, BorderRadius))
                using (Pen pen = new Pen(BorderColor, 1))
                {
                    e.Graphics.DrawPath(pen, pathBorder);
                }
            }
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}