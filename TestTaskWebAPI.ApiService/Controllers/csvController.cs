using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace TestTaskWebApi.ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class csvController : ControllerBase
    {
        public record Person(string Name, int Age, string Email);
        
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
        [HttpPost("upload_")]
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
