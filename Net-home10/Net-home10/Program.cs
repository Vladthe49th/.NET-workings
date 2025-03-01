
class Journal
{
    public string Name { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int Employees { get; set; } 

    public Journal() { }
    public Journal(string name, int year, string description, string phone, string email, int employees)
    {
        Name = name;
        Year = year;
        Description = description;
        Phone = phone;
        Email = email;
        Employees = employees;
    }

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
        Console.Write("Enter number of employees: ");
        Employees = int.Parse(Console.ReadLine());
    }

    public void DisplayData()
    {
        Console.WriteLine($"Journal: {Name}, Year of foundation: {Year}, Description: {Description}, Phone: {Phone}, Email: {Email}, Employees: {Employees}");
    }

    // Operator overload
    public static Journal operator +(Journal j, int number)
    {
        j.Employees += number;
        return j;
    }

    public static Journal operator -(Journal j, int number)
    {
        j.Employees = Math.Max(0, j.Employees - number);
        return j;
    }

    public static bool operator ==(Journal j1, Journal j2)
    {
        return j1.Employees == j2.Employees;
    }

    public static bool operator !=(Journal j1, Journal j2)
    {
        return j1.Employees != j2.Employees;
    }

    public static bool operator <(Journal j1, Journal j2)
    {
        return j1.Employees < j2.Employees;
    }

    public static bool operator >(Journal j1, Journal j2)
    {
        return j1.Employees > j2.Employees;
    }

    public override bool Equals(object obj)
    {
        if (obj is Journal journal)
        {
            return this == journal;
        }
        return false;
    }
}




