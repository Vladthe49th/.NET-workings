namespace Net_home1
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Task 1: ");
            Console.Write("Enter num: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number < 1 || number > 100)
                {
                    Console.WriteLine("I SAID 1 TO 100!.");
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

            ////////////////////////////////////////////////////////
            Console.WriteLine("\n Task 2 - percentage calculation ");
            Console.WriteLine("Enter num: ");
            double num = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter percentage: ");
            double percent = double.Parse(Console.ReadLine());
            double result = (num * percent) / 100;
            Console.WriteLine($"{percent}% from {num} = {result}");

            //////////////////////////////////////////////
            Console.WriteLine("\n Task 3 - concatenation");
            Console.WriteLine("Enter four digits: ");
            string digits = Console.ReadLine().Replace(" ", "");
            Console.WriteLine($"Formed number: {digits}");

            //////////////////////////////////////////////
            Console.WriteLine("\n Task 4 - swap digits ");
            Console.Write("Enter six digits number: ");
            string number1 = Console.ReadLine();
            if (number1.Length != 6 || !int.TryParse(number1, out _))
            {
                Console.WriteLine("SIX DIGITS!@!!");
                return;
            }

            Console.Write("Enter first digit (1-6): ");
            int firstIndex = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Enter second digit (1-6): ");
            int secondIndex = int.Parse(Console.ReadLine()) - 1;

            char[] digits1 = number1.ToCharArray();
            (digits1[firstIndex], digits1[secondIndex]) = (digits1[secondIndex], digits1[firstIndex]);

            Console.WriteLine($"Result: {new string(digits1)}");

            //////////////////////////////////////////////
            Console.WriteLine("\n Task 5 - swap digits ");

            Console.Write("Enter date (dd.MM.yyyy): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            string season = date.Month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Unknown"
            };
            Console.WriteLine($"{season} {date.DayOfWeek}");

            //////////////////////////////////////////////
            Console.WriteLine("\n Task 6 - temperature ");

            Console.Write("Enter temp: ");
            double temp = double.Parse(Console.ReadLine());
            Console.Write("Choose your convertion (1 - F to C, 2 - C to F): ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                double celsius = (temp - 32) * 5 / 9;
                Console.WriteLine($"{temp}°F = {celsius:F2}°C");
            }
            else if (choice == 2)
            {
                double fahrenheit = (temp * 9 / 5) + 32;
                Console.WriteLine($"{temp}°C = {fahrenheit:F2}°F");
            }
            else
            {
                Console.WriteLine("Wrong choice. You may die now.");
            }

            //////////////////////////////////////////////
            Console.WriteLine("\n Task 7 - diapazon ");

            Console.Write("Enter first num: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Введите second num: ");
            int num2 = int.Parse(Console.ReadLine());

            if (num1 > num2)
            {
                int temporal = num1;
                num1 = num2;
                num2 = temporal;
            }

            Console.WriteLine("Even numbers in diapazon:");
            for (int i = num1; i <= num2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(i + " ");
                }
            }

        }


    }
}

