using DBApi.Repository;
using DBApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getResultsController : Controller
    {
        private readonly IRepository _db;

        public getResultsController(IRepository db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> FilterResults(
            [FromQuery] string? fileName, 
            [FromQuery]DateTime? minDate, 
            [FromQuery]DateTime? maxDate, 
            [FromQuery]double? minAvgValue, 
            [FromQuery]double? maxAvgValue, 
            [FromQuery] double? minAvgExecutionTime,
            [FromQuery] double? maxAvgExecutionTime)
        {
            var result = await _db.getResults(
                     fileName,
                     minDate,
                     maxDate,
                     minAvgValue,
                     maxAvgValue,
                     minAvgExecutionTime,
                     maxAvgExecutionTime);
            return Ok(result);
        }
    }
}
