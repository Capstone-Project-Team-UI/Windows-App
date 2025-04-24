using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedListBox : ListBox
{
    public int BorderRadius { get; set; } = 20;

    public RoundedListBox()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        this.DrawMode = DrawMode.OwnerDrawFixed;
        this.ItemHeight = 24;
        this.BackColor = Color.White;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, BorderRadius);

        // Offset path for shadow
        Rectangle shadowRect = new Rectangle(this.ClientRectangle.X + 5, this.ClientRectangle.Y + 5,
                                             this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        GraphicsPath shadowPath = GetRoundedRectPath(shadowRect, BorderRadius);

        // Draw drop shadow first
        using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
        {
            shadowBrush.CenterColor = Color.FromArgb(80, Color.Black);
            shadowBrush.SurroundColors = new Color[] { Color.Transparent };
            e.Graphics.FillPath(shadowBrush, shadowPath);
        }

        // Set clipping region to rounded shape
        this.Region = new Region(path);

        // Draw the main background
        using (SolidBrush bgBrush = new SolidBrush(this.BackColor))
        {
            e.Graphics.FillPath(bgBrush, path);
        }

        // Optional: Draw border
        using (Pen pen = new Pen(Color.Gray, 2))
        {
            e.Graphics.DrawPath(pen, path);
        }

        // Draw items
        for (int i = 0; i < this.Items.Count; i++)
        {
            Rectangle itemRect = this.GetItemRectangle(i);
            bool selected = (this.SelectedIndex == i);
            e.Graphics.FillRectangle(selected ? Brushes.LightBlue : Brushes.White, itemRect);
            TextRenderer.DrawText(
                e.Graphics,
                this.Items[i].ToString(),
                this.Font,
                itemRect,
                this.ForeColor,
                TextFormatFlags.EndEllipsis | TextFormatFlags.Left
                  );

        }
    }



    private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
        int diameter = radius * 2;
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }

}
