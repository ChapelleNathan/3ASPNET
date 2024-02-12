namespace _3ASP.DTO.ProductDto;

public class UpdateProductDto
{
    public string Name { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public float Price { get; private set; } = 0;
    public bool Available { get; private set; } = false;
}