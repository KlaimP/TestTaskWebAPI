using Microsoft.AspNetCore.Mvc;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class testController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = "Всё работает!",
                timestamp = DateTime.UtcNow
            }); 
        }
    }
}
