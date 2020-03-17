using System.Drawing;

namespace Shapes
{
    class Line : Shape
    {
        public override void Draw(ref Bitmap bmp, int x1, int x2,int x3, int x4)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(pen, x1, x2, x3, x4);
        }
    
    }
}
