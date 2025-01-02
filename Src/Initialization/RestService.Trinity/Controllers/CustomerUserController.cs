using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RestService.Trinity.Controllers.Base;

namespace RestService.Trinity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerUserController : BaseController
    {
        private readonly ILogger<CustomerUserController> _logger;
        private readonly ICustomerUserService _userService;

        public CustomerUserController(ILogger<CustomerUserController> logger, ICustomerUserService userService): base(logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("CreateCustomerUser", Name = "CreateCustomerUser")]
        public async Task<IActionResult> Post(CustomerUser customerUser)
        {
            _logger.LogInformation("Creando el cliente usuario");
            return await HandleResponse(async () =>
            {
                return await _userService.CreateUser(customerUser);
            }, "Usuario creado correctamente");
        }

        [HttpGet("GetAllCustomerUsers", Name = "GetAllCustomerUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            _logger.LogInformation("Obteniendo todos los clientes usuarios");
            return await HandleResponse(async () =>
            {
                return await _userService.GetAllUser();
            }, "Usuarios obtenidos correctamente");
        }

        [HttpGet("GetCustomerUserById", Name = "GetCustomerUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string customerId)
        {
            _logger.LogInformation($"Obteneindo cliente usuario con id: {customerId}");
            return await HandleResponse(async () =>
            {
                return await _userService.GetUserById(customerId);
            }, $"Cliente usuario con CustomerId {customerId} obtenido correctamente");
        }

        [HttpDelete("DeleteCustomerUserById", Name = "DeleteCustomerUserById")]
        public async Task<IActionResult> DeleteUserById([FromQuery] string customerId)
        {
            _logger.LogInformation($"Eliminando usuario con id: {customerId}");
            return await HandleResponse(async () => {
                return await _userService.DeleteUserById(customerId);
            }, $"Usuario con Id {customerId} eliminado correctamente");
        }

        [HttpPut("EditCustomerUserById", Name = "EditCustomerUserById")]
        public async Task<IActionResult> UpdateUserById(string customerId, CustomerUser customerUser)
        {
            _logger.LogInformation($"Actualizando usuario con id: {customerId}");
            return await HandleResponse(async () => {
                return await _userService.UpdateUserById(customerId, customerUser);
            }, $"Usuario con Id {customerId} actualizado correctamente");
        }

        [HttpGet("GetCustomerUserByDocumentNumber", Name = "GetCustomerUserByDocumentNumber")]
        public async Task<IActionResult> GetUserByIdDocument([FromQuery] string documentNumber)
        {
            _logger.LogInformation($"Obteniendo cliente usuario con DocumentNumber: {documentNumber}");
            return await HandleResponse(async () => {
                return await _userService.GetUserByIdDocument(documentNumber);
            }, $"Cliente usuario con IdDocument {documentNumber} obtenido correctamente");
        }

        [HttpGet("GetIdByDocumentNumber", Name = "GetIdByDocumentNumber")]
        public async Task<IActionResult> GetIdByIdDocument([FromQuery] string documentNumber)
        {
            _logger.LogInformation($"Obteniendo CustomerId con DocumentNumber: {documentNumber}");
            return await HandleResponse(async () => {
                return await _userService.GetIdByIdDocument(documentNumber);
            }, $"CustomerId de usuario con DocumentNumber {documentNumber} obtenido correctamente");
        }
    }
}
