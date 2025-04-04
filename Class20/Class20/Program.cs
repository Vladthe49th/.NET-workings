using System.Text.Json;
using Newtonsoft.Json;

namespace Class20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "refrigerators.json";

            List<Refrigerator> refrigerators = new List<Refrigerator>
        {
            new Refrigerator { Name = "Samsung RT38", Price = 60000, Volume = 380, Manufacturer = "Samsung" },
            new Refrigerator { Name = "LG GA-B509", Price = 54000, Volume = 400, Manufacturer = "LG" },
            new Refrigerator { Name = "Bosch KGN39", Price = 75000, Volume = 390, Manufacturer = "Bosch" }
        };

            // Save to Json
            string json = JsonConvert.SerializeObject(refrigerators, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("File saved!");

            // Read from JSON
            string readJson = File.ReadAllText(filePath);
            List<Refrigerator> LoadedRefrigerators = JsonConvert.DeserializeObject<List<Refrigerator>>(readJson);

            // Searching for something expensive
            var ExpensiveRefrigerators = LoadedRefrigerators.Where(r => r.Price > 55000);

            Console.WriteLine("Fridgers costing more than 55000 uah:");
            foreach (var fridge in ExpensiveRefrigerators)
            {
                Console.WriteLine($"{fridge.Name} - {fridge.Price} uah, Volume: {fridge.Volume}, Manufacturer: {fridge.Manufacturer}");
            }


        }
    }
}

class Refrigerator
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Volume { get; set; }
    public string Manufacturer { get; set; }
}