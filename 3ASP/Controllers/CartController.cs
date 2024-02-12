using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.DTO.CartDto;
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
        public async Task<ActionResult<ServiceResponse<CartDto>>> GetOne(int userId)
        {
            var response = await _cartService.GetOne(userId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("{userId}/{productId}")]
        public async Task<ActionResult<ServiceResponse<CartDto>>> AddItems(int userId, int productId)
        {
            var response = await _cartService.AddItems(userId, productId);
            if (response.Success is false) return BadRequest(response);
            return Ok(response);
        }
    }
}
