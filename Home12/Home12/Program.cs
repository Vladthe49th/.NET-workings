

namespace Home12
{
    internal class Program
    {
        /// Hard level - Notification system


        // Priorities
        public enum PriorityLevel { Low, Medium, High }

        // Types
        public enum NotificationType { Info, Warning, Error }

        // User 
        public class User
        {
            public string Name { get; }
            public PriorityLevel Priority { get; }

            public User(string name, PriorityLevel priority)
            {
                Name = name;
                Priority = priority;
            }
        }

        // Not. system
        public class NotificationSystem
        {
            private Dictionary<NotificationType, Action<string, PriorityLevel>> notifications = new();
            private List<User> users = new();

            public void Register(User user)
            {
                users.Add(user);
            }

            public void Subscribe(NotificationType type, Action<string, PriorityLevel> handler)
            {
                if (!notifications.ContainsKey(type))
                {
                    notifications[type] = null;
                }
                notifications[type] += handler;
            }

            public void Notify(NotificationType type, string message, PriorityLevel priority)
            {
                if (notifications.ContainsKey(type))
                {
                    foreach (var user in users)
                    {
                        if (user.Priority >= priority)
                        {
                            notifications[type]?.Invoke(message, priority);
                        }
                    }
                }
            }
        }



        static void Main(string[] args)
        {

            Console.WriteLine("Base level: \n");
            int[] numbers = { -1, 2, 0, -3, 5, 0, -2, 8, 0 };

            Func<int[], int[]> countNumbers = delegate (int[] arr)
            {
                int negativeCount = 0, positiveCount = 0, zeroCount = 0;

                foreach (int num in arr)
                {
                    if (num < 0) negativeCount++;
                    else if (num > 0) positiveCount++;
                    else zeroCount++;
                }

                return new int[] { negativeCount, positiveCount, zeroCount };
            };

            int[] result = countNumbers(numbers);

            Console.WriteLine($"Negatives: {result[0]}, Positives: {result[1]}, Zeros: {result[2]}");


            ///////////////////////////////////
            Console.WriteLine("\nHard level: \n");

            NotificationSystem system = new NotificationSystem();

            User heihachi = new User("Heihachi", PriorityLevel.High);
            User kazuya = new User("Kazuya", PriorityLevel.Medium);
            User jin = new User("Jin", PriorityLevel.Low);

            system.Register(heihachi);
            system.Register(kazuya);
            system.Register(jin);

            system.Subscribe(NotificationType.Warning, (message, priority) =>
            {
                Console.WriteLine($"[WARNING] {message} (Priority: {priority})");
            });

            system.Subscribe(NotificationType.Error, (message, priority) =>
            {
                Console.WriteLine($"[ERROR] {message} (Priority: {priority})");
            });

            system.Notify(NotificationType.Warning, "This is a warning!", PriorityLevel.Medium);
            system.Notify(NotificationType.Error, "This is an error!", PriorityLevel.High);



            ///////////////////////////////////
            Console.WriteLine("\nSub-task 2 - Backpack \n");

            Backpack myBackpack = new Backpack("Black", "Nike", "Polyester", 1.2, 20);

            
            myBackpack.OnItemAdded += delegate (Item item)
            {
                Console.WriteLine($"New item - {item.Name}, size: {item.Size}");
            };

            try
            {
                myBackpack.AddItem(new Item("Book", 3));
                myBackpack.AddItem(new Item("PC", 5));
                myBackpack.AddItem(new Item("Coca-cola bottle", 2));
                myBackpack.AddItem(new Item("Fridger", 30)); // 
            }
            catch (OverCapacity ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }



        // Sub-task 2

        class OverCapacity : Exception
        {
            public OverCapacity(string message) : base(message) { }
        }


        class Backpack
        {
            public string Color { get; set; }
            public string Brand { get; set; }
            public string Material { get; set; }
            public double Weight { get; set; }
            public double Capacity { get; set; } 
            public List<Item> Contents { get; private set; } = new List<Item>();

            public delegate void ItemAddedHandler(Item item);
            public event ItemAddedHandler OnItemAdded;

            public Backpack(string color, string brand, string material, double weight, double capacity)
            {
                Color = color;
                Brand = brand;
                Material = material;
                Weight = weight;
                Capacity = capacity;
            }

            public void AddItem(Item item)
            {
                if (GetCurrentCapacity() + item.Size > Capacity)
                {
                    throw new OverCapacity($"Can`t add '{item.Name}' - no space left!");
                }
                Contents.Add(item);
                OnItemAdded?.Invoke(item);
            }

            private double GetCurrentCapacity()
            {
                double totalVolume = 0;
                foreach (var item in Contents)
                {
                    totalVolume += item.Size;
                }
                return totalVolume;
            }
        }

        class Item
        {
            public string Name { get; set; }
            public double Size { get; set; }

            public Item(string name, double size)
            {
                Name = name;
                Size = size;
            }
        }
    }
}
