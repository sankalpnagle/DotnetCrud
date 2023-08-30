using Microsoft.EntityFrameworkCore;
using Webapp.Model;

namespace Webapp.Data
{
    public class EmployeeApiDbContext : DbContext
    {
        public EmployeeApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }  
    }
}
