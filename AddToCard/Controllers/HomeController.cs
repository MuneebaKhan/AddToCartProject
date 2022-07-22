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
            c.prodid = id; //4
            c.proname = p.ProductName; //Bag
            c.Image = p.Image;//img6
            c.price = Convert.ToInt32(p.Price); //1000
            c.qty = Convert.ToInt32(qty);//2
            c.bill = c.qty * c.price; //2 * 1000 =2000

            if(Session["Cart"] == null)
            {
                li.Add(c);
                Session["Cart"] = li;
            }
            else
            {
                
                List<Cart> li2 =  Session["Cart"] as List<Cart>;
                int flag = 0;

                foreach(var item in li2)
                {
                    if(item.prodid == c.prodid)
                    {
                        item.bill += c.bill;
                        item.qty += c.qty;
                        flag = 1;

                    }
                    
                }
           
                if(flag == 0)
                {
                    li2.Add(c);
                }
                Session["Cart"] = li2;
            }

            if (Session["Cart"] != null)
            {
                int x = 0;
                List<Cart> li2 = Session["Cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                Session["total"] = x;
                Session["item_count"] = li2.Count();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult remove(int id)
        {
            if(Session["Cart"] == null)
            {
                Session.Remove("total");
                Session.Remove("Cart");
            }
            else
            {
                List<Cart> li2 = Session["Cart"] as List<Cart>;
                Cart c = li2.Where(x => x.prodid == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach(var item in li2)
                {
                    s += item.bill;
                }
                Session["total"] = s;
                Session["item_count"] = li2.Count();
            }
            return RedirectToAction("Index");
        }
    }
}