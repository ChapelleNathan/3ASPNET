using _3ASP.Data;
using _3ASP.DTO.CartDto;
using _3ASP.DTO.ProductDto;
using _3ASP.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _3ASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAll()
    {
        return Ok(await _productService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<ProductDto>>> GetOne(int id)
    {
        var response = await _productService.GetOne(id);
        if (response.Success is false) return BadRequest(response);
        
        return Ok(response);
    }

    [HttpPost, Authorize(Roles = "Seller,Admin")]
    public async Task<ActionResult<ServiceResponse<ProductDto>>> Create(PostProductDto request)
    {
        var response = await _productService.CreateProduct(request);
        if (response.Success is false) return BadRequest(response);
        return Ok(response);
    }

    [HttpPut("{id}"), Authorize(Roles = "Seller,Admin")]
    public async Task<ActionResult<ServiceResponse<ProductDto>>> Update([FromBody] UpdateProductDto request, int id)
    {
        var response = await _productService.Update(request, id);
        if (response.Success is false) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpDelete("{id}"), Authorize(Roles = "Seller, Admin")]
    public async Task<ActionResult<ServiceResponse<ProductDto>>> Delete(int id)
    {
        var response = await _productService.Delete(id);
        if (response.Success is false) return BadRequest(response);
        return Ok(response);
    }
}