namespace InternshipWebTask.Models;

public class Author
{
    public Author()
    {
        Id = new Guid();
    }
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateTime DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; }
}
