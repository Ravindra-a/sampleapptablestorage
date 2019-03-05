using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sampleapp.Todo.Models.DTO;
using sampleapp.Todo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sampleapp.Todo.Controllers
{
    public class TodoController : Controller
    {
        TodoRepository repository = new TodoRepository();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            var todoItems = repository.GetAll();
            var models = todoItems.Select(x => new TodoEntity
            {
                Id = x.RowKey,
                Group = x.PartitionKey,
                Content = x.Content,
                Completed = x.Completed,
                Due = x.Due,
                Timestamp = x.Timestamp
            });
            return View(models);
        }

        /// <summary>
        /// Get filtered view
        /// </summary>
        /// <returns></returns>
        public IActionResult Filter()
        {

            var todoItems = repository.GetAllCompletedActivitiesByFilter("PartitionKey","Vacation");
            var models = todoItems.Select(x => new TodoEntity
            {
                Id = x.RowKey,
                Group = x.PartitionKey,
                Content = x.Content,
                Completed = x.Completed,
                Due = x.Due,
                Timestamp = x.Timestamp
            });
            return View(models);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public ActionResult Details(string id, string group)
        {
            var item = repository.Get(group, id);
            return View(new TodoEntity
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Completed = item.Completed,
                Due = item.Due,
                Timestamp = item.Timestamp
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(TodoEntity model)
        {
            repository.Create(new Models.DAO.TodoEntity {
                PartitionKey = model.Group,
                RowKey = Guid.NewGuid().ToString(),
                Content = model.Content,
                Due = model.Due
            });
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public ActionResult ConfirmDelete(string id, string group)
        {
            var item = repository.Get(group, id);
            return View("Delete",new TodoEntity {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Completed = item.Completed,
                Due = item.Due,
                Timestamp = item.Timestamp
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id, string group)
        {
            var item = repository.Get(group, id);
            repository.Delete(item);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public ActionResult Edit(string id, string group)
        {
            var item = repository.Get(group, id);
            return View("Edit", new TodoEntity
            {
                Id = item.RowKey,
                Group = item.PartitionKey,
                Content = item.Content,
                Completed = item.Completed,
                Due = item.Due,
                Timestamp = item.Timestamp
            });
        }

        [HttpPost]
        public ActionResult Edit(TodoEntity model)
        {
            var item = repository.Get(model.Group, model.Id);
            item.Completed = model.Completed;
            item.Content = model.Content;
            item.Due = model.Due;
            repository.Upsert(item);
            return RedirectToAction("Index");
        }
    }
}
