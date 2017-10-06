using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreMongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Microsoft.Extensions.Configuration;
using System.Security.Authentication;

namespace ApiCoreMongoDb.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase empContext;
        public IConfiguration Config;
        public EmployeeRepository(IConfiguration config)
        {
            Config = config;
            //string connectionString = Startup.ConnectionString;
            string env = Config.GetSection("Environment").GetSection("Env").Value;

            if (env == "local")
            {
                _client = new MongoClient(Config.GetSection("Data").GetSection("MongoDBConnectionString").Value);
                _server = _client.GetServer();
                empContext = _server.GetDatabase("Employee");
            }
            else
            {
                //MongoClientSettings settings = new MongoClientSettings();
                //settings.Server = new MongoServerAddress(Config.GetSection("Data").GetSection("Host").Value, 10255);
                //settings.UseSsl = true;
                //settings.SslSettings = new SslSettings();
                //settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

                //MongoIdentity identity = new MongoInternalIdentity("Employee", Config.GetSection("Data").GetSection("employeemongodb").Value);
                //MongoIdentityEvidence evidence = new PasswordEvidence(Config.GetSection("Data").GetSection("Password").Value);

                //settings.Credentials = new List<MongoCredential>()
                //{
                //    new MongoCredential("SCRAM-SHA-1", identity, evidence)
                //};
                //_client = new MongoClient(settings);

                _client = new MongoClient(Config.GetSection("Data").GetSection("MongoDBConnectionString").Value);
                _server = _client.GetServer();
                empContext = _server.GetDatabase("Employee");
            }
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return empContext.GetCollection<Employee>("Employee").FindAll(); 
        }
        public Employee GetEmployee(int EmpId)
        {
            ObjectId objID;
            var empQ = Query<Employee>.EQ(e => e.EmployeeId, EmpId);
            var empObj = empContext.GetCollection<Employee>("Employee").FindOne(empQ);
            objID = empObj.Id;

            var emp = Query<Employee>.EQ(e => e.Id, objID);
            return empContext.GetCollection<Employee>("Employee").FindOne(emp);
        }
        public void AddEmployee(Employee emp)
        {
            empContext.GetCollection<Employee>("Employee").Save(emp);
        }
        public void UpdateEmployee(int EmpId, Employee emp)
        {
            ObjectId objID;
            var empQ = Query<Employee>.EQ(e => e.EmployeeId, EmpId);
            var empObj = empContext.GetCollection<Employee>("Employee").FindOne(empQ);
            objID = empObj.Id;

            UpdateBuilder ub = MongoDB.Driver.Builders.Update.Set("Id", emp.EmployeeId)
                .Set("FirstName", emp.FirstName)
                .Set("LastName", emp.LastName)
                .Set("Title", emp.Title);
            var res = Query<Employee>.EQ(pd => pd.Id, objID);
            
            empContext.GetCollection<Employee>("Employee").Update(res, ub);
        }
        public void DeleteEmployee(int EmpId)
        {
            var emp = Query<Employee>.EQ(e => e.EmployeeId, EmpId);
            var operation = empContext.GetCollection<Employee>("Employee").Remove(emp);
        }
    }
}
