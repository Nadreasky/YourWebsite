using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWebsite.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        OnlineshopEntities _productContext;

        public ProductRepository()
        {
            _productContext = new OnlineshopEntities();
        }

        public IEnumerable<Product> List
        {
            get
            {
                return _productContext.Products;
            }
        }

        public void Add(Product entity)
        {
            _productContext.Products.Add(entity);
            try
            {
                _productContext.SaveChanges();
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

        public void Delete(Product entity)
        {
            _productContext.Products.Remove(entity);
            _productContext.SaveChanges();
        }

        public Product FindById(int Id)
        {
            var result = (from r in _productContext.Products where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public void Update(Product entity)
        {
            _productContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _productContext.SaveChanges();
        }
    }
}