using System.ComponentModel.DataAnnotations;

namespace InternshipWebTask.ViewModels;

public class BookAddViewModel
{
    [Required]
    [Display(Name = "Название книги")]
    public string BookName { get; set; }
    
    [Required]
    [Display(Name = "Количество страниц")]
    [Range(1, int.MaxValue, ErrorMessage = "В книге не может быть меньше 1 страницы")]
    public int PageQty { get; set; }
    
    [Required]
    [Display(Name = "Жанр")]
    [RegularExpression("^(Хоррор|Триллер|Фентези)$", ErrorMessage = "Недопустимый жанр.")]
    public string Genre { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
}