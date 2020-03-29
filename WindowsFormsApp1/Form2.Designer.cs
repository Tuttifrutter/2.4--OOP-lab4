namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Shape = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PenColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Filling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LibFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawFont = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelet = new System.Windows.Forms.Button();
            this.PlugPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Shape,
            this.PenWidth,
            this.PenColor,
            this.ARR,
            this.Filling,
            this.LibFilePath,
            this.DrawFont,
            this.DrawStr,
            this.PlugPath});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(914, 312);
            this.dataGridView1.TabIndex = 0;
            // 
            // Shape
            // 
            this.Shape.HeaderText = "Shape";
            this.Shape.Name = "Shape";
            // 
            // PenWidth
            // 
            this.PenWidth.HeaderText = "PenWidth";
            this.PenWidth.Name = "PenWidth";
            // 
            // PenColor
            // 
            this.PenColor.HeaderText = "PenColor";
            this.PenColor.Name = "PenColor";
            // 
            // ARR
            // 
            this.ARR.HeaderText = "ARR";
            this.ARR.Name = "ARR";
            // 
            // Filling
            // 
            this.Filling.HeaderText = "Filling";
            this.Filling.Name = "Filling";
            // 
            // LibFilePath
            // 
            this.LibFilePath.HeaderText = "LibFilePath";
            this.LibFilePath.Name = "LibFilePath";
            // 
            // DrawFont
            // 
            this.DrawFont.HeaderText = "DrawFont";
            this.DrawFont.Name = "DrawFont";
            // 
            // DrawStr
            // 
            this.DrawStr.HeaderText = "DrawStr";
            this.DrawStr.Name = "DrawStr";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(65, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelet
            // 
            this.btnDelet.Location = new System.Drawing.Point(433, 333);
            this.btnDelet.Name = "btnDelet";
            this.btnDelet.Size = new System.Drawing.Size(80, 23);
            this.btnDelet.TabIndex = 2;
            this.btnDelet.Text = "Удалить";
            this.btnDelet.UseVisualStyleBackColor = true;
            this.btnDelet.Click += new System.EventHandler(this.BtnDelet_Click);
            // 
            // PlugPath
            // 
            this.PlugPath.HeaderText = "PlugPath";
            this.PlugPath.Name = "PlugPath";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 368);
            this.Controls.Add(this.btnDelet);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shape;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn PenColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filling;
        private System.Windows.Forms.DataGridViewTextBoxColumn LibFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawFont;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlugPath;
    }
}