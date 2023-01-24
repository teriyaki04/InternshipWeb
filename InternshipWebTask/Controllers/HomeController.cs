using InternshipWebTask.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipWebTask.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var authors = _context.Authors.ToList();
        var books = _context.Books.ToList();
        
        var authorsInfo = authors
            .Select(a => new { a.Id,a.LastName, a.FirstName, a.Patronymic, Count = books.Count(b => b.AuthorId == a.Id) })
            .ToList();
        
        ViewBag.authors = authorsInfo;
        return View();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var author = _context.Authors.FirstOrDefault(x => x.Id == id);

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Genres()
    {
        var genres = _context.Books.Select(b => b.Genre).Distinct().ToList();
        var books = _context.Books.ToList();

        var genresInfo = genres
            .Select(x => new { Genre = x, Count = books.Count(b => b.Genre == x) })
            .ToList();

        ViewBag.genres = genresInfo;
        return View();
    }
}
