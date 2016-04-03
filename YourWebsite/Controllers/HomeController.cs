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
        public ActionResult Index()
        {
            ImageService _imageService = new ImageService();
            List<Image> sliderImages = _imageService.getAllSliderImage();
            ViewBag.sliderImages = sliderImages;
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