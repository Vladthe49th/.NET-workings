using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Class16
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Film movie1 = new Film("Universal soldier", "Paramount Pictures", "Sci-fi", 169, 2014);
            movie1.DisplayInfo();

            Film movie2 = new Film("Puss in boots: The last Wish", "DreamWorks", "Animated", 155, 2000);
            movie2.DisplayInfo();

            // Garbage collector
            movie1 = null;
            movie2 = null;
            GC.Collect(); 

            Console.WriteLine("Display over. Choose your movie now!");


        }

        
    }
}


class Film
{
    private string Title { get; set; }
    private string Studio { get; set; }
    private string Genre { get; set; }
    private int Duration { get; set; }
    private int Releasedate { get; set; }


    // Constructor
    public Film(string title, string studio, string genre, int duration, int releaseYear)
    {
        Title = title;
        Studio = studio;
        Genre = genre;
        Duration = duration;
        Releasedate = releaseYear;
    }

    // Info
    public void DisplayInfo()
    {
        Console.WriteLine($"Movie: {Title}\nStudio: {Studio}\nGenre: {Genre}\nDuration: {Duration} min.\nRelease date: {Releasedate}\n");
    }

    // Destructor
    ~Film()
    {
        Console.WriteLine($"Movie \"{Title}\" has been destroyed forever, all footages gone into the abyss...");
    }
}


