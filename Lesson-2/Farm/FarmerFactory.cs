class FarmerFactory
{
    public static WaitCallback? GetFarmer(int farmerNumber)
    {
        switch (farmerNumber)
        {
            case 0: 
                WaitCallback waitCallback = new WaitCallback(Farmer_1);
                return waitCallback;
            case 1:
                return new WaitCallback(Farmer_2);
            default:
                return null;
        }
    }

    private static void Farmer_1(object obj)
    {
        char markerFarmer = 'X';

        if (obj is not null && obj is FieldDataControl)
        {
            FieldDataControl placeDataControl = (FieldDataControl)obj;

            int rows = placeDataControl.Place.Rows;
            int columns = placeDataControl.Place.Columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    lock (placeDataControl.Place.Field)
                    {
                        if (placeDataControl.Place.Field[i, j] == '.')
                        {
                            placeDataControl.Place.Field[i, j] = markerFarmer;
                        }
                    }

                    Thread.Sleep(10);
                }
            }

            placeDataControl.WaitHandler.Set();
        }
    }

    private static void Farmer_2(object obj)
    {
        char markerFarmer = 'O';

        if (obj is not null && obj is FieldDataControl)
        {
            FieldDataControl placeDataControl = (FieldDataControl)obj;

            int rows = placeDataControl.Place.Rows;
            int columns = placeDataControl.Place.Columns;

            for (int i = columns - 1; i > 0; i--)
            {
                for (int j = rows - 1; j > 0; j--)
                {
                    lock (placeDataControl.Place.Field)
                    {
                        if (placeDataControl.Place.Field[j, i] == '.')
                        {
                            placeDataControl.Place.Field[j, i] = markerFarmer;
                        }
                    }

                    Thread.Sleep(10);
                }
            }

            placeDataControl.WaitHandler.Set();
        }
    }
}
