using System.Drawing;

namespace Shapes
{
    class Triangle : Shape
    {
        public override void Draw(ref Bitmap bmp, int x1, int x2, int x3, int x4)
        {
            int x5 = 50;
            int x6 = 50;
            Graphics graph = Graphics.FromImage(bmp);
            Point[] PointARR = { new Point(x1, x2), new Point(x3, x4), new Point(x5, x6) };
            graph.DrawPolygon(pen, PointARR);
        }
    }
}
