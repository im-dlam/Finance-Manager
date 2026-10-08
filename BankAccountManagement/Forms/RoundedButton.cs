using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    [ToolboxItem(true)]
    public class RoundedButton : Button
    {
        private int _borderRadius = 8;
        private int _borderSize = 0;
        private Color _normalColor = Color.FromArgb(37, 99, 235);
        private Color _hoverColor = Color.FromArgb(59, 130, 246);
        private Color _pressedColor = Color.FromArgb(29, 78, 216);
        private Color _normalBorder = Color.Transparent;
        private Color _hoverBorder = Color.Transparent;
        private Color _pressedBorder = Color.Transparent;
        private Color _textColor = Color.White;

        private bool isHovered = false;
        private bool isPressed = false;

        [Category("Appearance")]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = value; Invalidate(); } }
        
        [Category("Appearance")]
        public int BorderSize { get => _borderSize; set { _borderSize = value; Invalidate(); } }

        [Category("Appearance")]
        public Color NormalColor { get => _normalColor; set { _normalColor = value; Invalidate(); } }
        
        [Category("Appearance")]
        public Color HoverColor { get => _hoverColor; set { _hoverColor = value; Invalidate(); } }
        
        [Category("Appearance")]
        public Color PressedColor { get => _pressedColor; set { _pressedColor = value; Invalidate(); } }

        [Category("Appearance")]
        public Color NormalBorder { get => _normalBorder; set { _normalBorder = value; Invalidate(); } }
        
        [Category("Appearance")]
        public Color HoverBorder { get => _hoverBorder; set { _hoverBorder = value; Invalidate(); } }
        
        [Category("Appearance")]
        public Color PressedBorder { get => _pressedBorder; set { _pressedBorder = value; Invalidate(); } }

        [Category("Appearance")]
        public Color TextColor { get => _textColor; set { _textColor = value; this.ForeColor = value; Invalidate(); } }

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = NormalColor;
            this.ForeColor = TextColor;
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { isHovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { isHovered = false; isPressed = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs mevent) { isPressed = true; Invalidate(); base.OnMouseDown(mevent); }
        protected override void OnMouseUp(MouseEventArgs mevent) { isPressed = false; Invalidate(); base.OnMouseUp(mevent); }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color currentColor = isPressed ? PressedColor : (isHovered ? HoverColor : NormalColor);
            Color currentBorder = isPressed ? PressedBorder : (isHovered ? HoverBorder : NormalBorder);

            using (GraphicsPath path = new GraphicsPath())
            {
                int r = _borderRadius > 0 ? _borderRadius : 1;
                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(Width - r - 1, 0, r, r, 270, 90);
                path.AddArc(Width - r - 1, Height - r - 1, r, r, 0, 90);
                path.AddArc(0, Height - r - 1, r, r, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(currentColor))
                {
                    pevent.Graphics.FillPath(brush, path);
                }

                if (_borderSize > 0)
                {
                    using (Pen pen = new Pen(currentBorder, _borderSize))
                    {
                        pevent.Graphics.DrawPath(pen, path);
                    }
                }
            }

            // Draw text manually
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.TextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
