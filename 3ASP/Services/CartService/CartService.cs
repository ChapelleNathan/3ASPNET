using _3ASP.Data;
using _3ASP.DTO.CartDto;

namespace _3ASP.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CartService(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<CartDto>> GetOne(int userId)
    {
        var serviceResponse = new ServiceResponse<CartDto>();
        try
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == userId);
            if (cart is null) throw new Exception("Cart not found");
            serviceResponse.Data = _mapper.Map<CartDto>(cart);
        }
        catch (Exception e)
        {
            serviceResponse.Message = e.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }

    public Task<ServiceResponse<CartDto>> AddItems(int userId, int productId)
    {
        throw new NotImplementedException();
    }


    public Task<ServiceResponse<CartDto>> RemoveProduct(int productId)
    {
        throw new NotImplementedException();
    }
}