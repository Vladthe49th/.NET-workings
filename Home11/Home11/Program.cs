namespace Home11
{
    internal class Program

    {
        static int Stairs(int n, int step = 1)
        {
            if (n < step)
                return 0;

            if (n == step)
                return 1;

            return Stairs(n - step, step + 1) + Stairs(n, step + 1);
        }


        // Sub-task 3 
        static int BinarySearch(int[] array, int target)
        {
            int left = 0, right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                    return mid;

                if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1; 
        }


        //Sub-task 5

        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Weight { get; set; }
            public double Price { get; set; }
        }

        //Warehouse
        class Warehouse
        {
            public int Id { get; set; }
            public string Address { get; set; }
            public List<Product> Products { get; set; } = new List<Product>();
        }

        //Truck
        class Auto
        {
            public int Id { get; set; }
            public string Brand { get; set; }
            public double MaxLoad { get; set; }
            public double Money { get; set; }
            public List<Product> LoadedProducts { get; set; } = new List<Product>();
            public double RemainingLoad { get; private set; }

            public Auto(double maxLoad, double money)
            {
                MaxLoad = maxLoad;
                Money = money;
                RemainingLoad = maxLoad;
            }

            public void Loadin(Warehouse warehouse)
            {
                var sortedProducts = warehouse.Products
                    .OrderByDescending(p => p.Price)
                    .ToList();

                foreach (var product in sortedProducts)
                {
                    if (Money <= 0 || RemainingLoad <= 0) break;

                    int maxFitQuantity = (int)(RemainingLoad / product.Weight);
                    int buyQuantity = Math.Min(maxFitQuantity, product.Quantity);
                    double totalPrice = buyQuantity * product.Price;

                    if (buyQuantity > 0 && Money >= totalPrice)
                    {
                        LoadedProducts.Add(new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Quantity = buyQuantity,
                            Weight = product.Weight,
                            Price = product.Price
                        });

                        product.Quantity -= buyQuantity;
                        Money -= totalPrice;
                        RemainingLoad -= buyQuantity * product.Weight;
                    }
                }
            }

            public void Report(Warehouse warehouse)
            {
                Console.WriteLine("Loaded products:");
                foreach (var product in LoadedProducts)
                {
                    Console.WriteLine($"{product.Name}, Quantity: {product.Quantity}, weight: {product.Quantity * product.Weight}, price: {product.Quantity * product.Price}");
                }
                Console.WriteLine($"Money left: {Money}");
                Console.WriteLine($"Remaining load: {RemainingLoad}");

                Console.WriteLine("Products left:");
                foreach (var product in warehouse.Products)
                {
                    Console.WriteLine($"{product.Name}, remaining: {product.Quantity}");
                }
            }
        }


        static void Main()
        {
            Console.WriteLine("Main task - cube stairs\n ");

            Console.WriteLine("Enter the number of cubes: ");
            int n = int.Parse(Console.ReadLine());

            int result = Stairs(n);
            Console.WriteLine($"The number of steps that could be made: {result}");



            ////////////////////////////////////////
            Console.WriteLine("\nSub task 1 - guess the number\n ");


            int low = 0, high = 100, attempts = 0;
            string input;

            Console.WriteLine("Think of a number from 0 to 100, I`ll try to guess!");

            while (low <= high)
            {
                int guess = (low + high) / 2;
                attempts++;

                Console.WriteLine($"My guess: {guess}");
                Console.WriteLine("Is your num >, < or = to mine ?:");

                input = Console.ReadLine();

                if (input == "=")
                {
                    Console.WriteLine($"Yass! I`ve guessed {guess} in {attempts} attempts!");
                    break;
                }
                else if (input == ">")
                {
                    low = guess + 1;
                }
                else if (input == "<")
                {
                    high = guess - 1;
                }
                else
                {
                    Console.WriteLine(" You should use only '>', '<' or '='.");
                    attempts--; 
                }


            }

            ////////////////////////////////////////
            Console.WriteLine("\nSub task 3 - find the number\n ");


            int[] sortedArray = { 1, 3, 5, 7, 9, 11, 15, 20, 25, 30 };
            Console.Write("Enter a num to search: ");
            int target = int.Parse(Console.ReadLine());

            int index = BinarySearch(sortedArray, target);

            if (index != -1)
                Console.WriteLine($"Number {target} found on position {index}.");
            else
                Console.WriteLine($"Num {target} not found.");



            ////////////////////////////////////////
            Console.WriteLine("\nSub task 5 - Warehouse\n ");


            Warehouse warehouse = new Warehouse
            {
                Id = 1,
                Address = "Vasilechek st., 36",
                Products = new List<Product>
            {
                new Product { Id = 1, Name = "TV", Quantity = 10, Weight = 15, Price = 500 },
                new Product { Id = 2, Name = "Fridger", Quantity = 5, Weight = 50, Price = 800 },
                new Product { Id = 3, Name = "Chandelier", Quantity = 15, Weight = 10, Price = 200 },
                new Product { Id = 4, Name = "PS5", Quantity = 20, Weight = 5, Price = 10000 }
            }
            };

            Auto auto = new Auto(200, 5000);
            auto.Loadin(warehouse);
            auto.Report(warehouse);


        }






    }
}
