using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCode
{
    public class DataContext : DbContext
    {
        public DbSet<>

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