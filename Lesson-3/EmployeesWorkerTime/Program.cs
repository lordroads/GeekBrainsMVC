Random random = new Random();
int countEmployee = 10;

AbstractEmployeeCard[] abstractEmployeeCards = new AbstractEmployeeCard[countEmployee];

for (int i = 0; i < countEmployee; i++)
{
    if (random.Next(0, 100) % 2 == 0)
    {
        abstractEmployeeCards[i] = new ConcreteEmployeeFixBet(new Employee($"FirstName_{i + 1}", $"LastName_{i + 1}", $"MiddleName_{i + 1}"), random.Next(50000, 100000), BetType.BET_IN_MOUNT);
    }
    else
    {
        abstractEmployeeCards[i] = new ConcreteEmployeeBetInHour(new Employee($"FirstName_{i + 1}", $"LastName_{i + 1}", $"MiddleName_{i + 1}"), random.Next(350, 550), BetType.BET_IN_HOUR);
    }
}

foreach (var employeeCard in abstractEmployeeCards)
{
    Console.WriteLine(employeeCard);
}
