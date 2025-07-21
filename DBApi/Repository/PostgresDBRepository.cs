using DBApi.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApi.Repository
{
    public class PostgresDBRepository : DbContext, IRepository
    {            
        public PostgresDBRepository(DbContextOptions<PostgresDBRepository> options) : base(options) { }

        public DbSet<Values> values { get; set; }
        public DbSet<Results> results { get; set; }
        public ObservableCollection<Values> GetValues()
        {
            return new ObservableCollection<Values>(values.OrderBy(p => p.Id));
        }
        public bool AddValue(Values _value)
        {
            _value.Id = values.OrderBy(p => p.Id).LastOrDefault()?.Id + 1 ?? 1;
            values.Add(_value);
            this.SaveChanges();
            return true;
        }
        public bool AddValues(List<CsvValues> _values)
        {
            int lastId = values.OrderBy(p => p.Id).LastOrDefault()?.Id ?? 0;

            foreach (CsvValues value in _values)
            {
                lastId++;
                var dateUtc = value.Date.Kind == DateTimeKind.Utc ? value.Date : value.Date.ToUniversalTime();
                values.Add(new Values(lastId, dateUtc, value.ExecutionTime, value.Value));
            }

            this.SaveChanges();
            return true;
        }
    }
}
