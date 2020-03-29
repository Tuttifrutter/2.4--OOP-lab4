using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Shapes;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.TopMost = true;
        }
       
        private void BtnDelet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1) 
            {
                int a = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.Remove(dataGridView1.Rows[a]);
            }
            
        }
        List<Draw> ShapeList; 

        public List<Draw> GetCorrectShapeList
        {
            get { return ShapeList; }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            ShapeList = new List<Draw>();
            var cvt = new FontConverter();
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                int[] arr;
                arr = new int[4];
                string Num ="";
                int k = 0;
                for (int j = 0; j <= (dataGridView1.Rows[i].Cells[3].Value.ToString()).Length - 1; j++)
                {
                    if ((dataGridView1.Rows[i].Cells[3].Value.ToString())[j] != ',')
                    {
                        Num += (dataGridView1.Rows[i].Cells[3].Value.ToString())[j];
                    }
                    else
                    {
                        arr[k] = Int32.Parse(Num);
                        k++;
                        Num = "";
                    }
                }
                string typeStr= Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                Draw shape;
                if (typeStr.Contains("Library")) 
                {
                    Assembly asm = Assembly.LoadFrom(dataGridView1.Rows[i].Cells[8].Value.ToString());
                    shape = (Draw)Activator.CreateInstance(asm.GetType(typeStr));
                }
                else
                {
                    shape = (Draw)Activator.CreateInstance(Type.GetType(typeStr));
                }
                
                    shape.Name = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    shape.PenWidth = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    shape.PenColor = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    shape.arr = arr;
                    shape.filling = Convert.ToBoolean(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    shape.LibFilePath = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    shape.DrawFont = cvt.ConvertFromString(dataGridView1.Rows[i].Cells[6].Value.ToString()) as Font;
                    shape.DrawStr = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    shape.PlugPath = dataGridView1.Rows[i].Cells[8].Value.ToString();

                ShapeList.Add(shape);
            }
            this.Close();
        }

    }
}
