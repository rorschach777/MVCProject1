using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
             IEnumerable<Expense> model = _db.Expenses;
            foreach (var expense in model)
            {
                expense.ExpenseCategory = _db.ExpenseCategories.FirstOrDefault(u => u.Id == expense.ExpenseCategoryId);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            //IEnumerable<SelectListItem> CategoryDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});
            //ViewBag.CategoryDropDown = CategoryDropDown;
            ExpenseVM expensViewModel = new ExpenseVM()
            {
                Expense = new Expense(),
                CategoryDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(expensViewModel);
        }

        [HttpPost]
        public IActionResult Create(ExpenseVM model)
        {
            // Server side 
            if (ModelState.IsValid)
            {
                // Checks the data attributes on the model
               
                _db.Expenses.Add(model.Expense);
                _db.SaveChanges();
                return Redirect("Index");
            }
            return View(model);

        }


        // Post Delete
        [HttpGet]
        public IActionResult Update(int? id)
        {
            ExpenseVM viewModel = new ExpenseVM();

            viewModel.Expense = _db.Expenses.Find(id);
            viewModel.CategoryDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            if (id == null || id == 0)
            {
                return NotFound();
            }
   
            if (viewModel == null)
            {
                return NotFound();
            }
   
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM expenseViewModel)
        {
            // Server side 
            if (ModelState.IsValid)
            {
                // Checks the data attributes on the model
                _db.Expenses.Update(expenseViewModel.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseViewModel.Expense);

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
