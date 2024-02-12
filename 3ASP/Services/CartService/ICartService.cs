using _3ASP.DTO.CartDto;
using _3ASP.DTO.ProductCartDto;

namespace _3ASP.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<CartDto>> GetOne(int userId);
    Task<ServiceResponse<CartProductDto>> AddItems(int userId, int productId);
    Task<ServiceResponse<CartDto>> RemoveProduct(int productId);
}