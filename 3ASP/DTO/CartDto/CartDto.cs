namespace _3ASP.DTO.CartDto;

public class CartDto
{
    public int Id { get; private set; }
    public required User User { get; set; }
    public bool Paid { get; private set; } = false;
}