using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourWebsite.Repository
{
    public class ImageRepository : IRepository<Image>
    {
        OnlineshopEntities _imageContext;

        public ImageRepository()
        {
            _imageContext = new OnlineshopEntities();
        }
        public IEnumerable<Image> List
        {
            get
            {
                return _imageContext.Images;
            }
        }

        public void Add(Image entity)
        {
            _imageContext.Images.Add(entity);
            try
            {
                _imageContext.SaveChanges();
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

        public void Delete(Image entity)
        {
            _imageContext.Images.Remove(entity);
            _imageContext.SaveChanges();
        }

        public Image FindById(int Id)
        {
            var result = (from r in _imageContext.Images where r.ID == Id select r).FirstOrDefault();
            return result;
        }

        public void Update(Image entity)
        {
            _imageContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _imageContext.SaveChanges();
        }

        public List<Image> getImagesByNameCode(int nameCode)
        {
            var result = (from i in _imageContext.Images where i.NameCode == nameCode select i).ToList();
            if (result == null)
            {
                result = new List<Image>();
            }
            return result;
        }
    }
}