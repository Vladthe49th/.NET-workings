namespace Class21
{

    public interface IEngine
    {
        void Refuel();
    }


    public interface IMovement
    {
        void Move();
    }


    public interface ICarFactory
    {
        IEngine CreateEngine();
        IMovement CreateMovement();
    }


    public class GasolineEngine : IEngine
    {
        public void Refuel() => Console.WriteLine("Fueling tank...");
    }

    public class ElectricEngine : IEngine
    {
        public void Refuel() => Console.WriteLine("Charging battery...");
    }


    public class WheeledMovement : IMovement
    {
        public void Move() => Console.WriteLine("We shall drive on wheels.");
    }

    public class FlyingMovement : IMovement
    {
        public void Move() => Console.WriteLine("We shall soar through the air!");
    }

    public class GasolineWheeledCarFactory : ICarFactory
    {
        public IEngine CreateEngine() => new GasolineEngine();
        public IMovement CreateMovement() => new WheeledMovement();
    }


    public class ElectricFlyingCarFactory : ICarFactory
    {
        public IEngine CreateEngine() => new ElectricEngine();
        public IMovement CreateMovement() => new FlyingMovement();
    }


    public class Car
    {
        private readonly IEngine _engine;
        private readonly IMovement _movement;

        public Car(ICarFactory factory)
        {
            _engine = factory.CreateEngine();
            _movement = factory.CreateMovement();
        }

        public void Start()
        {
            _engine.Refuel();
            _movement.Move();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("Let`s make a gasoline car:");
                var car1 = new Car(new GasolineWheeledCarFactory());
                car1.Start();

                Console.WriteLine("\nAnd an electric car:");
                var car2 = new Car(new ElectricFlyingCarFactory());
                car2.Start();
            }
        }
    }
}

