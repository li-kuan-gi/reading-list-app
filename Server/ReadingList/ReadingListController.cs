using Microsoft.AspNetCore.Mvc;

namespace Server.ReadingList;

[ApiController]
[Route("api/reading-list")]
public class ReadingListController() : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult<BookInfo>> AddBookToReadingList(AddingBookDTO dto)
    {
        string? error = dto.Validate();

        if (error is not null)
        {
            return BadRequest(new { error });
        }

        throw new NotImplementedException();
    }
}
