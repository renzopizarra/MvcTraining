using DemoApp.Data.Context;
using DemoApp.IBusiness;

namespace DemoApp.Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeContext _employeeDatabaseEntities;
        public IEmployeeRepository Employees { get; }
        
        public UnitOfWork(EmployeeContext employeeDatabaseEntities)
        {
            _employeeDatabaseEntities = employeeDatabaseEntities;

            Employees = new EmployeeRepository(_employeeDatabaseEntities);
        }

        public int Complete()
        {
            return _employeeDatabaseEntities.SaveChanges();
        }

        public void Dispose()
        {
            _employeeDatabaseEntities.Dispose();
        }
    }
}
