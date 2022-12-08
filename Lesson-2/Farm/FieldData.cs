










class FieldData
{
    public char[,] Field { get; set; }
    public int Rows  { get; set; }
    public int Columns  { get; set; }

    public FieldData(int rows, int columns)
    {
        Field = new char[rows, columns];
        Rows = rows;
        Columns = columns;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Field[i, j] = '.';
            }
        }
    }
}
