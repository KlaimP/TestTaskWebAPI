using DBApi.Models;
using DBApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getValuesController : Controller
    {
        private readonly IRepository _repository;

        public getValuesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult getValues(string fileName)
        {
            return Ok(new
            {
                message = _repository.GetValues(fileName),
                timestamp = DateTime.UtcNow
            }); 
        }
    }
}
