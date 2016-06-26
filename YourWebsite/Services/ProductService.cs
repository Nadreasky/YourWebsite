using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWebsite.Repository;

namespace YourWebsite.Services
{
    public class ProductService
    {
        ProductRepository _productRepository;
        CategoryService _categoryService;
        public ProductService()
        {
            _productRepository = new ProductRepository();
            _categoryService = new CategoryService();
        }
        public List<Product> getAll()
        {
            return _productRepository.List.ToList();
        }
        public List<Product> getAllProductByCategory(int categoryId)
        {
            return _productRepository.getByCategoryId(categoryId);
        }

        public string getProductName(int id)
        {
            return _productRepository.getProductName(id);
        }
        public string getProductName(string id)
        {
            int _id = -1;
            if (id != null && id != "")
            {
                int.TryParse(id, out _id);
            }
            return _productRepository.getProductName(_id);
        }

        public void addProduct(int id, string name, double price, int cateID, string des, int quantity, string imgPath1, string imgPath2, string imgPath3, string imgPath4, int trend)
        {
            Product p = findByID(id);
            if (p == null)
            {
                p = new Product();
                p.Name = name;
                p.Price = price;
                p.CateID = cateID;
                p.Descriptions = des;
                p.Quantity = quantity;
                p.Img1 = imgPath1;
                p.Img2 = imgPath2;
                p.Img3 = imgPath3;
                p.Img4 = imgPath4;
                p.Trend = trend;
                _productRepository.Add(p);
            }
            else
            {
                p.Name = name;
                p.Price = price;
                p.CateID = cateID;
                p.Descriptions = des;
                p.Quantity = quantity;
                if (imgPath1 != null && !imgPath1.Equals(""))
                {
                    p.Img1 = imgPath1;
                }
                if (imgPath2 != null && !imgPath2.Equals(""))
                {
                    p.Img2 = imgPath2;
                }
                if (imgPath3 != null && !imgPath3.Equals(""))
                {
                    p.Img3 = imgPath3;
                }
                if (imgPath4 != null && !imgPath4.Equals(""))
                {
                    p.Img4 = imgPath4;
                }
                p.Trend = trend;
                _productRepository.Update(p);
            }
        }
        public void delete(Product p)
        {
            _productRepository.Delete(p);
        }
        public Product findByID(int id)
        {
            if (id < 0)
            {
                return null;
            }
            Product p = _productRepository.FindById(id);
            return p;
        }

        public List<Product> getRelativeProducts(int id)
        {
            Product mainProduct = findByID(id);
            List<Product> relativeProducts = new List<Product>();
            List<Product> allProduct = getAll();
            int count = 0;

            int indexOfMainProduct = allProduct.IndexOf(mainProduct);



           
                for (int i = indexOfMainProduct + 1;  i<allProduct.Count && count<4; i++)
                {
                   
                        Product p = allProduct.ElementAt(i);
                        if (p.CateID == mainProduct.CateID)
                        {
                            relativeProducts.Add(p);
                            count++;
                        }
                }
            
            count = 0;

            for (int i = indexOfMainProduct - 1; i >= 0 && count < 4; i--)
            {

                Product p = allProduct.ElementAt(i);
                if (p.CateID == mainProduct.CateID)
                {
                    relativeProducts.Add(p);
                    count++;
                }
            }

            //while (count < 4 && size <= allProduct.Count)
            //{
            //    for (int i = 0; i < allProduct.Count; i++)
            //    {
            //        Product p = allProduct.ElementAt(i);

            //        if ((p.CateID == mainProduct.CateID) && (p.ID > id))
            //        {
            //            relativeProducts.Add(p);
            //            count++;

            //        }
            //        size++;
            //    }
            //}

            //count = 0;
            //while (count < 4 && size <= allProduct.Count)
            //{
            //    for (int i = allProduct.Count-1; i >= 0; i--)
            //    {
            //        Product p = allProduct.ElementAt(i);

            //        if ((p.CateID == mainProduct.CateID) && (p.ID < id))
            //        {
            //            relativeProducts.Add(p);
            //            count++;

            //        }
            //        size++;
            //    }
            //}

            return relativeProducts;
            
        }

        public List<Category> getProductTree(int id)
        {
            List<Category> productTree = new List<Category>();
            _categoryService = new CategoryService();

            Product mainProduct = findByID(id);

            if (mainProduct != null && mainProduct.CateID != null)
            {
                Category mainProCate = _categoryService.findByid(mainProduct.CateID.Value);

                productTree.Add(mainProCate);
                Category tmp = new Category();

                tmp = mainProCate;



                while(tmp != null && tmp.PreCateID != SLIMCONFIG.NONE_PRE_CATEGORY)
                {
                    if (tmp.PreCateID != SLIMCONFIG.NONE_PRE_CATEGORY && tmp.PreCateID != null)
                    {
                        
                        Category c = _categoryService.findByid(tmp.PreCateID.Value);
                        productTree.Add(c);
                        tmp = c;
                    }
                }
                
                
            }


            return productTree;
        }


        public List<Product> getNewProducts()
        {
            _categoryService = new CategoryService();
            List<Product> newProducts = new List<Product>();
            List<Product> allProduct = getAll();

            int count = 0;
            for (int i = allProduct.Count - 1; i >= 0 && count < 4; i--)
            {
                Product p = allProduct.ElementAt(i);
                if (p != null)
                {
                    if (p.CateID.Value != _categoryService.getEventGiftID())
                    {
                        newProducts.Add(p);
                        count++;
                    }
                }
                
            }

            return newProducts;
        }

        public List<Product> getTrendProducts()
        {
            List<Product> trendProducts = new List<Product>();
            List<Product> allProduct = getAll();

            for (int i = allProduct.Count - 1; i >= 0; i--)
            {
                Product p = allProduct.ElementAt(i);
                if (p.Trend == SLIMCONFIG.IS_TREND)
                {
                    trendProducts.Add(p);
                }
            }
            return trendProducts;
        }

        public bool isProductHasImage(int id)
        {
            Product p = findByID(id);
            if ((p != null) && ((p.Img1 != null) || (p.Img2 != null) || (p.Img3 != null)))
            {
                return true;
            }
            return false;
        }

        public List<Product> getAvailableProducts()
        {
            List<Product> allProduct = getAll();
            List<Product> avaiProducts = new List<Product>();
            _categoryService = new CategoryService();

            List<Category> allCate = _categoryService.getAll();
            int avaiCateID = -1;
            for(int i = 0; i < allCate.Count; i++)
            {
                Category c = allCate.ElementAt(i);
                if (c.Name.Equals(SLIMCONFIG.AVAILABLE_PRODUCT))
                {
                    avaiCateID = c.ID;
                }
            }

            for (int i = 0; i < allProduct.Count; i++)
            {
                Product p = allProduct.ElementAt(i);
                if (p.CateID == avaiCateID)
                {
                    avaiProducts.Add(p);
                }
            }
            return avaiProducts;
        }

        public Product findByProductName(string name)
        {
            return _productRepository.findByName(name);
        }
    }
}