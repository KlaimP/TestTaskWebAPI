using DBApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getResultsController : Controller
    {
        private readonly PostgresDBRepository _db;

        public getResultsController(PostgresDBRepository db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult FilterResults([FromQuery] string? fileName, [FromQuery]DateTime? minDate, [FromQuery]DateTime? maxDate, [FromQuery]double? minAvgValue, [FromQuery]double? maxAvgValue, [FromQuery] double? minAvgExecutionTime,[FromQuery] double? maxAvgExecutionTime)
        {
            return View();
        }
    }
}
