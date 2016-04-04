using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;

namespace YourWebsite.Controllers
{
    public class HomeController : Controller
    {
        CategoryService _categoryService = new CategoryService();
        ProductService _productService = new ProductService();
        public ActionResult Index()
        {
            Dictionary<Category, List<Product>> mainModel = new Dictionary<Category, List<Product>>();
            List<Category> allSubCategory = _categoryService.getAll();
            foreach(Category c in allSubCategory)
            {
                List<Product> l = _productService.getAllProductByCategory(c.ID);
                mainModel.Add(c, l);
            }
            ViewBag.mainModel = mainModel;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}