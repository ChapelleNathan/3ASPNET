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

    public async Task<ServiceResponse<CartDto>> AddItems(int userId, int productId)
    {
        var serviceResponse = new ServiceResponse<CartDto>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (user is null) throw new Exception("User not found");
            if (product is null) throw new Exception("Product not found");
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == user.Id && !c.Paid);
            if (cart is null)
            {
                cart = await CreateCart(user, product);
            }
            else
            {
                cart = await AddProductToCart(cart, product);
            }
            serviceResponse.Data = _mapper.Map<CartDto>(cart);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }


    public async Task<ServiceResponse<CartDto>> RemoveProduct(int userId, int productId)
    {
        var serviceResponse = new ServiceResponse<CartDto>();
        try
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == userId && !c.Paid);
            if (cart is null) throw new Exception("No cart linked to the user");

            var cartProduct = await _context.CartProducts.FirstOrDefaultAsync(c => c.Product.Id == productId);
            if (cartProduct is null) throw new Exception("Product not found in your cart");

            serviceResponse.Data = _mapper.Map<CartDto>(cart);
            _context.CartProducts.Remove(cartProduct);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetCart(int userId)
    {
        var serviceResponse = new ServiceResponse<List<ProductDto>>();
        var products = new List<Product>();
        try
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == userId && !c.Paid);
            if (cart is null) throw new Exception("No cart for this user");
            var cartProducts = _context.CartProducts.Where(c => c.Cart.Id == cart.Id)
                .Include(cartProduct => cartProduct.Product).ToList();
            products.AddRange(cartProducts.Select(cartProduct => cartProduct.Product));
            serviceResponse.Data = products.Select(p => _mapper.Map<ProductDto>(p)).ToList()!;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
    
    public async Task<ServiceResponse<CartDto>> PayCart(int userId)
    {
        var serviceResponse = new ServiceResponse<CartDto>();
        try
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User.Id == userId && !c.Paid);
            if (cart is null) throw new Exception("Didn't found cart for this user");
            if (cart.Paid) throw new Exception("Cart already paid");
            cart.Paid = true;
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<CartDto>(cart);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    private async Task<Cart> CreateCart(User user, Product product)
    {    
        var cart = await _context.Carts.AddAsync(new Cart{User = user});
        await _context.CartProducts.AddAsync(new CartProduct { Cart = cart.Entity, Product = product });
        await _context.SaveChangesAsync();
        return cart.Entity;
    }

    private async Task<Cart> AddProductToCart(Cart cart, Product product)
    {
        await _context.CartProducts.AddAsync(new CartProduct { Cart = cart, Product = product });
        await _context.SaveChangesAsync();
        return cart;
    }
}