using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ImportCsvToSQL.Models
{
    // Database connection
    public class SQLDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}