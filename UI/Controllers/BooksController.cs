using Application;
using Application.Dto;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var books = _bookService.Get();
        return Ok(books);
    }

    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        var book = _bookService.GetByName(name);
        return Ok(book);
    }

    [HttpPost]
    public IActionResult Post([FromBody] BookDto bookDto)
    {
        var submittedBook = _bookService.Create(bookDto);
        var routeValue = new { bookDto.Name };
        var actionName = "GetByName";
        return CreatedAtAction(actionName, routeValue, submittedBook);
    }


    [HttpPatch("AddCount/{name}")]
    public IActionResult PatchCount(string name)
    {
        _bookService.AddCount(name);
        return Ok();
    }

    [HttpPut("{oldName}")]
    public IActionResult Put(string oldName, [FromBody] string bookDtoString)
    {
        var bookDto = JsonSerializer.Deserialize<BookDto>(bookDtoString);
        _bookService.Update(oldName, bookDto);
        return Ok();
    }


    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        _bookService.Delete(name);
        return Ok();
    }
}
