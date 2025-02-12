internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\nTask 1 - array numbers\n");
        int[] array = { 1, 2, 3, 4, 5, 6, 2, 3, 7, 8, 9 };

        int evenCount = array.Count(x => x % 2 == 0);
        int oddCount = array.Count(x => x % 2 != 0);
        int uniqueCount = array.Distinct().Count();

        Console.WriteLine($"Even: {evenCount}");
        Console.WriteLine($"Odd: {oddCount}");
        Console.WriteLine($"Unique: {uniqueCount}");



        /////////////////////////////////////////////
        Console.WriteLine("\nTask 2 - less than...\n");

        int[] array2 = { 1, 5, 8, 3, 10, 7, 2, 6 };

        Console.Write("Enter your value limit: ");
        int threshold = int.Parse(Console.ReadLine());

        int count = array2.Count(x => x < threshold);

        Console.WriteLine($"Elements less than {threshold}: {count}");


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 3 - The posledovatelnost...\n");

        int[] array3 = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };

        Console.Write("Enter three nums: ");
        string[] input = Console.ReadLine().Split();
        int[] sequence = Array.ConvertAll(input, int.Parse);

        int count1 = 0;
        for (int i = 0; i <= array3.Length - 3; i++)
        {
            if (array3[i] == sequence[0] && array3[i + 1] == sequence[1] && array3[i + 2] == sequence[2])
            {
                count++;
            }
        }

        Console.WriteLine($"Number of repetitions in array: {count}");



        /////////////////////////////////////////////
        Console.WriteLine("\nTask 4 - common elements\n");

        int[] array4 = { 1, 2, 3, 4, 5, 6, 7, 8 };
        int[] array5 = { 5, 6, 7, 8, 9, 10, 11 };

        int[] commonElements = array4.Intersect(array5).ToArray();

        Console.WriteLine("Common elements without repeats: " + string.Join(", ", commonElements));


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 5 - min max\n");

        int[,] matrix =
        {
            {3, 7, 1 },
            {9, 2, 8},
            {4, 6, 5 }

        };

        int min = matrix[0, 0]; int max = matrix[0, 0];

        foreach (int value in matrix)
        {
            if (value < min) min = value;
            if (value > max) max = value;

        }

        Console.WriteLine($"Minimum value: {min}");
        Console.WriteLine($"Maximum value: {max}");


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 6 - count words\n");

        Console.WriteLine("Enter a sentence: ");
        string input1 = Console.ReadLine();

        static int Countwords(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? 0
                : text.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        Console.WriteLine("Number of words: " + Countwords(input1));


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 7 - reverse words\n");

        Console.WriteLine("Enter another sentence: ");
        string input2 = Console.ReadLine();

        static string ReverseWords(string text)
        {
            return string.Join(" ", text.Split(' ').Select(word => new string(word.Reverse().ToArray())));
        }

        Console.WriteLine("Reversed words: " + ReverseWords(input2));


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 8 - vowels\n");

        Console.WriteLine("Another one: ");
        string input3 = Console.ReadLine();

        static int CountVowels(string text)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y' };
            return text.Count(c => vowels.Contains(c));
        }

        Console.WriteLine("Vowel count: " + CountVowels(input3));


        /////////////////////////////////////////////
        Console.WriteLine("\nTask 9 - substring\n");


        Console.WriteLine("Write a sentence, I DARE YOU: ");
        string input4 = Console.ReadLine();

        static int CountSubstrings(string text, string substring)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(substring)) return 0;
            int count = 0, index = 0;
            while ((index = text.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                index += substring.Length;
            }
            return count;


        }

        Console.WriteLine("Enter substring to search for:");
        string substring = Console.ReadLine();
        Console.WriteLine("Number of vhojdenias: " + CountSubstrings(input4, substring));


    }
}