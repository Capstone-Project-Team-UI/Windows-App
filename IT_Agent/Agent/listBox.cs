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
        GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, BorderRadius);

        // Set control region to clip corners
        this.Region = new Region(path);

        using (Pen pen = new Pen(Color.Gray, 2))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(new SolidBrush(this.BackColor), path);
            e.Graphics.DrawPath(pen, path);
        }

        for (int i = 0; i < this.Items.Count; i++)
        {
            Rectangle itemRect = this.GetItemRectangle(i);
            bool selected = (this.SelectedIndex == i);
            e.Graphics.FillRectangle(selected ? Brushes.LightBlue : Brushes.White, itemRect);
            TextRenderer.DrawText(e.Graphics, this.Items[i].ToString(), this.Font, itemRect, this.ForeColor);
        }
    }


    private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}
