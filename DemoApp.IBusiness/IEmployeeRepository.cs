using DemoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.IBusiness
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // General 
        //Task<bool> SaveChangesAsync();

        // Employees
        //void AddEmployee(Employee employee);
        //void DeleteEmployee(Employee employee);
        //Task<Employee[]> GetAllEmployeesAsync();
        //Task<Employee> GetEmployeeAsync(int id);

        IEnumerable<Employee> GetBestEmployees(int amountOfEmployees);

    }
}
