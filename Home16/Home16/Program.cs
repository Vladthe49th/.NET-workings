using System;
using System.Collections.Generic;
using System.Linq;

class Visit
{
    public string ClientName { get; set; }
    public DateTime VisitDate { get; set; }

    public Visit(string clientName, DateTime visitDate)
    {
        ClientName = clientName;
        VisitDate = visitDate;
    }

    public override string ToString()
    {
        return $"{ClientName} - {VisitDate}";
    }
}

class GymManager
{
    private Dictionary<string, Stack<Visit>> visitHistory = new();

    public void AddVisit(string clientName)
    {
        if (!visitHistory.ContainsKey(clientName))
        {
            visitHistory[clientName] = new Stack<Visit>();
        }
        Visit newVisit = new(clientName, DateTime.Now);
        visitHistory[clientName].Push(newVisit);
        Console.WriteLine($"Visit added: {newVisit}");
    }

    public void UndoLastVisit(string clientName)
    {
        if (visitHistory.ContainsKey(clientName) && visitHistory[clientName].Count > 0)
        {
            Visit removedVisit = visitHistory[clientName].Pop();
            Console.WriteLine($"Visit cancelled: {removedVisit}");
        }
        else
        {
            Console.WriteLine("You don`t have visits yet!");
        }
    }

    public void ShowLastVisits(string clientName, int count)
    {
        if (visitHistory.ContainsKey(clientName) && visitHistory[clientName].Count > 0)
        {
            Console.WriteLine($"Last {count} visits of the client {clientName}:");
            foreach (var visit in visitHistory[clientName].Take(count))
            {
                Console.WriteLine(visit);
            }
        }
        else
        {
            Console.WriteLine("No visits yet!");
        }
    }

    public void ShowVisitsInRange(DateTime from, DateTime to)
    {
        Console.WriteLine($"Visits from {from} to {to}:");
        foreach (var client in visitHistory)
        {
            foreach (var visit in client.Value)
            {
                if (visit.VisitDate >= from && visit.VisitDate <= to)
                {
                    Console.WriteLine(visit);
                }
            }
        }
    }
}

class Program
{
    /// <summary>
    /// Sub- task Spectacle
    /// </summary>


    class Spectacle : IDisposable
    {
        public string Title { get; set; }
        public string Theater { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; } 
        public List<string> Actors { get; set; }
        private bool disposed = false;

        public Spectacle(string title, string theater, string genre, int duration, List<string> actors)
        {
            Title = title;
            Theater = theater;
            Genre = genre;
            Duration = duration;
            Actors = new List<string>(actors);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Spectacle: {Title}\nTheatre: {Theater}\nGenre: {Genre}\nDuration: {Duration} mins");
            Console.WriteLine("Actors: " + string.Join(", ", Actors));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Actors.Clear();
                }
                disposed = true;
            }
        }

        ~Spectacle()
        {
            Dispose(false);
        }
    }

    static void Main()
    {    
        Console.WriteLine("Main task: \n");

        GymManager manager = new();

        manager.AddVisit("Michael Bison");
        manager.AddVisit("Pietr pietechkin");
        System.Threading.Thread.Sleep(1000); 
        manager.AddVisit("Michael Bison");

        manager.ShowLastVisits("Michael Bison", 2);

        manager.UndoLastVisit("Michael Bison");

        manager.ShowVisitsInRange(DateTime.Now.AddMinutes(-10), DateTime.Now);


        ////////

        Console.WriteLine("\n Sub-task: spectacle \n");

        using (Spectacle spectacle = new Spectacle("Me, a dodger", "The great Odessa theatre", "Comical tragedy", 180, new List<string> { "Vlad koryagin", "Domovenok Kuzya", "President of Lietuva" }))
        {
            spectacle.ShowInfo();
        }
        Console.WriteLine("An object has been deleted from the spectacle.");

    }
}

