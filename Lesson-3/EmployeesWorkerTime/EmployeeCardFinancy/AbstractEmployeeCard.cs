using EmployeesWorkerTime.Model;

namespace EmployeesWorkerTime.EmployeeCardFinancy;


public abstract class AbstractEmployeeCard
{
    public Employee Employee { get; set; }
    public BetType UnitOfMeasure { get; set; }
    public decimal Bet { get; set; }

    public AbstractEmployeeCard(BetType unitOfMeasure)
    {
        UnitOfMeasure = unitOfMeasure;
    }

    public decimal CalculatingTheAverageMonthlySalary()
    {
        return SalaryСalculation();
    }

    protected abstract decimal SalaryСalculation();

    public override string ToString()
    {
        return $"{Employee}\t{SalaryСalculation()}\t\t/Type bet: {UnitOfMeasure}/";
    }
}
