using Microsoft.Azure.CosmosDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleapp.Todo.Models.DAO
{
    public class TodoEntity : TableEntity
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
        public string Due { get; set; }
    }
}
