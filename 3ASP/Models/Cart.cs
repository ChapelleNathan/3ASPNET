namespace _3ASP.Models;

public class Cart
{
    public int Id { get; private set; }
    public required User User { get; set; }
    public bool Paid { get;  set; } = false;
}