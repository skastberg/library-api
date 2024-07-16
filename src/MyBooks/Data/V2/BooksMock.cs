using MyBooks.Model.V2;
using Newtonsoft.Json;

namespace MyBooks.Data.V2;

public static class BooksMock
{

    // Get a list of books
    public static List<Book> GetBooks()
    {
        string path = "Data/V2/books.v2.json";
        string content = "";
        if (!File.Exists(path))
        {
            return new List<Book>();
        }
        content = File.ReadAllText(path);
        var books = JsonConvert.DeserializeObject<List<Book>>(content);
        if (books == null)
        {
            return new List<Book>();
        }
        // Return a list of books, read the list of books from a json file  named books.v1.json
        return books;
    }

    // Get a book by its id returning a tuple with a boolean indicating if the book was found, the provided id and the book itself
    public static (bool, string, Book?) GetBook(string id)
    {
        var books = GetBooks();
        var book = books.FirstOrDefault(b => b.Id == id);
        return (book != null, id, book);
    }

}
