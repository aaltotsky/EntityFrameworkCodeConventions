using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string currentDate = "GETDATE()";

            var applicationEntityTypes = typeof(BaseEntity).GetTypeInfo().Assembly.GetTypes().Where(
                t => t.GetTypeInfo().IsClass
                && typeof(BaseEntity).IsAssignableFrom(t)
                && !t.GetTypeInfo().IsAbstract
                && t.GetTypeInfo().IsSubclassOf(typeof(BaseEntity)));

            foreach (Type type in applicationEntityTypes)
            {
                var dbSetType = this.GetType()
                    .GetRuntimeProperties()
                    .Where(o => o.PropertyType.GetTypeInfo().IsGenericType
                    && o.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                    && o.PropertyType.GenericTypeArguments.Contains(type))
                    .FirstOrDefault();

                if (dbSetType != null)
                {
                    MethodInfo method = typeof(DataContext).GetMethod("BaseEntityDefaultValueSql", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (method == null)
                    {
                        throw new NotImplementedException("The 'BaseEntityDefaultValueSql' method is not implemented");
                    }

                    MethodInfo generic = method.MakeGenericMethod(type);
                    generic?.Invoke(this, new object[] { builder });
                }
            }
        }

        private ModelBuilder BaseEntityDefaultValueSql<TEntity>(ModelBuilder builder)
            where TEntity : BaseEntity
        {
            string currentDate = "GETDATE()";

            return builder.Entity<TEntity>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql(currentDate);
            });
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

    public class Person : BaseEntity
    {
        public string? FullName { get; set; }
    }

    public class Product : BaseEntity
    {
        public string? Name { get; set; }
    }
}