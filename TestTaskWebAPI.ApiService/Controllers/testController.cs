using DBApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class testController : Controller
    {
        private readonly IRepository _repository;

        public testController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = _repository.GetValues(),
                timestamp = DateTime.UtcNow
            }); 
        }
    }
}
