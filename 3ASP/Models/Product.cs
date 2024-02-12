namespace _3ASP.Models;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public float Price { get; private set; } = 0;
    public bool Available { get; private set; } = false;
    public DateTime AddedTime { get; private set; } = DateTime.Now;
}