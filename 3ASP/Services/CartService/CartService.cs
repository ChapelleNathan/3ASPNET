using _3ASP.Data;
using _3ASP.DTO.CartDto;
using _3ASP.DTO.ProductCartDto;
using _3ASP.DTO.ProductDto;

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

    public async Task<ServiceResponse<CartProductDto>> AddItems(int userId, int productId)
    {
        var serviceResponse = new ServiceResponse<CartProductDto>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (user is null) throw new Exception("User not found");
            if (product is null) throw new Exception("Product not found");
            CartProductDto? cartProduct = null;
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == user.Id);
            if (cart is null)
            {
                cartProduct = await CreateCart(user, product);
            }
            else
            {
                cartProduct = await AddProductToCart(cart, product);
            }

            if (cartProduct is null) throw new Exception("Error while trying to add an item to your cart");
            serviceResponse.Data = cartProduct;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }


    public Task<ServiceResponse<CartDto>> RemoveProduct(int productId)
    {
        throw new NotImplementedException();
    }

    private async Task<CartProductDto> CreateCart(User user, Product product)
    {    
        var cart = await _context.Carts.AddAsync(new Cart{User = user});
        var productCart = await _context.CartProducts.AddAsync(new CartProduct { Cart = cart.Entity, Product = product });
        await _context.SaveChangesAsync();
        return _mapper.Map<CartProductDto>(productCart.Entity)!;
    }

    private async Task<CartProductDto> AddProductToCart(Cart cart, Product product)
    {
        var cartProduct = await _context.CartProducts.AddAsync(new CartProduct { Cart = cart, Product = product });
        await _context.SaveChangesAsync();
        return _mapper.Map<CartProductDto>(cartProduct.Entity)!;
    }
}