namespace Net_home1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1: ");
            Console.Write("Введите число от 1 до 100: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number < 1 || number > 100)
                {
                    Console.WriteLine("Ошибка: число должно быть в диапазоне от 1 до 100.");
                }
                else if (number % 3 == 0 && number % 5 == 0)
                {
                    Console.WriteLine("Fizz Buzz");
                }
                else if (number % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (number % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(number);
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректное число.");
            }
        }
    }
}

