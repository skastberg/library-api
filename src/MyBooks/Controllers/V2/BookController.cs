using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyBooks.Model.V2;
using MyBooks.Data.V2;

namespace MyBooks.Controllers.V2;


[ApiVersion(2)]
[ApiVersion("2.0")]
[Route("api/v{version:apiversion}/[controller]")]
[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
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
