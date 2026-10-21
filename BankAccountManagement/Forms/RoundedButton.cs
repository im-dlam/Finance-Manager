using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Custom Button bo góc mềm mại (RoundedButton) với hỗ trợ hiệu ứng Hover/Pressed,
    /// khử răng cưa GDI+ AntiAlias và tối ưu bộ đệm kép chống nháy hình (Flicker-free).
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Text")]
    public class RoundedButton : Button
    {
        #region KHAI BÁO THUỘC TÍNH (PROPERTIES)

        private int _borderRadius = 8;
        private int _borderSize = 0;
        private Color _normalColor = Color.FromArgb(37, 99, 235);
        private Color _hoverColor = Color.FromArgb(59, 130, 246);
        private Color _pressedColor = Color.FromArgb(29, 78, 216);
        private Color _normalBorder = Color.Transparent;
        private Color _hoverBorder = Color.Transparent;
        private Color _pressedBorder = Color.Transparent;
        private Color _textColor = Color.White;

        private bool _isHovered;
        private bool _isPressed;

        private int _lastWidth = -1;
        private int _lastHeight = -1;

        /// <summary>Bán kính bo cong góc của nút (mặc định: 8px).</summary>
        [Category("Appearance"), DefaultValue(8)]
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

        /// <summary>Độ dày của đường viền (mặc định: 0px - không viền).</summary>
        [Category("Appearance"), DefaultValue(0)]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = Math.Max(0, value); Invalidate(); }
        }

        [Category("Appearance")] public Color NormalColor { get => _normalColor; set { _normalColor = value; Invalidate(); } }
        [Category("Appearance")] public Color HoverColor { get => _hoverColor; set { _hoverColor = value; Invalidate(); } }
        [Category("Appearance")] public Color PressedColor { get => _pressedColor; set { _pressedColor = value; Invalidate(); } }
        [Category("Appearance")] public Color NormalBorder { get => _normalBorder; set { _normalBorder = value; Invalidate(); } }
        [Category("Appearance")] public Color HoverBorder { get => _hoverBorder; set { _hoverBorder = value; Invalidate(); } }
        [Category("Appearance")] public Color PressedBorder { get => _pressedBorder; set { _pressedBorder = value; Invalidate(); } }

        /// <summary>Màu chữ của nút bấm.</summary>
        [Category("Appearance")]
        public Color TextColor
        {
            get => _textColor;
            set { _textColor = value; ForeColor = value; Invalidate(); }
        }

        #endregion

        #region KHỞI TẠO NÚT (CONSTRUCTOR)

        public RoundedButton()
        {
            // Bật các cờ điều khiển giao diện: vẽ mượt mà, khử nhấp nháy
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;
            Cursor = Cursors.Hand;
        }

        #endregion

        #region VÙNG HIỂN THỊ (REGION & GEOMETRY)

        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); UpdateRegion(); }
        protected override void OnSizeChanged(EventArgs e) { base.OnSizeChanged(e); UpdateRegion(); }

        /// <summary>
        /// Cập nhật vùng cắt xén (Region) của control khi kích thước thay đổi.
        /// </summary>
        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            // Nếu kích thước không đổi thì bỏ qua để tiết kiệm CPU
            if (_lastWidth == Width && _lastHeight == Height) return;
            _lastWidth = Width;
            _lastHeight = Height;

            if (_borderRadius <= 0) { Region = null; return; }

            using (var path = GetRoundedPath(new RectangleF(0, 0, Width, Height), _borderRadius))
            {
                var old = Region;
                Region = new Region(path);
                old?.Dispose();
            }
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN CHUỘT (MOUSE EVENTS)

        protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHovered = false; _isPressed = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) _isPressed = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _isPressed = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        #endregion

        #region VẼ GIAO DIỆN TÙY BIẾN (ONPAINT)

        /// <summary>Chặn WinForms tự ý vẽ nền trắng mặc định để chống nháy hình.</summary>
        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color fill, border, text;
            if (!Enabled)
            {
                fill = Color.FromArgb(226, 232, 240);
                border = Color.Transparent;
                text = Color.FromArgb(148, 163, 184);
            }
            else if (_isPressed) { fill = _pressedColor; border = _pressedBorder; text = _textColor; }
            else if (_isHovered) { fill = _hoverColor; border = _hoverBorder; text = _textColor; }
            else { fill = _normalColor; border = _normalBorder; text = _textColor; }

            if (_borderRadius > 0)
            {
                using (var path = GetRoundedPath(new RectangleF(0, 0, Width, Height), _borderRadius))
                {
                    if (fill.A > 0)
                    {
                        using (var brush = new SolidBrush(fill))
                            g.FillPath(brush, path);
                    }

                    if (_borderSize > 0 && border.A > 0)
                    {
                        using (var pathBorder = GetRoundedPath(
                            new RectangleF(_borderSize / 2f, _borderSize / 2f,
                                           Width - _borderSize, Height - _borderSize),
                            _borderRadius))
                        using (var pen = new Pen(border, _borderSize))
                        {
                            g.DrawPath(pen, pathBorder);
                        }
                    }
                }
            }
            else
            {
                if (fill.A > 0)
                {
                    using (var brush = new SolidBrush(fill))
                        g.FillRectangle(brush, 0, 0, Width, Height);
                }

                if (_borderSize > 0 && border.A > 0)
                {
                    using (var pen = new Pen(border, _borderSize))
                        g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }

            // Vẽ nhãn chữ căn giữa nút
            TextRenderer.DrawText(g, Text, Font,
                new Rectangle(0, 0, Width, Height), text,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0 || rect.Width <= 0 || rect.Height <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            float d = Math.Min(radius * 2f, Math.Min(rect.Width, rect.Height));
            if (d < 1f) d = 1f;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) Region?.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
