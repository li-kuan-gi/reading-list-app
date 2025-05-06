namespace Server.ReadingList;

public class AddingBookDTO
{
    public required string Title { get; set; }
    public required string Author { get; set; }

    public string? Validate()
    {
        if(Title.Length == 0) {
            return "Title cannot be empty";
        }
        if(Author.Length == 0) {
            return "Author cannot be empty";
        }
        return null;
    }
}
