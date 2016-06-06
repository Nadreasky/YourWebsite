using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourWebsite.Services;
using System.IO;
using System.Web.Helpers;
using System.Globalization;

namespace YourWebsite.Controllers
{
    public class QuanliController : Controller
    {
        CategoryService _categoryService = new CategoryService();
        ProductService _productService = new ProductService();
        ImageService _imageService = new ImageService();
        NewsService _newsService = new NewsService();
        // GET: Quanli
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {
            List<Category> allCategory = _categoryService.getAll();
            ViewBag.categories = allCategory;
            ViewBag.Error = TempData["error"];
            return View();
        }

        public ActionResult Product()
        {
            List<Category> allCategory = _categoryService.getAll();
            ViewBag.categories = allCategory;
            List<Product> allProduct = _productService.getAll();
            ViewBag.products = allProduct;
            ViewBag.Error = TempData["error"];
            return View();
        }

        public ActionResult SliderManager()
        {
            List<Image> allImage = _imageService.getAll();
            ViewBag.images = allImage;
            ViewBag.Error = TempData["error"];
            return View();
        }

        public ActionResult ImageManager()
        {
            List<Image> allImage = _imageService.getAll();
            ViewBag.images = allImage;
            ViewBag.Error = TempData["error"];
            return View();
        }

        public ActionResult TrendImageManager()
        {
            List<Category> categories = _categoryService.getAll();
            ViewBag.categories = categories;
            List<Image> trendImages = _imageService.getImagesByNameCode(SLIMCONFIG.IS_TREND);
            ViewBag.trendImages = trendImages;
            ViewBag.Error = TempData["error"];
            return View();
        }

