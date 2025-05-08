using Microsoft.EntityFrameworkCore;

namespace Server.ReadingList;

public class ReadingListContext(DbContextOptions<ReadingListContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; } = null!;
}
