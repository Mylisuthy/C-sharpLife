using RiwiMusic.Services;
using RiwiMusic.Controllers;

namespace RiwiMusic.Veaws;

public class ConcertView
{
    public static void Menu()
    {
        int selec = 0;
        ConsoleKey key;

        do
        {
            RenderMenu(selec);
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selec = (selec == 0) ? DataStore.concerts.Count - 1 : selec - 1;
            else if (key == ConsoleKey.DownArrow)
                selec = (selec == DataStore.concerts.Count - 1) ? 0 : selec + 1;

            switch (key)
            {
                case ConsoleKey.X:
                    selec = (selec + 1) % DataStore.concerts.Count;
                    break;
                case ConsoleKey.Z:
                    return;
                case ConsoleKey.C:
                    ConcertCont.CreateConcert();
                    break;
                case ConsoleKey.V:
                    ConcertCont.EditConcert(selec);
                    break;
                case ConsoleKey.M:
                    ConcertCont.DeleteConcert(selec);
                    break;
                case ConsoleKey.B:
                    ConcertCont.FilterConcert();
                    break;
            }

        } while (true);
    }

    private static void RenderMenu(int selec)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== Concert Management ===\n");
        Console.ForegroundColor = ConsoleColor.White;

        var concertsList = DataStore.concerts.Values.ToList();

        if (concertsList.Count == 0)
        {
            Console.WriteLine("No concerts registered.");
            Console.WriteLine("\nC create | Z back");
            return;
        }

        for (int i = 0; i < concertsList.Count; i++)
        {
            var c = concertsList[i];

            if (i == selec)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($@"> {c.Id} - {c.Name}
    More Info
    ---------------
    Site: {c.Location}
    Capacity: {c.Capacity}
    Date: {c.date}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {c.Id} - {c.Name}");
            }
        }

        Console.WriteLine("\n↑ ↓ move | X next | Z back | C create | V edit | M delete | B search");
    }
}
