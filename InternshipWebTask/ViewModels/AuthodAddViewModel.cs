using System.ComponentModel.DataAnnotations;

namespace InternshipWebTask.ViewModels;

public class AuthorAddViewModel
{
    [Required]
    [Display(Name = "Фамилия")]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [Display(Name = "Имя")]
    [StringLength(20)]
    public string FirstName { get; set; }
    
    [Display(Name = "Отчество")]
    [StringLength(50)]
    public string? Patronymic { get; set; } 
    
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата рождения")]
    public DateTime DateOfBirth { get; set; }
}