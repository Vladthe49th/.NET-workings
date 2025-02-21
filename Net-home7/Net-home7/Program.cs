//////////////

Console.WriteLine("Task 1 - range product");

int RangeProduct(int start, int end)
{
    int product = 1;
    for (int i = start; i <= end; i++)
    {
        product *= i;
    }
    return product;
}

//////////////

Console.WriteLine("Task 2 - is Fibonacchi");

bool IsFibonacci(int num)
{
    int a = 0, b = 1;
    while (b < num)
    {
        int temp = a + b;
        a = b;
        b = temp;
    }
    return b == num;
}

//////////////

Console.WriteLine("Task 3 - Array sorting");

void SortArray(int[] arr, bool ascending)
{
    for (int i = 0; i < arr.Length - 1; i++)
    {
        for (int j = i + 1; j < arr.Length; j++)
        {
            if ((ascending && arr[i] > arr[j]) || (!ascending && arr[i] < arr[j]))
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }
}

//////////////

Console.WriteLine("Task 4 - Sin city");

class City
{
    private string Name { get; set; }
    private string Country { get; set; }
    private int Population { get; set; }
    private string Phonecode { get; set; }
    private List<string> Districts { get; set; } = new List<string>();

    public void InputData()
    {
        Console.Write("Enter the city`s  name: ");
        Name = Console.ReadLine();
        Console.WriteLine("What country is it set in?");
        Country = Console.ReadLine();
        Console.WriteLine("What`s the number of it`s population?");
        Population = int.Parse(Console.ReadLine());
        Console.WriteLine("What`s the phone code?");
        Phonecode = Console.ReadLine();
        Console.WriteLine("Enter districts through comas: ");
        Districts = Console.ReadLine().Split(',').ToList();

    }

    public void DisplayData()
    {
        Console.WriteLine($"City: {Name}, Country:{Country}, Population: {Population}, Phone code: {Phonecode}");
        Console.WriteLine("Districts: " + string.Join(",", Districts));
    }

}

// Task 5 - Employee
class Employee
{
    private string FullName { get; set; }
    private DateTime BirthDate { get; set; }
    private string PhoneNumber { get; set; }
    private string WorkEmail { get; set; }
    private string Position { get; set; }
    private string Responsibilities { get; set; }

    public void InputData()
    {
        Console.Write("Enter full name: ");
        FullName = Console.ReadLine();
        Console.Write("Enter birth date (yyyy-mm-dd) : ");
        BirthDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter phone number: ");
        PhoneNumber = Console.ReadLine();
        Console.Write("Enter work email: ");
        WorkEmail = Console.ReadLine();
        Console.Write("Enter position: ");
        Position = Console.ReadLine();
        Console.Write("Enter responsibilities: ");
        Responsibilities = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine($"Employee: {FullName}, Birth date: {BirthDate.ToShortDateString()}, Phone: {PhoneNumber}, Email: {WorkEmail}, Position: {Position}");
        Console.WriteLine($"Responsibilities: {Responsibilities}");
    }
}

// Task 6 - Airplane 

class Airplane
{
    private string name;
    private string manufacturer;
    private int year;
    private string type;

    public Airplane() { }
    public Airplane(string name, string manufacturer, int year, string type)
    {
        this.name = name;
        this.manufacturer = manufacturer;
        this.year = year;
        this.type = type;
    }

    public void SetData(string name, string manufacturer, int year, string type)
    {
        this.name = name;
        this.manufacturer = manufacturer;
        this.year = year;
        this.type = type;
    }

    public void DisplayData()
    {
        Console.WriteLine($"Name: {name}, Manufacturer: {manufacturer}, Year: {year}, Type: {type}");
    }
}


// Task 7 - Matrix

class Matrix
{
    private int[,] data;

    public Matrix(int rows, int cols)
    {
        data = new int[rows, cols];
    }

    public void FillMatrix(int[,] values)
    {
        data = values;
    }

    public void DisplayMatrix()
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
                Console.Write(data[i, j] + " ");
            Console.WriteLine();
        }
    }

    public int GetMax() => data.Cast<int>().Max();
    public int GetMin() => data.Cast<int>().Min();
}

