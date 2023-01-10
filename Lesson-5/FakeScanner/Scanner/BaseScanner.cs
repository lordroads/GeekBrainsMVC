namespace FakeScanner.Scanner;

/// <summary>
/// Паттерны Наблюдатель, Шаблонный метод (Фабричный метод).
/// </summary>
public abstract class BaseScanner : IDisposable
{
    protected Timer _timer;
    protected TimeSpan _timeInterval = TimeSpan.FromSeconds(1);
    protected List<Action<byte[]>> _observers;

    public BaseScanner()
    {
        _observers = new List<Action<byte[]>>();
    }

    /// <summary>
    /// Запуск фейкого процесса сканирования процессора и памяти.
    /// </summary>
    public virtual void Start()
    {
        _timer = new Timer(callback => WorkingProcces(), null, _timeInterval, _timeInterval);

        Console.WriteLine("Scanner start!");
    }

    /// <summary>
    /// Добавление наблюдателя для получения фейковых данных.
    /// </summary>
    /// <param name="observer">Метод который будет вызываться каждый раз в указанный интервал времени.</param>
    public abstract void AddObserver(Action<byte[]> observer);

    /// <summary>
    /// Рабочий процесс генерации фейковых данных.
    /// </summary>
    protected abstract void WorkingProcces();

    public void Dispose()
    {
        if (_timer is not null)
        {
            _timer.Dispose();
        }
    }
}

