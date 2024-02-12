using _3ASP.Data;
using _3ASP.DTO.ProductDto;
using _3ASP.Services.ProductService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _3ASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IProductService _productService;

    public ProductController(DataContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAll()
    {
        return Ok(await _productService.GetAll());
    }
    
}