using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWebsite.Repository
{
    public class NewsRepository : IRepository<News>
    {
        OnlineshopEntities _newsContext;

        public NewsRepository()
        {
            _newsContext = new OnlineshopEntities();
        }

        public IEnumerable<News> List
        {
            get
            {
                return _newsContext.News;
            }
        }

        public void Add(News entity)
        {
            _newsContext.News.Add(entity);
            try
            {
                _newsContext.SaveChanges();
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

        public void Delete(News entity)
        {
            _newsContext.News.Remove(entity);
            _newsContext.SaveChanges();
        }

        public News FindById(int Id)
        {
            var result = (from r in _newsContext.News where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public void Update(News entity)
        {
            _newsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _newsContext.SaveChanges();
        }
    }
}