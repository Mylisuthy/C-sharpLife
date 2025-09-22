using RiwiMusic.Services;

namespace RiwiMusic.Utils;

public class IdGenerator
{
    public static int GenerateConcertId()
    {
        if (DataStore.concerts.Count == 0)
            return 1;

        return DataStore.concerts.Keys.Max() + 1;
    }

    public static int GenerateCustomerId()
    {
        if (DataStore.customers.Count == 0)
            return 1;

        return DataStore.customers.Keys.Max() + 1;
    }

    public static int GenerateTicketId()
    {
        if (DataStore.tikets.Count == 0)
            return 1;

        return DataStore.tikets.Keys.Max() + 1;
    }
}