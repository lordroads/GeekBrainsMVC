using EmployeesWebApplication.Models;

namespace EmployeesWebApplication.Servises.Impl;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly List<Employee> _employees;
    private int _maxFeedId;

    public EmployeesRepository()
    {
        _employees = Enumerable.Range(1, 10)
            .Select(i =>
                new Employee
                {
                    Id = i,
                    LastName = $"Фамилия-{i}",
                    FirstName = $"Имя-{i}",
                    Patronymic = $"Отчество-{i}",
                    Birthday = DateTime.Now.AddYears(-18 - i)
                })
            .ToList();
        _maxFeedId = _employees.Max(i => i.Id) + 1;
    }

    public int Add(Employee employee)
    {
        employee.Id = _maxFeedId;
        _maxFeedId++;
        _employees.Add(employee);
        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        var currentEmployee = GetById(employee.Id);
        if (currentEmployee is null)
        {
            return false;
        }
        currentEmployee.LastName = employee.LastName;
        currentEmployee.FirstName = employee.FirstName;
        currentEmployee.Patronymic = employee.Patronymic;
        currentEmployee.Birthday = employee.Birthday;

        return true;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employees;
    }

    public Employee? GetById(int id)
    {
        return _employees.FirstOrDefault(employee => employee.Id == id);
    }

    public bool Remove(int id)
    {
        var currentEmployee = GetById(id);
        if (currentEmployee is null)
        {
            return false;
        }

        _employees.Remove(currentEmployee);

        return true;
    }
}