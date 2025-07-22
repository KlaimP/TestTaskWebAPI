using Microsoft.AspNetCore.Mvc;
using DBApi.Models;
using DBApi.Repository;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class addValuesController : ControllerBase
    {
        private readonly PostgresDBRepository _db;

        public addValuesController(PostgresDBRepository db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Values value)
        {
            if (value == null)
                return BadRequest("Empty object");

            try
            {
                var result = _db.AddValue(value);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
    }
}
