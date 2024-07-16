namespace MyBooks.Model.V2;

/// <summary>
/// Represents a book with a title, author, publication year, category, and an identifier.
/// </summary>
public class Book
{
    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Gets or sets the year the book was published.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the book.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the category of the book.
    /// </summary>
    public string Category { get; set; }
}
