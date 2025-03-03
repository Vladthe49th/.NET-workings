class City
{

    public string Name { get; set; }
    public string Country { get; set; }
    public int Population { get; set; }
    public string Phonecode { get; set; }
    public List<string> Districts { get; set; } = new List<string>();


    public void InputData()
    {
        Console.Write("Enter the city's name: ");
        Name = Console.ReadLine();
        Console.Write("What country is it set in? ");
        Country = Console.ReadLine();
        Console.Write("What's the number of its population? ");
        Population = int.Parse(Console.ReadLine());
        Console.Write("What's the phone code? ");
        Phonecode = Console.ReadLine();
        Console.Write("Enter districts separated by commas: ");
        Districts = Console.ReadLine().Split(',').Select(d => d.Trim()).ToList();
    }


    public void DisplayData()
    {
        Console.WriteLine($"City: {Name}, Country: {Country}, Population: {Population}, Phone code: {Phonecode}");
        Console.WriteLine("Districts: " + string.Join(", ", Districts));
    }

    // + override
    public static City operator +(City city, int amount)
    {
        city.Population += amount;
        return city;
    }

    // - override
    public static City operator -(City city, int amount)
    {
        city.Population = Math.Max(0, city.Population - amount); // Защита от отрицательного населения
        return city;
    }

    // == override
    public static bool operator ==(City c1, City c2)
    {
        if (ReferenceEquals(c1, c2)) return true;
        if (c1 is null || c2 is null) return false;
        return c1.Population == c2.Population;
    }

    // != override
    public static bool operator !=(City c1, City c2)
    {
        return !(c1 == c2);
    }

    //  < override
    public static bool operator <(City c1, City c2)
    {
        return c1.Population < c2.Population;
    }

    // > override
    public static bool operator >(City c1, City c2)
    {
        return c1.Population > c2.Population;
    }

    // Equals
    public override bool Equals(object obj)
    {
        if (obj is City other)
        {
            return this == other;
        }
        return false;
    }

    // Hash code for equals
    public override int GetHashCode()
    {
        return Population.GetHashCode();
    }
}

