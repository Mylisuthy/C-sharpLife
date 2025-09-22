namespace RiwiMusic.Models;

public class Tiket
{
    public int IdTiket { get; set; }
    public int IdCustomer { get; set; }
    public int IdConcert { get; set; }
    public string InfTiket { get; set; }
    public string CustomerName { get; set; }
    public string ConcertName { get; set; }
}