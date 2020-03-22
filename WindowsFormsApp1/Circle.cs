using System.Drawing;
using System; 
using System.Windows.Forms;

namespace Shapes
{
    [Serializable]
    class CircleDraw : Draw
    {
        public override void DrawShape(ref Bitmap bmp)
        {
            Pen pen = new Pen(GetPenColor())
            {
                Width = PenWidth
            };
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawEllipse(pen, arr[0], arr[1], (arr[2] + arr[3]) / 2, (arr[2] + arr[3]) / 2);
            if (filling == true)
            {
                SolidBrush solidBrush = new SolidBrush(GetPenColor());
                graph.FillEllipse(solidBrush, arr[0], arr[1], (arr[2] + arr[3]) / 2, (arr[2] + arr[3]) / 2);
            }
        }
    }
}
