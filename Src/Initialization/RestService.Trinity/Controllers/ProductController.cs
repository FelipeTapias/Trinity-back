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
            _logger.LogInformation("Obteniendo todos los productos");
            return await HandleResponse(async () =>
            {
                return await _productService.CreateProduct(product);
            }, "Usuarios obtenidos correctamente");
        }
    }
}
