using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ExpenseCategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = _db.ExpenseCategories;
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateExpenseCategory(ExpenseCategory ec)
        {
            var mode = ec;
            if (ModelState.IsValid)
            {
                _db.ExpenseCategories.Add(ec);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var model = _db.ExpenseCategories.Find(id);
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(ExpenseCategory model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var model = _db.ExpenseCategories.Find(id);
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
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(ExpenseCategory model)
        {
            _db.Remove(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
