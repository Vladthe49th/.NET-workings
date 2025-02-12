using System.Linq;

namespace Net_home2
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Task 1 - quote: ");
            Console.WriteLine("It's easy to win forgiveness for being wrong;");
            Console.WriteLine("being right is what gets you into real trouble.");
            Console.WriteLine("Bjarne Stroustrup");

            ////////////////////////////////
            Console.Write("\n Task 2 - five numbers \n ");
            int[] numbers = new int[5];

            Console.WriteLine("Enter five nums:");
            for (int i = 0; i < 5; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());
            }

            int sum = numbers.Sum();
            int max = numbers.Max();
            int min = numbers.Min();
            int product = numbers.Aggregate(1, (acc, x) => acc * x);

            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Product: {product}");

            ////////////////////////////////
            Console.Write("\n Task 3 - reverse num \n ");

            Console.Write("Enter six-digit num: ");
            string input = Console.ReadLine();

            if (input.Length != 6 || !int.TryParse(input, out _))
            {
                Console.WriteLine("THE DIGIT NUMBER IS SIX!");
                return;
            }

            char[] reversed = input.ToCharArray();
            Array.Reverse(reversed);
            Console.WriteLine("Reversed num: " + new string(reversed));

            ////////////////////////////////
            Console.Write("\n Task 4 - Fibonachi \n ");

            Console.Write("Enter diapazon beginning: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Diapazon end: ");
            int end = int.Parse(Console.ReadLine());

            int a = 0, b = 1;
            Console.Write("Fibonacchi in diapazon: ");

            while (a <= end)
            {
                if (a >= start)
                {
                    Console.Write(a + " ");
                }
                int temp = a + b;
                a = b;
                b = temp;
            }

            ////////////////////////////////
            Console.Write("\n Task 5 - Numbers on numbers \n ");

            Console.Write("Enter A: ");
            int A = int.Parse(Console.ReadLine());

            Console.Write("Enter B: ");
            int B = int.Parse(Console.ReadLine());

            if (A >= B)
            {
                Console.WriteLine("A MUST BE LESSER THAN B!!!");
                return;
            }

            for (int i = A; i <= B; i++)
            {
                Console.WriteLine(string.Join(" ", new string[i].Select(_ => i.ToString())));
            }

            ////////////////////////////////
            Console.Write("\n Task 6 - End with the line \n ");

            Console.Write("Enter line length: ");
            int length = int.Parse(Console.ReadLine());

            Console.Write("Enter symbol to fill the line with: ");
            char fillChar = Console.ReadKey().KeyChar;
            Console.WriteLine();

            Console.Write("Enter direction (h - horizontal, v - vertical): ");
            char direction = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (direction == 'h')
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write(fillChar);
                }
                Console.WriteLine();
            }
            else if (direction == 'v')
            {
                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine(fillChar);
                }
            }
            else
            {
                Console.WriteLine("Wrong!");
            }


        }
    }
}
