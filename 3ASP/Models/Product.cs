namespace _3ASP.Models;

public class Product
{
    public int Id { get; private set; }
    public string Name { get;  set; } = string.Empty;
    public string Image { get;  set; } = string.Empty;
    public float Price { get;  set; } = 0;
    public bool Available { get;  set; } = false;
    public DateTime AddedTime { get; private set; } = DateTime.Now;
}