        public ActionResult News()
        {
            List<News> allNews = _newsService.getAll();
            ViewBag.allNews = allNews;
            ViewBag.Error = TempData["error"];
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveCategory(string id, string categoryName, string preCateID)
        {
            ViewBag.Error = "";
            int _id = -1;
            int.TryParse(id, out _id);
            int _preCateID = -1;
            int.TryParse(preCateID, out _preCateID);

            if(categoryName == null || categoryName.Equals(""))
            {
                ViewBag.Error += "Error: Không có tên Category!";
            }
            if (!"".Equals(ViewBag.Error))
            {
                TempData["error"] = ViewBag.Error;
                return RedirectToAction("Category");
            }
            _categoryService.addCategory(_id, categoryName, _preCateID);

            return RedirectToAction("Category");
        }

        [HttpPost]
        [ValidateInput(false)]
        public string editSubCate(string id, string name)
        {
            ViewBag.Error = "";
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
            string quantity, HttpPostedFileBase productImg1, HttpPostedFileBase productImg2, HttpPostedFileBase productImg3, HttpPostedFileBase productImg4, string trend)
        {
            int _id = -1;
            double _productPrice = -1;
            int _productCateID = -1;
            int _quantity = -1;
            string filePath1 = "";
            string filePath2 = "";
            string filePath3 = "";
            string filePath4 = "";
            int _trend = SLIMCONFIG.IS_NOT_TREND;
            ViewBag.Error = "";

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                ViewBag.Error += "Error: Không thể parse ProductID <br/>";
            }
            if (productName == null || productName.Equals(""))
            {
                ViewBag.Error += "Error: Không có tên sản phẩm <br/>";
            }
            //if (description == null || description.Equals(""))
            //{
            //    error += "Error: Không có mô tả sản phẩm";
            //}
            if (productPrice == null || productPrice.Equals(""))
            {
                _productPrice = 0;
            }
            else if (double.TryParse(productPrice, out _productPrice) == false)
            {
                ViewBag.Error += "Error: Không thể parse giá của sản phẩm <br/>";
            }
            if (productCateID == null || productCateID.Equals(""))
            {
                _productCateID = -1;
            }
            else if (int.TryParse(productCateID, out _productCateID) == false)
            {
                ViewBag.Error += "Error: Không thể parse cateID của sản phẩm <br/>";
            }
            if (quantity == null || quantity.Equals(""))
            {
                _quantity = 0;
            }
            else if (int.TryParse(quantity, out _quantity) == false)
            {
                ViewBag.Error += "Error: Không thể parse quantity của sản phẩm <br/>";
            }
            if (trend == null || trend.Equals(""))
            {
                _trend = SLIMCONFIG.IS_NOT_TREND;
            }
            else if (int.TryParse(trend, out _trend) == false)
            {
                ViewBag.Error += "Error: Không thể parse Trend <br/>";
            }

            //check image
            if (productImg1 == null && productImg2 == null && productImg3 == null && _productService.isProductHasImage(_id) == false)
            {
                ViewBag.Error += "Bạn chưa chọn hình ảnh cho sản phẩm, xin hãy chọn ít nhất 1 hình";
            }
            else
            {

                if (productImg1 != null && productImg1.FileName != null)
                {

                    string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                    string newPathBig = Server.MapPath(SLIMCONFIG.BIG_IMG_PATH + "ProductImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    if (!Directory.Exists(newPathBig))
                    {
                        System.IO.Directory.CreateDirectory(newPathBig);
                    }
                    WebImage imgBig = _imageService.reSizeImgBig(productImg1);
                    imgBig.FileName = productImg1.FileName;
                    imgBig.Save(newPathBig + "/" + imgBig.FileName);


                    WebImage img = _imageService.reSizeImg(imgBig);
                    img.FileName = productImg1.FileName;
                    img.Save(newPath + "/" + img.FileName);
                    //productImg1.SaveAs(newPath + "/" + productImg1.FileName);
                    filePath1 = "/Images/" + "ProductImages/" + productImg1.FileName;
                }
                else
                {
                    //ViewBag.Error += "Thiếu hình ảnh 1 <br/>";
                }
                if (productImg2 != null && productImg2.FileName != null)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                    string newPathBig = Server.MapPath(SLIMCONFIG.BIG_IMG_PATH + "ProductImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    if (!Directory.Exists(newPathBig))
                    {
                        System.IO.Directory.CreateDirectory(newPathBig);
                    }
                    WebImage imgBig = _imageService.reSizeImgBig(productImg2);
                    imgBig.FileName = productImg2.FileName;
                    imgBig.Save(newPathBig + "/" + imgBig.FileName);


                    WebImage img = _imageService.reSizeImg(imgBig);
                    img.FileName = productImg2.FileName;
                    img.Save(newPath + "/" + img.FileName);
                    filePath2 = "/Images/" + "ProductImages/" + productImg2.FileName;
                }
                else
                {
                    //ViewBag.Error += "Thiếu hình ảnh 2 <br/>";
                }
                if (productImg3 != null && productImg3.FileName != null)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                    string newPathBig = Server.MapPath(SLIMCONFIG.BIG_IMG_PATH + "ProductImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    if (!Directory.Exists(newPathBig))
                    {
                        System.IO.Directory.CreateDirectory(newPathBig);
                    }
                    WebImage imgBig = _imageService.reSizeImgBig(productImg3);
                    imgBig.FileName = productImg3.FileName;
                    imgBig.Save(newPathBig + "/" + imgBig.FileName);


                    WebImage img = _imageService.reSizeImg(imgBig);
                    img.FileName = productImg3.FileName;
                    img.Save(newPath + "/" + img.FileName);
                    filePath3 = "/Images/" + "ProductImages/" + productImg3.FileName;
                }
                else
                {
                    //ViewBag.Error += "Thiếu hình ảnh 3 <br/>";
                }
            }

            if (productImg4 != null && productImg4.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "ProductImages");
                string newPathBig = Server.MapPath(SLIMCONFIG.BIG_IMG_PATH + "ProductImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                if (!Directory.Exists(newPathBig))
                {
                    System.IO.Directory.CreateDirectory(newPathBig);
                }
                WebImage imgBig = _imageService.reSizeImgBig(productImg4);
                imgBig.FileName = productImg4.FileName;
                imgBig.Save(newPathBig + "/" + imgBig.FileName);


                WebImage img = _imageService.reSizeImg(imgBig);
                img.FileName = productImg4.FileName;
                img.Save(newPath + "/" + img.FileName);
                filePath4 = "/Images/" + "ProductImages/" + productImg4.FileName;
            }
            else
            {
                //ViewBag.Error += "Thiếu hình ảnh 4 <br/>";
            }
            
            if (!"".Equals(ViewBag.Error))
            {
                TempData["error"] = ViewBag.Error;
                return RedirectToAction("Product");
            }

            else
            {
                _productService.addProduct(_id, productName, _productPrice, _productCateID, description, _quantity, filePath1, filePath2, filePath3, filePath4, _trend);
                return RedirectToAction("Product");
            }
            
                        
            
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveImgSlider(string id, string nameCode, HttpPostedFileBase path, string utility)
        {
            ViewBag.Error = "";
            int _id = -1;
            int _nameCode = SLIMCONFIG.SLIDER_IMAGE;
            string _path = "";

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                ViewBag.Error += "Error: Không thể parse ImageID";
            }

            if (nameCode == null || nameCode.Equals(""))
            {
                _nameCode = SLIMCONFIG.SLIDER_IMAGE;
            }
            else if (int.TryParse(nameCode, out _nameCode) == false)
            {
                ViewBag.Error += "Error: Không thể parse cateID";
            }

            if (path != null && path.FileName != null)
            {
                string newPath = Server.MapPath(SLIMCONFIG.path + "SilderImages");
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                path.SaveAs(newPath + "/" + path.FileName);
                _path = "/Images/" + "SilderImages/" + path.FileName;
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }

            TempData["Error"] = ViewBag.Error;
            if (!"".Equals(ViewBag.Error))
            {
                TempData["error"] = ViewBag.Error;
                return RedirectToAction("SliderManager");
            }
            _imageService.addImage(_id, _nameCode, _path, utility);

            return RedirectToAction("SliderManager");
        }



        [HttpPost]
        public string deleteImage(string id)
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
            Image i = _imageService.findByID(_id);
            if (i == null)
            {
                return "Error: Không tìm thấy hình ảnh yêu cầu!";
            }
            _imageService.delete(i);
            return "Success";
        }

