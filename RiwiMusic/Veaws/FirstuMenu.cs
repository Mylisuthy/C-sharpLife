using RiwiMusic.Utils;

namespace RiwiMusic.Veaws;

public class FirstuMenu
{
    private static readonly List<string> options = new List<string>
    {
        "Concert Management",
        "Client Management",
        "Tickets Management",
        "Especific Information",
        "Exit"
    };

    public static void Start()
    {
        bool running = true;

        while (running)
        {
            int selection = MenuHelper.ShowMenu("RIWI MUSIC", options);

            if (selection == -1)
            {
                running = ConfirmExit();
                continue;
            }

            running = ExecuteOption(selection);
        }
    }

    private static bool ExecuteOption(int selection)
    {
        switch (selection)
        {
            case 0:
                ConcertView.Menu();
                break;

            case 1:
                CustomerView.Menu();
                break;

            case 2:
                TiketView.Menu();
                break;

            case 3:
                SpecificView.Menu();
                break;

            case 4:
                return ConfirmExit();
        }
        return true;
    }

    private static bool ConfirmExit()
    {
        Console.Clear();
        Console.WriteLine("Do you want to exit? (Y/N)");
        return Console.ReadKey(true).Key != ConsoleKey.Y;
    }
}