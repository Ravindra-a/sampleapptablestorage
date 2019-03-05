using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.CosmosDB.Table;
using sampleapp.Todo.Models.DAO;


namespace sampleapp.Todo.Repositories
{
    public class TodoRepository
    {
        private CloudTable todoTable = null;
        public TodoRepository()
        {
            //Add reference to storage account through connection string
            var storageAccount = CloudStorageAccount.Parse("<YOUR STORAGE CONNECTION STRING>");
            
            //Create table client
            var tableClient = storageAccount.CreateCloudTableClient();

            todoTable = tableClient.GetTableReference("Todo");

            //create table if it doesn't exists
            //todoTable.CreateIfNotExists();
        }

        /// <summary>
        /// Get alltodoitems
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoEntity> GetAll()
        {
            var query = new TableQuery<TodoEntity>();

            var todoList = todoTable.ExecuteQuery(query);

            return todoList;
        }

        /// <summary>
        /// Get by vacation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<TodoEntity> GetAllCompletedActivitiesByFilter(string column, string input)
        {
            var filter = TableQuery.GenerateFilterCondition(column, QueryComparisons.Equal, input);

            var isCompleted = TableQuery.GenerateFilterConditionForBool(nameof(TodoEntity.Completed), QueryComparisons.Equal, true);

            string finalFilter = TableQuery.CombineFilters(filter
                , TableOperators.And, isCompleted);

            //for more than 2 filters
            //string multipleFilters = TableQuery.CombineFilters(finalFilter
            //    , TableOperators.And, isCompleted);

            var query = new TableQuery<TodoEntity>().Where(finalFilter);

            var todoList = todoTable.ExecuteQuery(query);

            return todoList;
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TodoEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            todoTable.Execute(operation);
        }

        /// <summary>
        /// Create/update entity
        /// </summary>
        /// <param name="entity"></param>
        public void Upsert(TodoEntity entity)
        {
            var operation = TableOperation.InsertOrReplace(entity);
            todoTable.Execute(operation);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TodoEntity entity)
        {
            var operation = TableOperation.Delete(entity);
            todoTable.Execute(operation);
        }

        /// <summary>
        /// Get entity by partitionkey/rowkey
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public TodoEntity Get(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TodoEntity>(partitionKey, rowKey);
            var result = todoTable.Execute(operation);
            return result.Result as TodoEntity;
        }
    }
}
