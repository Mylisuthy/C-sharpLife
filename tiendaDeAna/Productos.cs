namespace tiendaAna;

public class Productos
{
    public static void MostrarProductos(List<string> items, List<double> prices, List<int> stock, int seleccion)
    {
        Console.Clear();
        Console.WriteLine("------ Ana's Products ------");
        Console.WriteLine("Use ↑↓ to navigate, X to select, Z to return\n");
        Console.WriteLine("Product\t\tPrice\tStock");

        for (int i = 0; i < items.Count; i++)
        {
            if (i == seleccion)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {items[i]}\t\t{prices[i]}\t{stock[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {items[i]}\t\t{prices[i]}\t{stock[i]}");
            }
        }
    }
}