        public Object getImageInfo(int id)
        {
            return JsonConvert.SerializeObject(_imageService.findByID(id));
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveImg(string id, string nameCode, HttpPostedFileBase path, string utility)
        {
            ViewBag.Error = "";
            int _id = -1;
            int _nameCode = SLIMCONFIG.UNIDENTIFIED_IMAGE;
            string _path = "";

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                ViewBag.Error += "Error: Không thể parse ImageID";
            }

            if (nameCode == null || nameCode.Equals(""))
            {
                _nameCode = SLIMCONFIG.UNIDENTIFIED_IMAGE;
            }
            else if (int.TryParse(nameCode, out _nameCode) == false)
            {
                ViewBag.Error += "Error: Không thể parse cateID";
            }

            if (path != null && path.FileName != null)
            {
                if (_nameCode == SLIMCONFIG.IS_TREND)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "TrendImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "TrendImages/" + path.FileName;
                }
                else if (_nameCode == SLIMCONFIG.SLIDER_IMAGE)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "SilderImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "SilderImages/" + path.FileName;
                }
                else
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "OtherImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "OtherImages/" + path.FileName;
                }
                
            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }

            
            if (!"".Equals(ViewBag.Error))
            {
                TempData["Error"] = ViewBag.Error;
                return RedirectToAction("ImageManager");
            }
            
            _imageService.addImage(_id, _nameCode, _path, utility);

            return RedirectToAction("ImageManager");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveTrendImg(string id, string nameCode, HttpPostedFileBase path, string utility)
        {
            ViewBag.Error = "";
            int _id = -1;
            int _nameCode = SLIMCONFIG.UNIDENTIFIED_IMAGE;
            string _path = "";

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                ViewBag.Error += "Error: Không thể parse ImageID";
            }

            if (nameCode == null || nameCode.Equals(""))
            {
                _nameCode = SLIMCONFIG.UNIDENTIFIED_IMAGE;
            }
            else if (int.TryParse(nameCode, out _nameCode) == false)
            {
                ViewBag.Error += "Error: Không thể parse cateID";
            }

            if (path != null && path.FileName != null)
            {
                if (_nameCode == SLIMCONFIG.IS_TREND)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "TrendImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "TrendImages/" + path.FileName;
                }
                else if (_nameCode == SLIMCONFIG.SLIDER_IMAGE)
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "SilderImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "SilderImages/" + path.FileName;
                }
                else
                {
                    string newPath = Server.MapPath(SLIMCONFIG.path + "OtherImages");
                    if (!Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    WebImage img = _imageService.reSizeImgBig(path);
                    img.Save(newPath + "/" + path.FileName);
                    //path.SaveAs(newPath + "/" + path.FileName);
                    _path = "/Images/" + "OtherImages/" + path.FileName;
                }

            }
            else
            {
                ViewBag.Error += "File name is not found <\br>";
            }
            if (!"".Equals(ViewBag.Error))
            {
                TempData["Error"] = ViewBag.Error;
                return RedirectToAction("TrendImageManager");
            }

            _imageService.addImage(_id, _nameCode, _path, utility);

            return RedirectToAction("TrendImageManager");
        }

        public Object getProductsByCate(int id)
        {
            return JsonConvert.SerializeObject(_productService.getAllProductByCategory(id));
        }

        public Object searchProductByName(string name)
        {
            
            return JsonConvert.SerializeObject(_productService.findByProductName(name).ID);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveNews(string id, string title, string popular, string publishDate, string mainImage, string content, string shortDes)
        {
            int _id = -1;
            int _popular = -1;
            ViewBag.Error = "";

            if (title == null || title.Equals(""))
            {
                ViewBag.Error += "Error: Không có tên tin tức <br/>";
            }
            if (publishDate == null || publishDate.Equals(""))
            {
                //ViewBag.Error += "Error: Không có ngày tin <br/>";
                publishDate = "22-12-2012";
            }
            if (shortDes == null || shortDes.Equals(""))
            {
                ViewBag.Error += "Error: Không có mô tả tin tức <br/>";
            }
            if (content == null || content.Equals(""))
            {
                ViewBag.Error += "Error: Không có nội dung tin tức <br/>";
            }

            if (id == null || id.Equals(""))
            {
                _id = -1;
            }
            else if (int.TryParse(id, out _id) == false)
            {
                ViewBag.Error += "Error: Không thể parse NewsID <br/>";
            }

            if (popular == null || popular.Equals(""))
            {
                _popular = -1;
            }
            else if (int.TryParse(popular, out _popular) == false)
            {
                ViewBag.Error += "Error: Không thể parse popularInt <br/>";
            }

            DateTime _publishDate;
            if (DateTime.TryParseExact(publishDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _publishDate) == false)
            {
                ViewBag.Error += "Error: Không thể parse publishDate <br/>";
            }

            if (!"".Equals(ViewBag.Error))
            {
                TempData["error"] = ViewBag.Error;
                return RedirectToAction("News");
            }

            else
            {
                _newsService.addNews(_id, title, _publishDate, mainImage, _popular, content, shortDes);
                return RedirectToAction("News");
            }

        }

        public Object getNewsInfo(int newsID)
        {
            return JsonConvert.SerializeObject(_newsService.findByID(newsID));
        }

        [HttpPost]
        public string deleteNews(string newsID)
        {
            int _id = -1;
            if (newsID == null || newsID.Equals(""))
            {
                return "Error: ID không hợp lệ!";
            }
            else if (int.TryParse(newsID, out _id) == false)
            {
                return "Error: Lỗi khi parse ID";
            }
            News n = _newsService.findByID(_id);
            if (n == null)
            {
                return "Error: Không tìm thấy Tin tức yêu cầu!";
            }
            _newsService.delete(n);
            return "Success";
        }
    }
}