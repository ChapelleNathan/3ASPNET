using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Enums;
using _3ASP.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using NuGet.Protocol;
using Org.BouncyCastle.Crypto.Generators;

namespace _3ASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> loger, DataContext context, IUserService userService)
        {
            _logger = loger;
            _context = context;
            _userService = userService;
        }

        [HttpGet] //Pourquoi je ne peux pas utiliser mon Enum Roles (Roles = Roles.Admin.ToString()) ?
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet( "{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateUser(UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }
}