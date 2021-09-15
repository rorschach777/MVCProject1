using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ItemController(ApplicationDBContext db)
        {
            _db = db;
        }

        [Route("Item")]
        [Route("Item/{id?}")]
        public IActionResult Index()
        {
            IEnumerable<Item> ObjList = _db.Items;
            return View(ObjList);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {

            return View();
        }
        
        [HttpPost]
        [Route("Create")]
        // This checks to see if we have an anti-forgery token, and makes your application safe. 
        public IActionResult Create(Item obj)
        {
            // We want to submit the database
            _db.Items.Add(obj);
            _db.SaveChanges();
            // you only need to specify the action if you are referring to the same controller. 
            return RedirectToAction("Index");
        }
    }
}
