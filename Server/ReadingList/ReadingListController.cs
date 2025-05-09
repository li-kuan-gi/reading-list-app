using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.ReadingList;

[ApiController]
[Route("api/reading-list")]
public class ReadingListController(ReadingListContext context, ILogger<ReadingListController> logger) : ControllerBase
{
    private readonly ReadingListContext _context = context;
    private readonly ILogger<ReadingListController> _logger = logger;

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

    [HttpPost("{id}/delete")]
    public async Task<ActionResult> DeleteBookFromReadingList(long id)
    {
        int maxAttempts = 3;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Book? book = await _context.Books.FindAsync(id);

            if (book is null)
            {
                return NotFound(new { error = $"Book with id {id} not found." });
            }

            _context.Books.Remove(book);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning("Concurrency conflict while deleting book with id {Id}. Attempt {Attempt} of {MaxAttempts}.", id, attempt + 1, maxAttempts);

                if (attempt == maxAttempts - 1)
                {
                    _logger.LogError(
                        "Failed to delete book with id {Id} after {MaxAttempts} attempts due to concurrency conflicts.",
                        id, maxAttempts
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred while deleting book with id {Id}: {ErrorMessage}", id, ex.Message);
                throw;
            }
        }

        throw new Exception("Failed to delete book after multiple attempts.");
    }
}
