using EmployeesWorkerTime.Model;

namespace EmployeesWorkerTime.EmployeeCardFinancy;


public class ConcreteEmployeeBetInHour : AbstractEmployeeCard
{
    public ConcreteEmployeeBetInHour(BetType unit) : base(unit)
    {
    }

    protected override decimal SalaryСalculation()
    {
        decimal salary = 20.8M * 8 * Bet;
        return salary;
    }
}
