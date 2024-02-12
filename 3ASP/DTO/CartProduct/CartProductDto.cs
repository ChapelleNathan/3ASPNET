namespace _3ASP.DTO.ProductCartDto;

public class CartProductDto
{
    public int Id { get; private set; }
    public required Cart Cart { get;  set; }
    public required Product Product { get;  set; }
}