using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiCoreMongoDb.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }
        [BsonElement("Id")]
        public int EmployeeId { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
    }
}
