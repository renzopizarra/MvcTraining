using DemoApp.Data.Models;
using System.Data.Entity;

namespace DemoApp.Data.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("EmployeeDatabaseEntities")
        {
            
        }

        public DbSet<Employee> DbSetEmployee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); 
        }
    }
}
