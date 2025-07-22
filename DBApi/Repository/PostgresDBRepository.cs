using DBApi.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using DBApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DBApi.Repository
{
    public class PostgresDBRepository : DbContext, IRepository
    {            
        public PostgresDBRepository(DbContextOptions<PostgresDBRepository> options) : base(options) { }

        public DbSet<Values> values { get; set; }
        public DbSet<Results> results { get; set; }
        public async Task<List<Values>> GetValues(string fileName)
        {
            List<Values> value = new List<Values>(values.Where(r => r.FileName == fileName).OrderByDescending(r => r.Date).Take(10));

            return value;
        }
        public async Task<bool> AddValue(Values _value)
        {
            _value.Id = values.OrderBy(p => p.Id).LastOrDefault()?.Id + 1 ?? 1;
            values.Add(_value);
            await this.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddValues(List<CsvValues> _values, string fileName)
        {
            int lastId = values.OrderBy(p => p.Id).LastOrDefault()?.Id ?? 0;

            foreach (CsvValues value in _values)
            {
                lastId++;
                var dateUtc = value.Date.Kind == DateTimeKind.Utc ? value.Date : value.Date.ToUniversalTime();
                values.Add(new Values(lastId, dateUtc, value.ExecutionTime, value.Value, fileName));
            }
            try
            {
                await this.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CalculateResults(List<CsvValues> _values, string filename)
        {
            if (_values == null || _values.Count == 0)
            {
                return false;
            }

            ResultsCalculate calculator = new ResultsCalculate();
            Results _result = calculator.CalculateResults(_values, filename);

            _result.Id = results.OrderBy(p => p.Id).LastOrDefault()?.Id + 1 ?? 1;

            try
            {
                results.Add(_result);
                await this.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Results>> getResults(
        [FromQuery] string? fileName,
        [FromQuery] DateTime? minDate,
        [FromQuery] DateTime? maxDate,
        [FromQuery] double? minAvgValue,
        [FromQuery] double? maxAvgValue,
        [FromQuery] double? minAvgExecutionTime,
        [FromQuery] double? maxAvgExecutionTime)
        {
            List<Results> query = new List<Results>(results);

            if (!string.IsNullOrWhiteSpace(fileName))
                query = new List<Results>(query.Where(r => r.FileName == fileName));

            if (minDate.HasValue)
                query = new List<Results>(query.Where(r => r.MinDate >= minDate.Value.ToUniversalTime()));

            if (maxDate.HasValue)
                query = new List<Results>(query.Where(r => r.MinDate <= maxDate.Value.ToUniversalTime()));

            if (minAvgValue.HasValue)
                query = new List<Results>(query.Where(r => r.AvgValue >= minAvgValue.Value));

            if (maxAvgValue.HasValue)
                query = new List<Results>(query.Where(r => r.AvgValue <= maxAvgValue.Value));

            if (minAvgExecutionTime.HasValue)
                query = new List<Results>(query.Where(r => r.AvgExecutionTime >= minAvgExecutionTime.Value));

            if (maxAvgExecutionTime.HasValue)
                query = new List<Results>(query.Where(r => r.AvgExecutionTime <= maxAvgExecutionTime.Value));

            return query;
        }
    }
}
