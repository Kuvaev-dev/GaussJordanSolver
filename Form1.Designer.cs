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
            resultTextBox = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            matrixTable = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // resultTextBox
            // 
            resultTextBox.Location = new Point(367, 167);
            resultTextBox.Name = "resultTextBox";
            resultTextBox.Size = new Size(308, 23);
            resultTextBox.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(367, 47);
            button1.Name = "button1";
            button1.Size = new Size(195, 23);
            button1.TabIndex = 2;
            button1.Text = "Завантажити матрицю";
            button1.UseVisualStyleBackColor = true;
            button1.Click += openFileButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(367, 76);
            button2.Name = "button2";
            button2.Size = new Size(195, 23);
            button2.TabIndex = 3;
            button2.Text = "Розв'язати матрицю";
            button2.UseVisualStyleBackColor = true;
            button2.Click += solveButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(367, 105);
            button3.Name = "button3";
            button3.Size = new Size(195, 23);
            button3.TabIndex = 4;
            button3.Text = "Зберегти результати";
            button3.UseVisualStyleBackColor = true;
            button3.Click += saveResultButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(367, 149);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 6;
            label2.Text = "Результати";
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
            matrixTable.Size = new Size(308, 174);
            matrixTable.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(matrixTable);
            groupBox1.Location = new Point(39, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(322, 198);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Матриця";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(708, 283);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(resultTextBox);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gaussian-Jordan Solver";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox resultTextBox;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label2;
        private TableLayoutPanel matrixTable;
        private GroupBox groupBox1;
    }
}