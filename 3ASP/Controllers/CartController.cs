using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.DTO.CartDto;
using _3ASP.DTO.ProductDto;
using _3ASP.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetOne(int userId)
        {
            var response = await _cartService.GetCart(userId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);;
        }

        [HttpPost("{userId}/{productId}")]
        public async Task<ActionResult<ServiceResponse<CartDto>>> AddItems(int userId, int productId)
        {
            var response = await _cartService.AddItems(userId, productId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<ActionResult<ServiceResponse<CartDto>>> RemoveItem(int userId, int productId)
        {
            var response = await _cartService.RemoveProduct(userId, productId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{userId}/user")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetCart(int userId)
        {
            var response = await _cartService.GetCart(userId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);
        }
    }
}
