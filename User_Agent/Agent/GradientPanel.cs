using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class GradientPanel : Panel
{
    public Color TopColor { get; set; } = Color.LightSkyBlue;
    public Color BottomColor { get; set; } = Color.MidnightBlue;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (LinearGradientBrush brush = new LinearGradientBrush(
            this.ClientRectangle,
            TopColor,
            BottomColor,
            90F)) // Vertical gradient
        {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
