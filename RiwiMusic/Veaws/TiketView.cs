using RiwiMusic.Services;
using RiwiMusic.Controllers;
using RiwiMusic.Utils;

namespace RiwiMusic.Veaws;

public class TiketView
{
    // Tickets menu
    public static void Menu()
    {
        int selec = 0;
        ConsoleKey key;

        do
        {
            RenderMenu(selec);
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selec = (selec == 0) ? DataStore.tikets.Count - 1 : selec - 1;
            else if (key == ConsoleKey.DownArrow)
                selec = (selec == DataStore.tikets.Count - 1) ? 0 : selec + 1;

            switch (key)
            {
                case ConsoleKey.X:
                    selec = (selec + 1) % Math.Max(1, DataStore.tikets.Count);
                    break;
                case ConsoleKey.Z:
                    return;
                case ConsoleKey.C:
                    TiketCont.CreateTiket();
                    break;
                case ConsoleKey.V:
                    if (DataStore.tikets.Count > 0)
                        TiketCont.EditTiket(DataStore.tikets.Values.ElementAt(selec).IdTiket);
                    else
                    {
                        Console.WriteLine("No tickets to edit.");
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.M:
                    if (DataStore.tikets.Count > 0)
                        TiketCont.DeleteTiket(DataStore.tikets.ElementAt(selec).Key);
                    else
                    {
                        Console.WriteLine("No tickets to delete.");
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.B:
                    TiketCont.FilterTiketByCustomer();
                    break;
            }

        } while (true);
    }

    // Render tickets list
    private static void RenderMenu(int selec)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Tickets Management ===\n");
        Console.ForegroundColor = ConsoleColor.White;

        var list = DataStore.tikets.Values.ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("No tickets sold.");
            Console.WriteLine("\nC register | Z back");
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var t = list[i];
            if (i == selec)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                string custName = DataStore.customers.TryGetValue(t.IdCustomer, out var c) ? c.Name : "Unknown";
                string concertName = DataStore.concerts.TryGetValue(t.IdConcert, out var s) ? s.Name : "Unknown";

                Console.WriteLine($@"> {t.IdTiket} - {concertName}
    Customer: {custName}
    Info: {t.InfTiket}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {list[i].IdTiket} - concert {list[i].IdConcert}");
            }
        }

        Console.WriteLine("\n ↑↓ move | X next | Z back | C register | V edit | M delete | B search");
    }
}
