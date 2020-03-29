using System;
using System.Drawing;
using System.Windows.Forms;
using Shapes;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button12.Enabled = false;
            button13.Enabled = false;
            textBox2.Enabled = false;
            button12.Visible = false;
            button13.Visible = false;
            textBox2.Visible = false;
        }

        List<Draw> ShapeList = new List<Draw>();
        static readonly int PictureBoxWidth = 634;
        static readonly int PictureBoxHeight = 355;
        Bitmap bmp = new Bitmap(PictureBoxWidth, PictureBoxHeight);

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Вы уверены, что хотите удалить все объекты?",
            "Сообщение",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result  == DialogResult.OK)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                ShapeList.Clear();
                pictureBox1.Image = bmp;
            }
            this.TopMost = true;
        }

        private void CircleBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.CircleDraw";
        }

        private void LineBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.LineDraw";
        }

        private void QuadrateBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.QuadrateDraw";
        }

        private void TriangleBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.TriangleDraw";
        }

        private void EllipseBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.EllipseDraw";
        }

        private void RectangleBtn_Click(object sender, EventArgs e)
        {
            Tag = "Shapes.RectangleDraw";
        }
        
        int  iMouseX, iMouseY;
        int PenWidth = 1;
        string PenColor = "Black";
        private void ColorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog
            {
                AllowFullOpen = true,
                ShowHelp = true
            };
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                PenColor = MyDialog.Color.Name;
                button7.BackColor = MyDialog.Color;
            }
        }

        Assembly asm;
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int iMouseX2 = e.Location.X;
            int iMouseY2 = e.Location.Y;
            if (e.Button == MouseButtons.Left)
            {
                int[] arr1 = { iMouseX, iMouseY, iMouseX2 - iMouseX, iMouseY2 - iMouseY };
                if (Tag == null)
                    Tag = "Shapes.CircleDraw";
                Draw shape;
                if (((string)Tag).Contains("Library"))
                {
                    shape = (Draw)Activator.CreateInstance(asm.GetType((string)Tag, true, true));
                }
                else
                {
                    shape = (Draw)Activator.CreateInstance(Type.GetType((string)Tag));
                }
                shape.PenWidth = PenWidth;
                shape.PenColor = PenColor;
                shape.arr = arr1;
                shape.filling = fillflag;
                shape.LibFilePath = LibFilePath;
                shape.PlugPath = PluginPath;
                shape.DrawFont = font;
                shape.DrawStr = PlugStr;
                shape.Name = (string)Tag;
                shape.DrawShape(ref bmp);
                ShapeList.Add(shape);
                pictureBox1.Image = bmp;
            }
        }

        public void SaveShapes(IFormatter formatter, string filePath, List<Draw> objlist)
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, objlist);
            }
        }

        public List<Draw> LoadShapes(IFormatter formatter, string filePath)
        {
             using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
             {
                    return (List<Draw>)formatter.Deserialize(stream);
             }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog FBD = new SaveFileDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                SaveShapes(new BinaryFormatter(), FBD.FileName, ShapeList);
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog() 
            {
            Filter = "Text(*.TXT)|*.TXT|All files (*.*)|*.*"
            };
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                ShapeList = LoadShapes(new BinaryFormatter(), FBD.FileName);
                foreach(Draw i in ShapeList)
                {
                    i.DrawShape(ref bmp);
                }
                pictureBox1.Image = bmp;
            }
        }

        public bool fillflag = false;
        private void FillCB_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                fillflag = true;
            }
            else
            {
                fillflag = false;
            }
        }

        private void BackArrowBtn_Click(object sender, EventArgs e)
        {
            if (ShapeList.Count > 0)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                ShapeList.RemoveAt(ShapeList.Count - 1);
                foreach (Draw i in ShapeList)
                {
                    i.DrawShape(ref bmp);
                }
                pictureBox1.Image = bmp;
            }
        }
        private List<string> GetLibClassList(string LibPath)
        {
            asm = Assembly.LoadFrom(LibPath);
            string LibName = Path.GetFileNameWithoutExtension(LibPath);
            Type t = asm.GetType(LibName + ".Main", true, true);
            Object obj = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("ReturnList");
            Object PlugList = method.Invoke(obj, new object[] { });
            return (List<string>)PlugList;
        }

        private void PluginBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog() 
            {
                Filter = "Dynamic Linking Library(*.DLL)|*.DLL|All files (*.*)|*.*"
            };
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                button12.Enabled = true;
                button13.Enabled = true;
                textBox2.Enabled = true;
                button12.Visible = true;
                button13.Visible = true;
                textBox2.Visible = true;

                List<string> PlugList = GetLibClassList(FBD.FileName);
                PluginPath = FBD.FileName;
                int listSize = ((List<string>)PlugList).Count;
                int top = 30;
                for (int i=0; i<= ((List<string>)PlugList).Count -1; i++)
                {
                    Button button = new Button()
                    {
                        Left = 716,
                        Top = top,
                        Name = PlugList[i],
                        Width = 80,
                        Height = 40,
                        Text = PlugList[i].ToUpper(),
                        UseVisualStyleBackColor = true
                    };

                    button.Click += ButtonOnClick;
                    this.Controls.Add(button);
                    top += button.Height + 3;
                }
            }
        }
        public string PluginPath="", LibFilePath="";

        public Font font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        private void FontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog.Font;
            }
        }

        public string PlugStr="";
        private void StringDrawTB_TextChanged(object sender, EventArgs e)
        {
            PlugStr = textBox2.Text;
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog()
            {
                Filter = "Image Files(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
            };
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                LibFilePath = FBD.FileName;
            }
        }

        private void BtnCorrect_Click(object sender, EventArgs e)
        {
            var cvt = new FontConverter();
            Form2 f = new Form2();
            for (int i = 0; i < ShapeList.Count; i++)
                {
                     String ArrStr = "";
                     for (int j = 0; j <= ShapeList[i].arr.Length-1; j++)
                     {
                         ArrStr= ArrStr + ShapeList[i].arr[j].ToString() + ",";
                     }
                    
                string FontDraw = cvt.ConvertToString(ShapeList[i].DrawFont);
                f.dataGridView1.Rows.Add(ShapeList[i].Name, ShapeList[i].PenWidth,
                                             ShapeList[i].PenColor, ArrStr,
                                             ShapeList[i].filling, ShapeList[i].LibFilePath,
                                             FontDraw, ShapeList[i].DrawStr, ShapeList[i].PlugPath);
                }
            f.ShowDialog();
            if (f.GetCorrectShapeList != null)
            {
                ShapeList = f.GetCorrectShapeList;
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                foreach (Draw i in ShapeList)
                {
                    i.DrawShape(ref bmp);
                }
                pictureBox1.Image = bmp;
            }

        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
                Tag = Path.GetFileNameWithoutExtension(PluginPath) + "." + button.Name;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            bool b =Int32.TryParse(textBox1.Text, out int x);
            if (b==true && x>=1 && x<=100)
            {
                PenWidth = x;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            iMouseX = e.Location.X;
            iMouseY = e.Location.Y;
        }
    }
}


