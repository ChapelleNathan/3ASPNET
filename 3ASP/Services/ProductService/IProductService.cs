using _3ASP.DTO.ProductDto;

namespace _3ASP.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<ProductDto>> CreateProduct(PostProductDto request);
    Task<ServiceResponse<List<ProductDto>>> GetAll();
    Task<ServiceResponse<ProductDto>> GetOne(int id);
    Task<ServiceResponse<ProductDto>> Update(UpdateProductDto request, int id);
    Task<ServiceResponse<ProductDto>> Delete(int id);
}