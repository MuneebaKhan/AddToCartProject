using AddToCard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddToCard.Controllers
{
    public class AdminController : Controller
    {
        EcommerceDbEntities db = new EcommerceDbEntities();
        // GET: Admin


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(tbl_Category category)
        {
            db.tbl_Category.Add(category);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult ListCategory()
        {
            var listcat  = db.tbl_Category.ToList();
            return View(listcat);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "catName");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(tbl_Product Prod,HttpPostedFileBase uploadImg)
        {
            List<tbl_Category> categorylist = db.tbl_Category.ToList();
            ViewBag.CatList = new SelectList(categorylist, "CatId", "catName");

            var filename = Path.GetFileName(uploadImg.FileName);
            uploadImg.SaveAs(Server.MapPath("~/images/" + filename));
            Prod.Image = filename;
            db.tbl_Product.Add(Prod);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("ListProduct");
        }
        public ActionResult ListProduct()
        {
            var listProd = db.tbl_Product.ToList();
            return View(listProd);
        }

    }
}