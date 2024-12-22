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

        [HttpPost("CreateUser", Name = "PostUser")]
        public async Task<string> Post(User user)
        {
            _logger.LogInformation("Creando el usuario");
            string idReturn = await _userService.CreateUser(user);
            _logger.LogInformation($"Se creo el usuario con el ID: {idReturn}");
            return idReturn;
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUser()
        {
            _logger.LogInformation("Obteniendo todos los usuarios");
            return await _userService.GetAllUser();
        }

        [HttpGet("GetUserById", Name = "GetUserById")]
        public async Task<User> GetUserById([FromQuery] string id)
        {
            _logger.LogInformation($"Obteneindo usuario con id: {id}");
            return await _userService.GetUserById(id);
        }

        [HttpDelete("DeleteUserById", Name = "DeleteUserById")]
        public async Task<string> DeleteUserById([FromQuery] string id)
        {
            _logger.LogInformation($"Eliminando usuario con id: {id}");
            return await _userService.DeleteUserById(id);
        }

        [HttpPut("EditUserById", Name = "EditUserById")]
        public async Task<string> UpdateUserById(string id, User user)
        {
            _logger.LogInformation($"Actualizando usuario con id: {id}");
            await _userService.UpdateUserById(id, user);
            return id;
        }

        [HttpGet("GetUserByIdDocument", Name = "GetUserByIdDocument")]
        public async Task<User> GetUserByIdDocument([FromQuery] int idDocument)
        {
            _logger.LogInformation($"Obteniendo usuario con IdDocumento: {idDocument}");
            return await _userService.GetUserByIdDocument(idDocument);
        }

        [HttpGet("GetIdByIdDocument", Name = "GetIdByIdDocument")]
        public async Task<string> GetIdByIdDocument([FromQuery] int idDocument)
        {
            _logger.LogInformation($"Obteniendo id con IdDocumento: {idDocument}");
            return await _userService.GetIdByIdDocument(idDocument);
        }
    }
}
