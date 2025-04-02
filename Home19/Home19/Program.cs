using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Home19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Mark bought 3 apples for 10$. Mike has 25.50$. Start 12345 End. @john, @mike are here. Цены начинаются от 20 грн и заканчиваются 150 грн.";

            // 1 - four symbol words

            var words4 = Regex.Matches(text, "\\b\\w{4}\\b").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n 1) Four symbol words: " + string.Join(", ", words4));

            // 2- M words

            var Mwords = Regex.Matches(text, "\\bM\\w*").Cast<Match>().Select(m =>m.Value);
            Console.WriteLine("\n 2) Words that begin with M: " + string.Join(", ", Mwords));

            // 3- Words longer than 4
            var WordsLongerThan4 = Regex.Matches(text, "\\b\\w{5,}\\b").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n3) Words longer than 4 : " + string.Join(", ", WordsLongerThan4));


            // 4- Separate words
            Console.WriteLine("\n4) Words by strings:");
            foreach (var word in Regex.Matches(text, "\\b\\w+\\b").Cast<Match>().Select(m => m.Value))
                Console.WriteLine(word);

            // 5- All nums from text
            var digits = Regex.Matches(text, "\\d+").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n5) All numbers: " + string.Join(", ", digits));

            // 6- Check a string for being num
            Console.WriteLine("\n6) Enter a string: ");
            string input = Console.ReadLine();
            Console.WriteLine(double.TryParse(input, out _) ? "Number" : "Not number");

            // 7- Place Ok after all os
            Console.WriteLine("\n7) Text with 'Ок': " + text.Replace("о", "оОк"));

            // 8- Delete all nums
            Console.WriteLine("\n8) No nums: " + Regex.Replace(text, "\\d", ""));

            // 9- Check if text has nums
            Console.WriteLine("\n9) Has numbers? : " + Regex.IsMatch(text, "\\d"));

            // 10- Check if a string is only letters or digits
            Console.WriteLine("\n10) Enter a string: ");
            string input2 = Console.ReadLine();
            Console.WriteLine(Regex.IsMatch(input2, "^[a-zA-Z]+$") ? " Only letters" : Regex.IsMatch(input2, "^\\d+$") ? "Only digits" : "Mixed");

            // 11- Replace point sequences with one point
            Console.WriteLine("\n11) Changed points: " + Regex.Replace(text, "\\.+", "."));

            // 12- Check is password is reliable
            Console.WriteLine("\n12) Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine(Regex.IsMatch(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$") ? "Reliable" : "Weak");

            // 13- Delete symbol from text
            Console.WriteLine("\n13) Enter a symbol to delete: ");
            char ch = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine("\nText without the symbol: " + Regex.Replace(text, "[" + ch + char.ToUpper(ch) + "]", ""));

            // 14- Extract floats
            var floats = Regex.Matches(text, "\\d+\\.\\d+").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n14) Floars: " + string.Join(", ", floats));

            // 15- Replace "рыба" with "123" 
            Console.WriteLine("\n15) " + Regex.Replace(text, "\\bрыба\\b", "123"));

            // 16- Count words in text
            Console.WriteLine("\n16) Number of words: " + Regex.Matches(text, "\\b\\w+\\b").Count);

            // 17- Extract nums between start and end
            var BetweenNums = Regex.Match(text, "Start (\\d+) End").Groups[1].Value;
            Console.WriteLine("\n17) Nums between 'Start' and 'End': " + BetweenNums);

            // 18- Find usernames
            var usernames = Regex.Matches(text, "@\\w+").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n18) Usernames: " + string.Join(", ", usernames));

            // 19- Find prices
            var prices = Regex.Matches(text, "\\b\\d+(?:\\.\\d+)? ?(?:грн|\\$)\\b").Cast<Match>().Select(m => m.Value);
            Console.WriteLine("\n19) Prices: " + string.Join(", ", prices));

            // 20- Login check
            Console.WriteLine("\n20) Enter login: ");
            string login = Console.ReadLine();
            Console.WriteLine(Regex.IsMatch(login, "^[a-zA-Z][a-zA-Z0-9]{1,9}$") ? "Correct login" : "Uncorrect");

        }
    }
}
