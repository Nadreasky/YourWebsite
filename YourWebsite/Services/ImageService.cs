using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourWebsite.Repository;

namespace YourWebsite.Services
{
    public class ImageService
    {
        ImageRepository _imageRepository;
        public ImageService()
        {
            _imageRepository = new ImageRepository();
        }
        public List<Image> getAll()
        {
            return _imageRepository.List.ToList();
        }
        public void addImage(int id, int proID, string imgPath)
        {
            Image i = findByID(id);
            if(i == null)
            {
                i = new Image();
                i.ProductID = proID;
                i.Link = imgPath;
                _imageRepository.Add(i);
            }
            else
            {
                i.Link = imgPath;
                _imageRepository.Update(i);
            }
        }
        public void delete(Image p)
        {
            _imageRepository.Delete(p);
        }
        public Image findByID(int id)
        {
            if (id < 0)
            {
                return null;
            }
            Image i = _imageRepository.FindById(id);
            return i;
        }
    }
}