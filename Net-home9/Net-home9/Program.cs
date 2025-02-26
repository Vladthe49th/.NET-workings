Console.WriteLine("Task 1 - num transition calculator");

Console.WriteLine("Choose transition direction:");
Console.WriteLine("1. From decimal to binary");
Console.WriteLine("2. From decimal to octal");
Console.WriteLine("3. From decimal to hexadecimal");
Console.WriteLine("4. From binary to decimal");
Console.WriteLine("5. From octal to decimal");
Console.WriteLine("6. From hexadecimal to decimal");

if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 6)
{
    Console.WriteLine("Wrong choice, now die");
    return;
}

Console.Write("Enter your num: ");
string input = Console.ReadLine()!;

try
{
    switch (choice)
    {
        case 1:
            Console.WriteLine($"Result: {Convert.ToString(int.Parse(input), 2)}");
            break;
        case 2:
            Console.WriteLine($"Result: {Convert.ToString(int.Parse(input), 8)}");
            break;
        case 3:
            Console.WriteLine($"Result: {Convert.ToString(int.Parse(input), 16)}");
            break;
        case 4:
            Console.WriteLine($"Result: {Convert.ToInt32(input, 2)}");
            break;
        case 5:
            Console.WriteLine($"Result: {Convert.ToInt32(input, 8)}");
            break;
        case 6:
            Console.WriteLine($"Result: {Convert.ToInt32(input, 16)}");
            break;
    }
}
catch (FormatException)
{
    Console.WriteLine("Wrong number format!");
}
catch (OverflowException)
{
    Console.WriteLine("Number is out of range!");
}


///////////////////
Console.WriteLine("\nTask 2 - words to nums\n");

var numbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
{
    {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
    {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}
};

Console.Write("Enter a number (zero to nine): ");
string? input1 = Console.ReadLine();

if (input1 != null && numbers.TryGetValue(input1, out int result))
{
    Console.WriteLine($"The digit is: {result}");
}
else
{
    Console.WriteLine("Invalid input. Please enter a valid number word.");
}



///////////////////
Console.WriteLine("\nTask 4 - More or less? \n");

try
{
    Console.Write("Enter logical problem (example 5>3: ");
    string input2 = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(input))
        throw new ArgumentException("Problem can`t be empty!!");


    string[] operators = { "<=", ">=", "==", "!=", "<", ">" };
    string selectedOperator = Array.Find(operators, input2.Contains) ?? throw new ArgumentException("Uncorrect operator!");

    string[] parts = input2.Split(selectedOperator);
    if (parts.Length != 2)
        throw new FormatException("Uncorrect format of thre problem!");

    if (!int.TryParse(parts[0].Trim(), out int leftOperand) || !int.TryParse(parts[1].Trim(), out int rightOperand))
        throw new FormatException("Problem must only have complete numbers!");

    // Вычисляем результат
    bool result1 = selectedOperator switch
    {
        "<" => leftOperand < rightOperand,
        ">" => leftOperand > rightOperand,
        "<=" => leftOperand <= rightOperand,
        ">=" => leftOperand >= rightOperand,
        "==" => leftOperand == rightOperand,
        "!=" => leftOperand != rightOperand,
        _ => throw new InvalidOperationException("Unknown operator!!")
    };

    Console.WriteLine($"Result: {result1}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}




///////////////////
Console.WriteLine("\nTask 3 - Passport\n");



class Passport
{
    private string PassportNumber { get; set; }
    private string FullName { get;  set; }
    private DateTime IssueDate { get;  set; }
    private DateTime ExpiryDate { get; set; }
    private string Nationality { get; set; }

    public Passport(string passportNumber, string fullName, DateTime issueDate, DateTime expiryDate, string nationality)
    {
        if (string.IsNullOrWhiteSpace(passportNumber))
            throw new ArgumentException("Password num can`t be empty!");
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name can`t be empty!");
        if (issueDate > DateTime.Now)
            throw new ArgumentException("Issue date can`t be in the future!");
        if (expiryDate <= issueDate)
            throw new ArgumentException("Expiry date can`t be in the past!");
        if (string.IsNullOrWhiteSpace(nationality))
            throw new ArgumentException("You must have a nation, citizen!");

        PassportNumber = passportNumber;
        FullName = fullName;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        Nationality = nationality;
    }

    public override string ToString()
    {
        return $"Passport: {PassportNumber}\nFull name: {FullName}\nIssue date: {IssueDate:dd.MM.yyyy}\nExpiry date: {ExpiryDate:dd.MM.yyyy}\nNationality: {Nationality}";
    }
}


