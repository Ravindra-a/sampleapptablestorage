using sampleapp.Todo.Models.DAO;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapp.Todo.Repositories
{
    public class EmployeeRepository
    {
        private CloudTable customerTable = null;

        public EmployeeRepository()
        {
            //Add reference to storage account through connection string
            var storageAccount = CloudStorageAccount.Parse("<YOUR STORAGE CONNECTION STRING>");

            //Create table client
            var tableClient = storageAccount.CreateCloudTableClient();

            customerTable = tableClient.GetTableReference("Employee");

            //create table if it doesn't exists
            customerTable.CreateIfNotExists();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeEntity> GetAll()
        {
            var query = new TableQuery<EmployeeEntity>();

            var customers = customerTable.ExecuteQuery(query);

            return customers;
        }

        /// <summary>
        /// Create/update entity
        /// </summary>
        /// <param name="entity"></param>
        public void Upsert(EmployeeEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);
            customerTable.Execute(operation);
        }
    }
}
