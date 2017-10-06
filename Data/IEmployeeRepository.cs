using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreMongoDb.Models;

namespace ApiCoreMongoDb.Data
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int EmpId);
        void AddEmployee(Employee emp);
        void UpdateEmployee(int EmpId, Employee emp);
        void DeleteEmployee(int EmpId);
    }
}
