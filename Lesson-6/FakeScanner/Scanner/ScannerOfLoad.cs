using System.Text;

namespace FakeScanner.Scanner;

public class ScannerOfLoad : BaseScanner
{
    public override void AddObserver(Action<byte[]> observer)
    {
        _observers.Add(observer);
    }

    protected override void WorkingProcces()
    {
        Random random = new Random();

        int processor = random.Next(0, 100);
        int memory = random.Next(100, 32000);

        byte[] bytes = Encoding.UTF8.GetBytes($"Processor loaded: {processor} %, Memory loaded: {memory} MB.");

        foreach (var observer in _observers)
        {
            observer.Invoke(bytes);
        }
    }
}

