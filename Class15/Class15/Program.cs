using System.Collections;
using System.Collections.Specialized;


namespace Class15

{
    internal class Program
    {
        class Passenger
        {
            public string Name { get; }

            public Passenger(string name)
            {
                Name = name;
            }
        }

        class Airport
        {
            private Queue<Passenger> queue = new Queue<Passenger>();

            public void RegisterPassenger(string name)
            {
                Passenger passenger = new Passenger(name);
                queue.Enqueue(passenger);
                Console.WriteLine($"Passenger {name} registered.");
            }

            public void BoardPlane()
            {
                if (queue.Count == 0)
                {
                    Console.WriteLine("Queue empty - passengers are aboard");
                    return;
                }

                Passenger passenger = queue.Dequeue();
                Console.WriteLine($"Passenger {passenger.Name} is on the plane.");
            }
        }


        static void Main(string[] args)
        {
            Airport airport = new Airport();

            airport.RegisterPassenger("Ken Masters");
            airport.RegisterPassenger("Grzegosh bzencheshchikevich");
            airport.RegisterPassenger("Vadim");

            Console.WriteLine("\n:");

            airport.BoardPlane();
            airport.BoardPlane();
            airport.BoardPlane();
            airport.BoardPlane();

        }
    }
}

