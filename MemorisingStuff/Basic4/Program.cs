record Books (string title, string author, int yearPublished, bool isCheckedOut);

class Program
{
   static void Main()
   {

        var books = new List<Books>
        {
            new ("The Great Gatsby", "F. Scott Fitzgerald", 1925, false),
            new ("To Kill a Mockingbird", "Harper Lee", 1960, true),
            new ("1984", "George Orwell", 1949, false),
            new ("Pride and Prejudice", "Jane Austen", 1813, true),
            new ("The Catcher in the Rye", "J.D. Salinger", 2011, false),
        };

        Console.WriteLine("Books published after the year 2000");
        foreach (var book in books.Where(b => b.yearPublished > 2000))
        {
            Console.WriteLine($"{book.title}, {book.author}, {book.yearPublished}, {book.isCheckedOut}");
        }

        Console.WriteLine("\nBooks that are checked out");
        foreach (var book in books.Where(b => b.isCheckedOut))
        {
            Console.WriteLine($"{book.title}, {book.author}, {book.yearPublished}, {book.isCheckedOut}");
        }

        Console.WriteLine("\nEnter author name to search:");
        string authorToFind = Console.ReadLine();
        Console.WriteLine($"Books by {authorToFind}");
        foreach (var book in books.Where(b => b.author.Contains(authorToFind, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"{book.title}, {book.author}, {book.yearPublished}, {book.isCheckedOut}");
        }
    }
}
