using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeManager.Models;

namespace CoffeeManager.Controllers
{
    public class CatogoryController : Controller
    {
        Coffee_managerEntities database = new Coffee_managerEntities();
        // GET: Catogory
        public ActionResult Index()
        {
            var list = database.Catogory.ToList();
            ViewBag.listCatogory = list;
            return PartialView(list);
        }
    }
}