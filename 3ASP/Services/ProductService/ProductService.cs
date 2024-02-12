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

    public async Task<ServiceResponse<ProductDto>> CreateProduct(PostProductDto request)
    {
        var serviceResponse = new ServiceResponse<ProductDto>();
        var newProduct = _mapper.Map<Product>(request)!;
        try
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        serviceResponse.Data = _mapper.Map<ProductDto>(newProduct);
        return serviceResponse;
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

    public async Task<ServiceResponse<ProductDto>> Update(UpdateProductDto request, int id)
    {
        var serviceResponse = new ServiceResponse<ProductDto>();
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) throw new Exception("Product not found");

            if (request.Available != product.Available) product.Available = request.Available;
            if (request.Name != product.Name) product.Name = request.Name;
            if (request.Image != product.Image) product.Image = request.Image;
            if (Math.Abs(request.Price - product.Price) > 0.2) product.Price = request.Price;

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<ProductDto>(product);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<ProductDto>> Delete(int id)
    {
        var serviceResponse = new ServiceResponse<ProductDto>();
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) throw new Exception("Product not found");
            serviceResponse.Data = _mapper.Map<ProductDto>(product);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}