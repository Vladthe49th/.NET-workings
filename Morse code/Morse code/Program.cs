Console.WriteLine("The great morse code translator");

// Morse code dictionary
var morseCodeDictionary = new Dictionary<char, string>()
{
    {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
    {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
    {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
    {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
    {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
    {'Z', "--.."}, {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
    {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."},
    {'9', "----."}, {' ', "/"} 
};

// Reverse dictionary for morse to text
var reverseMorseCodeDictionary = morseCodeDictionary.ToDictionary(pair => pair.Value, pair => pair.Key);

// Text to morse function
string TextToMorse(string text)
{
    text = text.ToUpper(); // All text shall be in upper register
    var morseCode = new System.Text.StringBuilder();

    foreach (var character in text)
    {
        if (morseCodeDictionary.ContainsKey(character))
        {
            morseCode.Append(morseCodeDictionary[character] + " ");
        }
        else
        {
            morseCode.Append(" "); // If symbol`s not found, we add some space
        }
    }

    return morseCode.ToString().Trim(); // Trim space in the end
}

// Morse to text function
string MorseToText(string morseCode)
{
    var text = new System.Text.StringBuilder();
    var morseWords = morseCode.Split(new[] { " / " }, StringSplitOptions.None); // Split the words

    foreach (var word in morseWords)
    {
        var morseChars = word.Split(' '); // Split the morse chars
        foreach (var morseChar in morseChars)
        {
            if (reverseMorseCodeDictionary.ContainsKey(morseChar))
            {
                text.Append(reverseMorseCodeDictionary[morseChar]);
            }
            else
            {
                text.Append(""); // If morse is not found, we shall skip
            }
        }
        text.Append(' '); //Add some space between words
    }

    return text.ToString().Trim(); 
}


// Translation choice
Console.WriteLine("Choose your translation:");
Console.WriteLine("1 - Text to morse");
Console.WriteLine("2 - Morse to text");
string choice = Console.ReadLine();

if (choice == "1")
{
    Console.WriteLine("Enter text for morse translation:");
    string inputText = Console.ReadLine();
    string morseResult = TextToMorse(inputText);
    Console.WriteLine("Result:");
    Console.WriteLine(morseResult);
}
else if (choice == "2")
{
    Console.WriteLine("Enter morse for text translation:");
    string inputMorse = Console.ReadLine();
    string textResult = MorseToText(inputMorse);
    Console.WriteLine("Result:");
    Console.WriteLine(textResult);
}
else
{
    Console.WriteLine("Wrong choice, you may die now.");
}