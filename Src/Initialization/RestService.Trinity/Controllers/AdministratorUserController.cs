using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestService.Trinity.Controllers.Base;

namespace RestService.Trinity.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AdministratorUserController : BaseController
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<AdministratorUserController> _logger;

        public AdministratorUserController(IAdminUserService adminUserService, ILogger<AdministratorUserController> logger) : base(logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        [HttpPost("CreateAdministratorUser", Name = "CreateAdministratorUser")]
        public async Task<IActionResult> Post([FromBody] AdministratorUser administratorUser)
        {
            _logger.LogInformation("Creando el administrador usuario");
            return await HandleResponse(async () =>
            {
                return await _adminUserService.CreateAdminUser(administratorUser);
            }, "Usuario administrador creado correctamente");
        }

        [HttpPost("LoginAdministratorUser", Name = "LoginAdministratorUser")]
        public async Task<IActionResult> LoginAdministratorUser(string email, string password)
        {
            _logger.LogInformation("Consultando credenciales Administrador");
            return await HandleResponse(async () =>
            { 
                return await _adminUserService.LoginAdmin(email, password);
            }, "Usuario administrador encontrado");
        }
    }
}
