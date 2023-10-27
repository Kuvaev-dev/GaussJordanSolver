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

                    int rowIdx = i;
                    int colIdx = j;

                    cell.Validating += (sender, e) =>
                    {
                        if (!double.TryParse(cell.Text, out double newValue))
                        {
                            e.Cancel = true;
                            cell.Text = matrix[rowIdx][colIdx].ToString();
                        }
                        else
                        {
                            matrix[rowIdx][colIdx] = newValue;
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

        private void solveButton_Click(object sender, EventArgs e)
        {
            viewModel.SolveMatrix();
            CheckSolutionStatus();
        }

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

        private void clearButton_Click(object sender, EventArgs e)
        {
            viewModel.ClearMatrix();
            matrixTable.Controls.Clear();
            resultRichTextBox.Clear();
        }

        private void CheckSolutionStatus()
        {
            if (viewModel.Matrix == null)
            {
                resultRichTextBox.Text += "Матриця не існує.\n";
                return;
            }

            int rowCount = viewModel.Matrix.Count;
            int colCount = viewModel.Matrix[0].Count;
            int lastColIdx = colCount - 1;

            // Перевірка на безліч розв'язків або відсутність розв'язку
            for (int i = 0; i < rowCount; i++)
            {
                bool isZeroRow = true;
                for (int j = 0; j < colCount - 1; j++)
                {
                    if (viewModel.Matrix[i][j] != 0)
                    {
                        isZeroRow = false;
                        break;
                    }
                }

                if (isZeroRow && viewModel.Matrix[i][lastColIdx] != 0)
                {
                    resultRichTextBox.Text += "Система має безліч розв'язків.\n";
                    return;
                }
            }

            // Перевірка на відсутність розв'язку
            for (int i = 0; i < rowCount; i++)
            {
                if (viewModel.Matrix[i][i] != 1)
                {
                    resultRichTextBox.Text += "Система не має розв'язку.\n";
                    return;
                }
            }

            resultRichTextBox.Text += "Система має єдиний розв'язок.\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
