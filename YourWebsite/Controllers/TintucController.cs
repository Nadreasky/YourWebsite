using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;

namespace YourWebsite.Controllers
{
    public class TintucController : Controller
    {
        NewsService _newsService = new NewsService();
        public ActionResult Index()
        {
            List<News> allNews = _newsService.getAll();
            ViewBag.allNews = allNews;
            return View();
        }
        public ActionResult Details(int? id)
        {
            News mainNews = null;
            if(id != null && id.HasValue)
            {
                mainNews = _newsService.findByID(id.Value);
            }
            if (mainNews == null)
            {
                return RedirectToAction("Tintuc/Index");
            }
            ViewBag.mainNews = mainNews;
            List<News> allNews = _newsService.getAll();
            ViewBag.allNews = allNews;
            return View();
            
        }
    }
}