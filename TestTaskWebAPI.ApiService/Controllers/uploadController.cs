using Microsoft.AspNetCore.Mvc;
using DBApi.Models;
using CsvHelper;
using static TestTaskWebApi.ApiServer.Controllers.csvController;
using System.Globalization;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class uploadController : Controller
    {
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Файл не выбран");

            List<Person> records;
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Person>().ToList();
            }

            var outPath = Path.Combine(Directory.GetCurrentDirectory(), "uploaded_output.csv");
            using (var writer = new StreamWriter(outPath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(records);
            }

            return Ok(new { count = records.Count, savedTo = outPath });
        }
    }
}
