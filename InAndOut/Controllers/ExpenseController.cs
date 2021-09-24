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


        // Post Delete
        [HttpGet]
        public IActionResult Update(int? id)
        {

            var model = _db.Expenses.Find(id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
   
            if (model == null)
            {
                return NotFound();
            }
   
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Expense expense)
        {
            // Server side 
            if (ModelState.IsValid)
            {
                // Checks the data attributes on the model
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);

        }

        // Post Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
  
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if (obj != null)
            {
                _db.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            else {
                return NotFound();
            }
        }
    }
}
