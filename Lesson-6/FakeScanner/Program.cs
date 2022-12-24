using FakeScanner.Factories;
using FakeScanner.Logger;
using FakeScanner.Scanner;
using Autofac;
using FakeScanner.StrategyLoggins;

var builder = new ContainerBuilder();
builder.RegisterType<StrategyLogger>().As<ILogger>();
builder.RegisterType<ScannerOfLoad>().As<BaseScanner>();
builder.RegisterType<ConsoleStrategyLogging>().As<IStrategyLogging>();
builder.RegisterType<FileStrategyLogging>().As<IStrategyLogging>();

IContainer container = builder.Build();

//Вариант 1 
ILogger logger = container.Resolve<ILogger>();
logger
    .AddStrategyLogging(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.CONSOLE))
    .AddStrategyLogging(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.FILE))
    .Execute();

//Вариант 2 
BaseScanner scanner = container.Resolve<BaseScanner>();

IReadOnlyList<IStrategyLogging> strategyLoggings = container.Resolve<IEnumerable<IStrategyLogging>>().ToList();

foreach (IStrategyLogging strategy in strategyLoggings)
{
    scanner.AddObserver(strategy.WriteLog);
}

scanner.Start();

Console.ReadKey(true);

