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

            int rowCount = matrix.Count;
            int colCount = rowCount > 0 ? matrix[0].Count : 0;

            table.RowCount = rowCount;
            table.ColumnCount = colCount;

            for (int i = 0; i < rowCount; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Додати авто-розмір для кожного рядка
                for (int j = 0; j < colCount; j++)
                {
                    TextBox cell = new()
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
                Filter = "Текстові файли (*.txt)|*.txt",
                Title = "Зберегти матрицю та розв'язання",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                SaveSolutionToFile(filePath);
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

        private void SaveSolutionToFile(string filePath)
        {
            try
            {
                using StreamWriter writer = new(filePath);
                writer.WriteLine("Оригінальна матриця:");

                if (viewModel.OriginalMatrix != null)
                {
                    foreach (var row in viewModel.OriginalMatrix)
                    {
                        writer.WriteLine(string.Join(" ", row.Select(d => d.ToString("G17"))));
                    }
                }

                writer.WriteLine("Розв'язання:");

                if (!string.IsNullOrEmpty(viewModel.ResultText))
                {
                    writer.WriteLine(viewModel.ResultText);
                }

                resultRichTextBox.Text = "Матрицю та розв'язок успішно збережено у файл.";
            }
            catch (Exception ex)
            {
                resultRichTextBox.Text = "Помилка при збереженні файлу: " + ex.Message + "\n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
