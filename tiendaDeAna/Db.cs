namespace tiendaAna;

public class Db
{
    public static List<string> items = new List<string>{"alfajor","revolcon","quipitos","papitas" };
    public static List<double> prices = new List<double> { 2500, 300, 1000, 2500};
    public static List<int> stock = new List<int>{ 23, 19, 27, 30};
    
    public static (List<string>, List<double>, List<int>) itemsDb()
    {
        return (items, prices, stock);
    }
}