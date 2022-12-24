namespace FakeScanner.StrategyLoggins;

/// <summary>
/// Контракт формата логирования.
/// </summary>
public interface IStrategyLogging
{
    void WriteLog(byte[] log);
}

