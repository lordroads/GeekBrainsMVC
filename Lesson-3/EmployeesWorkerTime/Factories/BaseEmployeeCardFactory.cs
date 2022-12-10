using EmployeesWorkerTime.EmployeeCardFinancy;

namespace EmployeesWorkerTime.Factories;

public abstract class BaseEmployeeCardFactiry
{
    public AbstractEmployeeCard GetEmployeeCard(BetType betType)
    {
        return GetEmployeeCardInstance(betType);
    }
    protected abstract AbstractEmployeeCard GetEmployeeCardInstance(BetType betType);
}