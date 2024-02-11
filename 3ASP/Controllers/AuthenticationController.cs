using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.DTO.UserDto;
using _3ASP.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Register(PostUserDto user)
        {
            return Ok(await _authService.Register(user));
        }
    }
}
