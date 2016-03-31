using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWebsite.Repository
{
    public class CategoryRepository:IRepository<Category>
    {
        OnlineshopEntities _categoryContext;

        public CategoryRepository()
        {
            _categoryContext = new OnlineshopEntities();
        }

        public IEnumerable<Category> List
        {
            get
            {
                return _categoryContext.Categories;
            }
        }

        public void Add(Category entity)
        {
            _categoryContext.Categories.Add(entity);
            try
            {
                _categoryContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public void Delete(Category entity)
        {
            _categoryContext.Categories.Remove(entity);
            _categoryContext.SaveChanges();
        }

        public Category FindById(int Id)
        {
            var result = (from r in _categoryContext.Categories where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public void Update(Category entity)
        {
            _categoryContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _categoryContext.SaveChanges();
        }
    }
}