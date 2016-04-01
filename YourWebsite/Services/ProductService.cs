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
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }
        public List<Product> getAll()
        {
            return _productRepository.List.ToList();
        }
        public void addProduct(int id, string name, double price, int cateID, string des, int quantity, string imgPath1, string imgPath2, string imgPath3, string imgPath4)
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
                _productRepository.Update(p);
            }
        }
        public void deleteProduct(Product p)
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
    }
}