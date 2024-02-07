using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Models;
using _3ASP.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet( "{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpPost]
        public ActionResult<List<User>> AddUser(PostUserDto user)
        {
            return Ok(_userService.AddUser(user));
        }
    }
}