using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        // GET: Test

        ProductDAL pd = new ProductDAL();
        public ActionResult Index()
        {
            var data = pd.GetAllProducts();
            return View(data);
        }

        public ActionResult AddProduct()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p) 
        {
            pd.AddProduct(p);
            TempData["succes"] = "<script>alert('inserted..')</script>";
            return RedirectToAction("Index");  
        }

        public ActionResult DeleteProduct(int Id) 
        { 
            pd.deleteProduct(Id);
            TempData["succes"] = "<script>alert('Deleted..')</script>";
            return RedirectToAction("Index");

        }


        public ActionResult EditProduct(int id)
        {
            var data = pd.GetAllProducts().Find(x => x.Id.Equals(id));
           
            return View(data);
        }
        [HttpPost]
        public ActionResult EditProduct(Product p) 
        {
            pd.EditProduct(p);
            TempData["succes"] = "<script>alert('Edited..')</script>";
            return RedirectToAction("Index");
        
        }

    }
}