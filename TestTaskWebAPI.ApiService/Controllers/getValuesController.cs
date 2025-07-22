using DBApi.Models;
using DBApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class getValuesController : Controller
    {
        private readonly IRepository _db;

        public getValuesController(IRepository repository)
        {
            _db = repository;
        }

        [HttpGet]
        public async Task<IActionResult> getValues(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name is empty");
            }
            List<Values> result = await _db.GetValues(fileName);
            return Ok(result); 
        }
    }
}
