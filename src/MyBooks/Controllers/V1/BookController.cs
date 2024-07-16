using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyBooks.Model.V1;
using MyBooks.Data.V1;

namespace MyBooks.Controllers.V1;


//[ApiVersion(1)]
//[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
//[Route("api/v{version:apiversion}/[controller]")]
[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>A list of books.</returns>
    [HttpGet]
    [MapToApiVersion("1.0")]
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
    [Route("{id}")]
    [MapToApiVersion("1.0")]
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
