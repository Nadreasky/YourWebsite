using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;

namespace YourWebsite.Controllers
{
    public class QuanliController : Controller
    {
        CategoryService _categoryService = new CategoryService();
        // GET: Quanli
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {
            List<Category> allCategory = _categoryService.getAll();
            ViewBag.categories = allCategory;
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveCategory(string id, string categoryName, string preCateID)
        {
            String error = "";
            int _id = -1;
            int.TryParse(id, out _id);
            int _preCateID = -1;
            int.TryParse(preCateID, out _preCateID);

            if(categoryName == null || categoryName.Equals(""))
            {
                error += "Error: Không có tên Category!";
            }
            _categoryService.addCategory(_id, categoryName, _preCateID);

            return RedirectToAction("Category");
        }


        public Object getCategoryInfo(int cateID)
        {
            return JsonConvert.SerializeObject(_categoryService.findByid(cateID));
        }

        [HttpPost]
        public string deleteCategory(string id)
        {
            int _id = -1;
            if (id == null || id.Equals(""))
            {
                return "Error: ID không hợp lệ!";
            }
            else if(int.TryParse(id, out _id) == false)
            {
                return "Error: Lỗi khi parse ID";
            }
            Category c = _categoryService.findByid(_id);
            if (c == null)
            {
                return "Error: Không tìm thấy Category yêu cầu!";
            }
            _categoryService.delete(c);
            return "Success";
        }
    }
}