namespace RiwiMusic.Utils;

public class MenuHelper
{
    public static int ShowMenu(string title, List<string> options)
    {
        int index = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine($"==== {title} ====\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            Console.WriteLine("\n↑ ↓ move | X select | Z back");
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                index = (index == 0) ? options.Count - 1 : index - 1;
            else if (key == ConsoleKey.DownArrow)
                index = (index == options.Count - 1) ? 0 : index + 1;

        } while (key != ConsoleKey.X && key != ConsoleKey.Z);

        return key == ConsoleKey.Z ? -1 : index;
    }
}