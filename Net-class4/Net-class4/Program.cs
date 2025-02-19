using System;
using System.Data;
class Program
{
    static void Main()
    {
        Console.WriteLine("\nTask 1 - the two arrays\n");


        double[] A = new double[5];
        double[,] B = new double[3, 4];
        Random rand = new Random();

        // Array A
        Console.WriteLine("Enter five elements for A:");
        for (int i = 0; i < A.Length; i++)
        {
            A[i] = double.Parse(Console.ReadLine());
        }

        // Filling B with random nums
        for (int i = 0; i < B.GetLength(0); i++)
        {
            for (int j = 0; j < B.GetLength(1); j++)
            {
                B[i, j] = rand.NextDouble() * 100; 
            }
        }

        // Writing down array A
        Console.WriteLine("Array A:");
        foreach (var item in A)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // Writing down B
        Console.WriteLine("Array B:");
        for (int i = 0; i < B.GetLength(0); i++)
        {
            for (int j = 0; j < B.GetLength(1); j++)
            {
                Console.Write(B[i, j].ToString("F2") + "\t");
            }
            Console.WriteLine();
        }

        // Finding min max
        double maxElement = double.MinValue;
        double minElement = double.MaxValue;
        double sum = 0;
        double product = 1;
        double sumEvenA = 0;
        double sumOddColumnsB = 0;

        foreach (var item in A)
        {
            if (item > maxElement) maxElement = item;
            if (item < minElement) minElement = item;
            sum += item;
            product *= item;
            if (item % 2 == 0) sumEvenA += item;
        }

        for (int i = 0; i < B.GetLength(0); i++)
        {
            for (int j = 0; j < B.GetLength(1); j++)
            {
                if (B[i, j] > maxElement) maxElement = B[i, j];
                if (B[i, j] < minElement) minElement = B[i, j];
                sum += B[i, j];
                product *= B[i, j];
                if (j % 2 != 0) sumOddColumnsB += B[i, j];
            }
        }

        // Вывод результатов
        Console.WriteLine($"Max elem: {maxElement}");
        Console.WriteLine($"Min elem: {minElement}");
        Console.WriteLine($"Sum of elems: {sum}");
        Console.WriteLine($"Product of elems: {product}");
        Console.WriteLine($"Sum of even elems: {sumEvenA}");
        Console.WriteLine($"Sum of odd column elems: {sumOddColumnsB}");


        ///////////////

        Console.WriteLine("\nTask 2 - between minimum and maximum\n");

        int[,] matrix = new int[5, 5];
        Random rand1 = new Random();

        // Filling up the array
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                matrix[i, j] = rand1.Next(-100, 101);
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        int min = int.MaxValue, max = int.MinValue;
        int minIndex = -1, maxIndex = -1;
        int linearIndex = 0; // Linear index for linearn traversal

        // Finding min and max
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j] < min)
                {
                    min = matrix[i, j];
                    minIndex = linearIndex;
                }
                if (matrix[i, j] > max)
                {
                    max = matrix[i, j];
                    maxIndex = linearIndex;
                }
                linearIndex++;
            }
        }

        // Let`s check that minIndex < maxIndex
        if (minIndex > maxIndex)
        {
            (minIndex, maxIndex) = (maxIndex, minIndex);
        }

        // Sum
        int sum1 = 0;
        linearIndex = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (linearIndex > minIndex && linearIndex < maxIndex)
                {
                    sum += matrix[i, j];
                }
                linearIndex++;
            }
        }

        Console.WriteLine($"Min elem: {min}, Max elem: {max}");
        Console.WriteLine($"Sum of elems: {sum}");


        ///////////////

        Console.WriteLine("\nTask 3 - Caesar cipre\n");

        // Cipher method
        static string CaesarCipher(string text, int shift)
        {
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];

                if (char.IsLetter(letter)) // Checking if it`s a letter
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a'; // Checking for upper letter
                    buffer[i] = (char)((letter - offset + shift + 26) % 26 + offset);
                }
            }
            return new string(buffer);
        }


        Console.WriteLine("Enter text for ciper:");
        string input = Console.ReadLine();

        Console.Write("Enter the sdvig (for example, 3): ");
        int shift = int.Parse(Console.ReadLine());

        Console.Write("Choose your action (1 - Encrypt, 2 - Decrypt): ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            string encrypted = CaesarCipher(input, shift);
            Console.WriteLine($"Encrypted: {encrypted}");
        }
        else if (choice == 2)
        {
            string decrypted = CaesarCipher(input, -shift);
            Console.WriteLine($"Decrypted: {decrypted}");
        }


        

        
    }
}



