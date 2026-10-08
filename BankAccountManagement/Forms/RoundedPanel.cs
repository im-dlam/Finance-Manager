using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    [ToolboxItem(true)]
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 16;
        private int _borderSize = 1;
        private Color _borderColor = Color.FromArgb(226, 232, 240);   
        private Color _fillColor = Color.White;

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color FillColor
        {
            get => _fillColor;
            set { _fillColor = value; this.BackColor = value; Invalidate(); }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = _fillColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (_borderRadius > 0)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = _borderRadius;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(Width - radius - 1, 0, radius, radius, 270, 90);
                    path.AddArc(Width - radius - 1, Height - radius - 1, radius, radius, 0, 90);
                    path.AddArc(0, Height - radius - 1, radius, radius, 90, 90);
                    path.CloseFigure();
                    
                    this.Region = new Region(path);

                    if (_borderSize > 0)
                    {
                        using (Pen pen = new Pen(_borderColor, _borderSize))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
            }
            else
            {
                this.Region = new Region(new Rectangle(0, 0, this.Width, this.Height));
                if (_borderSize > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderSize))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
    }
}
