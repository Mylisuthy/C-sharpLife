namespace tiendaAna;

public class Pago
{
    public static void ProcesarPago()
    {
        if (Compra.CarritoVacio())
        {
            Console.WriteLine("Your cart is empty! Press any key to return...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("------ Payment ------\n");

        double total = Compra.CalcularTotal();
        double descuento = 0;

        if (total >= 20000) descuento = total * (double)0.20m;
        else if (total >= 10000) descuento = total * (double)0.10m;

        double totalFinal = total - descuento;

        Console.WriteLine($"Subtotal: {total}");
        Console.WriteLine($"Discount: {descuento}");
        Console.WriteLine($"Total to pay: {totalFinal}");

        Console.Write("\nEnter payment amount: ");
        if (!double.TryParse(Console.ReadLine(), out double pagoCliente) || pagoCliente < totalFinal)
        {
            Console.WriteLine("Insufficient payment. Press any key to return...");
            Console.ReadKey();
            return;
        }

        double cambio = pagoCliente - totalFinal;
        Console.WriteLine($"Change to give: {cambio}");

        Console.Write("\nDo you want to finalize the purchase? (Y/N): ");
        string respuesta = Console.ReadLine().ToUpper();

        if (respuesta == "Y")
        {
            Compra.VaciarCarrito();
            Console.WriteLine("Thank you for your purchase!");
            Console.ReadKey();
            Environment.Exit(0); // Finaliza la app
        }
        else
        {
            Console.WriteLine("Returning to main menu...");
            Console.ReadKey();
        }
    }
}