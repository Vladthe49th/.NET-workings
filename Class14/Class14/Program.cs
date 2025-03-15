using System.Xml.Schema;
using System.Collections.Generic;
using System.Collections;

namespace Class14
{
    internal class Program
    {
        public class Product<T1, T2, T3, T4>
        {
            // Template type properties
            public T1 Id { get; set; }
            public T2 Name { get; set; }
            public T3 Price { get; set; }
            public T4 Quantity { get; set; }

            // Constructor
            public Product(T1 id, T2 name, T3 price, T4 quantity)
            {
                Id = id;
                Name = name;
                Price = price;
                Quantity = quantity;
            }

            // Info
            public void PrintInfo()
            {
                Console.WriteLine($"ID: {Id}");
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Price: {Price}");
                Console.WriteLine($"Quantity: {Quantity}");
            }

            // Types
            public void PrintTypes()
            {
                Console.WriteLine($"Type of Id: {typeof(T1)}");
                Console.WriteLine($"Type of name: {typeof(T2)}");
                Console.WriteLine($"Type of price: {typeof(T3)}");
                Console.WriteLine($"Type of quantity: {typeof(T4)}");
            }
        }


        static void Main(string[] args)
        {
            // An exampl
            var product = new Product<int, string, decimal, int>(1, "Laptop", 999.99m, 10);

            
            product.PrintInfo();

            product.PrintTypes();
        }
    }
}





