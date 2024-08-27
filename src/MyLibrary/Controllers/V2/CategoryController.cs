using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MyLibrary.Model.V2;
using MyLibrary.Data.V2;

namespace MyLibrary.Controllers.V2;

/// <summary>Controller for managing Categories. Version 2.0.</summary>
[ApiVersion("2.0")]
[Route("api/v{version:apiversion}/[controller]")]
[ApiController]
[Produces( "application/json")]
public class CategoryController : Controller
{
    /// <summary>Returns a list of books by a specific category.</summary>
    /// <returns>A list of books by the category, if no book found returns 404.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [Route("{category}/books")]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [ProducesResponseType(404)]
    public IActionResult GetBooksByCategory(string category)
    {
        var books = BooksMock.GetBooks().Where<Book>(b => b.Category == category);
        if (books.Count() == 0)
        {
            return NotFound($"No books found in Category: '{category}'");
        }
        return Ok(books);

    }

    /// <summary>Returns a list of categories.</summary>
    /// <returns>A list of categories.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(200, Type = typeof(List<String>))]
    public IActionResult GetCategories()
    {
        // Get a list of categories from the books list unique ordered
        var categories = BooksMock.GetBooks().Select(b => b.Category).Distinct().OrderBy(c => c);
        return Ok(categories.ToArray<string>());
    }
}
