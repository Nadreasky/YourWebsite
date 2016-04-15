using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWebsite.Repository;

namespace YourWebsite.Services
{
    public class CategoryService
    {
        CategoryRepository _categoryRepository;
        ProductService _productService;
        
        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
            
        }
        public List<Category> getAll()
        {
            return _categoryRepository.List.ToList();
        }

        public List<Category> getAllSubCate()
        {
            List<Category> c = _categoryRepository.List.ToList();
            List<Category> sc = new List<Category>();
            for (int i = 0; i < c.Count; i++)
            {
                Category ca = c.ElementAt(i);
                if(ca.PreCateID != SLIMCONFIG.NONE_PRE_CATEGORY)
                {
                    sc.Add(ca);
                }
            }
            return sc;
        }
        public void addCategory(int id, String name, int preCateID)
        {
            Category c;
            c = findByid(id);
            if (c == null)
            {
                c = new Category();
                c.Name = name;
                if (preCateID < 0 )
                {
                    c.PreCateID = SLIMCONFIG.NONE_PRE_CATEGORY;
                }
                else
                {
                    c.PreCateID = preCateID;
                }
                _categoryRepository.Add(c);
            }
            else
            {
                c.Name = name;
                if (preCateID < 0)
                {
                    c.PreCateID = SLIMCONFIG.NONE_PRE_CATEGORY;
                }
                else
                {
                    c.PreCateID = preCateID;
                }
                _categoryRepository.Update(c);
            }
        }
        public void delete(Category c)
        {
            _categoryRepository.Delete(c);
        }
        public Category findByid(int id)
        {
            if (id < 0)
            {
                return null;

            }
            Category c = _categoryRepository.FindById(id);
            return c;
        }
        public List<Category> getCateTree(int id)
        {
            List<Category> cateTree = new List<Category>();
            Category mainCate = findByid(id);
            if(mainCate != null && mainCate.PreCateID != null)
            {
                cateTree.Add(mainCate);
                Category tmp = new Category();
                tmp = mainCate;
                
                while(tmp != null && tmp.PreCateID != SLIMCONFIG.NONE_PRE_CATEGORY)
                {
                    if (tmp.PreCateID != SLIMCONFIG.NONE_PRE_CATEGORY && tmp.PreCateID != null)
                    {

                        Category c = findByid(tmp.PreCateID.Value);
                        cateTree.Add(c);
                        tmp = c;
                    }
                }
            }
            return cateTree;
        }
        public List<Product> getProductByCate(int id)
        {
            _productService = new ProductService();
            List<Product> allProduct = _productService.getAll();
            List<Product> productListByCate = new List<Product>();
            for(int i = 0; i < allProduct.Count; i++)
            {
                Product p = allProduct.ElementAt(i);
                if (p.CateID == id)
                {
                    productListByCate.Add(p);
                }
            }
            return productListByCate;
        }
    }
}