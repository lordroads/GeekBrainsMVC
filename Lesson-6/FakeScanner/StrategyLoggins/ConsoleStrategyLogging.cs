using System.Text;

namespace FakeScanner.StrategyLoggins;

public class ConsoleStrategyLogging : IStrategyLogging
{
    public void WriteLog(byte[] log)
    {
        string logString = Encoding.ASCII.GetString(log);

        Console.WriteLine(logString);
    }
}

