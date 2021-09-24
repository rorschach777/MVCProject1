using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ExpenseController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = _db.Expenses;
            return View(model);
        }

        [HttpGet]
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            // Server side 
            if (ModelState.IsValid)
            {
                // Checks the data attributes on the model
                _db.Expenses.Add(expense);
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View(expense);
    
        }
    }
}
