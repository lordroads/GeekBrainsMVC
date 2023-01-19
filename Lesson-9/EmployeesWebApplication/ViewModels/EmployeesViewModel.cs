using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeesWebApplication.ViewModels;

public class EmployeesViewModel :IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "�������� ������������"), 
     DisplayName("�������"),
     StringLength(100, MinimumLength = 2, ErrorMessage = "�������� - 100, ������� - 2"),
     RegularExpression(@"([�-��][�-��\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "�������� ������")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "�������� ������������"),
     DisplayName("���"),
     StringLength(100, MinimumLength = 2, ErrorMessage = "�������� - 100, ������� - 2")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "�������� ������������")]
    [DisplayName("��������")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "�������� - 100, ������� - 2")]
    public string Patronymic { get; set; }

    public DateTime Birthday { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LastName == "������" & Birthday <= DateTime.Now.AddYears(-30))
        {
            yield return new ValidationResult("������ ������� ���� ��� ����� �����", new[] { "LastName", "Birthday" });
        }

        yield break;
    }
}