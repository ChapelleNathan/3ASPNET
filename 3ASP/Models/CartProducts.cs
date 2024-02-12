namespace _3ASP.Models;

public class CartProducts
{
    public int Id { get; private set; }
    public required Cart Cart { get;  set; }
    public required Product Product { get;  set; }
}