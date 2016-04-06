using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        public List<Image> getAllSliderImage()
        {
            List<Image> allImage = getAll();
            List<Image> sliderImages = new List<Image>();
            for (int i = 0; i < allImage.Count; i++)
            {
                Image m = allImage.ElementAt(i);
                if(m.NameCode == SLIMCONFIG.SLIDER_IMAGE)
                {
                    sliderImages.Add(m);
                }
            }
            return sliderImages;
        }
        public List<Image> getMenuImage()
        {
            List<Image> allImage = getAll();
            List<Image> menuImages = new List<Image>();
            for (int i = 0; i < allImage.Count; i++)
            {
                Image m = allImage.ElementAt(i);
                if (m.NameCode == SLIMCONFIG.MENU_BACKGROUND_IMAGE)
                {
                    menuImages.Add(m);
                }
            }
            return menuImages;
        }

        public void addImage(int id, int nameCode, string path, string utility)
        {
            Image i = findByID(id);
            if(i == null)
            {
                i = new Image();
                i.NameCode = nameCode;
                i.Path = path;
                i.Utility = utility;
                _imageRepository.Add(i);
            }
            else
            {
                i.NameCode = nameCode;
                if (path != null && !path.Equals(""))
                {
                    i.Path = path;
                }
                i.Utility = utility;
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

        public WebImage reSizeImg(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);

            img.Resize(SLIMCONFIG.IMG_WIDTH, SLIMCONFIG.IMG_HEIGHT, true, true);

            return img;
        }
    }
}