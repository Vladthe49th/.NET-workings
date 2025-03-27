using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


//Main task

class Phone
{
    public string Name { get; set; }

    public string Manufacturer { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

}

namespace Home18
{ //Sub-task Customer

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderLine> Lines { get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
    }



    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Main task: \n");

            List<Phone> phones = new List<Phone>
            {

                new Phone {Name = "Galaxy S21", Manufacturer = "Samsung", Price = 799, CreatedAt = new DateTime(2021, 1, 29) },

                new Phone {Name = "Iphone 14", Manufacturer = "Apple", Price = 999, CreatedAt = new DateTime(2021, 2, 24) },
                new Phone {Name = "Pixel 66", Manufacturer = "Google", Price = 599, CreatedAt = new DateTime(2021, 3, 25) },
                new Phone {Name = "I`m really bored, imagine a name yourself", Manufacturer = "Some company", Price = 49, CreatedAt = new DateTime(2000, 4, 26) },
                 new Phone {Name = "Xiaomy", Manufacturer = "Huawei", Price = 449, CreatedAt = new DateTime(2010, 6, 17) }

            };

            Console.WriteLine($"Number of phones: {phones.Count}");
            Console.WriteLine($"Phones with price below 100: {phones.Count(p => p.Price > 100)}");
            Console.WriteLine($"With price bigger than 400 and less than 700: {phones.Count(p => p.Price > 400 && p.Price < 700)}");
            Console.WriteLine($"Apple phones: {phones.Count(p => p.Manufacturer == "Apple")}");

            var MinPrice = phones.OrderBy(p => p.Price).First();
            Console.WriteLine($"Phone with minimal price - {MinPrice.Name}, it`s price is {MinPrice.Price}");

            var MaxPrice = phones.OrderByDescending(p => p.Price).First();
            Console.WriteLine($"Phone with max price - {MaxPrice.Name}, the price is {MaxPrice.Price}");

            var Oldest = phones.OrderBy(p => p.CreatedAt).First();
            Console.WriteLine($"Oldest phone - {Oldest.Name}, created at {Oldest.CreatedAt.ToShortDateString()}");

            var Newest = phones.OrderByDescending(p => p.CreatedAt).First();
            Console.WriteLine($"Freshest phone -  {Newest.Name}, created at {Newest.CreatedAt.ToShortDateString()}");

            Console.WriteLine($"Average phone price: {phones.Average(p => p.Price):F2}");

            Console.WriteLine("Five most expensive phones:");
            foreach (var phone in phones.OrderByDescending(p => p.Price).Take(5))
                Console.WriteLine($"{phone.Name}, price: {phone.Price}");

            Console.WriteLine("Five cheapest phones:");
            foreach (var phone in phones.OrderBy(p => p.Price).Take(5))
                Console.WriteLine($"{phone.Name}, price: {phone.Price}");

            Console.WriteLine("Three oldest phones:");
            foreach (var phone in phones.OrderBy(p => p.CreatedAt).Take(3))
                Console.WriteLine($"{phone.Name}, created at  {phone.CreatedAt.ToShortDateString()}");

            Console.WriteLine("Тhree newest phones:");
            foreach (var phone in phones.OrderByDescending(p => p.CreatedAt).Take(3))
                Console.WriteLine($"{phone.Name}, created at {phone.CreatedAt.ToShortDateString()}");



            ////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine("\nSub-task - customer\n");

            // Пример данных
            List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Id = 1, Name = "Sophitia", Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 1, Date = DateTime.Now, Lines = new List<OrderLine>
                        {
                            new OrderLine { Id = 1, ItemName = "Laptop", Price = 1200},
                            new OrderLine { Id = 2, ItemName = "Mouse", Price = 25}
                        }
                    },
                    new Order
                    {
                        Id = 2, Date = DateTime.Now, Lines = new List<OrderLine>
                        {
                            new OrderLine { Id = 3, ItemName = "Keyboard", Price = 45 }
                        }
                    }
                }
            },
            new Customer
            {
                Id = 2, Name = "Bruce Lee", Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 3, Date = DateTime.Now, Lines = new List<OrderLine>
                        {
                            new OrderLine { Id = 4, ItemName = "Monitor", Price = 200m },
                            new OrderLine { Id = 5, ItemName = "Headphones", Price = 50m }
                        }
                    }
                }
            }
        };


            // SelectMany
            var OrderLinesSort = customers
                .SelectMany(c => c.Orders)
                .SelectMany(o => o.Lines)
                .OrderBy(line => line.Price)
                .ToList();


            foreach (var line in OrderLinesSort)
            {
                Console.WriteLine($"{line.ItemName}: {line.Price}$");
            }


            ////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine("\nSub-task - usernames\n");

            List<string> userNames = new List<string>
        {
            "Glup Pippo",
            "Petr petrovich",
            "Isabella Valentine",
            "Anna koryagina",
            "Vladik"
        };

            var uniqueWords = userNames
                .SelectMany(name => name.Split(' '))
                .Distinct()
                .ToList();

            Console.WriteLine("Unique words in usernames:");
            foreach (var word in uniqueWords)
            {
                Console.WriteLine(word);
            }




            ////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine("\nSub-task - debts\n");

            List<Client> clients = new List<Client>()
        {
            new Client(563.23m, "Petrov", 33, 2),
            new Client(0, "Samleev", 28 ,2),
            new Client(1400.45m, "Keriv", 71,1),
            new Client(8200.23m, "Podolniy", 2,3),
            new Client(0, "Lavron", 3,3),
            new Client(47.27m, "Tolchak", 5,3),
            new Client(236.89m, "Caplya", 39,3),
            new Client(0m, "Kolen", 39,4),
            new Client(0m, "Helpon", 39,4) };

            for (int floor = 9; floor >= 1; floor--)
            {
                var debtors = clients.Where(c => c.Floor == floor && c.Debt > 0).ToList();
                decimal totalDebt = debtors.Sum(c => c.Debt);
                int debtorCount = debtors.Count;

                Console.WriteLine($"Floor {floor}: total debt = {totalDebt} UAH,  = {debtorCount}");
            }
        }

    }
}


/// <summary>
/// Sub-task Debts
/// </summary>

class Client
{
    public decimal Debt { get; }
    public string Surname { get; }
    public int ApartmentNumber { get; }
    public int Floor { get; }

    public Client(decimal debt, string surname, int apartmentNumber, int floor)
    {
        Debt = debt;
        Surname = surname;
        ApartmentNumber = apartmentNumber;
        Floor = floor;
    }
}
