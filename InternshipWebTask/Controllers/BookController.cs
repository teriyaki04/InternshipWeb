using InternshipWebTask.Data;
using InternshipWebTask.Models;
using InternshipWebTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipWebTask.Controllers;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "Author", new Author{Id = book.AuthorId});
    }
}