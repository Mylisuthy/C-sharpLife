using RiwiMusic.Models;
using RiwiMusic.Services;
using RiwiMusic.Utils;

namespace RiwiMusic.Controllers;

public class ConcertCont
{
    public static void CreateConcert()
    {
        Console.Clear();
        Console.WriteLine("=== Register new concert ===\n");
        Console.WriteLine("Press 'x' to cancel at any step.\n");
        
        int id = IdGenerator.GenerateConcertId();

        // Name
        string name = InputValidator.ReadString("Name");
        if (name.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        // Capacity
        Console.Write("Capacity: ");
        string capacityInput = Console.ReadLine();
        if (capacityInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        int capacity;
        while (!int.TryParse(capacityInput, out capacity))
        {
            Console.Write("Error: Enter a valid number (or 'x' to cancel): ");
            capacityInput = Console.ReadLine();
            if (capacityInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        }

        // Location
        string location = InputValidator.ReadString("Location");
        if (location.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        // Date
        Console.Write("Date (yyyy-MM-dd): ");
        string dateInput = Console.ReadLine();
        if (dateInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        DateTime date;
        while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
        {
            Console.Write("Error: Use yyyy-MM-dd (or 'x' to cancel): ");
            dateInput = Console.ReadLine();
            if (dateInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        }

        // Save concert
        DataStore.concerts.Add(id, new Concert
        {
            Id = id,
            Name = name,
            Capacity = capacity,
            Location = location,
            ventas = 0,
            date = date.ToString("yyyy-MM-dd")
        });

        Console.WriteLine("\nConcert registered successfully.");
        Console.ReadKey();
    }

    
    public static void EditConcert(int index)
    {
        var concert = DataStore.concerts.Values.ElementAt(index);

        Console.Clear();
        Console.WriteLine($"=== Edit concert: {concert.Name} ===\n");

        Console.Write($"New name (current: {concert.Name}): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
            concert.Name = newName;

        Console.Write($"New capacity (current: {concert.Capacity}): ");
        string newCap = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCap) && int.TryParse(newCap, out int cap))
            concert.Capacity = cap;

        Console.Write($"New location (current: {concert.Location}): ");
        string newLoc = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newLoc))
            concert.Location = newLoc;

        Console.Write($"New date (current: {concert.date}, format yyyy-MM-dd): ");
        string newDate = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDate))
            concert.date = newDate;

        Console.WriteLine("\nConcert updated successfully.");
        Console.ReadKey();
    }

    public static void DeleteConcert(int index)
    {
        var concert = DataStore.concerts.ElementAt(index);

        Console.WriteLine($"\nAre you sure to delete '{concert.Value.Name}'? (Y/N)");
        if (Console.ReadKey(true).Key == ConsoleKey.Y)
        {
            DataStore.concerts.Remove(concert.Key);
            Console.WriteLine("\nConcert deleted.");
        }
        else
        {
            Console.WriteLine("\nOperation canceled.");
        }
        Console.ReadKey();
    }

    public static void FilterConcert()
    {
        Console.Clear();
        Console.WriteLine("=== Search Concert ===");
        int id = InputValidator.ReadInt("Enter ID");

        if (DataStore.concerts.TryGetValue(id, out Concert concert))
        {
            Console.WriteLine($"\nID: {concert.Id}");
            Console.WriteLine($"Name: {concert.Name}");
            Console.WriteLine($"Location: {concert.Location}");
            Console.WriteLine($"Date: {concert.date}");
            Console.WriteLine($"Capacity: {concert.Capacity}");
            Console.WriteLine($"Sales: {concert.ventas}");
        }
        else
        {
            Console.WriteLine("\nConcert not found.");
        }

        Console.ReadKey();
    }
}
