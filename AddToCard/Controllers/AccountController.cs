using AddToCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AddToCard.Controllers
{
    public class AccountController : Controller
    {
        EcommerceDbEntities db = new EcommerceDbEntities();
        // GET: Account

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tbl_User user)
        {
            user.Role = "V";
            db.tbl_User.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(tbl_User user)
        {


            var query = db.tbl_User.SingleOrDefault(x =>x.Name == user.Name && x.Password == user.Password);
            if(query != null)
            {
                if(query.Role == "V")
                {
                    FormsAuthentication.SetAuthCookie(query.Name, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Home");
                }
                else if(query.Role == "A")
                {
                    FormsAuthentication.SetAuthCookie(query.Name, false);
                    Session["User"] = query.Name;
                    return RedirectToAction("Index", "Admin");
                }
                

            }
            else
            {
                TempData["msg"] = "user name and password is in correct";

            }

            return View();
            //var count = db.tbl_User.Where(x => x.Name == user.Name && x.Password == user.Password).Count();

            //if (count != 0)
            //{
            //    FormsAuthentication.SetAuthCookie(user.Name, false);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    TempData["msg"] = "user name and password is in correct";
            //    return View();
            //}

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
