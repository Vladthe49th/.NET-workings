using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Home14
{

    // Key-value class
    public class Value<K, V>
    {
        public K Key { get; set; }
        public V ValueData { get; set; }

        public Value(K key, V value)
        {
            Key = key;
            ValueData = value;
        }

        public override string ToString()
        {
            return $"[{Key}: {ValueData}]";
        }
    }

    // Class to encapsulate the array
    public class Storage<K, V>
    {
        private Value<K, V>[] items;
        private int count;

        public Storage(int capacity = 10)
        {
            items = new Value<K, V>[capacity];
            count = 0;
        }

        //Add
        public void Add(K key, V value)
        {
            if (count >= items.Length)
                Array.Resize(ref items, items.Length * 2);

            items[count++] = new Value<K, V>(key, value);
        }


        //Insert
        public void Insert(int index, K key, V value)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Wrong index!");

            items[index] = new Value<K, V>(key, value);
        }


        //Indexator
        public Value<K, V> this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Wrong index");
                return items[index];
            }
        }

        //Tostring
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(items[i].ToString());
            }
            return sb.ToString();
        }
    }



    //Sub-task 2 - Record

    public record MyRecord(string Name, int[] Numbers)
    {
        public int CompareRecords(MyRecord other)
        {
            return Name == other.Name && Numbers.SequenceEqual(other.Numbers) ? 1 : 0;
        }
    }


    //Sub-task 4 - Library 

    // Base book class
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public Book(string title, string author, int year, string genre)
        {
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} by {Author}, {Year} ({Genre})";
        }
    }

    // Printed books
    public class PrintedBook : Book
    {
        public int Pages { get; set; }

        public PrintedBook(string title, string author, int year, string genre, int pages)
            : base(title, author, year, genre)
        {
            Pages = pages;
        }

        public override string ToString()
        {
            return base.ToString() + $", {Pages} pages";
        }
    }

    // EBooks
    public class EBook : Book
    {
        public string FileFormat { get; set; }

        public EBook(string title, string author, int year, string genre, string fileFormat)
            : base(title, author, year, genre)
        {
            FileFormat = fileFormat;
        }

        public override string ToString()
        {
            return base.ToString() + $", Format: {FileFormat}";
        }
    }

    // Library
    public class Library<T> : IEnumerable<T> where T : Book
    {
        private List<T> books = new List<T>();

        public void AddBook(T book)
        {
            books.Add(book);
        }

        public IEnumerable<T> FindBooks(Func<T, bool> predicate)
        {
            foreach (var book in books)
            {
                if (predicate(book))
                    yield return book;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return books.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Main task: \n");
            Storage<int, string> storage = new Storage<int, string>();
            storage.Add(1, "Apple");
            storage.Add(2, "Banana");
            storage.Add(3, "Accurate mini-model of the Bishmark lincore (1939)");

            Console.WriteLine("Storage:");
            Console.WriteLine(storage);

            Console.WriteLine("Elem with index 1:");
            Console.WriteLine(storage[1]);

            storage.Insert(1, 2, "Coca-cola");
            Console.WriteLine("After insert to index 1:");
            Console.WriteLine(storage);


            /////////////////////////

            Console.WriteLine("\n Sub-task - record:  \n");

            var record1 = new MyRecord("Example", new int[] { 1, 2, 3 });
            var record2 = record1 with { Numbers = (int[])record1.Numbers.Clone() }; // Clone the array

            // Changing the new array to be independent
            record2.Numbers[0] = 99;

            // Info
            Console.WriteLine($"Record 1: {record1.Name}, Numbers: {string.Join(", ", record1.Numbers)}");
            Console.WriteLine($"Record 2: {record2.Name}, Numbers: {string.Join(", ", record2.Numbers)}");

            // Comparing records
            Console.WriteLine($"Comparison result: {record1.CompareRecords(record2)}");


            ////////////////////////////////
            ///
            Console.WriteLine("\n Sub-task - library:  \n");

            var library = new Library<Book>();

            library.AddBook(new PrintedBook("1984", "George Orwell", 1949, "Dystopian", 328));
            library.AddBook(new EBook("How to protect your house from Shaheds for newbies", "Vladislav Yerts", 2022, "Dystopian guide", "EPUB"));
            library.AddBook(new PrintedBook("The Witcher", "A. Sapkowsky", 1986, "Fantasy", 310));

            Console.WriteLine("All books in library:");
            foreach (var book in library)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\nBooks published after 1985:");
            foreach (var book in library.FindBooks(b => b.Year > 1985))
            {
                Console.WriteLine(book);
            }

        }
    }
}
