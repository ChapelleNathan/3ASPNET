namespace _3ASP.DTO.ProductDto;

public class UpdateProductDto
{
    public required string Name { get;  set; }
    public required string Image { get;  set; }
    public required float Price { get;  set; } 
    public required bool Available { get;  set; } 
}