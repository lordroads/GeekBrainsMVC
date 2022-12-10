


public abstract class AbstractEmployeeCard
{
    public Employee Employee { get; }
    public BetType UnitOfMeasure { get; set; }
    public decimal Bet { get; set; }

    public AbstractEmployeeCard(Employee employee, decimal bet, BetType unitOfMeasure)
    {
        Employee = employee;
        Bet = bet;
        UnitOfMeasure = unitOfMeasure;
    }

    public decimal CalculatingTheAverageMonthlySalary()
    {
        return SalaryСalculation();
    }

    protected abstract decimal SalaryСalculation();

    public override string ToString()
    {
        return $"{Employee} - {SalaryСalculation()} /{UnitOfMeasure}/";
    }
}
