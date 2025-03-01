using System;
using System.Collections.Generic;


// Top-level statements
var bookList = new BookList();

while (true)
{
    Console.WriteLine("\nChoose your action:");
    Console.WriteLine("1. Add a book");
    Console.WriteLine("2. Delete a book");
    Console.WriteLine("3. Check the availability of a book");
    Console.WriteLine("4. Show book list");
    Console.WriteLine("5. Exit");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter the book`s name: ");
            string title = Console.ReadLine();
            Console.Write("Enter the book`s author: ");
            string author = Console.ReadLine();
            bookList += new Book(title, author);
            Console.WriteLine("Book added.");
            break;

        case "2":
            Console.Write("Enter book name: ");
            title = Console.ReadLine();
            Console.Write("Enter book author: ");
            author = Console.ReadLine();
            bookList -= new Book(title, author);
            Console.WriteLine("Book deleted.");
            break;

        case "3":
            Console.Write("Enter book name: ");
            title = Console.ReadLine();
            Console.Write("Enter book author: ");
            author = Console.ReadLine();
            if (bookList.ContainsBook(new Book(title, author)))
            {
                Console.WriteLine("Book is in the list.");
            }
            else
            {
                Console.WriteLine("Book`s not on the list.");
            }
            break;

        case "4":
            bookList.DisplayBooks();
            break;

        case "5":
            return;

        default:
            Console.WriteLine("Wrong choice. Try again or die!");
            break;
    }
}







// Book 
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }
}

// Book list
class BookList
{
    private List<Book> books = new List<Book>();

    // Book indexator
    public Book this[int index]
    {
        get => books[index];
        set => books[index] = value;
    }

    // Add a book
    public void AddBook(Book book)
    {
        books.Add(book);
    }

    // Delete a book
    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    // Check if the book is here
    public bool ContainsBook(Book book)
    {
        return books.Contains(book);
    }

    // Operator + overload to add
    public static BookList operator +(BookList bookList, Book book)
    {
        bookList.AddBook(book);
        return bookList;
    }

    // Operator - overload to delete
    public static BookList operator -(BookList bookList, Book book)
    {
        bookList.RemoveBook(book);
        return bookList;
    }

    // List display
    public void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("List empty");
        }
        else
        {
            Console.WriteLine("Book list:");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
    }
}

