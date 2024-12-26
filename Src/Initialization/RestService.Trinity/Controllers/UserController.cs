using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RestService.Trinity.Controllers.Base;

namespace RestService.Trinity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService): base(logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("CreateUser", Name = "PostUser")]
        public async Task<IActionResult> Post(User user)
        {
            _logger.LogInformation("Creando el usuario");
            return await HandleResponse(async () => 
            {
                return await _userService.CreateUser(user);
            }, "Usuario creado correctamente");
        }

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            _logger.LogInformation("Obteniendo todos los usuarios");
            return await HandleResponse(async () => 
            {                
                return await _userService.GetAllUser();
            }, "Usuarios obtenidos correctamente");
        }

        [HttpGet("GetUserById", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            _logger.LogInformation($"Obteneindo usuario con id: {id}");
            return await HandleResponse(async () =>
            {
                return await _userService.GetUserById(id);
            }, $"Usuario con Id {id} obtenido correctamente");
            
        }

        [HttpDelete("DeleteUserById", Name = "DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([FromQuery] string id)
        {
            _logger.LogInformation($"Eliminando usuario con id: {id}");
            return await HandleResponse(async () => {
                return await _userService.DeleteUserById(id);
            }, $"Usuario con Id {id} eliminado correctamente");
        }

        [HttpPut("EditUserById", Name = "EditUserById")]
        public async Task<IActionResult> UpdateUserById(string id, User user)
        {
            _logger.LogInformation($"Actualizando usuario con id: {id}");
            return await HandleResponse(async () => {
                return await _userService.UpdateUserById(id, user);
            }, $"Usuario con Id {id} actualizado correctamente");
        }

        [HttpGet("GetUserByIdDocument", Name = "GetUserByIdDocument")]
        public async Task<IActionResult> GetUserByIdDocument([FromQuery] int idDocument)
        {
            _logger.LogInformation($"Obteniendo usuario con IdDocumento: {idDocument}");
            return await HandleResponse(async () => {
                return await _userService.GetUserByIdDocument(idDocument);
            }, $"Usuario con IdDocument {idDocument} obtenido correctamente");
        }

        [HttpGet("GetIdByIdDocument", Name = "GetIdByIdDocument")]
        public async Task<IActionResult> GetIdByIdDocument([FromQuery] int idDocument)
        {
            _logger.LogInformation($"Obteniendo id con IdDocumento: {idDocument}");
            return await HandleResponse(async () => {
                return await _userService.GetIdByIdDocument(idDocument);
            }, $"Id de usuario con IdDocument {idDocument} obtenido correctamente");
        }
    }
}
