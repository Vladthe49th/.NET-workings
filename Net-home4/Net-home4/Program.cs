using System;

namespace Net_home4
{
    internal class Program
    {


        static void Main(string[] args)
        {
            // Matrix creation
            static int[,] CreateMatrix(int rows, int cols)
            {
                Random rand = new Random();
                int[,] matrix = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = rand.Next(1, 10); // Num generation
                    }
                }
                return matrix;
            }

            // Print
            static void PrintMatrix(int[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write(matrix[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }

            // Multiply matrix on number
            static int[,] MultiplyByNumber(int[,] matrix, int number)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                int[,] result = new int[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = matrix[i, j] * number;
                    }
                }
                return result;
            }

            // Add matrices
            static int[,] AddMatrices(int[,] matrix1, int[,] matrix2)
            {
                int rows = matrix1.GetLength(0);
                int cols = matrix1.GetLength(1);
                int[,] result = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return result;
            }

            // Multiply matrices
            static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
            {
                int rows1 = matrix1.GetLength(0);
                int cols1 = matrix1.GetLength(1);
                int rows2 = matrix2.GetLength(0);
                int cols2 = matrix2.GetLength(1);

                if (cols1 != rows2)
                {
                    throw new Exception("Нельзя перемножить матрицы: число столбцов первой не равно числу строк второй.");
                }

                int[,] result = new int[rows1, cols2];

                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < cols2; j++)
                    {
                        for (int k = 0; k < cols1; k++)
                        {
                            result[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
                return result;
            }

            Console.Write("Введите количество строк матрицы: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Введите количество столбцов матрицы: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] matrix1 = CreateMatrix(rows, cols);
            Console.WriteLine("Матрица 1:");
            PrintMatrix(matrix1);

            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1 - Умножение на число");
            Console.WriteLine("2 - Сложение двух матриц");
            Console.WriteLine("3 - Произведение двух матриц");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Введите число для умножения: ");
                int num = int.Parse(Console.ReadLine());
                int[,] result = MultiplyByNumber(matrix1, num);
                Console.WriteLine("Результат умножения:");
                PrintMatrix(result);
            }
            else if (choice == 2)
            {
                int[,] matrix2 = CreateMatrix(rows, cols);
                Console.WriteLine("Матрица 2:");
                PrintMatrix(matrix2);

                int[,] result = AddMatrices(matrix1, matrix2);
                Console.WriteLine("Результат сложения:");
                PrintMatrix(result);
            }
            else if (choice == 3)
            {
                Console.Write("Введите количество строк второй матрицы: ");
                int rows2 = int.Parse(Console.ReadLine());
                Console.Write("Введите количество столбцов второй матрицы: ");
                int cols2 = int.Parse(Console.ReadLine());

                int[,] matrix2 = CreateMatrix(rows2, cols2);
                Console.WriteLine("Матрица 2:");
                PrintMatrix(matrix2);

                try
                {
                    int[,] result = MultiplyMatrices(matrix1, matrix2);
                    Console.WriteLine("Результат умножения:");
                    PrintMatrix(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Некорректный выбор.");
            }


            Console.WriteLine("Введите арифметическое выражение (+ и -):");
            string input = Console.ReadLine();

            try
            {
                // Убираем пробелы
                input = input.Replace(" ", "");

                // Проверяем корректность ввода
                if (!IsValidExpression(input))
                {
                    Console.WriteLine("Ошибка: некорректное выражение.");
                    return;
                }

                // Вычисляем результат
                int result = EvaluateExpression(input);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Проверка корректности выражения (разрешены только цифры, + и -)
        static bool IsValidExpression(string expression)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(expression, @"^-?\d+([\+\-]\d+)*$");
        }

        // Метод вычисления выражения
        static int EvaluateExpression(string expression)
        {
            int result = 0;
            int currentNumber = 0;
            char lastOperator = '+'; // Начинаем с + для первого числа
            string numberBuffer = "";

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (char.IsDigit(c))
                {
                    numberBuffer += c; // Собираем число в строке
                }

                if (c == '+' || c == '-' || i == expression.Length - 1)
                {
                    if (i == expression.Length - 1 && char.IsDigit(c)) // Последнее число
                    {
                        numberBuffer += c;
                    }

                    if (!string.IsNullOrEmpty(numberBuffer)) // Конвертируем число
                    {
                        currentNumber = int.Parse(numberBuffer);
                        numberBuffer = "";
                    }

                    if (lastOperator == '+')
                        result += currentNumber;
                    else
                        result -= currentNumber;

                    lastOperator = c;
                }
            }
            return result;

        }
    }


}



    

