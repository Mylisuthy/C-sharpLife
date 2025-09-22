
using RiwiMusic.Models;
using RiwiMusic.Services;
using RiwiMusic.Utils;

namespace RiwiMusic.Controllers;

public class TiketCont
{
    // Register new ticket (purchase)
    public static void CreateTiket()
    {
        Console.Clear();
        Console.WriteLine("=== Register Ticket ===\n");
        Console.WriteLine("Press 'x' to cancel at any step.\n");

        int id = IdGenerator.GenerateTicketId();

        // Choose customer
        Console.WriteLine("Choose customer by ID or type 'list' to see customers:");
        string input = Console.ReadLine();
        if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        if (string.Equals(input, "list", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var c in DataStore.customers.Values) Console.WriteLine($"{c.Id} - {c.Name}");
            Console.Write("Enter customer ID: ");
            input = Console.ReadLine();
            if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        }
        if (!int.TryParse(input, out int custId) || !DataStore.customers.ContainsKey(custId))
        {
            Console.WriteLine("Customer not found. Canceling.");
            Console.ReadKey();
            return;
        }

        // Choose concert
        Console.WriteLine("\nChoose concert by ID or type 'list' to see concerts:");
        input = Console.ReadLine();
        if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        if (string.Equals(input, "list", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var s in DataStore.concerts.Values) Console.WriteLine($"{s.Id} - {s.Name} ({s.date})");
            Console.Write("Enter concert ID: ");
            input = Console.ReadLine();
            if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase)) { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        }
        if (!int.TryParse(input, out int concertId) || !DataStore.concerts.ContainsKey(concertId))
        {
            Console.WriteLine("Concert not found. Canceling.");
            Console.ReadKey();
            return;
        }

        // Info
        string info = InputValidator.ReadString("Ticket info (seat, type)");
        if (info.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        // update concert sales
        DataStore.concerts[concertId].ventas += 1;

        // save ticket
        DataStore.tikets.Add(id, new Tiket
        {
            IdTiket = id,
            IdCustomer = custId,
            IdConcert = concertId,
            InfTiket = info,
            CustomerName = DataStore.customers[custId].Name,
            ConcertName = DataStore.concerts[concertId].Name
        });

        Console.WriteLine($"\nTicket registered. ID: {id}");
        Console.ReadKey();
    }

    // Edit ticket
    public static void EditTiket(int id)
    {
        if (!DataStore.tikets.TryGetValue(id, out Tiket tiket))
        {
            Console.WriteLine("Ticket not found.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine($"=== Edit ticket: {id} ===\n");

        Console.Write($"New info (current: {tiket.InfTiket}): ");
        string newInfo = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newInfo)) tiket.InfTiket = newInfo.Trim();

        Console.WriteLine("\nTicket updated.");
        Console.ReadKey();
    }

    // Delete ticket by id
    public static void DeleteTiket(int id)
    {
        if (!DataStore.tikets.TryGetValue(id, out Tiket tiket))
        {
            Console.WriteLine("Ticket not found.");
            Console.ReadKey();
            return;
        }

        bool confirm = InputValidator.ConfirmAction($"Are you sure to delete ticket {id}?");
        if (confirm)
        {
            // reduce concert sales count
            if (DataStore.concerts.TryGetValue(tiket.IdConcert, out var concert))
            {
                if (concert.ventas > 0) concert.ventas -= 1;
            }

            DataStore.tikets.Remove(id);
            Console.WriteLine("\nTicket deleted.");
        }
        else
        {
            Console.WriteLine("\nOperation canceled.");
        }

        Console.ReadKey();
    }

    // List tickets by customer id
    public static void FilterTiketByCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Tickets by Customer ===");
        int id = InputValidator.ReadInt("Enter customer ID");

        var list = DataStore.tikets.Values.Where(t => t.IdCustomer == id).ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("\nNo tickets found for this customer.");
        }
        else
        {
            foreach (var t in list)
            {
                Console.WriteLine($"\nTicket ID: {t.IdTiket}");
                Console.WriteLine($"Concert: {t.ConcertName} ({t.IdConcert})");
                Console.WriteLine($"Info: {t.InfTiket}");
            }
        }

        Console.ReadKey();
    }
}
