using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestService.Trinity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerUserController : ControllerBase
    {
        private readonly ILogger<CustomerUserController> _logger;
        private readonly ICustomerUserService _userService;

        public CustomerUserController(ILogger<CustomerUserController> logger, ICustomerUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("CreateCustomerUser", Name = "CreateCustomerUser")]
        public async Task<string> Post(CustomerUser user)
        {
            _logger.LogInformation("Creando el cliente usuario");
            string idReturn = await _userService.CreateUser(user);
            _logger.LogInformation($"Se creo el cliente usuario con el ID: {idReturn}");
            return idReturn;
        }

        [HttpGet("GetAllCustomerUsers", Name = "GetAllCustomerUsers")]
        public async Task<IEnumerable<CustomerUser>> GetAllUser()
        {
            _logger.LogInformation("Obteniendo todos los clientes usuarios");
            return await _userService.GetAllUser();
        }

        [HttpGet("GetCustomerUserById", Name = "GetCustomerUserById")]
        public async Task<CustomerUser> GetUserById([FromQuery] string customerId)
        {
            _logger.LogInformation($"Obteneindo cliente usuario con id: {customerId}");
            return await _userService.GetUserById(customerId);
        }

        [HttpDelete("DeleteCustomerUserById", Name = "DeleteCustomerUserById")]
        public async Task<string> DeleteUserById([FromQuery] string customerId)
        {
            _logger.LogInformation($"Eliminando usuario con id: {customerId}");
            return await _userService.DeleteUserById(customerId);
        }

        [HttpPut("EditCustomerUserById", Name = "EditCustomerUserById")]
        public async Task<string> UpdateUserById(string customerId, CustomerUser user)
        {
            _logger.LogInformation($"Actualizando usuario con id: {customerId}");
            await _userService.UpdateUserById(customerId, user);
            return customerId;
        }

        [HttpGet("GetCustomerUserByDocumentNumber", Name = "GetCustomerUserByDocumentNumber")]
        public async Task<CustomerUser> GetUserByIdDocument([FromQuery] string documentNumber)
        {
            _logger.LogInformation($"Obteniendo cliente usuario con DocumentNumber: {documentNumber}");
            return await _userService.GetUserByIdDocument(documentNumber);
        }

        [HttpGet("GetIdByDocumentNumber", Name = "GetIdByDocumentNumber")]
        public async Task<string> GetIdByIdDocument([FromQuery] string documentNumber)
        {
            _logger.LogInformation($"Obteniendo CustomerId con DocumentNumber: {documentNumber}");
            return await _userService.GetIdByIdDocument(documentNumber);
        }
    }
}
