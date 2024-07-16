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
public class AuthorController : Controller
{
    [HttpGet]
    [MapToApiVersion("2.0")]
    [Route("{Author}/books")]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    public IActionResult GetBooksByAuthor(string author)
    {
        var books = BooksMock.GetBooks().Where<Book>(b => b.Author == author);
        return Ok(books);

    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(200, Type = typeof(List<String>))]
    [Produces("application/json")]
    public IActionResult GetCategories()
    {
        // Get a list of categories from the books list unique ordered
        var categories = BooksMock.GetBooks().Select(b => b.Author).Distinct().OrderBy(c => c);
        return Ok(categories.ToArray<string>());
    }
}
