using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    using Shapes;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            List<Shape> shapes = new List<Shape>()
            { 
            new Line(),
            new Rectangle(),
            new Ellipse(),
            new Triangle(),
            new Circle(),
            new Quadrate()
            };

            int x = 20;
            foreach(Shape i in shapes)
            {

                i.Draw(ref bmp, 100+x, 30+x, 30+x, 30+x);
                x+= 20;
            }
            pictureBox1.Image = bmp;
        }
    }
    
}

namespace Shapes
{
    using WindowsFormsApp1;
    class Shape:Form1
    {
        public Pen pen = new Pen(Color.Green);
        public virtual void Draw(ref Bitmap bmp, int x1, int x2, int x3, int x4) { }
    }
}

             /*line.Draw(ref bmp, 120, 50, 240, 50);
             rectangle.Draw(ref bmp, 80, 120, 80, 40);
             ellipse.Draw(ref bmp, 50, 25, 75, 25);
             triangle.Draw(ref bmp, 200, 100, 250, 150, 100, 100);
             circle.Draw(ref bmp, 55, 55, 55, 55);
             quadrate.Draw(ref bmp, 25, 25, 25, 25);*/
