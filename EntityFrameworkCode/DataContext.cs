using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCode
{
    public class DataContext : DbContext
    {
        private const string sqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;server=localhost;database=EntityFrameworkCodeConventions";
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(sqlConnectionString, builder => builder.EnableRetryOnFailure());
            }
        }
    }
    public class Department
    {
        // Primary key
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
    }

    public class Employee
    {
        // Primary key
        public int EmployeeId { get; set; }

        public int Title { get; set; }

        // Foreign key
        public int DepartmentId { get; set; }

        // Foreign key??? - No
        public int PersonId { get; set; }

    }

    public class Person
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
    }
}