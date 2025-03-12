namespace Home13
{
    internal class Program
    {   

        /// <summary>
        /// Main task
        /// </summary>
        /// 


        // Product class
        class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public int Rating { get; set; }

            public Product(string name, double price, int rating)
            {
                Name = name;
                Price = price;
                Rating = rating;
            }

            public override string ToString()
            {
                return $"{Name} - {Price} $ (Rating: {Rating})";
            }
        }

        // Category class
        class Category
        {
            public string Name { get; set; }
            public List<Product> Products { get; set; }

            public Category(string name)
            {
                Name = name;
                Products = new List<Product>();
            }

            public void DisplayProducts()
            {
                Console.WriteLine($"Category: {Name}");
                Products.ForEach(p => Console.WriteLine(p));
            }
        }

        // Cart class
        class Cart
        {
            public List<Product> Items { get; set; } = new List<Product>();

            public delegate void CartUpdatedHandler();
            public event CartUpdatedHandler CartUpdated;

            public void AddProduct(Product product)
            {
                Items.Add(product);
                CartUpdated?.Invoke();
            }

            public void DisplayCart()
            {
                Console.WriteLine("Products in cart:");
                Items.ForEach(p => Console.WriteLine(p));
            }
        }

        // User class
        class User
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public Cart UserCart { get; set; }

            public User(string login, string password)
            {
                Login = login;
                Password = password;
                UserCart = new Cart();
                UserCart.CartUpdated += () => Console.WriteLine("Cart updated!");
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Main task: \n");
            // Categories and products
            Category electronics = new Category("Electronics");
            electronics.Products.Add(new Product("Iphone 666", 12000, 5));
            electronics.Products.Add(new Product("Dell PC", 25000, 4));

            Category books = new Category("Books");
            books.Products.Add(new Product("Harry Potter", 300, 5));
            books.Products.Add(new Product("Atlas Shrugged", 800, 4));

            // Products display
            electronics.DisplayProducts();
            books.DisplayProducts();

            // Make a user
            User user = new User("Vladislav", "damnpassword");

            // Add products to cart
            user.UserCart.AddProduct(electronics.Products[0]);
            user.UserCart.AddProduct(books.Products[1]);

           
            user.UserCart.DisplayCart();


            Console.WriteLine("\nSub-task: \n");

            try
            {
                Console.Write("Enter initial number of books: ");
                int initialBooks = int.Parse(Console.ReadLine());
                Bookshelf shelf = new Bookshelf(initialBooks);

                shelf.BooksAdded += count => Console.WriteLine($"Books added: {count}. Now on shelf: {shelf.BookCount}");
                shelf.BooksRemoved += count => Console.WriteLine($"Books removed: {count}. Оn shelf now: {shelf.BookCount}");
                shelf.ErrorOccurred += message => Console.WriteLine("Oops, error: " + message);

                while (true)
                {
                    Console.WriteLine("\nChoose your action: \n1 - Add books\n2 - Delete books\n0 - Exit");
                    Console.Write("Your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("How much books to add? ");
                            int addCount = int.Parse(Console.ReadLine());
                            shelf.AddBooks(addCount);
                            break;
                        case "2":
                            Console.Write("How much books to delete? ");
                            int removeCount = int.Parse(Console.ReadLine());
                            shelf.RemoveBooks(removeCount);
                            break;
                        case "0":
                            Console.WriteLine("Exit the programm.");
                            return;
                        default:
                            Console.WriteLine("Wrong choice, try again.");
                            break;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter a correct number!.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There appears to have been an error: " + ex.Message);
            }

        }


        /// Sub-task Bookshelf///
        /// 
        class Bookshelf
        {
            public event Action<int> BooksAdded;
            public event Action<int> BooksRemoved;
            public event Action<string> ErrorOccurred;

            private int _bookCount;

            public int BookCount => _bookCount;

            public Bookshelf(int initialBooks)
            {
                if (initialBooks < 0)
                {
                    ErrorOccurred?.Invoke("Book number can`t be negative!");
                    return;
                }
                _bookCount = initialBooks;
                BooksAdded?.Invoke(initialBooks);
            }

            public void AddBooks(int count)
            {
                if (count <= 0)
                {
                    ErrorOccurred?.Invoke("I expect you to actually add some books!");
                    return;
                }
                _bookCount += count;
                BooksAdded?.Invoke(count);
            }

            public void RemoveBooks(int count)
            {
                if (count <= 0)
                {
                    ErrorOccurred?.Invoke("I expect you to actually delete some books!");
                    return;
                }
                if (count > _bookCount)
                {
                    ErrorOccurred?.Invoke("Can`t take more books than there is on the shelf!");
                    return;
                }
                _bookCount -= count;
                BooksRemoved?.Invoke(count);
            }
        }
    }
}
