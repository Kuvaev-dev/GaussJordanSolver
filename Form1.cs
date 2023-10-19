using System.ComponentModel;

namespace GaussJordanSolver
{
    public partial class Form1 : Form
    {
        private readonly MatrixViewModel viewModel;

        public Form1()
        {
            InitializeComponent();
            viewModel = new MatrixViewModel();
            BindViewModelToUI();
        }

        private void BindViewModelToUI()
        {
            FillMatrixTable(matrixTable, viewModel.Matrix);
            resultRichTextBox.DataBindings.Add("Text", viewModel, "ResultText", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private static void FillMatrixTable(TableLayoutPanel table, BindingList<BindingList<double>> matrix)
        {
            table.Controls.Clear();

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    TextBox? cell = new()
                    {
                        Text = matrix[i][j].ToString(),
                        Width = 50,
                        TextAlign = HorizontalAlignment.Center,
                        Name = $"cell_{i}_{j}"
                    };

                    cell.Validating += (sender, e) =>
                    {
                        if (!double.TryParse(cell.Text, out double newValue))
                        {
                            e.Cancel = true;
                            cell.Text = matrix[i][j].ToString();
                        }
                        else
                        {
                            matrix[i][j] = newValue;
                        }
                    };

                    table.Controls.Add(cell, j, i);
                }
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog? openFileDialog = new()
            {
                Filter = "Текстові файли (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                viewModel.LoadMatrixFromFile(filePath);
                FillMatrixTable(matrixTable, viewModel.Matrix);
            }
        }

        private void solveButton_Click(object sender, EventArgs e) => viewModel.SolveMatrix();

        private void saveResultButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Текстові файли (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                viewModel.SaveResultToFile(filePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
