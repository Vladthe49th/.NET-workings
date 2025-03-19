using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace Home15
{
    //Main task
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class Document
    {
        public string Name { get; }
        public Priority DocPriority { get; }

        public Document(string name, Priority priority)
        {
            Name = name;
            DocPriority = priority;
        }
    }

    public class PrintQueue
    {
        private readonly List<Document> _queue = new List<Document>();

        public void Enqueue(Document doc)
        {
            _queue.Add(doc);
            _queue.Sort((d1, d2) => d2.DocPriority.CompareTo(d1.DocPriority));
        }

        public Document Dequeue()
        {
            if (!HasDocuments())
                throw new InvalidOperationException("Queue is empty.");

            var doc = _queue[0];
            _queue.RemoveAt(0);
            return doc;
        }

        public bool HasDocuments()
        {
            return _queue.Count > 0;
        }
    }



    internal class Program
    {

        //Sub-task employees

        class Employee
        {
            public string Name { get; }
            public string Position { get; }
            public int Pryority { get; }

            public Employee (string name, string position, int pryority)
            {
                Name = name;
                Position = position;
                Pryority = pryority;
            }

            public override string ToString()
            {
                return $"{Position} - {Name} (Priority {Pryority})";
            }
        }

        class Firm
        {
            private Queue<Employee> employees = new Queue<Employee>();

            public void AddEmployee(Employee employee)
            { 
                employees.Enqueue(employee);
            }

            public void ShowEmployees()
            {
                
                var sortedEmployees = employees.OrderBy(s => s.Pryority);

                Console.WriteLine("List of all employees: ");
                foreach (var sotrudnik in sortedEmployees)
                {
                    Console.WriteLine(sotrudnik);
                }
            }
        }


        static void Main(string[] args)
        {

            Console.WriteLine("Main task\n");
            PrintQueue queue = new PrintQueue();

            queue.Enqueue(new Document("Doc. 1", Priority.Low));
            queue.Enqueue(new Document("Doc. 2", Priority.High));
            queue.Enqueue(new Document("Doc. 3", Priority.Medium));

            while (queue.HasDocuments())
            {
                Document doc = queue.Dequeue();
                Console.WriteLine($"Printing: {doc.Name} with priority {doc.DocPriority}");
            }


            ////////////////////////////////

            Console.WriteLine("\nSub-task - employees\n");

            Firm vodokanal = new Firm();

            // Добавляем сотрудников в хаотичном порядке
            vodokanal.AddEmployee(new Employee("Cammy White", "Manager", 2));
            vodokanal.AddEmployee(new Employee("Billie Jean", "Director", 1));
            vodokanal.AddEmployee(new Employee("Boris", "General inspector", 3));
            vodokanal.AddEmployee(new Employee("Vladik", "Buhgalter", 4));

            // Выводим в порядке убывания важности должности
            vodokanal.ShowEmployees();



        }
    }
}
