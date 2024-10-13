using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyLibrary.Model.V1;
using MyLibrary.Data.V1;

namespace MyLibrary.Controllers.V1;


/// <summary>
/// Controller for managing books. Version 1.0.
/// </summary>
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BookController : Controller
{
    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>A list of books.</returns>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
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
    [Route("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(200, Type = typeof(Book))]
    [ProducesResponseType(404)]
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
