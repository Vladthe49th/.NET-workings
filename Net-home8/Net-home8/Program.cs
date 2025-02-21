// Task 1 - draw me a square

static void DrawSquare(int size, char symbol)
{
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
            Console.Write(symbol + " ");
        Console.WriteLine();
    }
}

// Task 2 - is palindrome

static bool IsPalindrome(int number)
{
    string str = number.ToString();
    return str.SequenceEqual(str.Reverse());
}


// Task 3 - Array filtering 

int[] FilterArray(int[] original, int[] filter)
{
    return original.Where(x => !filter.Contains(x)).ToArray();
}

//Testing 
int[] original = { 1, 2, 6, -1, 88, 7, 6 };
int[] filter = { 6, 88, 7 };
Console.WriteLine(string.Join(", ", FilterArray(original, filter)));


// Task 4 - Website

class Website
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public string IpAddress { get; set; }

    public void InputData()
    {
        Console.Write("Enter site name: ");
        Name = Console.ReadLine();
        Console.Write("Enter url: ");
        Url = Console.ReadLine();
        Console.Write("Enter description: ");
        Description = Console.ReadLine();
        Console.Write("Enter your site`s IP adress: ");
        IpAddress = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Site: {Name}, URL: {Url}, Description: {Description}, IP: {IpAddress}");
    }
}

//Task 5 - Journal


class Journal
{
    public string Name { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public void InputData()
    {
        Console.Write("Enter journal name: ");
        Name = Console.ReadLine();
        Console.Write("Enter year of foundation: ");
        Year = int.Parse(Console.ReadLine());
        Console.Write("Enter description: ");
        Description = Console.ReadLine();
        Console.Write("Enter contact phone: ");
        Phone = Console.ReadLine();
        Console.Write("Enter contact email: ");
        Email = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Journal: {Name}, Year of foundation: {Year}, Description: {Description}, Phone: {Phone}, Email: {Email}");
    }
}



// Task 6 - Store


class Store
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Profile { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public void InputData()
    {
        Console.Write("Enter store name: ");
        Name = Console.ReadLine();
        Console.Write("Enter store address: ");
        Address = Console.ReadLine();
        Console.Write("Enter store profile: ");
        Profile = Console.ReadLine();
        Console.Write("Enter phone: ");
        Phone = Console.ReadLine();
        Console.Write("Email: ");
        Email = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Store: {Name}, Address: {Address}, Profile: {Profile}, Phone: {Phone}, Email: {Email}");
    }
}