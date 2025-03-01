Console.WriteLine("Welcome to guess the number!");
Console.Write("Enter diapazon minimum: ");
int min = int.Parse(Console.ReadLine());

Console.Write("Enter diapazon maximum: ");
int max = int.Parse(Console.ReadLine());

Console.WriteLine($"Imagine a number from {min} to {max}, and I`ll try to guess it");
Console.WriteLine("Press enter, when you`re ready.");
Console.ReadLine();

int guess;
int attempts = 0;
string response;

do
{
    attempts++;
    guess = (min + max) / 2;
    Console.WriteLine($"My attempt: №{attempts}: {guess}");
    Console.Write("Is this number more (>), less  (<) or equal (=) to yours? ");
    response = Console.ReadLine();

    if (response == ">")
    {
        max = guess - 1;
    }
    else if (response == "<")
    {
        min = guess + 1;
    }
    else if (response != "=")
    {
        Console.WriteLine("Please, type '>', '<' or '='.");
    }

} while (response != "=");

Console.WriteLine($"Hehehe! I guessed the num after {attempts} tries!");