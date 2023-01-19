using EmployeesWebApplication.Models;

namespace EmployeesWebApplication.Servises;

public interface IEmployeesRepository
{
    public IEnumerable<Employee> GetAll();
    public Employee? GetById(int? id);
    public int Add(Employee employee);
    public bool Edit(Employee employee);
    public bool Remove(int id);
}