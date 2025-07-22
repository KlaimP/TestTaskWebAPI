using DBApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace DBApi.Repository
{
    public interface IRepository
    {
        public Task<List<Values>> GetValues(string fileName);
        public Task<bool> AddValue(Values _value);
        public Task<bool> AddValues(List<CsvValues> _values, string fileName);
        public Task<bool> CalculateResults(List<CsvValues> _values, string filename);
        public Task<List<Results>> getResults([FromQuery] string? fileName,
        [FromQuery] DateTime? minDate,
        [FromQuery] DateTime? maxDate,
        [FromQuery] double? minAvgValue,
        [FromQuery] double? maxAvgValue,
        [FromQuery] double? minAvgExecutionTime,
        [FromQuery] double? maxAvgExecutionTime);
    }
}
