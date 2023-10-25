using System.ComponentModel;

namespace GaussJordanSolver
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private List<List<double>>? matrix; // Матриця, яка використовується для обчислень
        private List<List<double>>? originalMatrix; // Вихідна матриця без змін
        private string? resultText; // Рядок для відображення результатів на формі

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
                List<List<double>>? newMatrix = new List<List<double>>(); // Створюємо нову матрицю для обчислень
                List<List<double>>? newOriginalMatrix = new List<List<double>>(); // Створюємо вихідну матрицю без змін

                string[]? lines = File.ReadAllLines(filePath); // Зчитуємо рядки з файлу

                foreach (var line in lines)
                {
                    List<double>? row = new List<double>(); // Створюємо новий рядок матриці для обчислень
                    List<double>? originalRow = new List<double>(); // Створюємо вихідний рядок матриці

                    string[]? values = line.Split(' '); // Розділяємо рядок на значення

                    foreach (var value in values)
                    {
                        if (double.TryParse(value, out double parsedValue)) // Спроба перетворення рядкового значення в число
                        {
                            row.Add(parsedValue); // Додаємо число до рядка матриці для обчислень
                            originalRow.Add(parsedValue); // Додаємо число до вихідного рядка матриці
                        }
                        else
                        {
                            ResultText = "Невірний формат даних у файлі."; // Повідомлення про помилку
                            return;
                        }
                    }

                    newMatrix.Add(row); // Додаємо рядок до матриці для обчислень
                    newOriginalMatrix.Add(originalRow); // Додаємо рядок до вихідної матриці
                }

                matrix = newMatrix; // Зберігаємо нову матрицю для обчислень
                originalMatrix = newOriginalMatrix; // Зберігаємо вихідну матрицю без змін
                UpdateMatrixBinding(); // Оновлюємо відображення матриці на формі
                ResultText = string.Empty; // Очищаємо рядок результатів
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при зчитуванні файлу: " + ex.Message; // Повідомлення про помилку
            }
        }

        public void SolveMatrix()
        {
            if (matrix == null || matrix.Count == 0)
            {
                ResultText = "Матриця не існує або її розмірність нуль."; // Повідомлення про помилку
                return;
            }

            int? rowCount = matrix.Count; // Кількість рядків матриці
            int colCount = matrix[0].Count; // Кількість стовпців матриці

            for (int row = 0; row < rowCount; row++)
            {
                if (matrix[row][row] == 0)
                {
                    ResultText = "Матриця має нульовий головний елемент, розв'язок неможливий."; // Повідомлення про помилку
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
                using StreamWriter? writer = new StreamWriter(filePath); // Відкриваємо файл для запису

                // Запис вихідної матриці в файл
                foreach (var row in originalMatrix)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при збереженні результату: " + ex.Message; // Повідомлення про помилку
            }
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
