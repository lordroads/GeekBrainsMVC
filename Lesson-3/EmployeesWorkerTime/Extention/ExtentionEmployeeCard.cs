using EmployeesWorkerTime.EmployeeCardFinancy;
using EmployeesWorkerTime.Model;

namespace EmployeesWorkerTime.Extention;

public static class ExtentionEmployeeCard
{
    public static AbstractEmployeeCard SetEmployee(this AbstractEmployeeCard _currentEmployee, Employee employee)
    {
        _currentEmployee.Employee = employee;
        return _currentEmployee;
    }
    public static AbstractEmployeeCard SetBet(this AbstractEmployeeCard _currentEmployee, decimal bet)
    {
        _currentEmployee.Bet = bet;
        return _currentEmployee;
    }
}