using EmployeesWorkerTime.EmployeeCardFinancy;
using EmployeesWorkerTime.Extention;
using EmployeesWorkerTime.Factories;
using EmployeesWorkerTime.Model;

Random random = new Random();
ConcreteEmployeeCardFactory employeeFactory = new ConcreteEmployeeCardFactory();
int countEmployee = 10;

AbstractEmployeeCard[] abstractEmployeeCards = new AbstractEmployeeCard[countEmployee];

for (int i = 0; i < countEmployee; i++)
{
    BetType betType;
    decimal bet;

    if (random.Next(0, 100) % 2 == 0)
    {
        betType = BetType.BET_IN_MOUNT;
        bet = random.Next(50000, 100000);
    }
    else
    {
        betType = BetType.BET_IN_HOUR;
        bet = random.Next(350, 550);
    }

    abstractEmployeeCards[i] = employeeFactory
            .GetEmployeeCard(betType)
            .SetEmployee(new Employee($"FirstName_{i + 1}", $"LastName_{i + 1}", $"MiddleName_{i + 1}"))
            .SetBet(bet);
}

foreach (var employeeCard in abstractEmployeeCards)
{
    Console.WriteLine(employeeCard);
}
