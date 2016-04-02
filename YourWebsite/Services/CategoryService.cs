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
    }
}