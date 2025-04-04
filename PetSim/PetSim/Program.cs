using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Pet
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Hunger { get; set; }
    public int Happiness { get; set; }
    public int Energy { get; set; }
    public int Age { get; set; }
    private static Random random = new Random();

    public Pet(string name, string type)
    {
        Name = name;
        Type = type;
        Hunger = 50;
        Happiness = 50;
        Energy = 50;
        Age = 0;
    }

    public void Feed()
    {
        Hunger = Math.Max(0, Hunger - 20);
        Happiness = Math.Max(0, Happiness - 5);
        Console.WriteLine($"{Name} has eaten something tasty! Hunger: {Hunger}, Happiness: {Happiness}");
        TriggerRandomEvent();
    }

    public void Play()
    {
        Happiness = Math.Min(100, Happiness + 20);
        Energy = Math.Max(0, Energy - 10);
        Console.WriteLine($"You have played with{Name} ! Happiness: {Happiness}, Energy: {Energy}");
        TriggerRandomEvent();
    }

    public void Sleep()
    {
        Energy = Math.Min(100, Energy + 30);
        Console.WriteLine($"{Name} has taken a nap! Energy: {Energy}");
        TriggerRandomEvent();
    }

    public void AgeUp()
    {
        Age++;
        Console.WriteLine($"{Name} has become a day older! How much days have passed since this pet has stormed into your life: {Age}");
    }

    private void TriggerRandomEvent()
    {
        int chance = random.Next(1, 101);
        if (chance <= 10)
        {
            Console.WriteLine($"{Name} found a toy! The happiness is almost immesurable!");
            Happiness = Math.Min(100, Happiness + 10);
        }
        else if (chance <= 20)
        {
            Console.WriteLine($"{Name} got sick! More hunger, less energy!");
            Hunger = Math.Min(100, Hunger + 20);
            Energy = Math.Max(0, Energy - 20);
        }
        else if (chance <= 30)
        {
            Console.WriteLine($"{Name} ran away into the night, but came back! Tired, but really happy!");
            Happiness = Math.Min(100, Happiness + 5);
            Energy = Math.Max(0, Energy - 10);
        }
        else if (chance <= 40)
        {
            Console.WriteLine($"{Name} found some food! Hunger severely dwindled.");
            Hunger = Math.Max(0, Hunger - 15);
        }
    }
}

class Program
{
    static List<Pet> pets = new List<Pet>();
    static string saveFile = "pets.json";

    static void Main()
    {
        LoadPets();
        while (true)
        {
            Console.WriteLine("1. Make a pet\n2. Check out your pets\n3. Feed\n4. Play\n5. Sleep\n6. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": CreatePet(); break;
                case "2": ShowPets(); break;
                case "3": InteractPet("Feed"); break;
                case "4": InteractPet("Play"); break;
                case "5": InteractPet("Sleep"); break;
                case "6": SavePets(); return;
            }
        }
    }

    static void CreatePet()
    {
        Console.Write("Pet name : ");
        string name = Console.ReadLine();
        Console.Write("Species: ");
        string type = Console.ReadLine();
        pets.Add(new Pet(name, type));
        Console.WriteLine("Pet created!");
    }

    static void ShowPets()
    {
        foreach (var pet in pets)
            Console.WriteLine($"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Happiness: {pet.Happiness}, Energy: {pet.Energy}, Age: {pet.Age}");
    }

    static void InteractPet(string action)
    {
        Console.Write("Enter a pet`s name: ");
        string name = Console.ReadLine();
        var pet = pets.Find(p => p.Name == name);
        if (pet == null) { Console.WriteLine("Pet not found."); return; }
        if (action == "Feed") pet.Feed();
        if (action == "Play") pet.Play();
        if (action == "Sleep") pet.Sleep();
        pet.AgeUp();
    }

    static void SavePets()
    {
        File.WriteAllText(saveFile, JsonConvert.SerializeObject(pets));
        Console.WriteLine("State saved!");
    }

    static void LoadPets()
    {
        if (File.Exists(saveFile))
            pets = JsonConvert.DeserializeObject<List<Pet>>(File.ReadAllText(saveFile)) ?? new List<Pet>();
    }
}
