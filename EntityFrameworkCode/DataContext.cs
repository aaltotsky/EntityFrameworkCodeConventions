using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCode
{
    public class DataContext : DbContext
    {
        private const string sqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;server=localhost;database=EntityFrameworkCodeConventions";
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }

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
        public string? PersonId { get; set; }
        public string? ProductId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Person Person { get; set; }
        public virtual Product Product { get; set; }

    }

    public class Person
    {
        public string Id { get; set; }

        public string? FullName { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }

        public string? Name { get; set; }
    }
}