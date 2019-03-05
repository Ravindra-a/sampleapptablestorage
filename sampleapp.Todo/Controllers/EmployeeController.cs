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
    public class EmployeeController : Controller
    {
        EmployeeRepository repository = new EmployeeRepository();
        // GET: /<controller>/
        public IActionResult Index()
        {

            var customers = repository.GetAll();
            var models = customers.Select(x => new Employee
            {
                RowKey = x.RowKey,
                PartitionKey = x.PartitionKey,
                EmployeeID = x.EmployeeID,
                EmployeeType = x.EmployeeType,
                EmployeeDetails = x.EmployeeDetails,
                EmployeeName = x.EmployeeName,
                Timestamp = x.Timestamp
            });
            return View(models);
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
        public ActionResult Create(Employee model)
        {
            repository.Upsert(new Models.DAO.EmployeeEntity(model.EmployeeID, model.EmployeeType)
            {
                EmployeeID = model.EmployeeID,
                EmployeeType = model.EmployeeType,
                EmployeeDetails = model.EmployeeDetails,
                EmployeeName = model.EmployeeName,
            });
            return RedirectToAction("Index");
        }
    }
}
