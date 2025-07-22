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
            try
            {
                this.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool CalculateResults(List<CsvValues> _values, string filename)
        {
            if (_values == null || _values.Count == 0)
            {
                return false;
            }

            double sumExecutionTime = 0, sumValue = 0;
            double minValue = double.MaxValue, maxValue = double.MinValue;
            DateTime minDate = _values[0].Date, maxDate = _values[0].Date;

            double[] valueList = new double[_values.Count];


            for (int i = 0; i < _values.Count; i++) 
            {
                sumExecutionTime += _values[i].ExecutionTime;
                sumValue += _values[i].Value;

                if (_values[i].Value < minValue) 
                    minValue = _values[i].Value;

                if (_values[i].Value > maxValue) 
                    maxValue = _values[i].Value;

                if (_values[i].Date < minDate) 
                    minDate = _values[i].Date;

                if (_values[i].Date > maxDate) 
                    maxDate = _values[i].Date;
                valueList[i] = _values[i].Value;
            }

            Array.Sort(valueList); 
            double median = _values.Count % 2 == 0 ? (valueList[_values.Count / 2 - 1] + valueList[_values.Count / 2]) / 2.0 : valueList[_values.Count / 2];
            float timeDelta = (float)(maxDate - minDate).TotalSeconds;
            double avgExecutionTime = sumExecutionTime / _values.Count;
            double avgValue = sumValue / _values.Count;

            Results _result = new Results(filename, timeDelta, minDate.ToUniversalTime(), avgExecutionTime, avgValue, median, maxValue, minValue);
            try
            {
                results.Add(_result);
                this.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
