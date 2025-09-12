namespace tiendaAna;

public class tienda
{
    static void Main(string[] args)
    {
        Menu.Start();
    }
}
/*
Console.WriteLine("welcome to the ana's store");
while (true)
{
    Console.Clear();
    Console.WriteLine("what you did today?");
    Console.WriteLine("1. view the menu");
    Console.WriteLine("2. buy items");
    Console.WriteLine("3. whatching the car");
    Console.WriteLine("0. Salir");
    string input = Console.ReadLine();
            
    if (!int.TryParse(input, int ))
    {
        Console.WriteLine("Error, número inválido.");
        continue;
    }
            
    int opcion = Convert.ToInt32(input);

    switch (opcion)
    {
        case 1:
            funciones.first();
            continue;
                
        case 2:
            funciones.second();
            Console.ReadLine();
            continue;
                
        case 0:
            Console.WriteLine("Saliendo del programa...");
            break;

        default:
            Console.WriteLine("Opción no válida. Intenta de nuevo.");
            continue;
    }
} 
*/