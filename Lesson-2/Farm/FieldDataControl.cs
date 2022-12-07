










class FieldDataControl
{
    public FieldData Place { get; set; }
    public AutoResetEvent WaitHandler { get; set; }

    public FieldDataControl(FieldData place, AutoResetEvent waitHandler)
    {
        Place = place;
        WaitHandler = waitHandler;
    }
}