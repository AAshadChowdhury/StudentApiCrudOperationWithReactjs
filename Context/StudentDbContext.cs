using Microsoft.EntityFrameworkCore;
using StudentCrudOperationWithReactjs.Model;

namespace StudentCrudOperationWithReactjs.Context
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; database = studentdb; TrustServerCertificate = True");
        }
    }
}