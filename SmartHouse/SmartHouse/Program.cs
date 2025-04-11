namespace SmartHouse
{
    internal class Program
    {
        // Heating interface

        public interface IHeatingStrategy
        {
            double Heat(double temperature, double area);
        }

        public class GasHeating : IHeatingStrategy
        {
            public double Heat(double temperature, double area)
            {
                // Lower the temperature, higher the gas usage
                return (20 - temperature) * area * 0.5;
            }
        }

        //Electric heating
        public class ElectricHeating : IHeatingStrategy
        {
            public double Heat(double temperature, double area)
            {
                return (20 - temperature) * area * 0.8;
            }
        }

        //Solar heating

        public class SolarHeating : IHeatingStrategy
        {
            public double Heat(double temperature, double area)
            {
                
                return area * 0.3;
            }
        }


        // Temperature observer
        public interface ITemperatureObserver
        {
            void OnTemperatureChanged(double newTemperature);
        }

        public class TemperatureSensor
        {
            private List<ITemperatureObserver> _observers = new();
            private double _threshold;

            public TemperatureSensor(double threshold)
            {
                _threshold = threshold;
            }

            public void AddObserver(ITemperatureObserver observer)
            {
                _observers.Add(observer);
            }

            public void RemoveObserver(ITemperatureObserver observer)
            {
                _observers.Remove(observer);
            }

            public void UpdateTemperature(double currentTemperature)
            {
                if (currentTemperature < _threshold)
                {
                    Console.WriteLine($"Temperature got lower than {_threshold}°C! ({currentTemperature}°C)");
                    foreach (var observer in _observers)
                    {
                        observer.OnTemperatureChanged(currentTemperature);
                    }
                }
            }
        }


        public class HeatingSystem : ITemperatureObserver
        {
            private IHeatingStrategy _strategy;
            private double _area;

            public HeatingSystem(IHeatingStrategy strategy, double area)
            {
                _strategy = strategy;
                _area = area;
            }

            public void SetStrategy(IHeatingStrategy strategy)
            {
                _strategy = strategy;
            }

            public void OnTemperatureChanged(double newTemperature)
            {
                double energyUsed = _strategy.Heat(newTemperature, _area);
                Console.WriteLine($"Heating on. Using energy: {energyUsed:F2} points.");
            }
        }


        //Main program
        static void Main(string[] args)
        {

            var sensor = new TemperatureSensor(threshold: 18.0);

            var heatingSystem = new HeatingSystem(new GasHeating(), area: 50);
            sensor.AddObserver(heatingSystem);

            // Temp during the day
            double[] temperatures = { 20, 19, 17, 15, 18.5, 16 };

            foreach (var temp in temperatures)
            {
                Console.WriteLine($"\nTemperature check: {temp}°C");
                sensor.UpdateTemperature(temp);
            }

            // Change the strategy
            Console.WriteLine("\n>>> Changing to electric heating <<<");
            heatingSystem.SetStrategy(new ElectricHeating());

            sensor.UpdateTemperature(16); 

        }
    }
}
