

// Testing
Store store1 = new Store("Piatorochka", "123 Ivan St", "Supermarket", "123-456-7890", "piat@world.com", 120.5);
Store store2 = new Store("Vialaya rozochka", "456 somnitelny St", "Groceries", "987-654-3210", "grocery@viyalaya.com", 150.0);

store1.DisplayData();
store2.DisplayData();

store1 += 30;  // Area enlarge
store2 -= 20;  // Area shrink

Console.WriteLine("\nAfter area modification:");
store1.DisplayData();
store2.DisplayData();

Console.WriteLine($"\nAre areas equal? {store1 == store2}");
Console.WriteLine($"Is store1 smaller than store2? {store1 < store2}");
Console.WriteLine($"Is store1 larger than store2? {store1 > store2}");


class Store
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Profile { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public double Area { get; set; }  

    public Store() { }

    public Store(string name, string address, string profile, string phone, string email, double area)
    {
        Name = name;
        Address = address;
        Profile = profile;
        Phone = phone;
        Email = email;
        Area = area >= 0 ? area : throw new ArgumentException("Area can`t be negative.");
    }

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
        Console.Write("Enter store area (m²): ");

        if (double.TryParse(Console.ReadLine(), out double area) && area >= 0)
            Area = area;
        else
            Console.WriteLine("Invalid area value. Area set to 0.");
    }

    public void DisplayData()
    {
        Console.WriteLine($"Store: {Name}, Address: {Address}, Profile: {Profile}, Phone: {Phone}, Email: {Email}, Area: {Area} m²");
    }

    // Operator overload
    public static Store operator +(Store store, double value)
    {
        store.Area += value;
        return store;
    }

    public static Store operator -(Store store, double value)
    {
        store.Area = Math.Max(0, store.Area - value); 
        return store;
    }

    public static bool operator ==(Store s1, Store s2)
    {
        return s1?.Area == s2?.Area;
    }

    public static bool operator !=(Store s1, Store s2)
    {
        return !(s1 == s2);
    }

    public static bool operator <(Store s1, Store s2)
    {
        return s1.Area < s2.Area;
    }

    public static bool operator >(Store s1, Store s2)
    {
        return s1.Area > s2.Area;
    }

    public override bool Equals(object obj)
    {
        if (obj is Store store)
        {
            return this.Area == store.Area;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Area.GetHashCode();
    }
}

