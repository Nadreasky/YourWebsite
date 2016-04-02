using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;
using System.IO;

namespace YourWebsite.Controllers
{
    public class QuanliController : Controller
    {
        CategoryService _categoryService = new CategoryService();
        ProductService _productService = new ProductService();
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
            List<Category> allCategory = _categoryService.getAll();
            ViewBag.categories = allCategory;
            List<Product> allProduct = _productService.getAll();
            ViewBag.products = allProduct;
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

        [HttpPost]
        [ValidateInput(false)]
        public string editSubCate(string id, string name)
        {
            int _id = -1;
            if (id == null || id.Equals(""))
            {
                return "Error: ID không hợp lệ!";
            }
            else if (int.TryParse(id, out _id) == false)
            {
                return "Error: Lỗi khi parse ID";
            }
            if (name == null || name.Equals(""))
            {
                return "Error: Không có tên sub-category";
            }
            Category c = _categoryService.findByid(_id);
            if (c == null)
            {
                return "Error: Không tìm thấy Category yêu cầu!";
            }
            c.Name = name;
            _categoryService.addCategory(c.ID, c.Name, (int)c.PreCateID);
            return "success";
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveProduct(string id, string productName, string productPrice, string productCateID, string description,
            string quantity, HttpPostedFileBase productImg1, HttpPostedFileBase productImg2, HttpPostedFileBase productImg3, HttpPostedFileBase productImg4)
        {
            string error = "";
            int _id = -1;
            double _productPrice = -1;
            int _productCateID = -1;
            int _quantity = -1;
            string filePath1 = "";
            string filePath2 = "";
            string filePath3 = "";
            string filePath4 = "";

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                error += "Error: Không thể parse ProductID";
            }
            if (productName == null || productName.Equals(""))
            {
                error += "Error: Không có tên sản phẩm";
            }
            if (description == null || description.Equals(""))
            {
                error += "Error: Không có mô tả sản phẩm";
            }
            if (productPrice == null || productPrice.Equals(""))
            {
                _productPrice = 0;
            } else if (double.TryParse(productPrice, out _productPrice) == false)
            {
                error += "Error: Không thể parse giá của sản phẩm";
            }
            if (productCateID == null || productCateID.Equals(""))
            {
                _productCateID = -1;
            }
            else if (int.TryParse(productCateID, out _productCateID) == false)
            {
                error += "Error: Không thể parse cateID của sản phẩm";
            }
            if (quantity == null || quantity.Equals(""))
            {
                _quantity = 0;
            }
            else if (int.TryParse(quantity, out _quantity) == false)
            {
                error += "Error: Không thể parse quantity của sản phẩm";
            }

            //check image
            if (productImg1 != null && productImg1.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                productImg1.SaveAs(newPath + "/" + productImg1.FileName);
                filePath1 = "/Images/" + "ProductImages/" + productImg1.FileName;
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }
            if (productImg2 != null && productImg2.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                productImg2.SaveAs(newPath + "/" + productImg2.FileName);
                filePath2 = "/Images/" + "ProductImages/" + productImg2.FileName;
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }
            if (productImg3 != null && productImg3.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                productImg3.SaveAs(newPath + "/" + productImg3.FileName);
                filePath3 = "/Images/" + "ProductImages/" + productImg3.FileName;
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }
            if (productImg4 != null && productImg4.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                productImg4.SaveAs(newPath + "/" + productImg4.FileName);
                filePath4 = "/Images/" + "ProductImages/" + productImg4.FileName;
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }


            TempData["Error"] = error;
            if(error.Equals(""))
            {
                _productService.addProduct(_id, productName, _productPrice, _productCateID, description, _quantity, filePath1, filePath2, filePath3, filePath4);
            }




            return RedirectToAction("Product");
        }


        public Object getProductInfo(int proId)
        {
            return JsonConvert.SerializeObject(_productService.findByID(proId));
        }

        [HttpPost]
        public string deleteProduct(string proId)
        {
            int _id = -1;
            if (proId == null || proId.Equals(""))
            {
                return "Error: ID không hợp lệ!";
            }
            else if (int.TryParse(proId, out _id) == false)
            {
                return "Error: Lỗi khi parse ID";
            }
            Product p = _productService.findByID(_id);
            if (p == null)
            {
                return "Error: Không tìm thấy Category yêu cầu!";
            }
            _productService.delete(p);
            return "Success";
        }
    }
}