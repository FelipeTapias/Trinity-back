using Aplication.Interfaces.Application;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RestService.Trinity.Controllers.Base;

namespace RestService.Trinity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger): base(logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment", Name = "CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            _logger.LogInformation("Creando Pago");
            return await HandleResponse(async () =>
            {
                return await _paymentService.CreatePayment(payment);
            }, "Pago creado correctamente");
        }
    }
}
