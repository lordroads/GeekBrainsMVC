using FakeScanner.Factories;
using FakeScanner.Logger;
using FakeScanner.Scanner;

//Вариант 1 
new StrategyLogger(new ScannerOfLoad())
    .AddStrategyLogging(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.CONSOLE))
    .AddStrategyLogging(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.FILE))
    .Execute();

//Вариант 2 
ScannerOfLoad scanner = new ScannerOfLoad();
scanner.AddObserver(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.CONSOLE).WriteLog);
scanner.AddObserver(StrategyLoggingsFactory.GetStrategyLogging(LoggerTypes.FILE).WriteLog);
scanner.Start();

Console.ReadKey(true);

