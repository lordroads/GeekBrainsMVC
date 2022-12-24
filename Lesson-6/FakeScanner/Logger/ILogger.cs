using FakeScanner.Logger;
using FakeScanner.StrategyLoggins;

public interface ILogger
{
    ILogger AddStrategyLogging(IStrategyLogging? strategyLogging);
    void Execute();
}