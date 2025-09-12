namespace tiendaAna;

public class Compra
{
    private static List<string> carritoProductos = new();
    private static List<int> carritoCantidades = new();
    private static List<double> carritoPrecios = new();

    // === 1. Primer menú: seleccionar productos ===
    public static void SeleccionarProductos()
    {
        var (items, prices, stock) = Db.itemsDb();
        int seleccion = 0;
        ConsoleKey tecla;

        do
        {
            Productos.MostrarProductos(items, prices, stock, seleccion);
            MostrarCarrito();

            tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.UpArrow)
                seleccion = (seleccion == 0) ? items.Count - 1 : seleccion - 1;

            else if (tecla == ConsoleKey.DownArrow)
                seleccion = (seleccion == items.Count - 1) ? 0 : seleccion + 1;

            else if (tecla == ConsoleKey.X) // Seleccionar producto
                AgregarProducto(items, prices, stock, seleccion);

            else if (tecla == ConsoleKey.Z) // Regresar al menú principal
                return;

        } while (true);
    }

    // === 2. Confirmar y editar compra ===
    public static void ConfirmarCompra()
    {
        if (carritoProductos.Count == 0)
        {
            Console.WriteLine("Your cart is empty! Press any key to return...");
            Console.ReadKey();
            return;
        }

        int seleccion = 0;
        ConsoleKey tecla;

        do
        {
            DibujarCarritoInteractivo(seleccion);
            tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.UpArrow)
                seleccion = (seleccion == 0) ? carritoProductos.Count - 1 : seleccion - 1;

            else if (tecla == ConsoleKey.DownArrow)
                seleccion = (seleccion == carritoProductos.Count - 1) ? 0 : seleccion + 1;

            else if (tecla == ConsoleKey.X)
                EditarProductoCarrito(seleccion);

            else if (tecla == ConsoleKey.Z)
                return; // volver al menú principal

        } while (true);
    }

    // === Funciones internas ===
    private static void AgregarProducto(List<string> items, List<double> prices, List<int> stock, int seleccion)
    {
        Console.Clear();
        Console.WriteLine($"Selected: {items[seleccion]}");
        Console.WriteLine($"Available stock: {stock[seleccion]}");
        Console.Write("Enter quantity: ");

        if (!int.TryParse(Console.ReadLine(), out int cantidadDeseada) || cantidadDeseada <= 0)
        {
            Console.WriteLine("Invalid input. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        if (cantidadDeseada > stock[seleccion])
        {
            Console.WriteLine("Not enough stock available. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        stock[seleccion] -= cantidadDeseada;
        carritoProductos.Add(items[seleccion]);
        carritoCantidades.Add(cantidadDeseada);
        carritoPrecios.Add(prices[seleccion] * cantidadDeseada);

        Console.WriteLine($"{cantidadDeseada} {items[seleccion]} added to your cart!");
        Console.ReadKey();
    }

    private static void MostrarCarrito()
    {
        Console.WriteLine("\n--- Current Cart ---");
        if (carritoProductos.Count == 0)
        {
            Console.WriteLine("Cart is empty...");
        }
        else
        {
            for (int i = 0; i < carritoProductos.Count; i++)
            {
                Console.WriteLine($"{carritoProductos[i]} x{carritoCantidades[i]} = {carritoPrecios[i]}");
            }
        }
    }

    private static void DibujarCarritoInteractivo(int seleccion)
    {
        Console.Clear();
        Console.WriteLine("------ Confirm Purchase ------");
        Console.WriteLine("Use ↑↓ to navigate, X to edit, Z to return\n");

        for (int i = 0; i < carritoProductos.Count; i++)
        {
            if (i == seleccion)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {carritoProductos[i]} x{carritoCantidades[i]} = {carritoPrecios[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {carritoProductos[i]} x{carritoCantidades[i]} = {carritoPrecios[i]}");
            }
        }
    }

    private static void EditarProductoCarrito(int seleccion)
    {
        Console.Clear();
        Console.WriteLine($"Editing {carritoProductos[seleccion]}");
        Console.WriteLine("Enter new quantity (0 to remove): ");

        if (!int.TryParse(Console.ReadLine(), out int nuevaCantidad) || nuevaCantidad < 0)
        {
            Console.WriteLine("Invalid input. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        if (nuevaCantidad == 0)
        {
            carritoProductos.RemoveAt(seleccion);
            carritoCantidades.RemoveAt(seleccion);
            carritoPrecios.RemoveAt(seleccion);
            return;
        }

        double precioUnitario = carritoPrecios[seleccion] / carritoCantidades[seleccion];
        carritoCantidades[seleccion] = nuevaCantidad;
        carritoPrecios[seleccion] = precioUnitario * nuevaCantidad;
    }

    // === Totales y limpieza ===
    public static double CalcularTotal()
    {
        decimal total = 0;
        foreach (var precio in carritoPrecios) 
            total += (decimal)precio;
        return (double)total;
    }

    public static void VaciarCarrito()
    {
        carritoProductos.Clear();
        carritoCantidades.Clear();
        carritoPrecios.Clear();
    }

    public static bool CarritoVacio()
    {
        return carritoProductos.Count == 0;
    }
}
