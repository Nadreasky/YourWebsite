using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;

namespace YourWebsite.Controllers
{
    public class DanhMucController : Controller
    {
        CategoryService _categoryService = new CategoryService();
        ProductService _productService = new ProductService();
        ImageService _imageService = new ImageService();

        public ActionResult Index(int? id)
        {
            Category mainCate = null;
            CategoryService _categoryService = new CategoryService();
            ProductService _productService = new ProductService();
            if (id != null && id.HasValue)
            {
                mainCate = _categoryService.findByid(id.Value);
                
            }
            if (mainCate == null)
            {
                return RedirectToAction("Home/Index");
            }
            ViewBag.mainCate = mainCate;
            List<Category> cateTree = _categoryService.getCateTree(id.Value);
            ViewBag.cateTree = cateTree;
            List<Product> productListByCate = _categoryService.getProductByCate(id.Value);
            ViewBag.productListByCate = productListByCate;
            List<Image> trendProducts = _imageService.getImagesByNameCode(SLIMCONFIG.IS_TREND);
            ViewBag.trendProducts = trendProducts;

            return View();
        }
    }
}