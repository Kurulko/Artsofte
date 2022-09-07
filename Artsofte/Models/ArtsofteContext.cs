using Microsoft.EntityFrameworkCore;

namespace Artsofte.Models
{
    public class ArtsofteContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ArtsofteContext(DbContextOptions<ArtsofteContext> options) : base(options)
            //=> Database.EnsureCreated();
            {}
    }
}
