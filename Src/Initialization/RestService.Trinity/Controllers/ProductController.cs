using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RestService.Trinity.Controllers.Base;

namespace RestService.Trinity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger): base(logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            _logger.LogInformation("Creando Producto");
            return await HandleResponse(async () =>
            {
                return await _productService.CreateProduct(product);
            }, "Producto creado correctamente");
        }

        [HttpGet("GetAllProductsByCustomer", Name = "GetAllProductsByCustomer")]
        public async Task<IActionResult> GetAllProductsByCustomer([FromQuery] string customerId)
        {
            _logger.LogInformation($"Obteneido Productos del cliente: {customerId}");
            return await HandleResponse(async () =>
            {
                return await _productService.GetAllProductByCustomerId(customerId);
            }, "Productos obtenidos correctamente");
        }
    }
}
