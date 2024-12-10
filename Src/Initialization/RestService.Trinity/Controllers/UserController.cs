using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestService.Trinity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;

        }

        [HttpPost(Name = "PostUser")]
        public async Task<string> Post(User user)
        {
            _logger.LogInformation("Creando el usuario");
            string idReturn = await _userService.CreateUser(user);
            _logger.LogInformation($"Se creo el usuario con el ID: {idReturn}");
            return idReturn;
        }
    }
}
