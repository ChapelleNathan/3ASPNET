using _3ASP.Data;
using _3ASP.DTO.ProductDto;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<ServiceResponse<ProductDto>> GetOne(int id)
    {
        var serviceResponse = new ServiceResponse<ProductDto>();
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) throw new Exception("Product not found");
            serviceResponse.Data = _mapper.Map<ProductDto>(product);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        
        return serviceResponse;
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