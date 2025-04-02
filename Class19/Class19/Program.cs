namespace Class19
{

    class Program
    {
        static void Main()
        {

            Person[] people = CreatePeople(30);

            // 1. People older than 25
            var OlderThan25 = people.Where(p => p.Age > 25);

            // 2. Average salary
            var AverageSalary = people.Average(p => p.Salary);

            // 3. Top 5 salaries
            var Top5Salaries = people.OrderByDescending(p => p.Salary).Take(5);

            // 4. Wallmart employees
            var WallmartEmployeesCount = people.Count(p => p.Company.Name == "WallMart");

            // 5. Unique companies
            var UniqueCompanies = people.Select(p => p.Company.Name).Distinct();

            // 6. More than two cars
            var MoreThan2Cars = people.Where(p => p.Cars.Count > 2);

            // 7. All car models
            var AllCarModels = people.SelectMany(p => p.Cars).Select(c => c.Model).Distinct();

            // 8.Person with most phones
            var MostPhonesPerson = people.OrderByDescending(p => p.PhoneNumbers.Count).FirstOrDefault();

            // 9. Group by company
            var GroupedByCompany = people.GroupBy(p => p.Company.Name);

            // 10. Over 10 hours of work
            var Over10HoursWork = people.Where(p => p.WorkTimePerDay.TotalHours > 10);

            // 11. PersonView
            var PersonView = people.Select(p => new PersonView { Name = p.Name, Age = p.Age }).ToList();

            // 12. BMW owners
            var BMWOwners = people.Where(p => p.Cars.Any(c => c.Model == "BMW 3 Series")).ToList();

            // 13. Total cars
            int TotalCars = people.Sum(p => p.Cars.Count);

            // 14. Oldest person
            var OldestPerson = people.OrderByDescending(p => p.Age).FirstOrDefault();

            // 15. Sort the people
            var SortPeople = people.OrderBy(p => p.Name).ThenBy(p => p.Salary).ToList();

            // 16. Higher than 13000
            bool HasHigher = people.Any(p => p.Salary > 13000);

            // 17. Registrations after 2024
            var RecentRegis = people.Where(p => p.Date.Year > 2024).Take(4).ToList();

            // 18. Two arrays unite
            var MergeArrays = people.Union(peopleSecond).Distinct().ToList();

            // 19. People with 3 numbers
            var ThreeNumbers = people.Where(p => p.PhoneNumbers.Count == 3).ToList();

            // 20. Average work hours
            double AvgWorkHours = people.Average(p => p.WorkTimePerDay.TotalHours);
        }



        int[] array = CreateArray(30);
            int[] arraySecond = CreateArray(30);

            string[] strs = { "Far Cry 5", "Olivia", "Fallout 3", "Daxter", "System Shock 2",
            "Gta V", "Man in Black", "Hallo", "Hitman", "Destiny 2", "Tekken 8", "Smite",
            "Path of Exile", "Book of Yog Idle RPG"};

            Person[] people = CreatePeople(30);
            Person[] peopleSecond = CreatePeople(2);
        

        static int[] CreateArray(int size)
        {
            Random rnd = new Random();
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(10, 1000);
            }
            return array;
        }

        static Person[] CreatePeople(int count = 10)
        {
            Person[] persons = new Person[count];
            Random r = new Random();

            List<string> generatePhones()
            {
                List<string> phones = new List<string>(r.Next(5));
                for (int i = 0; i < phones.Capacity; i++)
                {
                    phones.Add("+380" + r.Next(100000000, 999999999));
                }
                return phones;
            }

            List<Car> generateCars()
            {
                string[] carModels = {
            "Toyota Camry","Honda Accord", "Ford Mustang",
            "Chevrolet Malibu", "BMW 3 Series","Mercedes-Benz C-Class", "Audi A4","Tesla Model 3",
            "Nissan Altima","Hyundai Sonata","Kia Optima","Volkswagen Passat","Subaru Legacy","Mazda 6","Dodge Charger"
            };
                List<Car> cars = new List<Car>(r.Next(9));
                for (int i = 0; i < cars.Capacity; i++)
                {
                    cars.Add(new Car
                    {
                        Model = carModels[r.Next(carModels.Length)],
                    });
                }
                return cars;
            }

            string[] names = { "John", "Jane", "Alice", "Bob", "Charlie", "Emma", "Michael", "Olivia", "David", "Sophia", "Kate", "Sarah", "Jacob" };
            string[] companyNames = { "Wendys", "WallMart", "BestBuy", "Abbys", "Dueno" };

            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person
                {
                    Name = names[r.Next(names.Length)],
                    Age = r.Next(10, 100),
                    Date = DateTime.Now.AddDays(r.Next(-300, 300)).AddMonths(r.Next(-36, 36)),
                    Salary = r.Next(1000, 15000),
                    IsAdult = Convert.ToBoolean(r.Next(2)),
                    WorkTimePerDay = TimeSpan.FromHours(r.Next(2, 13)),
                    Company = new Company
                    {
                        Name = companyNames[r.Next(companyNames.Length)],
                        Address = "123 Main St"
                    },
                    Cars = generateCars(),
                    PhoneNumbers = new List<string>(generatePhones())
                };
            }
            return persons;
        }
    }

    class PersonView
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string CompanyName { get; set; }
    }
    class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public decimal Salary { get; set; }
        public bool IsAdult { get; set; }
        public TimeSpan WorkTimePerDay { get; set; }
        public Company Company { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<Car> Cars { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return person.Name.Equals(Name) && person.Age == Age;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age);
        }
    }
    class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Model { get; set; }
        public List<int> DriveDays { get; private set; }

        public Car()
        {
            DriveDays = Enumerable.Range(1, 30).OrderBy(_ => Guid.NewGuid()).Take(new Random().Next(2, 26)).ToList();
        }
    }

    class Company
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Address { get; set; }
    }

}
