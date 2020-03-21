﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Shapes;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PenColor = "Black";
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
        
        int  iMouseX, iMouseY,PenWidth;
        string PenColor;
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
                    Assembly asm = Assembly.LoadFrom(PluginPath);
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
                shape.LibFilePath = LibFuncPath;
                shape.DrawShape(ref bmp);
                ShapeList.Add(shape);
                pictureBox1.Image = bmp;
            }
        }

        public void SaveShapes(string filePath, List<Draw> objlist)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, objlist);
            }
        }

        public List<Draw> LoadShapes(string filePath)
        {
             IFormatter formatter = new BinaryFormatter();
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
                SaveShapes(FBD.FileName, ShapeList);
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                ShapeList = LoadShapes(FBD.FileName);
                foreach(Draw i in ShapeList)
                {
                    i.DrawShape(ref bmp);
                }
                pictureBox1.Image = bmp;
            }
        }
        public bool fillflag;
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

        private void PluginBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog() 
            {
                Filter = "Dynamic Linking Library(*.DLL)|*.DLL|All files (*.*)|*.*"
            };
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                
                int top = 30;
                Button button = new Button()
                {
                    Left = 716,
                    Top = top,
                    Name = FBD.FileName,
                    Width = 40,
                    Height = 40,
                    UseVisualStyleBackColor = true
                };
                PluginPath = FBD.FileName;
                button.Click += ButtonOnClick;
                   this.Controls.Add(button);
               //     top += button.Height + 3;

            }
        }
        public string PluginPath, LibFuncPath;
        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            if (button != null)
            {
                Tag = "ShapesClassLibrary.ImgDraw";
                OpenFileDialog FBD = new OpenFileDialog() 
                {
                    Filter= "Image Files(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
                };
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    LibFuncPath = FBD.FileName;
                }
            }
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


