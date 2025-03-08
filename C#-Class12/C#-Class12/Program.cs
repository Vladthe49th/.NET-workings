using Humanizer;
using System.Diagnostics;
using System.Globalization;

namespace ConsoleApp1;

class Program
{


    static void Main(string[] args)
    {

        int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        
        var Evens = Where(nums, x => x % 2 == 0);

        Console.WriteLine("Evens:");
        foreach (var num in Evens)
        {
            Console.WriteLine(num);
        }

       
        var users = new[]
        {
            new User { Name = "Selvestre", Age = 35 },
            new User { Name = "Arnold", Age = 40 },
            new User { Name = "Vadim", Age = 17 }
        };

        var usersover35 = Where(users, user => user.Age > 35);

        Console.WriteLine("Users over 35:");
        foreach (var user in usersover35)
        {
            Console.WriteLine($"{user.Name} - {user.Age} years");
        }
    }




 public static IEnumerable<T> Where<T>(T[] array, Func<T, bool> predicate)
    {
        foreach (var item in array)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
}

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}