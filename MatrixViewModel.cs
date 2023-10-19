using System.ComponentModel;

namespace GaussJordanSolver
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private List<List<double>>? matrix;
        private string? resultText;

        public BindingList<BindingList<double>> Matrix { get; } = new BindingList<BindingList<double>>();
        public string ResultText
        {
            get => resultText;
            set
            {
                if (resultText != value)
                {
                    resultText = value;
                    OnPropertyChanged(nameof(ResultText));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void LoadMatrixFromFile(string filePath)
        {
            try
            {
                List<List<double>>? newMatrix = new();
                string[]? lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    List<double>? row = new List<double>();
                    string[]? values = line.Split(' ');

                    foreach (var value in values)
                    {
                        if (double.TryParse(value, out double parsedValue))
                        {
                            row.Add(parsedValue);
                        }
                        else
                        {
                            ResultText = "Невірний формат даних у файлі.";
                            return;
                        }
                    }

                    newMatrix.Add(row);
                }

                matrix = newMatrix;
                UpdateMatrixBinding();
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при зчитуванні файлу: " + ex.Message;
            }
        }

        public void SolveMatrix()
        {
            if (matrix == null || matrix.Count == 0)
            {
                ResultText = "Матриця не існує або її розмірність нуль.";
                return;
            }

            int? rowCount = matrix.Count;
            int colCount = matrix[0].Count;

            for (int row = 0; row < rowCount; row++)
            {
                if (matrix[row][row] == 0)
                {
                    ResultText = "Матриця має нульовий головний елемент, розв'язок неможливий.";
                    return;
                }

                for (int i = 0; i < rowCount; i++)
                {
                    if (i != row)
                    {
                        double factor = matrix[i][row] / matrix[row][row];
                        for (int j = row; j < colCount; j++)
                        {
                            matrix[i][j] -= factor * matrix[row][j];
                        }
                    }
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                double divisor = matrix[i][i];
                for (int j = i; j < colCount; j++)
                {
                    matrix[i][j] /= divisor;
                }
            }

            ResultText = "Розв'язок системи:\n";

            for (int i = 0; i < rowCount; i++)
            {
                ResultText += "X" + (i + 1) + " = " + matrix[i][colCount - 1] + "\n";
            }
        }

        public void SaveResultToFile(string filePath)
        {
            try
            {
                using StreamWriter? writer = new(filePath);
                foreach (var row in matrix)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при збереженні результату: " + ex.Message;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateMatrixBinding()
        {
            Matrix.Clear();

            if (matrix != null)
            {
                foreach (var row in matrix)
                {
                    Matrix.Add(new BindingList<double>(row));
                }
            }
        }
    }
}
