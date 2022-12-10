


public class ConcreteEmployeeBetInHour : AbstractEmployeeCard
{
    public ConcreteEmployeeBetInHour(Employee employee, decimal bet, BetType unit) : base(employee, bet, unit)
    {
    }

    protected override decimal SalaryСalculation()
    {
        decimal salary = 20.8M * 8 * Bet;
        return salary;
    }
}
