// Arrays for vowels and consonants
char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

// Word generation
string GenerateWord(int vowelCount, int consonantCount, int maxLength)
{
    Random random = new Random();
    string word = "";
    int totalLetters = vowelCount + consonantCount;
    totalLetters = Math.Min(totalLetters, maxLength); // Limit word length

    for (int i = 0; i < totalLetters; i++)
    {
        if (vowelCount > 0 && (consonantCount == 0 || random.Next(2) == 0))
        {
            word += vowels[random.Next(vowels.Length)];
            vowelCount--;
        }
        else if (consonantCount > 0)
        {
            word += consonants[random.Next(consonants.Length)];
            consonantCount--;
        }
    }

    return word;
}

// Text generation
string GeneratePseudoText(int vowelCount, int consonantCount, int maxWordLength, int wordCount)
{
    string text = "";
    for (int i = 0; i < wordCount; i++)
    {
        text += GenerateWord(vowelCount, consonantCount, maxWordLength) + " ";
    }
    return text.Trim(); // Remove the last space
}

// Ask for params:
Console.Write("Enter number of vowels: ");
int vowelsInput = int.Parse(Console.ReadLine());

Console.Write("Enter number of consonants: ");
int consonantsInput = int.Parse(Console.ReadLine());

Console.Write("Enter max word length: ");
int maxLengthInput = int.Parse(Console.ReadLine());

Console.Write("Enter number of words in text: ");
int wordCountInput = int.Parse(Console.ReadLine());

// Генерируем и выводим псевдотекст
string pseudoText = GeneratePseudoText(vowelsInput, consonantsInput, maxLengthInput, wordCountInput);
Console.WriteLine("Generated text:");
Console.WriteLine(pseudoText);