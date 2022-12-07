const int COUNT_FARMERS = 2;
Console.Write("Укажите размер поля по оси X: ");
int x = Convert.ToInt32(Console.ReadLine());

Console.Write("Укажите размер поля по оси Y: ");
int y = Convert.ToInt32(Console.ReadLine());

FieldData field = new FieldData(y, x);
AutoResetEvent[] autoResetEvents = new AutoResetEvent[COUNT_FARMERS];

for (int i = 0; i < COUNT_FARMERS; i++)
{
    autoResetEvents[i] = new AutoResetEvent(false);
    WaitCallback waitCallback = FarmerFactory.GetFarmer(i);
    if (waitCallback is not null)
    {
        ThreadPool.QueueUserWorkItem(waitCallback, new FieldDataControl(field, autoResetEvents[i]));
    }
}

AutoResetEvent.WaitAll(autoResetEvents);

ViewField(field);



void ViewField(FieldData field)
{
    for (int i = 0; i < field.Rows; i++)
    {
        for (int j = 0; j < field.Columns; j++)
        {
            Console.Write($"{field.Field[i, j]} \t");
        }
        Console.WriteLine();
    }
}
