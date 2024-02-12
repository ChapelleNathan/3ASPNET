using _3ASP.Data;
using _3ASP.DTO.ProductDto;

namespace _3ASP.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public Task<ServiceResponse<ProductDto>> CreateProduct(PostProductDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetAll()
    {
        var serviceResponse = new ServiceResponse<List<ProductDto>>();
        var products = await _context.Products.ToListAsync();
        serviceResponse.Data = products.Select(p => _mapper.Map<ProductDto>(p)).ToList()!;       
        return serviceResponse;
    }

    public Task<ServiceResponse<ProductDto>> GetOne(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<ProductDto>> Update(UpdateProductDto request, int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<ProductDto>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}