using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeManager.Models;

namespace CoffeeManager.Controllers
{
    public class CustomersController : Controller
    {
        Coffee_managerEntities db = new Coffee_managerEntities();
        // GET: Customers
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            db.Customer.Add(cus);
            db.SaveChanges();
            return RedirectToAction("Show","ShoppingCart");
        }
    }
}