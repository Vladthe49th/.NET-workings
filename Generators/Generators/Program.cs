using System;
using NumberGenerators;




// Top-level statements
var evenGenerator = new EvenNumberGenerator();
var oddGenerator = new OddNumberGenerator();
var primeGenerator = new PrimeNumberGenerator();
var fibGenerator = new FibonacciNumberGenerator();

Console.WriteLine("Even numbers:");
for (int i = 0; i < 10; i++)
{
    Console.Write(evenGenerator.Next() + " ");
}

Console.WriteLine("\nOdd numbers:");
for (int i = 0; i < 10; i++)
{
    Console.Write(oddGenerator.Next() + " ");
}

Console.WriteLine("\nPrime numbers:");
for (int i = 0; i < 10; i++)
{
    Console.Write(primeGenerator.Next() + " ");
}

Console.WriteLine("\nFibonacci numbers:");
for (int i = 0; i < 10; i++)
{
    Console.Write(fibGenerator.Next() + " ");
}







namespace NumberGenerators
{
    public class EvenNumberGenerator
    {
        private int _current;

        public EvenNumberGenerator(int start = 0)
        {
            _current = start % 2 == 0 ? start : start + 1;
        }

        public int Next()
        {
            int result = _current;
            _current += 2;
            return result;
        }
    }

    public class OddNumberGenerator
    {
        private int _current;

        public OddNumberGenerator(int start = 1)
        {
            _current = start % 2 != 0 ? start : start + 1;
        }

        public int Next()
        {
            int result = _current;
            _current += 2;
            return result;
        }
    }

    public class PrimeNumberGenerator
    {
        private int _current;

        public PrimeNumberGenerator(int start = 2)
        {
            _current = start;
        }

        public int Next()
        {
            while (true)
            {
                if (IsPrime(_current))
                {
                    int result = _current;
                    _current++;
                    return result;
                }
                _current++;
            }
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }

    public class FibonacciNumberGenerator
    {
        private int _prev = 0;
        private int _current = 1;

        public int Next()
        {
            int result = _prev;
            _prev = _current;
            _current = result + _current;
            return result;
        }
    }
}



