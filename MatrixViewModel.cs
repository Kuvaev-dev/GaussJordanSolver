using System.ComponentModel;

namespace GaussJordanSolver
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        private List<List<double>>? matrix; // Матриця, яка використовується для обчислень
        private string? resultText; // Рядок для відображення результатів на формі

        // Властивість для відображення матриці на формі
        public BindingList<BindingList<double>> Matrix { get; } = new BindingList<BindingList<double>>();

        // Властивість для відображення результатів на формі
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

        // Метод для завантаження матриці з текстового файлу
        public void LoadMatrixFromFile(string filePath)
        {
            try
            {
                List<List<double>>? newMatrix = new(); // Створюємо нову матрицю

                string[]? lines = File.ReadAllLines(filePath); // Зчитуємо рядки з файлу

                foreach (var line in lines)
                {
                    List<double>? row = new(); // Створюємо новий рядок матриці
                    string[]? values = line.Split(' '); // Розділяємо рядок на значення

                    foreach (var value in values)
                    {
                        if (double.TryParse(value, out double parsedValue)) // Спроба перетворення рядкового значення в число
                        {
                            row.Add(parsedValue); // Додаємо число до рядка
                        }
                        else
                        {
                            ResultText = "Невірний формат даних у файлі."; // Повідомлення про помилку
                            return;
                        }
                    }

                    newMatrix.Add(row); // Додаємо рядок до матриці
                }

                matrix = newMatrix; // Зберігаємо нову матрицю
                UpdateMatrixBinding(); // Оновлюємо відображення матриці на формі
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при зчитуванні файлу: " + ex.Message; // Повідомлення про помилку
            }
        }

        // Метод для розв'язання системи лінійних рівнянь методом Гаусса-Жордана
        public void SolveMatrix()
        {
            if (matrix == null || matrix.Count == 0)
            {
                ResultText = "Матриця не існує або її розмірність нуль.";
                return;
            }

            int? rowCount = matrix.Count; // Кількість рядків матриці
            int colCount = matrix[0].Count; // Кількість стовпців матриці

            // Проходимося по матриці для знаходження розв'язку
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

            // Нормалізація рядків матриці
            for (int i = 0; i < rowCount; i++)
            {
                double divisor = matrix[i][i];
                for (int j = i; j < colCount; j++)
                {
                    matrix[i][j] /= divisor;
                }
            }

            ResultText = "Розв'язок системи:\n";

            // Формування рядку з розв'язками
            for (int i = 0; i < rowCount; i++)
            {
                ResultText += "X" + (i + 1) + " = " + matrix[i][colCount - 1] + "\n";
            }
        }

        // Метод для збереження результату в текстовий файл
        public void SaveResultToFile(string filePath)
        {
            try
            {
                using StreamWriter? writer = new(filePath);

                // Запис матриці в файл
                foreach (var row in matrix)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
            catch (Exception ex)
            {
                ResultText = "Помилка при збереженні результату: " + ex.Message; // Повідомлення про помилку
            }
        }

        // Метод для оновлення відображення матриці на формі
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

        // Метод для сповіщення про зміни властивостей
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
