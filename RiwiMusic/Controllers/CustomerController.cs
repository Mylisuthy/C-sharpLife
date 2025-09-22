
using RiwiMusic.Models;
using RiwiMusic.Services;
using RiwiMusic.Utils;

namespace RiwiMusic.Controllers;

public class CustomerCont
{
    // Create customer
    public static void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Register new customer ===\n");
        Console.WriteLine("Press 'x' to cancel at any step.\n");

        int id = IdGenerator.GenerateCustomerId();

        // Name
        string name = InputValidator.ReadString("Name");
        if (name.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        // Email
        string email = InputValidator.ReadString("Email");
        if (email.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        // Age
        Console.Write("Age: ");
        string ageInput = Console.ReadLine();
        if (ageInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        int age;
        while (!int.TryParse(ageInput, out age))
        {
            Console.Write("Error: Enter valid age (or 'x' to cancel): ");
            ageInput = Console.ReadLine();
            if (ageInput.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }
        }

        // Tickets needed (string in your model)
        string ticketsNeeded = InputValidator.ReadString("Tickets needed");
        if (ticketsNeeded.ToLower() == "x") { Console.WriteLine("\nCanceled."); Console.ReadKey(); return; }

        DataStore.customers.Add(id, new Customer
        {
            Id = id,
            Name = name,
            Email = email,
            Age = age,
            TiketsOugneed = ticketsNeeded
        });

        Console.WriteLine($"\nCustomer registered. ID: {id}");
        Console.ReadKey();
    }

    // Edit customer by id
    public static void EditCustomer(int id)
    {
        if (!DataStore.customers.TryGetValue(id, out Customer customer))
        {
            Console.WriteLine("Customer not found.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine($"=== Edit customer: {customer.Name} ===\n");

        Console.Write($"New name (current: {customer.Name}): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) customer.Name = newName.Trim();

        Console.Write($"New email (current: {customer.Email}): ");
        string newEmail = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEmail)) customer.Email = newEmail.Trim();

        Console.Write($"New age (current: {customer.Age}): ");
        string newAge = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newAge) && int.TryParse(newAge, out int age)) customer.Age = age;

        Console.Write($"New tickets needed (current: {customer.TiketsOugneed}): ");
        string newTk = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTk)) customer.TiketsOugneed = newTk.Trim();

        Console.WriteLine("\nCustomer updated.");
        Console.ReadKey();
    }

    // Delete customer by id
    public static void DeleteCustomer(int id)
    {
        if (!DataStore.customers.TryGetValue(id, out Customer customer))
        {
            Console.WriteLine("Customer not found.");
            Console.ReadKey();
            return;
        }

        bool confirm = InputValidator.ConfirmAction($"Are you sure to delete '{customer.Name}'?");
        if (confirm)
        {
            DataStore.customers.Remove(id);
            Console.WriteLine("\nCustomer deleted.");
        }
        else
        {
            Console.WriteLine("\nOperation canceled.");
        }

        Console.ReadKey();
    }

    // Search customer by id
    public static void FilterCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Search Customer ===");
        int id = InputValidator.ReadInt("Enter ID");

        if (DataStore.customers.TryGetValue(id, out Customer customer))
        {
            Console.WriteLine($"\nID: {customer.Id}");
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Age: {customer.Age}");
            Console.WriteLine($"Tickets needed: {customer.TiketsOugneed}");
        }
        else
        {
            Console.WriteLine("\nCustomer not found.");
        }

        Console.ReadKey();
    }
}
