using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyLibrary.Model.V2;
using MyLibrary.Data.V2;

namespace MyLibrary.Controllers.V2;

/// <summary>
/// Controller for managing Authors. Version 2.0.
/// </summary>

[ApiVersion("2.0")]
[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    /// <summary>Returns a list of books by a specific author.</summary>
    /// <returns>A list of books by the author, if no book found returns 404.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [Route("{author}/books")]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    public IActionResult GetBooksByAuthor(string author)
    {
        var books = BooksMock.GetBooks().Where<Book>(b => b.Author == author);
        if (books.Count() == 0)
        {
            return NotFound($"No books found by Author: '{author}'");
        }
        return Ok(books);

    }

    /// <summary>Returns a list of authors.</summary>
    /// <returns>A list of authors.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(200, Type = typeof(List<String>))]
    [Produces("application/json")]
    public IActionResult GetAuthors()
    {
        
        var authors = BooksMock.GetBooks().Select(b => b.Author).Distinct().OrderBy(c => c);
        return Ok(authors.ToArray<string>());
    }
}
