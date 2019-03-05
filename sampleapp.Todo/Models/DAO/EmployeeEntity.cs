using Microsoft.Azure.CosmosDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapp.Todo.Models.DAO
{
    public class EmployeeEntity : TableEntity
    {
        private int employeeID;
        private string employeeName;
        private string employeeDetails;
        private string employeeType;

        public EmployeeEntity()
        {
        }

        public EmployeeEntity(int employeeID, string employeeType)
        {
            this.RowKey = employeeID.ToString();
            this.PartitionKey = employeeType;
        }

        public int EmployeeID
        {
            get
            {
                return employeeID;
            }

            set
            {
                employeeID = value;
            }
        }
        public string EmployeeName
        {
            get
            {
                return employeeName;
            }

            set
            {
                employeeName = value;
            }
        }
        public string EmployeeDetails
        {
            get
            {
                return employeeDetails;
            }

            set
            {
                employeeDetails = value;
            }
        }

        public string EmployeeType
        {
            get
            {
                return employeeType;
            }

            set
            {
                employeeType = value;
            }
        }
    }
}
