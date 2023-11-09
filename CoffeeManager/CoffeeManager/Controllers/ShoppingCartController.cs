using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeManager.Models;
namespace CoffeeManager.Controllers
{
    public class ShoppingCartController : Controller
    {
        Coffee_managerEntities db = new Coffee_managerEntities();
        public CartCK GetCartCK()
        {
            CartCK cart = Session["CartCK"] as CartCK;
            if(cart == null|| Session["CartCK"]==null) {
            
            cart = new CartCK();
                Session["CartCK"] = cart;
            }
            return cart;
        }
        // GET: ShoppingCart
        public ActionResult AddtoCart(int id)
        {
            var pro = db.Product.SingleOrDefault(s => s.id_product == id);
            if (pro != null)
            {
                GetCartCK().Add(pro);
            }
            return RedirectToAction("Show", "ShoppingCart");
        }
        public ActionResult Show()
        {
            if (Session["CartCK"] == null)
            {
                return RedirectToAction("Show", "ShoppingCart");
            }
            CartCK cart = Session["CartCK"]as CartCK;
            return View(cart);
        }
        public ActionResult Update_Cart(FormCollection form)
        {
            CartCK cart = Session["CartCK"] as CartCK;
            int id_pro =int.Parse( form["Id_pro"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_quantity(id_pro, quantity);
            return RedirectToAction("Show", "ShoppingCart");
        }
        public ActionResult Remove_Cart(int id)
        {
            CartCK cart = Session["CartCK"] as CartCK;
            cart.Remove(id);
            return RedirectToAction("Show", "ShoppingCart");
        }
        
        public PartialViewResult BagCart()
        {
            int total_bill = 0;
            CartCK cart = Session["CartCk"] as CartCK;
            if(cart != null)
            
                total_bill = cart.Total_Quantity_in_CartCK();
                ViewBag.Total_Quantity_in_ = total_bill;
                return PartialView("BagCart");
        }

        public ActionResult Shopping_Success()
        {
            return View();
        }
        //Method Checkout
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                CartCK cart = Session["CartCk"] as CartCK;
                Orders _orders = new Orders();
                _orders.customer_id = int.Parse(form["CodeCustomer"]);
                _orders.IsType = form["Type"];
                _orders.price_total = int.Parse(form["TotalPrice"]);
                
                db.Orders.Add(_orders);
                 foreach(var item in cart.Items)
                 {
                    Order_Dentail _order_Dentail = new Order_Dentail();
                    _order_Dentail.product_id = item.shop_pro.id_product;
                    _order_Dentail.price = item.shop_pro.price;
                    _order_Dentail.price_total = Convert.ToDouble(item.shop_pro.price * item.shop_quan);
                    _order_Dentail.num = item.shop_quan; 
                    db.Order_Dentail.Add(_order_Dentail);
                    
                 }
                db.SaveChanges();
                cart.ClearCartCK();
                return RedirectToAction("Shopping_Success", "ShoppingCart");

            }
            catch
            { 
                return Content("Error Checkout. Please information of Customer....");
            }
        }
    }
}