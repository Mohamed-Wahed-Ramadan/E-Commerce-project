using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.pressentation.Designs
{

    // Custom rounded textbox
    public class RoundedTextBox : TextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xF || m.Msg == 0x133)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    using (Pen pen = new Pen(this.Focused ? Color.FromArgb(102, 126, 234) : Color.FromArgb(225, 229, 233), 2))
                    {
                        g.DrawRoundedRectangle(pen, 0, 0, Width - 1, Height - 1, 8);
                    }
                }
            }
        }
    }

    // Custom gradient button
    public class GradientButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(102, 126, 234),
                Color.FromArgb(118, 75, 162),
                LinearGradientMode.ForwardDiagonal))
            {
                using (GraphicsPath path = GetRoundedPath(this.ClientRectangle, 8))
                {
                    g.FillPath(brush, path);
                }
            }

            // Draw text
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(this.Text, this.Font, textBrush, this.ClientRectangle, sf);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }

    // Extension method for drawing rounded rectangles
    public static class GraphicsExtensions
    {
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, int x, int y, int width, int height, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(x, y, radius, radius, 180, 90);
                path.AddArc(x + width - radius, y, radius, radius, 270, 90);
                path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
                path.AddArc(x, y + height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                graphics.DrawPath(pen, path);
            }
        }

    }
}
