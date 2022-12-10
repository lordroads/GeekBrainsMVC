using EmployeesWorkerTime.Model;
using System.Runtime.CompilerServices;

namespace EmployeesWorkerTime.EmployeeCardFinancy;


public class ConcreteEmployeeFixBet : AbstractEmployeeCard
{
    public ConcreteEmployeeFixBet(BetType unit) : base(unit)
    {
    }

    protected override decimal SalaryСalculation()
    {
        return Bet;
    }
}
