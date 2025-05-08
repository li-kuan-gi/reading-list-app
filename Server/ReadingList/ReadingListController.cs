using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.ReadingList;

[ApiController]
[Route("api/reading-list")]
public class ReadingListController(ReadingListContext context) : ControllerBase
{
    private readonly ReadingListContext _context = context;

    [HttpPost("add")]
    public async Task<ActionResult<BookDTO>> AddBookToReadingList(AddingBookDTO dto)
    {
        string? error = dto.Validate();

        if (error is not null)
        {
            return BadRequest(new { error });
        }

        Book book = new()
        {
            Title = dto.Title,
            Author = dto.Author,
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return StatusCode(201, new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
        });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetReadingList()
    {
        IEnumerable<BookDTO> books = await _context.Books.Select(book => new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
        }).ToListAsync();

        return Ok(books);
    }
}
