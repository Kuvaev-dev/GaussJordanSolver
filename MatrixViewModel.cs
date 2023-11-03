using System.ComponentModel;

namespace GaussJordanSolver
{
    /// <summary>
    /// ViewModel для розв'язку системи лінійних рівнянь за допомогою методу Гауса-Жордана.
    /// </summary>
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private List<List<double>>? matrix;
        private List<List<double>>? originalMatrix;
        private string? resultText;

        /// <summary>
        /// Основна матриця, представлена у вигляді BindingList для взаємодії з UI.
        /// </summary>
        public BindingList<BindingList<double>> Matrix { get; } = new BindingList<BindingList<double>>();

        /// <summary>
        /// Рядок для відображення результатів або повідомлень про помилки.
        /// </summary>
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

        /// <summary>
        /// Поле для запису оригінальної матриці до файлу.
        /// </summary>
        public List<List<double>>? OriginalMatrix { get => originalMatrix; }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Завантаження матриці з файлу за вказаним шляхом.
        /// </summary>
        public void LoadMatrixFromFile(string filePath)
        {
            try
            {
                List<List<double>>? newMatrix = new List<List<double>>();
                List<List<double>>? newOriginalMatrix = new List<List<double>>();

                string[]? lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    List<double>? row = new List<double>();
                    List<double>? originalRow = new List<double>();

                    string[]? values = line.Split(' ');

                    foreach (var value in values)
                    {
                        if (double.TryParse(value, out double parsedValue))
                        {
                            row.Add(parsedValue);
                            originalRow.Add(parsedValue);
                        }
                        else
                        {
                            ResultText = "Невірний формат даних у файлі.\n";
                            return;
                        }
                    }

                    newMatrix.Add(row);
                    newOriginalMatrix.Add(originalRow);
                }

                matrix = newMatrix;
                originalMatrix = newOriginalMatrix;
                UpdateMatrixBinding();
                ResultText = string.Empty;
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при зчитуванні файлу: " + ex.Message + "\n";
            }
        }

        /// <summary>
        /// Розв'язання системи лінійних рівнянь за допомогою методу Гауса-Жордана.
        /// </summary>
        public void SolveMatrix()
        {
            if (matrix == null || matrix.Count == 0)
            {
                ResultText = "Матриця не існує або її розмірність нуль.\n";
                return;
            }

            int? rowCount = matrix.Count;
            int colCount = matrix[0].Count;

            for (int row = 0; row < rowCount; row++)
            {
                if (matrix[row][row] == 0)
                {
                    ResultText = "Матриця має нульовий головний елемент, розв'язок неможливий.\n";
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

        /// <summary>
        /// Збереження результату у файл за вказаним шляхом.
        /// </summary>
        public void SaveResultToFile(string filePath)
        {
            try
            {
                using StreamWriter? writer = new StreamWriter(filePath);

                foreach (var row in originalMatrix)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при збереженні результату: " + ex.Message + "\n";
            }
        }

        /// <summary>
        /// Очищення матриці та результату.
        /// </summary>
        public void ClearMatrix()
        {
            matrix = null;
            originalMatrix = null;
            UpdateMatrixBinding();
            ResultText = string.Empty;
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
