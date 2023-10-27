namespace GaussJordanSolver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            matrixTable = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            resultRichTextBox = new RichTextBox();
            groupBox2 = new GroupBox();
            button4 = new Button();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(6, 19);
            button1.Name = "button1";
            button1.Size = new Size(195, 23);
            button1.TabIndex = 2;
            button1.Text = "Завантажити матрицю";
            button1.UseVisualStyleBackColor = true;
            button1.Click += openFileButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(6, 48);
            button2.Name = "button2";
            button2.Size = new Size(195, 23);
            button2.TabIndex = 3;
            button2.Text = "Розв'язати матрицю";
            button2.UseVisualStyleBackColor = true;
            button2.Click += solveButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(6, 77);
            button3.Name = "button3";
            button3.Size = new Size(195, 23);
            button3.TabIndex = 4;
            button3.Text = "Зберегти результати";
            button3.UseVisualStyleBackColor = true;
            button3.Click += saveResultButton_Click;
            // 
            // matrixTable
            // 
            matrixTable.AutoScroll = true;
            matrixTable.AutoSize = true;
            matrixTable.BackColor = Color.White;
            matrixTable.ColumnCount = 4;
            matrixTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            matrixTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            matrixTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            matrixTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            matrixTable.Location = new Point(6, 19);
            matrixTable.Margin = new Padding(3, 3, 3, 30);
            matrixTable.Name = "matrixTable";
            matrixTable.RowCount = 1;
            matrixTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            matrixTable.Size = new Size(308, 275);
            matrixTable.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(matrixTable);
            groupBox1.Location = new Point(39, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(322, 303);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Матриця";
            // 
            // resultRichTextBox
            // 
            resultRichTextBox.Location = new Point(6, 21);
            resultRichTextBox.Name = "resultRichTextBox";
            resultRichTextBox.Size = new Size(293, 120);
            resultRichTextBox.TabIndex = 9;
            resultRichTextBox.Text = "";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(button2);
            groupBox2.Location = new Point(367, 39);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(305, 139);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Операції";
            // 
            // button4
            // 
            button4.Location = new Point(6, 106);
            button4.Name = "button4";
            button4.Size = new Size(195, 23);
            button4.TabIndex = 5;
            button4.Text = "Очистити результати";
            button4.UseVisualStyleBackColor = true;
            button4.Click += clearButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(resultRichTextBox);
            groupBox3.Location = new Point(367, 192);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(305, 150);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Лог";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(708, 372);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gaussian-Jordan Solver";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private TableLayoutPanel matrixTable;
        private GroupBox groupBox1;
        private RichTextBox resultRichTextBox;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button button4;
    }
}