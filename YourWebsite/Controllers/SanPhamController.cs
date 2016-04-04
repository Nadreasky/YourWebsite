using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;

namespace YourWebsite.Controllers
{
    public class SanPhamController : Controller
    {
        ProductService _productService = new ProductService();
        public ActionResult Index(int? id)
        {
            Product mainProduct = null;
            if (id!=null && id.HasValue)
            mainProduct = _productService.findByID(id.Value);
            if(mainProduct == null)
            {
                return RedirectToAction("Home/Index");
            }
            ViewBag.mainProduct = mainProduct;

            List<Product> relativeProducts = _productService.getRelativeProducts((int)id);

            ViewBag.relativeProducts = relativeProducts;

            List<Category> proTrees = _productService.getProductTree((int)id);
            ViewBag.proTrees = proTrees;

            return View();
        }
    }
}