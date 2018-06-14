using Ch24ShoppingCartMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ch24ShoppingCartMVC.Controllers
{
    public class CheckoutController : Controller
    {
        private CartModel cart = new CartModel();
        //
        // GET: /Checkout/

        [HttpGet]
        public ViewResult Index()
        {
            double total=0.0; 
           CartViewModel model = cart.GetCart();
            foreach(var item in model.Cart)
            {
                total += ((Convert.ToDouble(item.Quantity) * Convert.ToDouble(item.UnitPrice)));
            }
            model.TotalPrice = total;

            return View(model);
        }
        [HttpPost]
        public ViewResult Submit()
        {
            ViewBag.Message = "Order is complete";
            cart.DeleteCart();
            return View(cart) ;
        }
    }
}
