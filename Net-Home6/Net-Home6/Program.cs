using System.Text;
using System.Text.RegularExpressions;

namespace Net_Home6
{
    internal class Program
    {

        static string CapitalizeSentences(string text)
        {
            StringBuilder result = new StringBuilder();
            bool newSentence = true;

            foreach (char c in text)
            {
                if (newSentence && char.IsLetter(c))
                {
                    result.Append(char.ToUpper(c)); // Сapitalize
                    newSentence = false;
                }
                else
                {
                    result.Append(c);
                }

                // Flags for uppet letter
                if (c == '.' || c == '!' || c == '?')
                {
                    newSentence = true;
                }
            }

            return result.ToString();
        }
        static void Main(string[] args)
        {

            ///////////////////////////
            Console.WriteLine("\nTask 6\n");

            Console.WriteLine("Enter text:");
            string input = Console.ReadLine();

            string correctedText = CapitalizeSentences(input);
            Console.WriteLine("\nCorrected text:");
            Console.WriteLine(correctedText);


            ///////////////////////////
            Console.WriteLine("\nTask 7\n");

            string text = "To be, or not to be, that is the question,\n"
                    + "Whether 'tis nobler in the mind to suffer\n"
                    + "The slings and arrows of outrageous fortune,\n"
                    + "Or to take arms against a sea of troubles,\n"
                    + "And by opposing end them? To die: to sleep;\n"
                    + "No more; and by a sleep to say we end\n"
                    + "The heart-ache and the thousand natural shocks\n"
                    + "That flesh is heir to, 'tis a consummation\n"
                    + "Devoutly to be wish'd. To die, to sleep";

            string forbiddenWord = "die";
            string pattern = $"\\b{Regex.Escape(forbiddenWord)}\\b";
            string replacement = new string('*', forbiddenWord.Length);

            int count = 0;
            string result = Regex.Replace(text, pattern, match => { count++; return replacement; }, RegexOptions.IgnoreCase);

            Console.WriteLine("Redacted text:");
            Console.WriteLine(result);
            Console.WriteLine($"Stats: {count} replacements of the word '{forbiddenWord}'");
        }

       
    }
}
