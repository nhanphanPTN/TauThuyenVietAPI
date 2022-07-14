using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet ("Add")]
        public double Add( double a, double b)
        {
            return a + b; 
        }

        [HttpGet("Minus")]
        public double Minus(double a, double b)
        {
            return a - b;
        }

        [HttpGet("Multiply")]
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        [HttpGet("Divide")]
        public double Divide(double a, double b)
        {
            if (b == 0)
                return 0;
            return (a / b);

        }
    }
}
