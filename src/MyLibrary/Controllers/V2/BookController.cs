using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyLibrary.Model.V2;
using MyLibrary.Data.V2;

namespace MyLibrary.Controllers.V2;


/// <summary>
/// Controller for managing books. Version 2.0.
/// </summary>

[ApiVersion("2.0")]
[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>A list of books.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    // Return a list of books
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [Produces("application/json")]
    public IActionResult Get()
    {
        var books = BooksMock.GetBooks();
        return Ok(books);

    }

    /// <summary>
    /// Retrieves a specific book by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    /// <returns>The book if found; otherwise, a 404 Not Found.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(Book))]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    public IActionResult Get(string id)
    {
        var (found, _, book) = BooksMock.GetBook(id);
        if (!found)
        {
            return NotFound();
        }
        return Ok(book);
    }
}
