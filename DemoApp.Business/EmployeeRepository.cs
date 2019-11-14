using DemoApp.Data.Context;
using DemoApp.Data.Models;
using DemoApp.IBusiness;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DemoApp.Business
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        //private readonly EmployeeContext _context;
        private readonly EmployeeContext _employeeDatabaseEntities;

        public EmployeeRepository(EmployeeContext employeeDatabaseEntities): base(employeeDatabaseEntities)
        {
            _employeeDatabaseEntities = employeeDatabaseEntities;
        }

        public IEnumerable<Employee> GetBestEmployees(int amountOfEmployees)
        {
            return _employeeDatabaseEntities.DbSetEmployee.OrderByDescending(x => x.Id).Take(amountOfEmployees).ToList();
        }

        //public EmployeeRepository(EmployeeContext context)
        //{
        //    _context = context;
        //}
        //public void AddEmployee(Employee employee)
        //{
        //    _context.DbSetEmployee.Add(employee);
        //}

        //public void DeleteEmployee(Employee employee)
        //{
        //    _context.DbSetEmployee.Remove(employee);
        //}

        //public async Task<Employee[]> GetAllEmployeesAsync()
        //{
        //    IQueryable<Employee> query = _context.DbSetEmployee;

        //    query = query.OrderByDescending(c => c.Id);

        //    return await query.ToArrayAsync();
        //}

        //public async Task<Employee> GetEmployeeAsync(int id)
        //{
        //    IQueryable<Employee> query = _context.DbSetEmployee;

        //    query = query.Where(c => c.Id == id);

        //    return await query.FirstOrDefaultAsync(); ;
        //}

        //public async Task<bool> SaveChangesAsync()
        //{
        //    // Only return success if at least one row was changed
        //    return (await _context.SaveChangesAsync()) > 0;
        //}


    }
}
