using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3ASP.Data;
using _3ASP.DTO.UserDto;
using _3ASP.Models;
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

        private static List<User> _users = new List<User>
        {
            new User()
            {
                Id = 1, Pseudo = "Toto", Email = "toto@g.c", Password = BCrypt.Net.BCrypt.HashPassword("azerty")
            },
            new User()
            {
                Id = 2, Pseudo = "Tata", Email = "tata@g.c", Password = BCrypt.Net.BCrypt.HashPassword("tatatoto")
            }
        };

        public UserController(ILogger<UserController> loger, DataContext context)
        {
            _logger = loger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(_users);
        }

        [HttpGet( "{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_users.Find((u) => u.Id == id));
        }

        [HttpPost]
        public ActionResult<List<User>> Post(PostUserDto user)
        {
            var newUser = new User()
            {
                Id = _users.Max(u => u.Id) + 1,
                Pseudo = user.Pseudo,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Email = user.Email
            };
            
            _users.Add(newUser);
            return Ok(_users);
        }
    }
}