using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public class RoundedPanel : Panel
    {
        public int BorderRadius { get; set; } = 30;

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
            if (BorderRadius > 0)
            {
                using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
                {
                    this.Region = new Region(path);

                    using (Pen pen = new Pen(this.BackColor, 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
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
