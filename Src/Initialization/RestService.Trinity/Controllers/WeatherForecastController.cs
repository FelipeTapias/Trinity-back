using Aplication.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace RestService.Trinity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository _userRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost(Name = "PostUser")]
        public async Task<string> Post()
        {
            _logger.LogInformation("Creando el usuario");
            string idReturn = await _userRepository.InsertDocument();
            _logger.LogInformation($"Se creo el usuario con el ID: {idReturn}");
            return idReturn;
        }
    }
}
