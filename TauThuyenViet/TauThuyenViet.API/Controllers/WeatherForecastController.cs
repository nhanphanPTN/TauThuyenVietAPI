using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public object Get()
        {
            var rng = new Random();
            var TemperatureC = rng.Next(1, 9);
            return TemperatureC;
        }
    }
}