using System.Text;

namespace FakeScanner.StrategyLoggins;

public class FileStrategyLogging : IStrategyLogging
{
    public void WriteLog(byte[] log)
    {
        string logString = Encoding.ASCII.GetString(log);

        File.AppendAllText("scanner.log", logString + "\n");
    }
}

