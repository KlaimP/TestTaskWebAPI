using Microsoft.AspNetCore.Mvc;
using DBApi.Repository;

namespace DBApi.Web.Controllers // или другой namespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbCheckController : ControllerBase
    {
        private readonly PostgresDBRepository _db;

        public DbCheckController(PostgresDBRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                bool canConnect = _db.Database.CanConnect();
                return Ok(new { connected = canConnect });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
