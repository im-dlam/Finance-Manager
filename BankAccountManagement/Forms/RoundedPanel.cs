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
        private bool _isCircle = false;

        private int _lastWidth = -1;
        private int _lastHeight = -1;
        private int _lastRadius = -1;

        [Category("Appearance")]
        public bool IsCircle
        {
            get => _isCircle;
            set { if (_isCircle == value) return; _isCircle = value; UpdateRegion(); Invalidate(); }
        }

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                int v = Math.Max(0, value);
                if (_borderRadius == v) return;
                _borderRadius = v;
                UpdateRegion();
                Invalidate();
            }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = Math.Max(0, value); Invalidate(); }
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
            set { _fillColor = value; base.BackColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                if (value != Color.Transparent)
                {
                    _fillColor = value;
                }
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            this.BackColor = _fillColor;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            if (_lastWidth == Width && _lastHeight == Height && _lastRadius == _borderRadius) return;
            _lastWidth = Width;
            _lastHeight = Height;
            _lastRadius = _borderRadius;

            bool isThumb = (this.Name != null && this.Name.ToLower().Contains("thumb"));
            bool isCircle = _isCircle || isThumb || (Width == Height && _borderRadius > 0 && _borderRadius >= Width / 2);

            if ((isCircle || _borderRadius > 0) && Width > 0 && Height > 0)
            {
                using (GraphicsPath path = GetPath(isCircle))
                {
                    var old = this.Region;
                    this.Region = new Region(path);
                    old?.Dispose();
                }
            }
            else
            {
                var old = this.Region;
                this.Region = null;
                old?.Dispose();
            }
        }

        private GraphicsPath GetPath(bool isCircle)
        {
            GraphicsPath path = new GraphicsPath();
            if (isCircle || (Width == Height && _borderRadius >= Width / 2))
            {
                path.AddEllipse(0, 0, Width - 1, Height - 1);
            }
            else
            {
                int d = Math.Min(_borderRadius * 2, Math.Min(Width, Height));
                if (d < 1) d = 1;
                path.AddArc(0, 0, d, d, 180, 90);
                path.AddArc(Width - d - 1, 0, d, d, 270, 90);
                path.AddArc(Width - d - 1, Height - d - 1, d, d, 0, 90);
                path.AddArc(0, Height - d - 1, d, d, 90, 90);
                path.CloseFigure();
            }
            return path;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Ngăn chặn base.OnPaintBackground để triệt tiêu hoàn toàn nháy trắng khi vẽ lại
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            bool isThumb = (this.Name != null && this.Name.ToLower().Contains("thumb"));
            bool isCircle = _isCircle || isThumb || (Width == Height && _borderRadius > 0 && _borderRadius >= Width / 2);

            if ((isCircle || _borderRadius > 0) && Width > 0 && Height > 0)
            {
                using (GraphicsPath path = GetPath(isCircle))
                {
                    using (SolidBrush brush = new SolidBrush(_fillColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                    if (_borderSize > 0 && _borderColor.A > 0)
                    {
                        using (Pen pen = new Pen(_borderColor, _borderSize))
                        {
                            try { pen.Alignment = PenAlignment.Inset; } catch { }
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(_fillColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
                }
                if (_borderSize > 0 && _borderColor.A > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderSize))
                    {
                        try { pen.Alignment = PenAlignment.Inset; } catch { }
                        e.Graphics.DrawRectangle(pen, 0, 0, Math.Max(0, this.Width - 1), Math.Max(0, this.Height - 1));
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Region?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
