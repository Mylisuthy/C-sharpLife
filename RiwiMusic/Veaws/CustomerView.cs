using System;
using System.Collections.Generic;
using System.Linq;
using RiwiMusic.Services;
using RiwiMusic.Controllers;
using RiwiMusic.Utils;

namespace RiwiMusic.Veaws;

public class CustomerView
{
    // Menu for customer management
    public static void Menu()
    {
        int selec = 0;
        ConsoleKey key;

        do
        {
            RenderMenu(selec);
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selec = (selec == 0) ? DataStore.customers.Count - 1 : selec - 1;
            else if (key == ConsoleKey.DownArrow)
                selec = (selec == DataStore.customers.Count - 1) ? 0 : selec + 1;

            switch (key)
            {
                case ConsoleKey.X:
                    selec = (selec + 1) % Math.Max(1, DataStore.customers.Count);
                    break;
                case ConsoleKey.Z:
                    return;
                case ConsoleKey.C:
                    CustomerCont.CreateCustomer();
                    break;
                case ConsoleKey.V:
                    if (DataStore.customers.Count > 0)
                        CustomerCont.EditCustomer(DataStore.customers.Values.ElementAt(selec).Id);
                    else
                    {
                        Console.WriteLine("No customers to edit.");
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.M:
                    if (DataStore.customers.Count > 0)
                        CustomerCont.DeleteCustomer(DataStore.customers.ElementAt(selec).Key);
                    else
                    {
                        Console.WriteLine("No customers to delete.");
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.B:
                    CustomerCont.FilterCustomer();
                    break;
            }

        } while (true);
    }

    // Render list of customers and selected details
    private static void RenderMenu(int selec)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== Client Management ===\n");
        Console.ForegroundColor = ConsoleColor.White;

        var list = DataStore.customers.Values.ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("No customers registered.");
            Console.WriteLine("\nC create | Z back");
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var c = list[i];
            if (i == selec)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($@"> {c.Id} - {c.Name}
    Email: {c.Email}
    Age: {c.Age}
    Tickets needed: {c.TiketsOugneed}");
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
