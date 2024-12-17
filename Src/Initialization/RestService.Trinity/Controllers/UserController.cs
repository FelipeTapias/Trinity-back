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

        [HttpGet(Name = "GetAllUser")]
        public async Task<IEnumerable<User>> GetAllUser()
        {
            _logger.LogInformation("Obteniendo todos los usuarios");
            return await _userService.GetAllUser();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<User> GetUserById(string id) 
        {
            _logger.LogInformation($"Obteneindo usuario con id: {id}");
            return await _userService.GetUserById(id);
        }

        [HttpDelete("{id}", Name = "DeleteUserById")]
        public async Task<string> DeleteUserById(string id)
        {
            _logger.LogInformation($"Eliminando usuario con id: {id}");
            return await _userService.DeleteUserById(id);
        }

        [HttpPut(Name = "PutUserById")]
        public async Task<string> UpdateUserById(string id, User user)
        {
            _logger.LogInformation($"Actualizando usuario con id: {id}");
            await _userService.UpdateUserById(id, user);
            return id;
        }
    }
}
