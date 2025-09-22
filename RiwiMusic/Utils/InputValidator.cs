using System.Globalization;

namespace RiwiMusic.Utils;

public class InputValidator
{
    // Read integer
    public static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Error: Please enter a valid integer number.");
        }
    }

    // Read decimal
    public static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal value))
                return value;

            Console.WriteLine("Error: Please enter a valid decimal number.");
        }
    }

    // Read non-empty text
    public static string ReadString(string message)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            string text = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(text))
                return text.Trim();

            Console.WriteLine("Error: This field cannot be empty.");
        }
    }

    // Read date with format yyyy-MM-dd
    public static DateTime ReadDate(string message)
    {
        while (true)
        {
            Console.Write($"{message} (Format yyyy-MM-dd): ");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                return date;

            Console.WriteLine("Error: Invalid format. Use yyyy-MM-dd.");
        }
    }

    // Read email with basic validation
    public static string ReadEmail(string message)
    {
        while (true)
        {
            string email = ReadString(message);

            if (email.Contains("@") && email.Contains("."))
                return email;

            Console.WriteLine("Error: Please enter a valid email.");
        }
    }

    // Confirm action with Y or N
    public static bool ConfirmAction(string message)
    {
        while (true)
        {
            Console.Write($"{message} (Y/N): ");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Y)
                return true;
            if (key == ConsoleKey.N)
                return false;

            Console.WriteLine("\nError: Only Y or N are allowed.");
        }
    }
}
