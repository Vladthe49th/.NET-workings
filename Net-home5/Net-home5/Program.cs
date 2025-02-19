using System;

class SimpleCalculator
{
    static void Main()
    {
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
