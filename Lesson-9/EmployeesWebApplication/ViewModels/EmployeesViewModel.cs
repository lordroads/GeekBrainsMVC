using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeesWebApplication.ViewModels;

public class EmployeesViewModel :IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Является обязвтельным"), 
     DisplayName("Фамилия"),
     StringLength(100, MinimumLength = 2, ErrorMessage = "Максимум - 100, минимум - 2"),
     RegularExpression(@"([А-яЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Является обязвтельным"),
     DisplayName("Имя"),
     StringLength(100, MinimumLength = 2, ErrorMessage = "Максимум - 100, минимум - 2")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Является обязвтельным")]
    [DisplayName("Отчество")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Максимум - 100, минимум - 2")]
    public string Patronymic { get; set; }

    public DateTime Birthday { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LastName == "Иванов" & Birthday <= DateTime.Now.AddYears(-30))
        {
            yield return new ValidationResult("Иванов слишком стар для этого всего", new[] { "LastName", "Birthday" });
        }

        yield break;
    }
}