namespace InternshipWebTask.Models;

public class Book
{
    public Book()
    {
        Id = new Guid();
    }
    public Guid Id { get; set; }
    
    public string BookName { get; set; }
    
    public int PageQty { get; set; }
    
    public string Genre { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public Author Author { get; set; }
}