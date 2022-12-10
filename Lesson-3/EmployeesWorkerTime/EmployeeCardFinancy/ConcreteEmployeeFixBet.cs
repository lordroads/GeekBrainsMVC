


public class ConcreteEmployeeFixBet : AbstractEmployeeCard
{
    public ConcreteEmployeeFixBet(Employee employee, decimal bet, BetType unit) : base(employee, bet, unit)
    {
    }

    protected override decimal SalaryСalculation()
    {
        return Bet;
    }
}
