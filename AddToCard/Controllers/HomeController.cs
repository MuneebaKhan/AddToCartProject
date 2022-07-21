using AddToCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddToCard.Controllers
{
 
    public class HomeController : Controller
    {
        EcommerceDbEntities db = new EcommerceDbEntities();
        List <Cart> li = new List<Cart> ();
        // GET: Home
        public ActionResult Index()
        {
           var prodlist  = db.tbl_Product.ToList();
            return View(prodlist);
        }

        [HttpGet]
        public ActionResult AddtoCart(int id)
        {
            var query = db.tbl_Product.Where(x => x.ProdId == id).SingleOrDefault();
            return View(query);
        }
        [HttpPost]
        public ActionResult AddtoCart(int id,int qty)
        {
            tbl_Product p = db.tbl_Product.Where(x => x.ProdId == id).SingleOrDefault();
            Cart c = new Cart();
            c.prodid = id;
            c.proname = p.ProductName;
            c.Image = p.Image;
            c.price = Convert.ToInt32(p.Price);
            c.qty = Convert.ToInt32(qty);
            c.bill = c.qty * c.price;

            if(Session["Cart"] == null)
            {
                li.Add(c);
                Session["Cart"] = li;
            }
            return RedirectToAction("Index");
        }


        public ActionResult Checkout()
        {
            return View();
        }
    }
}