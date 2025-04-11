using System.Text;

namespace RouteSystem
{

    // 1. Route interface
    public interface IRoute
    {
        double CalculateCost();
        string Describe();
    }

    // 2. Single route between two points
    public class SingleRoute : IRoute
    {
        public string From { get; }
        public string To { get; }
        public string TransportType { get; }
        public double BaseCost { get; }

        public SingleRoute(string from, string to, string transportType, double baseCost)
        {
            From = from;
            To = to;
            TransportType = transportType;
            BaseCost = baseCost;
        }

        public double CalculateCost() => BaseCost;

        public string Describe() => $"Route from {From} to {To} by {TransportType}, base cost: {BaseCost}";
    }

    // 3. Composite route
    public class CompositeRoute : IRoute
    {
        private readonly List<IRoute> _routes = new();

        public void AddRoute(IRoute route)
        {
            _routes.Add(route);
        }

        public double CalculateCost() => _routes.Sum(r => r.CalculateCost());

        public string Describe()
        {
            var description = new StringBuilder("Composite Route:\n");
            foreach (var route in _routes)
            {
                description.AppendLine(route.Describe());
            }
            return description.ToString();
        }
    }


    //Route builder
    public class RouteBuilder
    {
        private string _from;
        private string _to;
        private string _transportType;
        private double _baseCost;

        public RouteBuilder From(string from)
        {
            _from = from;
            return this;
        }

        public RouteBuilder To(string to)
        {
            _to = to;
            return this;
        }

        public RouteBuilder WithTransport(string transportType)
        {
            _transportType = transportType;
            return this;
        }

        public RouteBuilder WithBaseCost(double baseCost)
        {
            _baseCost = baseCost;
            return this;
        }

        public SingleRoute Build()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(_from) || string.IsNullOrWhiteSpace(_to))
                throw new InvalidOperationException("Route must have a finishing point!");
            if (string.IsNullOrWhiteSpace(_transportType))
                throw new InvalidOperationException("Transport type must be typed in!");
            if (_baseCost <= 0)
                throw new InvalidOperationException("Route cost must be positive!.");

            return new SingleRoute(_from, _to, _transportType, _baseCost);
        }
    }


    // Abstract decorator
    public abstract class RouteDecorator : IRoute
    {
        protected readonly IRoute _route;

        public RouteDecorator(IRoute route)
        {
            _route = route;
        }

        public virtual double CalculateCost() => _route.CalculateCost();

        public virtual string Describe() => _route.Describe();
    }


    public class InsuredRoute : RouteDecorator
    {
        private readonly double _insuranceCost;

        public InsuredRoute(IRoute route, double insuranceCost = 200) : base(route)
        {
            _insuranceCost = insuranceCost;
        }

        public override double CalculateCost() => base.CalculateCost() + _insuranceCost;

        public override string Describe() =>
            base.Describe() + $" + Insurance({_insuranceCost})";
    }


    public class RefrigeratedRoute : RouteDecorator
    {
        private readonly double _refrigerationCost;

        public RefrigeratedRoute(IRoute route, double refrigerationCost = 300) : base(route)
        {
            _refrigerationCost = refrigerationCost;
        }

        public override double CalculateCost() => base.CalculateCost() + _refrigerationCost;

        public override string Describe() =>
            base.Describe() + $" + Refrigeration({_refrigerationCost})";
    }


    public class ExpressRoute : RouteDecorator
    {
        private readonly double _expressMultiplier;

        public ExpressRoute(IRoute route, double expressMultiplier = 1.5) : base(route)
        {
            _expressMultiplier = expressMultiplier;
        }

        public override double CalculateCost() => base.CalculateCost() * _expressMultiplier;

        public override string Describe() =>
            base.Describe() + $" + Express(x{_expressMultiplier})";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Test route - Kyiv to Lviv via an insured truck
            IRoute route1 = new RouteBuilder()
                .From("Kyiv")
                .To("Lviv")
                .WithTransport("Truck")
                .WithBaseCost(1200)
                .Build();

            route1 = new InsuredRoute(route1);

            //  Refrigerated + express from lviv to uzhgorod
            IRoute route2 = new RouteBuilder()
                .From("Lviv")
                .To("Uzhgorod")
                .WithTransport("Van")
                .WithBaseCost(800)
                .Build();

            route2 = new RefrigeratedRoute(route2);
            route2 = new ExpressRoute(route2);

           

            // Compose a route

            CompositeRoute bigRoute = new CompositeRoute();
            bigRoute.AddRoute(route1);
            bigRoute.AddRoute(route2);
       

        

            Console.WriteLine("=== FULL DELIVERY ROUTE ===");
            Console.WriteLine(bigRoute.Describe());
            Console.WriteLine($"Total cost: {bigRoute.CalculateCost()} UAH");
        }
    }
}
