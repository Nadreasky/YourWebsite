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
                    c.PreCateID = 0;
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
                    c.PreCateID = 0;
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