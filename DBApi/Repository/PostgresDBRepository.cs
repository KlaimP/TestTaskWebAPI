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
        public bool AddValues(Values _values)
        {
            _values.Id = values.OrderBy(p => p.Id).LastOrDefault()?.Id + 1 ?? 1;
            values.Add(_values);
            this.SaveChanges();
            return true;
        }
    }
}
