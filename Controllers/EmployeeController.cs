using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiCoreMongoDb.Data;
using ApiCoreMongoDb.Models;

namespace ApiCoreMongoDb.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository empRepository)
        {
            employeeRepository = empRepository;
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeRepository.GetAllEmployees();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public Employee Get(int id)
        {
            return employeeRepository.GetEmployee(id);
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody]Employee value)
        {
            employeeRepository.AddEmployee(value);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Employee value)
        {
            employeeRepository.UpdateEmployee(id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeRepository.DeleteEmployee(id);
        }
    }
}