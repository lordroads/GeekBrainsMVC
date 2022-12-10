using EmployeesWorkerTime.EmployeeCardFinancy;
using EmployeesWorkerTime.Model;

namespace EmployeesWorkerTime.Factories;

public class ConcreteEmployeeCardFactory : BaseEmployeeCardFactiry
{
    protected override AbstractEmployeeCard? GetEmployeeCardInstance(BetType betType)
    {
        switch (betType)
        {
            case BetType.BET_IN_MOUNT:
                return new ConcreteEmployeeFixBet(BetType.BET_IN_MOUNT);
            case BetType.BET_IN_HOUR:
                return new ConcreteEmployeeBetInHour(BetType.BET_IN_HOUR);
            default:
                return null;
        }
    }
}