using Microsoft.AspNetCore.Mvc;

namespace Server.ReadingList;

[ApiController]
[Route("api/reading-list")]
public class ReadingListController() : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult<BookInfo>> AddBookToReadingList(AddingBookDTO dto)
    {
        throw new NotImplementedException();
    }
}
