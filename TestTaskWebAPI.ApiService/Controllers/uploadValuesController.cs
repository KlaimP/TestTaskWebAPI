using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using DBApi.Models;
using DBApi.Repository;
using Google.Protobuf.WellKnownTypes;

namespace TestTaskWebAPI.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class uploadValuesController : ControllerBase
    {
        private readonly IRepository _db;

        public uploadValuesController(IRepository db)
        {
            _db = db;
        }

        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
        [HttpPost]
        public async Task<IActionResult> UploadValues(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("The file is not selected");
            }

            List<CsvValues> records;
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<CsvValues>().ToList();
                }
            }
            catch (HeaderValidationException)
            {
                return BadRequest("Incorrect CSV headers. Required: Date;Execution Time;Value");
            }
            catch (Exception error)
            {
                return BadRequest($"Error reading CSV: {error.Message}");
            }
            

            if(records.Count is < 1 or > 10000)
            {
                return BadRequest($"The allowed number of rows is from 1 to 10000.");
            }

            int errorRow = -1;
            DateTime minDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime maxDate = DateTime.UtcNow;

            for(int i = 0; i < records.Count; i++)
            {
                if (records[i].Date > maxDate || records[i].Date < minDate)
                {
                    errorRow = i;
                    return BadRequest($"The date on line {errorRow} is incorrect. The date can be from 01.01.2000 to today.");
                }

                if (records[i].ExecutionTime < 0)
                {
                    errorRow = i;
                    return BadRequest($"The error is in line {errorRow}. The execution time cannot be less than 0.");
                }
            }

            bool added = await _db.AddValues(records, file.FileName);
            if (!added)
                return BadRequest("Couldn't add values");

            bool calculated = await _db.CalculateResults(records, file.FileName);
            if (!calculated)
                return BadRequest("Couldn't calculate the results");

            return Ok(new { count = records.Count, message = records });
        }
    }
}
