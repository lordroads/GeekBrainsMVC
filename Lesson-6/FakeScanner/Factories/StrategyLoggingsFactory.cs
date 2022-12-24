using FakeScanner.Logger;
using FakeScanner.StrategyLoggins;

namespace FakeScanner.Factories;

/// <summary>
/// Паттерн Фабрика
/// </summary>
public class StrategyLoggingsFactory
{
    /// <summary>
    /// Получение реализацию контракта IStrategyLogging по формату логирования.
    /// </summary>
    /// <param name="loggerTypes">Формат логгирования данных.</param>
    /// <returns></returns>
    public static IStrategyLogging? GetStrategyLogging(LoggerTypes loggerTypes)
    {
        return GetStrategyLoggingInstance(loggerTypes);
    }

    private static IStrategyLogging? GetStrategyLoggingInstance(LoggerTypes loggerTypes)
    {
        switch (loggerTypes)
        {
            case LoggerTypes.CONSOLE:
                return new ConsoleStrategyLogging();
            case LoggerTypes.FILE:
                return new FileStrategyLogging();
            default:
                return null;
        }
    }
}

