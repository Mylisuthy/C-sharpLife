namespace tiendaAna;

public class Menu
{
    private static readonly string[] options =
    {
        "View Menu and Select Products",
        "Confirm and Purchase Product",
        "Pay for Order",
        "Exit"
    };

    public static void Start()
    {
        int seleccion = 0;
        ConsoleKey tecla;

        do
        {
            DibujarMenu(seleccion);

            tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.UpArrow)
                seleccion = (seleccion == 0) ? options.Length - 1 : seleccion - 1;
            else if (tecla == ConsoleKey.DownArrow)
                seleccion = (seleccion == options.Length - 1) ? 0 : seleccion + 1;
            else if (tecla == ConsoleKey.Enter)
            {
                switch (seleccion + 1)
                {
                    case 1:
                        Compra.SeleccionarProductos();
                        break;
                    case 2:
                        Compra.ConfirmarCompra();
                        Pago.ProcesarPago(); // Flujo directo al pago
                        break;
                    case 3:
                        Pago.ProcesarPago();
                        break;
                    case 4:
                        Console.WriteLine("Thank you for visiting Ana's store!");
                        return;
                }
            }

        } while (true);
    }

    private static void DibujarMenu(int seleccion)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("---------- Ana's Store ----------\n");
        Console.ForegroundColor = ConsoleColor.Cyan;

        for (int i = 0; i < options.Length; i++)
        {
            if (i == seleccion)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {options[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {options[i]}");
            }
        }
    }
}

    /*
   // Options Menu
   private static readonly string[] options = 
   { 
       "View Menu", 
       "Purchase Product", 
       "Pay for Order", 
       "Exit" 
   };

   // Main Flow Control (it Controling All the Flow)
   public static void start()
   {
       int catchOption = NavMenu();
       EjecutarOpcion(catchOption);
   }

   // ================== Principal Functions ==================

   // 1. Veaw Menu and Catch Movements
   private static int NavMenu()
   {
       int CatchOpt = 0;
       ConsoleKey Input;
       Console.CursorVisible = false;

       do
       {
           DibujarMenu(CatchOpt);

           // Leer tecla
           Input = Console.ReadKey(true).Key;

           if (Input == ConsoleKey.UpArrow)
               CatchOpt = (CatchOpt == 0) ? options.Length - 1 : CatchOpt - 1;

           else if (Input == ConsoleKey.DownArrow)
               CatchOpt = (CatchOpt == options.Length - 1) ? 0 : CatchOpt + 1;

       } while (Input != ConsoleKey.Enter); // Continue Only With Int

       Console.CursorVisible = true;
       return CatchOpt; // Returns the Select Option
   }

   // 2. Render Menu Options
   private static void DibujarMenu(int catchOpt)
   {
       Console.Clear();
       Console.ForegroundColor = ConsoleColor.DarkBlue;
       Console.WriteLine("----------hey, welcome to ana's menu----------\n");
       Console.ForegroundColor = ConsoleColor.Cyan;

       for (int i = 0; i < options.Length; i++)
       {
           if (i == catchOpt)
           {
               Console.ForegroundColor = ConsoleColor.Black;
               Console.BackgroundColor = ConsoleColor.Cyan;
               Console.WriteLine($"> {options[i]}");
               Console.ResetColor();
           }
           else
           {
               Console.WriteLine($"  {options[i]}");
           }
       }
   }

   // 3. Catch the case an interact with that option
   private static void EjecutarOpcion(int CatchOpt)
   {
       switch (CatchOpt + 1) // +1 porque el array inicia en 0
       {
           case 1:
               Productos.mostrarProductos();
               break;

           case 2:
               Compra.adicionarCompra();
               break;

           case 3:
               Console.WriteLine("Procesando pago...");
               Console.ReadKey();
               break;

           case 4:
               Console.WriteLine("Thank's for purchasing!");
               break;

           default:
               Console.WriteLine("Opción no válida.");
               Console.ReadKey();
               start(); // Vuelve al menú si hay error
               break;
       }
   }
    */
/*
public static void menu()
{
    string[] options = { "View Menu", "Purchase Product", "Pay for Order", "Exit" };
    int selection = 0;
    ConsoleKey input;

    Console.CursorVisible = false; // for don't veaw coursor

    do
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("----------hey, welcome to ana's menu----------\n");
        Console.ForegroundColor = ConsoleColor.Cyan;

        // render options
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selection)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {options[i]}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {options[i]}");
            }
        }

        // take a input
        input = Console.ReadKey(true).Key;

        if (input == ConsoleKey.UpArrow)
        {
            selection = (selection == 0) ? options.Length - 1 : selection - 1;
        }
        else if (input == ConsoleKey.DownArrow)
        {
            selection = (selection == options.Length - 1) ? 0 : selection + 1;
        }

    } while (input != ConsoleKey.Enter); // Enter confirma selección

    Console.CursorVisible = true;

    // Usamos switch-case como en tu código original
    switch (selection + 1) // +1 porque seleccion inicia en 0
    {
        case 1:
            Productos.mostrarProductos();
            break;
        case 2:
            Compra.adicionarCompra();
            break;
        case 3:
            Console.WriteLine("Procesando pago...");
            Console.ReadKey();
            break;
        case 4:
            Console.WriteLine("Thank's for purchasing!");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            Console.ReadKey();
            menu();
            break;
    }
}
*/
/*
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("----------hey, welcome to ana's menu----------");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("1. View Menu");
    Console.WriteLine("2. Purchase Product");
    Console.WriteLine("3. Pay for Order");
    Console.WriteLine("4. Exit");
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("select an option: (1/4)");
    string opcion= Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Productos.mostrarProductos();
            break;
        case "2":
            Compra.adicionarCompra();
            break;
        case "3":
            break;
        case "4":
            Console.WriteLine("Thank's for purchasing");
            break;
        default:
            Console.WriteLine("Opcion no valida");
            menu();
            break;
    }
    */
