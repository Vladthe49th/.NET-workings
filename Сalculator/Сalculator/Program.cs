using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter operation:");
        string input = Console.ReadLine();

        // Separate input
        string[] parts = input.Split(' ');

        if (parts.Length == 3)
        {
            // Transform parts into digits
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
                        Console.WriteLine("Invalid operation");
                        return;
                }

                Console.WriteLine("Result: " + result);
            }
            else
            {
                Console.WriteLine("Uncorrect numbers.");
            }
        }
       
    }
}