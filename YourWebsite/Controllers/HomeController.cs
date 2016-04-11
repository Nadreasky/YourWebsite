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
            ImageService _imageService = new ImageService();
            List<Image> sliderImages = _imageService.getAllSliderImage();
            ViewBag.sliderImages = sliderImages;

            List<Product> newProducts = _productService.getNewProducts();
            ViewBag.newProducts = newProducts;

            List<Product> trendProducts = _productService.getTrendProducts();
            //List<Image> trendProducts = _imageService.getImagesByNameCode(SLIMCONFIG.IS_TREND);
            ViewBag.trendProducts = trendProducts;

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