using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapp.Todo.Models.DTO
{
    public class Employee
    {
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDetails { get; set; }
        public string EmployeeType { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
