namespace Server.ReadingList;

public class Book
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
