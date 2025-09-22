using RiwiMusic.Services;
using RiwiMusic.Models;

namespace RiwiMusic.Veaws;

public class SpecificView
{
    public static void Menu()
    {
        int selec = 0;
        ConsoleKey key;

        do
        {
            MenuRender(selec);
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow) selec = (selec == 0) ? 4 : selec - 1;
            else if (key == ConsoleKey.DownArrow) selec = (selec == 4) ? 0 : selec + 1;

            switch (key)
            {
                case ConsoleKey.Enter:
                    Execute(selec);
                    break;
                case ConsoleKey.Z:
                    return;
            }

        } while (true);
    }

    // render menu
    private static void MenuRender(int selec)
    {
        string[] ops =
        {
            "1. Concerts by city",
            "2. Concerts by date range",
            "3. Concert with most tickets sold",
            "4. Total income of a concert",
            "5. Customer with most purchases"
        };

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== Specific Information ===\n");
        Console.ForegroundColor = ConsoleColor.White;

        for (int i = 0; i < ops.Length; i++)
        {
            if (i == selec)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"> {ops[i]}");
                Console.ResetColor();
            }
            else Console.WriteLine($"  {ops[i]}");
        }

        Console.WriteLine("\n↑ ↓ move | Enter select | Z back");
    }

    // run selected option
    private static void Execute(int selec)
    {
        Console.Clear();
        if (selec == 0) ByCity();
        else if (selec == 1) ByDateRange();
        else if (selec == 2) MostTickets();
        else if (selec == 3) TotalIncome();
        else if (selec == 4) TopCustomer();
    }

    // 1. concerts by city
    private static void ByCity()
    {
        Console.WriteLine("=== Concerts by City ===");
        Console.Write("City: ");
        string city = Console.ReadLine();

        var result = DataStore.concerts.Values
            .Where(c => c.Location.Equals(city, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("\nNo concerts found.");
        }
        else
        {
            Console.WriteLine("\nResults:");
            foreach (var c in result)
                Console.WriteLine($"ID: {c.Id} | Name: {c.Name} | Date: {c.date} | Cap: {c.Capacity}");
        }
        Console.ReadKey();
    }

    // 2. concerts by date range 
    private static void ByDateRange()
    {
        Console.WriteLine("=== Concerts by Date Range ===");

        Console.Write("Start date (yyyy-MM-dd): ");
        DateTime start = DateTime.Parse(Console.ReadLine());

        Console.Write("End date (yyyy-MM-dd): ");
        DateTime end = DateTime.Parse(Console.ReadLine());

        var result = DataStore.concerts.Values
            .Where(c => DateTime.Parse(c.date) >= start && DateTime.Parse(c.date) <= end)
            .OrderBy(c => DateTime.Parse(c.date))
            .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("\nNo concerts found.");
        }
        else
        {
            Console.WriteLine("\nResults:");
            foreach (var c in result)
                Console.WriteLine($"ID: {c.Id} | Name: {c.Name} | Date: {c.date}");
        }
        Console.ReadKey();
    }

    // 3. concert with most tickets sold
    private static void MostTickets()
    {
        Console.WriteLine("=== Concert with Most Tickets Sold ===");

        var top = DataStore.concerts.Values
            .OrderByDescending(c => c.ventas)
            .FirstOrDefault();

        if (top == null)
        {
            Console.WriteLine("\nNo concerts found.");
        }
        else
        {
            Console.WriteLine($"\nConcert: {top.Name}");
            Console.WriteLine($"City: {top.Location}");
            Console.WriteLine($"Tickets sold: {top.ventas}");
        }
        Console.ReadKey();
    }

    // 4. total income of a concert
    private static void TotalIncome()
    {
        Console.WriteLine("=== Total Income of a Concert ===");
        Console.Write("Concert ID: ");
        int id = int.Parse(Console.ReadLine());

        var joinResult = DataStore.tikets.Values
            .Join(DataStore.concerts.Values,
                t => t.IdConcert,
                c => c.Id,
                (t, c) => new { Ticket = t, Concert = c })
            .Where(tc => tc.Concert.Id == id)
            .ToList();

        if (joinResult.Count == 0)
        {
            Console.WriteLine("\nConcert not found or no tickets sold.");
        }
        else
        {
            decimal price = 50;
            decimal total = joinResult.Count * price;
            var concert = joinResult.First().Concert;

            Console.WriteLine($"\nConcert: {concert.Name}");
            Console.WriteLine($"Tickets sold: {joinResult.Count}");
            Console.WriteLine($"Total income: ${total}");
        }
        Console.ReadKey();
    }

    // 5. customer with most purchases
    private static void TopCustomer()
    {
        Console.WriteLine("=== Customer with Most Purchases ===");

        var joinResult = DataStore.customers.Values
            .Join(DataStore.tikets.Values,
                c => c.Id,
                t => t.IdCustomer,
                (c, t) => new { Customer = c, Ticket = t })
            .GroupBy(ct => ct.Customer.Id)
            .Select(g => new
            {
                Id = g.Key,
                Name = g.First().Customer.Name,
                Email = g.First().Customer.Email,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault();

        if (joinResult == null)
        {
            Console.WriteLine("\nNo customers found.");
        }
        else
        {
            Console.WriteLine($"\nCustomer: {joinResult.Name}");
            Console.WriteLine($"Email: {joinResult.Email}");
            Console.WriteLine($"Tickets purchased: {joinResult.Count}");
        }
        Console.ReadKey();
    }
}
