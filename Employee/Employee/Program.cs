class Employee
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string WorkEmail { get; set; }
    public string Position { get; set; }
    public string Responsibilities { get; set; }
    public decimal Salary { get; set; }

    public void InputData()
    {
        Console.Write("Enter full name: ");
        FullName = Console.ReadLine();
        Console.Write("Enter birth date (yyyy-mm-dd): ");
        BirthDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter phone number: ");
        PhoneNumber = Console.ReadLine();
        Console.Write("Enter work email: ");
        WorkEmail = Console.ReadLine();
        Console.Write("Enter position: ");
        Position = Console.ReadLine();
        Console.Write("Enter responsibilities: ");
        Responsibilities = Console.ReadLine();
        Console.Write("Enter salary: ");
        Salary = decimal.Parse(Console.ReadLine());
    }

    public void DisplayData()
    {
        Console.WriteLine($"Employee: {FullName}, Birth date: {BirthDate.ToShortDateString()}, Phone: {PhoneNumber}, Email: {WorkEmail}, Position: {Position}");
        Console.WriteLine($"Responsibilities: {Responsibilities}");
        Console.WriteLine($"Salary: {Salary:C}");
    }

    public static Employee operator +(Employee employee, decimal amount)
    {
        employee.Salary += amount;
        return employee;
    }

    public static Employee operator -(Employee employee, decimal amount)
    {
        employee.Salary -= amount;
        return employee;
    }

    public static bool operator ==(Employee emp1, Employee emp2)
    {
        return emp1?.Salary == emp2?.Salary;
    }

    public static bool operator !=(Employee emp1, Employee emp2)
    {
        return !(emp1 == emp2);
    }

    public static bool operator >(Employee emp1, Employee emp2)
    {
        return emp1?.Salary > emp2?.Salary;
    }

    public static bool operator <(Employee emp1, Employee emp2)
    {
        return emp1?.Salary < emp2?.Salary;
    }

    public override bool Equals(object obj)
    {
        if (obj is Employee employee)
        {
            return this == employee;
        }
        return false;
    }
}

