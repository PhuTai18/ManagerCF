using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeManager.Models;

namespace CoffeeManager.Controllers
{
    public class ProductClientController : Controller
    {
        Coffee_managerEntities database = new Coffee_managerEntities();
        // GET: ProductClient
        public ActionResult Index(int? Category)
        {
            if (Category != null)
                return View(database.Product.Where(s => s.id_catogory == Category).ToList());
            else
                return View(database.Product.ToList());
        }
    }
}