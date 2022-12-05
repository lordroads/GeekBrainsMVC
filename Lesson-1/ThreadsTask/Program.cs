Task1();
Task2();

void Task1()
{
    float[] arr = new float[100_000_000];

    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = 1;
    }

    DateTime start = DateTime.Now;

    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = (float)(arr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
    }

    DateTime finish = DateTime.Now;

    Console.WriteLine($"Инициализация массива заняла у Метода Task1():  {finish - start} сек.");
}

void Task2()
{
    int countThreads = 6;
    float[] arr = new float[100_000_000];

    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] = 1;
    }

    DateTime start = DateTime.Now;

    AutoResetEvent[] autoResetEvents = new AutoResetEvent[countThreads];
    Thread[] threads = new Thread[countThreads];

    for (int i = 0; i < threads.Length; ++i)
    {
        int basket = arr.Length / threads.Length;
        float[] newArr = new float[basket];

        autoResetEvents[i] = new AutoResetEvent(false);

        Console.WriteLine($"Init Task #{i}");

        threads[i] = new Thread((o) =>
        {
            int count = int.Parse(Thread.CurrentThread.Name);
            int index = basket * count;

            System.Array.Copy(arr, index, newArr, 0, basket);

            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = (float)(newArr[i] * Math.Sin(0.2f + i / 5) * Math.Cos(0.2f + i / 5) * Math.Cos(0.4f + i / 2));
            }

            System.Array.Copy(newArr, 0, arr, index, basket);

            if (o is not null && o is AutoResetEvent)
            {
                ((AutoResetEvent)o).Set();
            }
        });
        
        //TODO: Решил схалявить так как в условии задачи просили работать с Thread и хотелось как то более гибким решение сделать,
        //если переделать через делегат будет смотреться лучше =) типа => delegate void TestMath1(AutoResetEvent autoResetEvent, int count);
        //или delegate float[] TestMath2(AutoResetEvent autoResetEvent, float[] arr);
        threads[i].Name = i.ToString(); 
        
        threads[i].Start(autoResetEvents[i]);
    }

    AutoResetEvent.WaitAll(autoResetEvents);

    DateTime finish = DateTime.Now;

    Console.WriteLine($"Инициализация массива заняла у Метода Task2():  {finish - start} сек.");
}
