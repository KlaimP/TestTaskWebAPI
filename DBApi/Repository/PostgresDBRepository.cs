using DBApi.Models;
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

        public DbSet<Values> Values { get; set; }
        public DbSet<Results> Results { get; set; }
    }
}
