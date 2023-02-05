using InternshipWebTask.Data;
using InternshipWebTask.Models;
using InternshipWebTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InternshipWebTask.Controllers;
    
public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;
    private static readonly List<Book> _books = new List<Book>();

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AuthorAddViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var author = new Author
        {
            FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth,
            Patronymic = model.Patronymic
        };

        await _context.AddAsync(author);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        return View(author);
    }

    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var author = _context.Authors.FirstOrDefault(x => x.Id == id);
        var model = new AuthorAddViewModel
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            Patronymic = author.Patronymic,
            DateOfBirth = author.DateOfBirth
        };
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(AuthorAddViewModel model,Guid id)
    {
        if (!ModelState.IsValid) return View(model);

        var author = _context.Authors.FirstOrDefault(x => x.Id == id);

        if (author == null) return NotFound();

        author.FirstName = model.FirstName;
        author.LastName = model.LastName;
        author.Patronymic = model.Patronymic;
        author.DateOfBirth = model.DateOfBirth;

        _context.Authors.Update(author);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public IActionResult AddBook(BookAddViewModel model)
    {
        if (!ModelState.IsValid) return PartialView("_AddBookPartial",model);
    
        var book = new Book
        {
            BookName = model.BookName,
            PageQty = model.PageQty,
            Genre = model.Genre,
            AuthorId = model.AuthorId
        };

        _books.Add(book);
        
        return RedirectToAction("Details", "Author", new  Author {Id = model.AuthorId});
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateBook(BookAddViewModel model, Guid id)
    {
        if (!ModelState.IsValid) return PartialView("_UpdateBookPartial", model);

        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        if (book == null) return NotFound();
        model.AuthorId = book.AuthorId;

        book.BookName = model.BookName;
        book.PageQty = model.PageQty;
        book.Genre = model.Genre;


        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Author", new Author {Id = model.AuthorId});
    }

    [HttpGet]
    public async Task<IActionResult> SaveBooksToDb(Guid id)
    {
        var books = _books.Where(x => x.AuthorId == id).ToList();
        if (books.Count == 0) return RedirectToAction("Details", "Author", new Author {Id = id});
        
        await _context.Books.AddRangeAsync(books);
        await _context.SaveChangesAsync();
        _books.RemoveAll(x => x.AuthorId == id);

        return RedirectToAction("Details", "Author", new Author {Id = id});
    }

    [HttpGet]
    public IActionResult ResetBookCount(Guid id)
    {
        var books = _books.Where(x => x.AuthorId == id).ToList();
        if (books.Count == 0) return RedirectToAction("Details", "Author", new Author {Id = id});

        _books.RemoveAll(x => x.AuthorId == id);
        return RedirectToAction("Details", "Author", new Author {Id = id});
    }
}