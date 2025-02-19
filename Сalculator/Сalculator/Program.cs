using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите выражение (например, 10 + 5):");
        string input = Console.ReadLine();

        // Разделяем ввод на части по пробелам
        string[] parts = input.Split(' ');

        if (parts.Length == 3)
        {
            // Пытаемся преобразовать части в числа
            if (int.TryParse(parts[0], out int num1) && int.TryParse(parts[2], out int num2))
            {
                string operation = parts[1];

                int result = 0;
                switch (operation)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    default:
                        Console.WriteLine("Неподдерживаемая операция. Используйте + или -.");
                        return;
                }

                Console.WriteLine("Результат: " + result);
            }
            else
            {
                Console.WriteLine("Ошибка: введены некорректные числа.");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введите выражение в формате 'число операция число' (например, 10 + 5).");
        }
    }
}