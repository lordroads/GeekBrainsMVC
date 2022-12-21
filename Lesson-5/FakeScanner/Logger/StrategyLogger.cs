using FakeScanner.Scanner;
using FakeScanner.StrategyLoggins;

namespace FakeScanner.Logger;

/// <summary>
/// Паттерн Стратегия, Строитель
/// </summary>
public class StrategyLogger
{
    private readonly BaseScanner _scanner;
    private List<IStrategyLogging> _strategyLoggings;

    public StrategyLogger(BaseScanner scanner)
    {
        _scanner = scanner;
        _strategyLoggings = new List<IStrategyLogging>();
    }

    /// <summary>
    /// Добавить формат логирования данных. 
    /// </summary>
    /// <param name="strategyLogging">Реализация котракта IStrategyLogging.</param>
    /// <returns></returns>
    public StrategyLogger AddStrategyLogging(IStrategyLogging? strategyLogging)
    {
        _strategyLoggings.Add(strategyLogging);

        return this;
    }

    /// <summary>
    /// Иницилизация добавление всех наблюдательлей для логирования и запуск сканера.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public void Execute()
    {
        if (_scanner is null)
        {
            throw new ArgumentNullException("Scanner can not be null");
        }


        foreach (var strategy in _strategyLoggings)
        {
            if (strategy is null)
            {
                throw new ArgumentNullException("Strategy Logger not be null");
            }

            _scanner.AddObserver(strategy.WriteLog);
        }

        _scanner.Start();
    }
}

